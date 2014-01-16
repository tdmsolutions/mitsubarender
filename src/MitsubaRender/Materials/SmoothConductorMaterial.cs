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

using System.Linq;
using System.Runtime.InteropServices;
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using MitsubaRender.UI;
using Rhino.Display;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO SmoothConductorMaterial summary
    /// </summary>
    [Guid("92a6d732-06c3-4950-a15d-53338bf1ad84")]
    public sealed class SmoothConductorMaterial : MitsubaMaterial, IConductor
    {
        /// <summary>
        /// Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        public static uint _count;

        /// <summary>
        /// This field handles the comboBox for the Material property.
        /// </summary>
        private MaterialCombo _materialCombo;

        #region Material Parameters

        /// <summary>
        /// Name of material preset.
        /// Default: Cu / copper
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Spectrum. Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material.
        /// </summary>
        public Color4f Eta { get; set; }

        /// <summary>
        /// Spectrum. Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material.
        /// </summary>
        public Color4f K { get; set; }

        /// <summary>
        /// Real-valued index of refraction of the surrounding dielectric, or a material name of a dielectric.
        /// Default: air
        /// </summary>
        public MitsubaType<float, string> ExtEta { get; set; }

        /// <summary>
        /// Optional factor that can be used to modulate the specular reflection component. 
        /// Note that for physical realism, this parameter should never be touched. 
        /// Default: 1.0
        /// </summary>
        //public MitsubaType<Color4f, string> SpecularReflectance;

        #endregion

        /// <summary>
        /// Main ctor.
        /// </summary>
        public SmoothConductorMaterial()
        {
            ExtEta = new MitsubaType<float, string>();
            CreateUserInterface();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__smoothconductor" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            var eta_field = Fields.Add(ETA_FIELD, Eta, "eta");
            var k_field = Fields.Add(K_FIELD, K, "k");
            var extEtaSpectrum_field = Fields.Add(EXT_ETA_SPECTRUM_FIELD, ExtEta.FirstParameter, "extEta spectrum");
            var extEtaFloat_field = Fields.Add(EXT_ETA_FLOAT_FIELD, ExtEta.SecondParameter, "extEta float");

            BindParameterToField(ETA_FIELD, eta_field, ChangeContexts.UI);
            BindParameterToField(K_FIELD, k_field, ChangeContexts.UI);
            BindParameterToField(EXT_ETA_SPECTRUM_FIELD, extEtaSpectrum_field, ChangeContexts.UI);
            BindParameterToField(EXT_ETA_FLOAT_FIELD, extEtaFloat_field, ChangeContexts.UI);
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            if (_materialCombo != null)
            {
                var myValue = StandardConductorTypes.Types.FirstOrDefault(x => x.Value == _materialCombo.SelectedItem).Key;
                Material = myValue;
            }

            Color4f colorEta;
            Fields.TryGetValue(ETA_FIELD, out colorEta);
            Eta = colorEta;

            Color4f colorK;
            Fields.TryGetValue(K_FIELD, out colorK);
            Eta = colorK;

            //ExtEta
            bool hasTexture;
            var textureParam = GetChildSlotParameter(EXT_ETA_TEXTURE_FIELD, EXT_ETA_TEXTURE_SLOT);
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

        protected override void OnAddUserInterfaceSections()
        {
            var material_section = AddUserInterfaceSection(typeof(MaterialCombo), "Material Type", true, true);
            _materialCombo = (MaterialCombo)material_section.Window;

            var data = new string[StandardConductorTypes.Types.Count];
            int i = 0;
            foreach (var value in StandardConductorTypes.Types)
            {
                data[i] = value.Value;
                i += 1;
            }

            _materialCombo.Data = data;
            _materialCombo.OnChange += Combo_OnChange;

            base.OnAddUserInterfaceSections();
        }

        private void Combo_OnChange(object sender, System.EventArgs e)
        {
            ReadDataFromUI();
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Smooth Conductor material"; }
        }

        public override string TypeName
        {
            get { return "Mitsuba Smooth Conductor material"; }
        }

        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            //TODO simulate conductor como simular oro, cobre... ?????
        }
    }
}