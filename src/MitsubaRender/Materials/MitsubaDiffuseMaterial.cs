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
    /// <summary>
    ///   Mitsuba material parser, create a new material for each Mitsuba material type.
    ///   See Documantation
    /// </summary>
    [Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa")]
    public class MitsubaDiffuseMaterial : MitsubaMaterial
    {
        //public static Guid MitsubaMaterialId = new Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa");
        private static uint _count;

        public MitsubaDiffuseMaterial()
        {
            var paramName = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            var reflectance_field = Fields.AddTextured("reflectance", MaterialColor, "Diffuse Color");
            var texture_field = Fields.AddTextured("texture", false, "Texture");
            BindParameterToField("reflectance", reflectance_field, ChangeContexts.UI);
            BindParameterToField(paramName, StandardChildSlots.Diffuse.ToString(), texture_field, ChangeContexts.UI);
        }

        public override string TypeName
        {
            get { return "Mitsuba Diffuse material"; }
        }

        public override string TypeDescription
        {
            get { return "The smooth diffuse material (also referred to as “Lambertian”) " +
                         "represents an ideally diffuse material with a user-specified " +
                         "amount of reflectance."; } //TODO better description maybe ?
        }

        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__diffuse" + _count++;
            return MaterialId;
        }

        public override void SimulateMaterial(ref Material simulatedMaterial, bool forDataOnly)
        {
            bool hasTexture;
            if (Fields.TryGetValue("texture", out hasTexture))
            {
                HasTexture = hasTexture;
                if (HasTexture)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(),
                                                             StandardChildSlots.Diffuse.ToString());

                    if (textureParam != null)
                    {
                        var tx = FindChild("texture");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            TextureFile = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                TextureFile = texSim.Filename;
                            }
                        }

                        simulatedMaterial.SetBitmapTexture(TextureFile);
                        return;
                    }
                }
            }

            Color4f color;
            if (Fields.TryGetValue("reflectance", out color))
            {
                MaterialColor = color;
                simulatedMaterial.DiffuseColor = color.AsSystemColor();
                //_colorString = color.R.ToString() + " ," + color.G.ToString() + " ," + color.B.ToString();
            }
            else base.SimulateMaterial(ref simulatedMaterial, forDataOnly);
        }
    }
}