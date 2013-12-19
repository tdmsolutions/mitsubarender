// This file is part of MitsubaRenderPlugin project.
//  
// This program is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation; either version 3 of the License, or (at your
// option) any later version. This program is distributed in the hope that
// it will be useful, but WITHOUT ANY WARRANTY; without even the implied
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details. 
// 
// You should have received a copy of the GNU General Public License
// along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2014 TDM Solutions SL

using System.Runtime.InteropServices;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    [Guid("c69b06b1-ef5e-47fd-b455-3eef6ec8c38e")]
    public class MitsubaRoughMaterial : MitsubaMaterial
    {
        //public static Guid MitsubaMaterialId = new Guid("c69b06b1-ef5e-47fd-b455-3eef6ec8c38e");
        private static uint _count;

        //private float _bump;
        private bool _bumpOn;

        private Color4f _color;

        private string _colorString;
        //private float _enviroment;
        private bool _enviromentOn;
        private string _file;
        private string _fileBump;
        private string _fileEnvi;
        private string _fileRough;
        private string _fileTrans;
        private string _idMat;
        private bool _qualityOn;
        private float _rough;
        private bool _roughOn;
        private bool _textureOn;
        //private float _transparency;
        private bool _transparencyOn;

        public MitsubaRoughMaterial()
        {
            var paramName = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            var paramNameRough = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            /* Color */
            var field = Fields.Add("reflectance", _color, "Diffuse Color");
            BindParameterToField("reflectance", field, ChangeContexts.UI);
            /* Texture */
            var bfield = Fields.AddTextured("texture", false, "Texture");
            BindParameterToField(paramName, StandardChildSlots.Diffuse.ToString(), bfield, ChangeContexts.UI);
            /* Roughness Texture */
            var boolField = Fields.AddTextured("textureRough", false, "Texture Rough");
            BindParameterToField(paramNameRough, StandardChildSlots.Diffuse.ToString(), boolField, ChangeContexts.UI);
            /* Roughness Spectrum*/
            _rough = 0.2f;
            var fField = Fields.Add("roughness", _rough, "Roughness");
            BindParameterToField("roughness", fField, ChangeContexts.UI);
            /* High Quality Version*/
            var qField = Fields.Add("bool", false, "Fast Aproximation Version");
            BindParameterToField("bool", qField, ChangeContexts.UI);
        }

        public override string TypeName
        {
            get { return "Mitsuba Rough Material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Rough Material"; }
        }

        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__rough" + _count++;
            return MaterialId;
        }

        public override void SimulateMaterial(ref Material simulatedMaterial, bool forDataOnly)
        {
            simulateTexture(ref simulatedMaterial, forDataOnly);
            /* Diffuse Color */
            Color4f color;
            if (Fields.TryGetValue("reflectance", out color))
            {
                simulatedMaterial.DiffuseColor = color.AsSystemColor();
                _colorString = color.R.ToString() + " ," + color.G.ToString() + " ," + color.B.ToString();
            }
            else base.SimulateMaterial(ref simulatedMaterial, forDataOnly);
            /* Rough Texture */
            simTextureRough(ref simulatedMaterial, forDataOnly);
            if (Fields.TryGetValue("roughness", out _rough)) {}
            if (Fields.TryGetValue("bool", out _qualityOn)) {}
        }

        //public override XmlElement GetMitsubaMaterialXml(string destFile)
        //{
        //    string destTexture = null;
        //    string destRoughTexture = null;
        //    if (_rough <= 0.0f) _rough = 0.0f;
        //    else if (_rough > 0.7f) _rough = 0.7f;
        //    /* Copy texture image to destination folder */
        //    if (_file != null)
        //    {
        //        destTexture = Path.Combine(destFile, Path.GetFileName(_file));
        //        File.Copy(_file, destTexture.Replace('$', '-'), true);
        //    }
        //    if (_fileRough != null)
        //    {
        //        destRoughTexture = Path.Combine(destFile, Path.GetFileName(_fileRough));
        //        File.Copy(_fileRough, destRoughTexture.Replace('$', '-'), true);
        //    }
        //    _idMat = Id.ToString();
        //    //var material = doc.CreateElement("bsdf");
        //    //material.SetAttribute("type", "roughdiffuse");
        //    //material.SetAttribute("id", Id.ToString());
        //    //if (_textureOn)
        //    //{
        //    //    var texture = XmlMitsuba.AddElement(doc, "texture", "reflectance", null, "bitmap");
        //    //    texture.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename", destTexture.Replace('$', '-'), null));
        //    //    material.AppendChild(texture);
        //    //}
        //    //else
        //    //{
        //    //    var color = XmlMitsuba.AddElement(doc, "srgb", "reflectance", _colorString);
        //    //    material.AppendChild(color);
        //    //}
        //    //if (_roughOn)
        //    //{
        //    //    var textureRough = XmlMitsuba.AddElement(doc, "texture", "alpha", null, "bitmap");
        //    //    textureRough.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename",
        //    //                                                   destRoughTexture.Replace('$', '-'), null));
        //    //    material.AppendChild(textureRough);
        //    //}
        //    //else material.AppendChild(XmlMitsuba.AddElement(doc, "float", "alpha", _rough.ToString()));
        //    //material.AppendChild(XmlMitsuba.AddElement(doc, "boolean", "useFastApprox", _qualityOn.ToString().ToLower()));
        //    //return material;
        //    return null;
        //}

        //public override string GetIdMaterial()
        //{
        //    return _idMat;
        //}

        //public override string GetFileNameMaterial()
        //{
        //    return _file;
        //}

        private void simulateTexture(ref Material simulatedMaterial, bool forDataOnly)
        {
            /* Diffuse Texture */
            if (Fields.TryGetValue("texture", out _textureOn))
            {
                if (_textureOn)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(),
                                                             StandardChildSlots.Diffuse.ToString());
                    //Object textureParam = null;
                    if (textureParam != null)
                    {
                        var tx = FindChild("texture");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            _file = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _file = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBitmapTexture(_file);
                    }
                }
            }
        }

        private void simTextureRough(ref Material simulatedMaterial, bool forDataOnly)
        {
            /* Diffuse Texture */
            if (Fields.TryGetValue("textureRough", out _roughOn))
            {
                if (_roughOn)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(),
                                                             StandardChildSlots.Diffuse.ToString());
                    if (textureParam != null)
                    {
                        var tx = FindChild("textureRough");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            _fileRough = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileRough = texSim.Filename;
                            }
                        }
                        //simulatedMaterial.SetBitmapTexture(_fileRough);
                    }
                }
            }
        }

        private void simTransparency(ref Material simulatedMaterial, bool forDataOnly)
        {
            /* Transparency Texture */
            if (Fields.TryGetValue("transparency", out _transparencyOn))
            {
                if (_transparencyOn)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Transparency.ToString(),
                                                             StandardChildSlots.Transparency.ToString());
                    if (textureParam != null)
                    {
                        var tx = FindChild("transparency");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            _fileTrans = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileTrans = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetTransparencyTexture(_fileTrans);
                    }
                }
            }
        }

        private void simBump(ref Material simulatedMaterial, bool forDataOnly)
        {
            /* Bump Texture */
            if (Fields.TryGetValue("bump", out _bumpOn))
            {
                if (_bumpOn)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Bump.ToString(),
                                                             StandardChildSlots.Bump.ToString());
                    if (textureParam != null)
                    {
                        var tx = FindChild("bump");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            _fileBump = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileBump = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBumpTexture(_fileBump);
                    }
                }
            }
        }

        private void simEnviroment(ref Material simulatedMaterial, bool forDataOnly)
        {
            /* Bump Texture */
            if (Fields.TryGetValue("enviroment", out _enviromentOn))
            {
                if (_enviromentOn)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Environment.ToString(),
                                                             StandardChildSlots.Environment.ToString());
                    if (textureParam != null)
                    {
                        var tx = FindChild("enviroment");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            _fileEnvi = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileEnvi = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetEnvironmentTexture(_fileEnvi);
                    }
                }
            }
        }
    }
}