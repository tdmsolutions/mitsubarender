/*
 * This file is part of MitsubaRenderPlugin project.
 * 
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version. This program is distributed in the hope that
 * it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Copyright 2014 TDM Solutions SL
 */

using System;
using System.Runtime.InteropServices;
using System.Xml;
using Rhino.Display;
using Rhino.Render;
using Rhino.Render.Fields;

namespace MitsubaRender.MitsubaMaterials
{
    [Guid("c69b06b1-ef5e-47fd-b455-3eef6ec8c38e")]
    public class MitsubaRoughMaterial: MitsubaMaterial
    {
        public static Guid MitsubaMaterialId = new Guid("c69b06b1-ef5e-47fd-b455-3eef6ec8c38e");

        private Color4f _color;
        
        private string _colorString;
        string _file = null;
        string _fileRough = null;
        bool _textureOn;
        bool _roughOn;
        float _rough;
        bool _qualityOn;
        private string _idMat;


        string _fileTrans = null;
        string _fileBump = null;
        string _fileEnvi = null;
        bool _transparencyOn;
        bool _bumpOn;
        bool _enviromentOn;
        float _transparency;
        float _bump;
        float _enviroment;

        public MitsubaRoughMaterial()
        {
            string paramName = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            string paramNameRough = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());

            /* Color */
            var field = Fields.Add("reflectance", _color, "Diffuse Color");
            BindParameterToField("reflectance", field, ChangeContexts.UI);

            /* Texture */
            var bfield = Fields.AddTextured("texture", false, "Texture");
            BindParameterToField(paramName, StandardChildSlots.Diffuse.ToString(), bfield, ChangeContexts.UI);

            /* Roughness Texture */
            var boolField = Fields.AddTextured("textureRough", false, "Texture Rough");
            BindParameterToField(paramNameRough,StandardChildSlots.Diffuse.ToString(), boolField, ChangeContexts.UI);

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

        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            
            simTexture(ref simulatedMaterial, forDataOnly);

            /* Diffuse Color */
            Rhino.Display.Color4f color;
            if (Fields.TryGetValue("reflectance", out color))
            {
                simulatedMaterial.DiffuseColor = color.AsSystemColor();
                _colorString = color.R.ToString() + " ," + color.G.ToString() + " ," + color.B.ToString();
            }
            else base.SimulateMaterial(ref simulatedMaterial, forDataOnly);

            /* Rough Texture */
            simTextureRough(ref simulatedMaterial, forDataOnly);

            if (Fields.TryGetValue("roughness", out _rough))
            {
                
            }

            if (Fields.TryGetValue("bool", out _qualityOn))
            {
                
            }
            
        }

        public override XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile)
        {
            string destTexture = null;
            string destRoughTexture = null;

            if (_rough <= 0.0f) _rough = 0.0f;
            else if (_rough > 0.7f) _rough = 0.7f;
            
            /* Copy texture image to destination folder */
            if (_file != null)
            {
                destTexture = System.IO.Path.Combine(destFile, System.IO.Path.GetFileName(_file));
                System.IO.File.Copy(_file, destTexture.Replace('$','-'), true);
            }

            if (_fileRough != null)
            {
                destRoughTexture = System.IO.Path.Combine(destFile, System.IO.Path.GetFileName(_fileRough));
                System.IO.File.Copy(_fileRough, destRoughTexture.Replace('$', '-'), true);
            }

            _idMat = this.Id.ToString();
            XmlElement material = doc.CreateElement("bsdf");
            material.SetAttribute("type", "roughdiffuse");
            material.SetAttribute("id", this.Id.ToString());

            if (_textureOn)
            {
                var texture = XmlMitsuba.AddElement(doc, "texture", "reflectance", null, "bitmap");
                texture.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename", destTexture.Replace('$','-'), null));
                material.AppendChild(texture);
            }
            else
            {
                var color = XmlMitsuba.AddElement(doc, "srgb", "reflectance", _colorString);
                material.AppendChild(color);
            }

            if (_roughOn)
            {
                var textureRough = XmlMitsuba.AddElement(doc, "texture", "alpha", null, "bitmap");
                textureRough.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename", destRoughTexture.Replace('$', '-'), null));

                material.AppendChild(textureRough);
            }
            else
            {
                material.AppendChild(XmlMitsuba.AddElement(doc, "float", "alpha", _rough.ToString()));
            }

            material.AppendChild(XmlMitsuba.AddElement(doc, "boolean", "useFastApprox", _qualityOn.ToString().ToLower()));
            return material;
        }

        public override string GetIdMaterial()
        {
            return _idMat;
        }

        public override string GetFileNameMaterial()
        {
            return _file;
        }

        private void simTexture(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            /* Diffuse Texture */
            if (Fields.TryGetValue("texture", out _textureOn))
            {
                if (_textureOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(), StandardChildSlots.Diffuse.ToString());
                    if (textureParam != null)
                    {
                        RenderContent tx = FindChild("texture");
                        if (tx == null) return;

                        if (tx.Fields.ContainsField("filename"))
                        {
                            Field fields = tx.Fields.GetField("filename");
                            _file = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            RenderTexture text = tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _file = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBitmapTexture(_file);
                    }
                }
            }
        }

        private void simTextureRough(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            /* Diffuse Texture */
            if (Fields.TryGetValue("textureRough", out _roughOn))
            {
                if (_roughOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(), StandardChildSlots.Diffuse.ToString());
                    if (textureParam != null)
                    {
                        RenderContent tx = FindChild("textureRough");
                        if (tx == null) return;

                        if (tx.Fields.ContainsField("filename"))
                        {
                            Field fields = tx.Fields.GetField("filename");
                            _fileRough = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            RenderTexture text = tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileRough = texSim.Filename;
                            }
                        }
                        //simulatedMaterial.SetBitmapTexture(_fileRough);
                    }
                }
            }
        }

        private void simTransparency(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            /* Transparency Texture */
            if (Fields.TryGetValue("transparency", out _transparencyOn))
            {
                if (_transparencyOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Transparency.ToString(), StandardChildSlots.Transparency.ToString());
                    if (textureParam != null)
                    {
                        RenderContent tx = FindChild("transparency");
                        if (tx == null) return;

                        if (tx.Fields.ContainsField("filename"))
                        {
                            Field fields = tx.Fields.GetField("filename");
                            _fileTrans = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            RenderTexture text = tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileTrans = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetTransparencyTexture(_fileTrans);
                    }
                }
            }
        }

        private void simBump(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            /* Bump Texture */
            if (Fields.TryGetValue("bump", out _bumpOn))
            {
                if (_bumpOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Bump.ToString(), StandardChildSlots.Bump.ToString());
                    if (textureParam != null)
                    {
                        RenderContent tx = FindChild("bump");
                        if (tx == null) return;

                        if (tx.Fields.ContainsField("filename"))
                        {
                            Field fields = tx.Fields.GetField("filename");
                            _fileBump = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            RenderTexture text = tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _fileBump = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBumpTexture(_fileBump);
                    }
                }
            }
        }

        private void simEnviroment(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            /* Bump Texture */
            if (Fields.TryGetValue("enviroment", out _enviromentOn))
            {
                if (_enviromentOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Environment.ToString(), StandardChildSlots.Environment.ToString());
                    if (textureParam != null)
                    {
                        RenderContent tx = FindChild("enviroment");
                        if (tx == null) return;

                        if (tx.Fields.ContainsField("filename"))
                        {
                            Field fields = tx.Fields.GetField("filename");
                            _fileEnvi = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            RenderTexture text = tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
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
