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
//using Mitsuba.Tools;
using Rhino;
using Rhino.Display;
using Rhino.Render;
using Rhino.Render.Fields;

namespace MitsubaRender.MitsubaMaterials
{
    /// <summary>
    /// Mitsuba material parser, create a new material for each Mitsuba material type.
    /// See Documantation
    /// </summary>
    [Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa")]
    public class MitsubaDiffuseMaterial : MitsubaMaterial
    {
        public static Guid MitsubaMaterialId = new Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa");

        private Color4f _color;
        private string _colorString;
        string _file = null;
        bool _textureOn;
        private string _idMat;


        public MitsubaDiffuseMaterial()
        {
            string paramName = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            var field = Fields.Add("reflectance", _color, "Diffuse Color");
            BindParameterToField("reflectance", field, ChangeContexts.UI);

            var bfield = Fields.AddTextured("texture", false, "Texture");
            BindParameterToField(paramName, StandardChildSlots.Diffuse.ToString(), bfield, ChangeContexts.UI);
        }

        protected override void OnAddUserInterfaceSections()
        {
            base.OnAddUserInterfaceSections();
        }

        public override string TypeName
        {
            get { return "Mitsuba Diffuse material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Diffuse material"; }
        }
 
        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulatedMaterial, bool forDataOnly)
        {
            //Rhino.DocObjects.Texture mapDiffuse = simulatedMaterial.GetBitmapTexture();
            if(Fields.TryGetValue("texture", out _textureOn))
            {
                if(_textureOn)
                {
                    Object textureParam = this.GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(), StandardChildSlots.Diffuse.ToString());
                    if(textureParam != null)
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
                            RenderTexture text =  tx as RenderTexture;
                            if (text != null)
                            {
                                SimulatedTexture texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                _file = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBitmapTexture(_file);
                        return;
                    }
                }
            }

            Rhino.Display.Color4f color;
            if (Fields.TryGetValue("reflectance", out color))
            {
                simulatedMaterial.DiffuseColor = color.AsSystemColor();
                _colorString = color.R.ToString() + " ," + color.G.ToString() + " ," +color.B.ToString();
            }
            else base.SimulateMaterial(ref simulatedMaterial, forDataOnly);
        }

        public override XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile)
        {
            
            /* Copy texture image to destination folder */
            if (_file != null)
            {
                destFile = System.IO.Path.Combine(destFile, System.IO.Path.GetFileName(_file));
                System.IO.File.Copy(_file, destFile.Replace('$', '-'), true);
            }

            _idMat = this.Id.ToString();
            XmlElement material = doc.CreateElement("bsdf");
            material.SetAttribute("type", "diffuse");
            material.SetAttribute("id", this.Id.ToString());
            if(_textureOn)
            {
                var texture = XmlMitsuba.AddElement(doc, "texture", "reflectance", null, "bitmap");

                texture.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename", destFile.Replace('$', '-'), null));

                material.AppendChild(texture);
            }
            else
            {
                material.AppendChild(XmlMitsuba.AddElement(doc, "srgb", "reflectance", _colorString));
            }
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
    }
}
