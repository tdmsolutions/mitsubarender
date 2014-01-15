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
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using MitsubaRender.Materials.Interfaces;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO RoughConductorMaterial
    /// </summary>
    [Guid("D9698F8B-59D1-48fd-88C9-82B19DF1CCC2")]
    public sealed class RoughConductorMaterial : MitsubaMaterial, IRough<float>, IConductor
    {
        /// <summary>
        /// Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static int _count;

        #region Material Parameters

        /// <summary>
        /// Specifies the type of microfacet normal distribution used to model the surface roughness.
        /// </summary>
        public string Distribution { get; set; }

        /// <summary>
        /// Specifies the roughness of the unresolved surface microgeometry.
        /// </summary>
        public MitsubaType<float, string> Alpha { get; set; }

        /// <summary>
        /// Specifies the anisotropic roughness values along the tangent and bitangent directions. 
        /// These parameter are only valid when distribution=as. 
        /// Default: 0.1
        /// </summary>
        public MitsubaType<float, string> AlphaU { get; set; }

        /// <summary>
        /// Specifies the anisotropic roughness values along the tangent and bitangent directions. 
        /// These parameter are only valid when distribution=as. 
        /// Default: 0.1
        /// </summary>
        public MitsubaType<float, string> AlphaV { get; set; }

        /// <summary>
        /// Name of a material preset.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material
        /// </summary>
        public Color4f Eta { get; set; }

        /// <summary>
        /// Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material
        /// </summary>
        public Color4f K { get; set; }

        /// <summary>
        /// Real-valued index of refraction of the surrounding dielectric, or a material name of a dielectric. 
        /// Default: air
        /// </summary>
        public MitsubaType<float, string> ExtEta { get; set; }

        #endregion

        /// <summary>
        /// Main ctor.
        /// </summary>
        public RoughConductorMaterial()
        {
            Distribution = "beckmann"; //TODO delete me, comboBox rules!
            Alpha = new MitsubaType<float, string>();
            AlphaU = new MitsubaType<float, string>();
            AlphaV = new MitsubaType<float, string>();
            ExtEta = new MitsubaType<float, string>();

            CreateUserInterface();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeName
        {
            get { return "Mitsuba Rough Conductor material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeDescription
        {
            get { return "Mitsuba Rough Conductor material"; } //TODO better TypeDescription
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__roughdiffuse" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            //TODO combobox in Rhino UI !!
            var distribution_field = Fields.Add(DISTRIBUTION_FIELD, Distribution, "Distribution");
            var alpha_float_field = Fields.Add(ALPHA_FLOAT_FIELD, Alpha.FirstParameter, "Alpha Float");
            var alpha_texture_field = Fields.AddTextured(ALPHA_TEXTURE_FIELD, false, "Alpha Texture");
            var alphaU_float_field = Fields.Add(ALPHAU_FLOAT_FIELD, AlphaU.FirstParameter, "AlphaU Float");
            var alphaU_texture_field = Fields.AddTextured(ALPHAU_TEXTURE_FIELD, false, "AlphaU Texture");
            var alphaV_float_field = Fields.Add(ALPHAV_FLOAT_FIELD, AlphaV.FirstParameter, "AlphaV Float");
            var alphaV_texture_field = Fields.AddTextured(ALPHAV_TEXTURE_FIELD, false, "AlphaV Texture");
            var material_field = Fields.Add(MATERIAL_FIELD, Material, "Material Name");
            var eta_field = Fields.Add(ETA_FIELD, Eta, "eta");
            var k_field = Fields.Add(K_FIELD, K, "k");
            var extEta_float_field = Fields.Add(EXT_ETA_FLOAT_FIELD, ExtEta.FirstParameter, "ExtEta Float");
            var extEta_texture_field = Fields.AddTextured(EXT_ETA_TEXTURE_FIELD, false, "ExtEta Texture");

            BindParameterToField(DISTRIBUTION_FIELD, distribution_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_FLOAT_FIELD, alpha_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT, 
                alpha_texture_field, ChangeContexts.UI);
            BindParameterToField(ALPHAU_FLOAT_FIELD, alphaU_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHAU_TEXTURE_FIELD, ALPHAU_TEXTURE_SLOT, 
                alphaU_texture_field, ChangeContexts.UI);
            BindParameterToField(ALPHAV_FLOAT_FIELD, alphaV_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHAV_TEXTURE_FIELD, ALPHAV_TEXTURE_SLOT, 
                alphaV_texture_field, ChangeContexts.UI);
            BindParameterToField(MATERIAL_FIELD, material_field, ChangeContexts.UI);
            BindParameterToField(ETA_FIELD, eta_field, ChangeContexts.UI);
            BindParameterToField(K_FIELD, k_field, ChangeContexts.UI);
            BindParameterToField(EXT_ETA_FLOAT_FIELD, extEta_float_field, ChangeContexts.UI);
            BindParameterToField(EXT_ETA_TEXTURE_FIELD, EXT_ETA_TEXTURE_SLOT, 
                extEta_texture_field, ChangeContexts.UI);
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            //Distribution
            string distribution;
            Fields.TryGetValue(DISTRIBUTION_FIELD, out distribution);
            Distribution = distribution;

            //Alpha
            bool hasTexture;
            var textureParam = GetChildSlotParameter(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT);
            Fields.TryGetValue(ALPHA_TEXTURE_FIELD, out hasTexture);
            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(ALPHA_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        Alpha.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            Alpha.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else Alpha.SecondParameter = string.Empty;
            }
            else
            {
                float alpha;
                Fields.TryGetValue(ALPHA_FLOAT_FIELD, out alpha);
                Alpha.FirstParameter = alpha;
            }

            //AlphaU
            textureParam = GetChildSlotParameter(ALPHAU_TEXTURE_FIELD, ALPHAU_TEXTURE_SLOT);
            Fields.TryGetValue(ALPHAU_TEXTURE_FIELD, out hasTexture);
            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(ALPHAU_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        AlphaU.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            AlphaU.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else AlphaU.SecondParameter = string.Empty;
            }
            else
            {
                float alphaU;
                Fields.TryGetValue(ALPHAU_FLOAT_FIELD, out alphaU);
                AlphaU.FirstParameter = alphaU;
            }

            //AlphaV
            textureParam = GetChildSlotParameter(ALPHAV_TEXTURE_FIELD, ALPHAV_TEXTURE_SLOT);
            Fields.TryGetValue(ALPHAV_TEXTURE_FIELD, out hasTexture);
            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(ALPHAV_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        AlphaV.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            AlphaV.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else AlphaV.SecondParameter = string.Empty;
            }
            else
            {
                float alphaV;
                Fields.TryGetValue(ALPHAV_FLOAT_FIELD, out alphaV);
                AlphaV.FirstParameter = alphaV;
            }

            //Material
            string material;
            Fields.TryGetValue(MATERIAL_FIELD, out material);
            Material = material;

            //Eta
            Color4f eta;
            Fields.TryGetValue(ETA_FIELD, out eta);
            Eta = eta;

            //k
            Color4f k;
            Fields.TryGetValue(K_FIELD, out k);
            K = k;

            //ExtEta
            textureParam = GetChildSlotParameter(EXT_ETA_TEXTURE_FIELD, EXT_ETA_TEXTURE_SLOT);
            Fields.TryGetValue(EXT_ETA_TEXTURE_FIELD, out hasTexture);
            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(EXT_ETA_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        ExtEta.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            ExtEta.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else ExtEta.SecondParameter = string.Empty;
            }
            else
            {
                float extEta;
                Fields.TryGetValue(EXT_ETA_FLOAT_FIELD, out extEta);
                ExtEta.FirstParameter = extEta;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            //TODO simulate RoughConductorMaterial
        }
    }
}