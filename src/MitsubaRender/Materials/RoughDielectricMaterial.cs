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

using System;
using System.Runtime.InteropServices;
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO summary
    /// </summary>
    [Guid("4F1D6722-307B-41bf-ADC1-BE53C241331A")]
    public sealed class RoughDielectricMaterial : MitsubaMaterial, IDielectric, IRough<float>
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
        /// When the Beckmann distribution is used, this parameter is equal to the root mean square (RMS) slope of the microfacets. 
        /// This parameter is only valid when Distribution is beckmann/phong/ggx. 
        /// Default: 0.1
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
        /// Interior index of refraction specified numerically or using a known material name. 
        /// Default: bk7 / 1.5046
        /// </summary>
        public MitsubaType<float, string> IntIOR { get; set; }

        /// <summary>
        /// Exterior index of refraction specified numerically or using a known material name.
        /// Default: air / 1.000277
        /// </summary>
        public MitsubaType<float, string> ExtIOR { get; set; }

        #endregion

        /// <summary>
        /// Main ctor.
        /// </summary>
        public RoughDielectricMaterial()
        {
            Distribution = "beckmann"; //TODO delete me, comboBox rules!
            Alpha = new MitsubaType<float, string>();
            AlphaU = new MitsubaType<float, string>();
            AlphaV = new MitsubaType<float, string>();
            IntIOR = new MitsubaType<float, string>();
            ExtIOR = new MitsubaType<float, string>();
            CreateUserInterface();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeName
        {
            get { return "Mitsuba Rough Dielectric material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeDescription
        {

            get { return "This material implements a realistic microfacet scattering model for rendering rough " +
                         "interfaces between dielectric materials, such as a transition from air to ground glass.\n\n" +
                         "Microfacet theory describes rough surfaces as an arrangement of unresolved and ideally specular " +
                         "facets, whose normal directions are given by a specially chosen microfacet distribution."; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__roughdielectric" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            var distribution_field = Fields.Add(DISTRIBUTION_FIELD, Distribution, "Distribution");
            var alpha_float_field = Fields.Add(ALPHA_FLOAT_FIELD, Alpha.FirstParameter, "Alpha Float");
            var alpha_texture_field = Fields.AddTextured(ALPHA_TEXTURE_FIELD, false, "Alpha Texture");
            var alphaU_float_field = Fields.Add(ALPHAU_FLOAT_FIELD, AlphaU.FirstParameter, "AlphaU Float");
            var alphaU_texture_field = Fields.AddTextured(ALPHAU_TEXTURE_FIELD, false, "AlphaU Texture");
            var alphaV_float_field = Fields.Add(ALPHAV_FLOAT_FIELD, AlphaV.FirstParameter, "AlphaV Float");
            var alphaV_texture_field = Fields.AddTextured(ALPHAV_TEXTURE_FIELD, false, "AlphaV Texture");
            var intIOR_field = Fields.Add(INTIOR_FIELD, IntIOR.FirstParameter, "Interior Index of Refraction");
            var extIOR_field = Fields.Add(EXTIOR_FIELD, ExtIOR.FirstParameter, "Exterior Index of Refraction");

            BindParameterToField(DISTRIBUTION_FIELD, distribution_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_FLOAT_FIELD, alpha_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT, alpha_texture_field, ChangeContexts.UI);
            BindParameterToField(ALPHAU_FLOAT_FIELD, alphaU_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHAU_TEXTURE_FIELD, ALPHAU_TEXTURE_SLOT, alphaU_texture_field, ChangeContexts.UI);
            BindParameterToField(ALPHAV_FLOAT_FIELD, alphaV_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHAV_TEXTURE_FIELD, ALPHAV_TEXTURE_SLOT, alphaV_texture_field, ChangeContexts.UI);
            BindParameterToField(INTIOR_FIELD, intIOR_field, ChangeContexts.UI);
            BindParameterToField(EXTIOR_FIELD, extIOR_field, ChangeContexts.UI);
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

            //Interior Index of Refraction
            float extIOR;
            Fields.TryGetValue(EXTIOR_FIELD, out extIOR);
            ExtIOR.FirstParameter = extIOR;

            //Exterior Index of Refraction
            float intIOR;
            if (Fields.TryGetValue(INTIOR_FIELD, out intIOR))
                IntIOR.FirstParameter = intIOR;
            else IntIOR.FirstParameter = -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            //TODO simulate RoughDielectricMaterial in RhinO!
        }
    }
}