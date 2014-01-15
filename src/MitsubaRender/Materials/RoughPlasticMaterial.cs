﻿// This file is part of MitsubaRenderPlugin project.
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
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// </summary>
    [Guid("B61557C0-0BD1-483f-A16B-B82830E69947")]
    public sealed class RoughPlasticMaterial : MitsubaMaterial, IPlastic, IRough<float>
    {
        /// <summary>
        ///   Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static uint _count;

        #region Mitsuba parameters

        /// <summary>
        /// Specifies the type of microfacet normal distribution used to model the surface roughness.
        /// Default: beckmann
        /// </summary>
        public string Distribution { get; set; }

        /// <summary>
        ///   Specifies the type of microfacet normal distribution used to model the surface roughness.
        ///   Default: beckmann
        /// </summary>
        public MitsubaType<Color4f, string> DiffuseReflectance { get; set; }

        /// <summary>
        ///   Specifies the roughness of the unresolved surface microgeometry. When the Beckmann distribution is used,
        ///   this parameter is equal to the root mean square (RMS) slope of the microfacets.
        ///   Default: 0.1
        /// </summary>
        public MitsubaType<float, string> Alpha { get; set; }

        /// <summary>
        ///   Interior index of refraction specified numerically or using a known material name.
        ///   Default: polypropylene / 1.49
        /// </summary>
        public MitsubaType<float, string> IntIOR { get; set; }

        /// <summary>
        ///   Exterior index of refraction specified numerically or using a known material name.
        ///   Default: air / 1.000277
        /// </summary>
        public MitsubaType<float, string> ExtIOR { get; set; }

        /// <summary>
        ///   Account for nonlinear color shifts due to internal scattering? See the plastic plugin for details.
        ///   Default: Don’t account for them and preserve the texture colors, i.e. false
        /// </summary>
        public bool Nonlinear { get; set; }

        #endregion

        /// <summary>
        /// Main ctor.
        /// </summary>
        public RoughPlasticMaterial()
        {
            Distribution = "beckmann"; //TODO delete me !
            DiffuseReflectance = new MitsubaType<Color4f, string>();
            Alpha = new MitsubaType<float, string>();
            IntIOR = new MitsubaType<float, string>();
            ExtIOR = new MitsubaType<float, string>();
            CreateUserInterface();
        }

        public override string TypeName
        {
            get { return "Mitsuba Rough Plastic Material"; }
        }

        public override string TypeDescription
        {
            get
            {
                return "This plugin implements a realistic microfacet scattering model for rendering rough " +
                       "dielectric materials with internal scattering, such as plastic.";
            }
        }

        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__roughplastic" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            var distribution_field = Fields.Add(DISTRIBUTION_FIELD, Distribution, "Distribution"); //TODO combobox
            var reflectance_field = Fields.Add(REFLECTANCE_COLOR_FIELD, DiffuseReflectance.FirstParameter, "Diffuse Reflectance Color");
            var texture_field = Fields.AddTextured(REFLECTANCE_TEXTURE_FIELD, false, "Diffuse Reflectance Texture");
            var alpha_float_field = Fields.Add(ALPHA_FLOAT_FIELD, Alpha.FirstParameter, "Alpha Float");
            var alpha_texture_field = Fields.AddTextured(ALPHA_TEXTURE_FIELD, false, "Alpha Texture");
            var intIOR_field = Fields.Add(INTIOR_FIELD, IntIOR.FirstParameter, "Interior Index of Refraction");
            var extIOR_field = Fields.Add(EXTIOR_FIELD, ExtIOR.FirstParameter, "Exterior Index of Refraction");
            var nonlinear_field = Fields.Add(NONLINEAR_FIELD, Nonlinear, "Nonlinear");

            BindParameterToField(DISTRIBUTION_FIELD, distribution_field, ChangeContexts.UI);
            BindParameterToField(REFLECTANCE_COLOR_FIELD, reflectance_field, ChangeContexts.UI);
            BindParameterToField(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT, texture_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_FLOAT_FIELD, alpha_float_field, ChangeContexts.UI);
            BindParameterToField(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT, alpha_texture_field, ChangeContexts.UI);
            BindParameterToField(INTIOR_FIELD, intIOR_field, ChangeContexts.UI);
            BindParameterToField(EXTIOR_FIELD, extIOR_field, ChangeContexts.UI);
            BindParameterToField(NONLINEAR_FIELD, nonlinear_field, ChangeContexts.UI);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ReadDataFromUI()
        {
            //Distribution
            string distribution;
            Fields.TryGetValue(DISTRIBUTION_FIELD, out distribution);
            Distribution = distribution;

            //Exterior Index of Refraction
            float extIOR;
            Fields.TryGetValue(EXTIOR_FIELD, out extIOR);
            ExtIOR.FirstParameter = extIOR;

            //Interior Index of Refraction
            float intIOR;
            if (Fields.TryGetValue(INTIOR_FIELD, out intIOR)) IntIOR.FirstParameter = intIOR;

            //Diffuse Refectance
            bool hasTexture;
            var textureParam = GetChildSlotParameter(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT);
            Fields.TryGetValue(REFLECTANCE_TEXTURE_FIELD, out hasTexture);

            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(REFLECTANCE_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        DiffuseReflectance.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            DiffuseReflectance.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else DiffuseReflectance.SecondParameter = string.Empty;
            }
            else
            {
                //We have color
                Color4f color;
                Fields.TryGetValue(REFLECTANCE_COLOR_FIELD, out color);
                DiffuseReflectance.FirstParameter = color;
                DiffuseReflectance.SecondParameter = string.Empty;
            }

            //Alpha
            textureParam = GetChildSlotParameter(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT);
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

            //Nonlinear
            bool nonLinear;
            if (Fields.TryGetValue(NONLINEAR_FIELD, out nonLinear)) Nonlinear = nonLinear;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            //TODO simulate RoughPlasticMaterial in Rhino !
        }
    }
}