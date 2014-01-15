using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using Rhino.DocObjects;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO SmoothDielectricMaterial summary
    /// TODO this class it is NOT using the second parameter (strings) !!
    /// </summary>
    [Guid("2ECFA438-663A-42fd-A7B9-B0EAFAC66249")]
    public class SmoothDielectricCoatingMaterial : MitsubaMaterial, ICoating, IDielectric
    {
        /// <summary>
        /// Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static uint _count;

        #region Mitsuba parameters

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

        /// <summary>
        /// Denotes the thickness of the layer (to model absorption — should be specified in inverse units of sigmaA)
        /// Default: 1
        /// </summary>
        public float Thickness { get; set; }

        /// <summary>
        /// The absorption coefficient of the coating layer.
        /// Default: 0, i.e. there is no absorption
        /// </summary>
        public MitsubaType<Color4f, string> SigmaA { get; set; }

        //specularReflectance

        /// <summary>
        /// A nested BSDF model that should be coated.
        /// </summary>
        public MitsubaMaterial NestedPlugin { get; set; }

        #endregion

        public SmoothDielectricCoatingMaterial()
        {
            IntIOR = new MitsubaType<float, string>();
            ExtIOR = new MitsubaType<float, string>();
            SigmaA = new MitsubaType<Color4f, string>();
            //TODO nestedplugin
        }

        public override string TypeName
        {
            get { return "Mitsuba Smooth Dielectric Coating"; }
        }
        
        public override string TypeDescription
        {
            get
            {
                return "This plugin implements a smooth dielectric coating (e.g. a layer of varnish) in the style " +
                       "of the paper “Arbitrarily LayeredMicro-Facet Surfaces” byWeidlich andWilkie [52]. Any BSDF " +
                       "inMitsuba can be coated using this plugin, and multiple coating layers can even be applied in " +
                       "sequence. This allows designing interesting custom materials like car paint or glazed metal foil.";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__coating" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            var intIOR_field = Fields.Add(INTIOR_FIELD, IntIOR.FirstParameter, "Interior Index of Refraction");
            var extIOR_field = Fields.Add(EXTIOR_FIELD, ExtIOR.FirstParameter, "Exterior Index of Refraction");
            var thickness_field = Fields.Add(THICKNESS_FIELD, Thickness, "Thickness");

            //var sigmaA_color_field = Fields.Add(REFLECTANCE_COLOR_FIELD, Reflectance.FirstParameter, "Reflectance Color");
            //var sigmaA_texture_field = Fields.AddTextured(REFLECTANCE_TEXTURE_FIELD, false, "Reflectance Texture");

            //BindParameterToField(REFLECTANCE_COLOR_FIELD, reflectance_field, ChangeContexts.UI);
            //BindParameterToField(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT, texture_field, ChangeContexts.UI);

            BindParameterToField(INTIOR_FIELD, intIOR_field, ChangeContexts.UI);
            BindParameterToField(EXTIOR_FIELD, extIOR_field, ChangeContexts.UI);
            BindParameterToField(THICKNESS_FIELD, thickness_field, ChangeContexts.UI);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void ReadDataFromUI()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            //TODO simulate SmoothDielectricCoatingMaterial in Rhino !
        }
    }
}
