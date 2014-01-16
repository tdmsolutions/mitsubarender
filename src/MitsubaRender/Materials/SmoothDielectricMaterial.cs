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

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO SmoothDielectricMaterial summary
    /// TODO this class is NOT using the second parameter (strings) !!
    /// </summary>
    [Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4")]
    public sealed class SmoothDielectricMaterial : MitsubaMaterial, IDielectric
    {
        /// <summary>
        /// This field handles the comboBox for the IntIOR property.
        /// </summary>
        private static MaterialCombo _intIORCombo;

        /// <summary>
        /// This field handles the comboBox for the ExtIOR property.
        /// </summary>
        private static MaterialCombo _extIORCombo;

        /// <summary>
        /// Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static uint _count;

        #region Material Parameters

        /// <summary>
        /// Interior Index of Refraction.
        /// </summary>
        public MitsubaType<float, string> IntIOR { get; set; } //FIXME:string NOT used!
        
        /// <summary>
        /// Exterior Index of Refraction.
        /// </summary>
        public MitsubaType<float, string> ExtIOR { get; set; } //FIXME: string NOT used!

        //TODO: specular Reflectance && specular Transmittance
        //Note that for physical realism, this parameter should never be touched. (Default: 1.0)

        #endregion

        /// <summary>
        /// Main ctor.
        /// </summary>
        public SmoothDielectricMaterial()
        {
            IntIOR = new MitsubaType<float, string>();
            ExtIOR = new MitsubaType<float, string>();
            CreateUserInterface();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeName
        {
            get { return "Mitsuba Smooth Dielectric material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string TypeDescription
        {
            get { return "Smooth Dielectric material"; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__dielectric" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void CreateUserInterface()
        {
            var intIOR_field = Fields.Add(INTIOR_FIELD, IntIOR.FirstParameter, "Interior Index of Refraction");
            var extIOR_field = Fields.Add(EXTIOR_FIELD, ExtIOR.FirstParameter, "Exterior Index of Refraction");
            BindParameterToField(INTIOR_FIELD, intIOR_field, ChangeContexts.UI);
            BindParameterToField(EXTIOR_FIELD, extIOR_field, ChangeContexts.UI);
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnAddUserInterfaceSections()
        {
            if (_intIORCombo == null)
            {
                var intIOR_section = AddUserInterfaceSection(typeof(MaterialCombo), "Interior Index of Refraction", true, true);
                _intIORCombo = (MaterialCombo)intIOR_section.Window;
                
                var data = new string[StandardIORTypes.Types.Count];
                int i = 0;
                foreach (var value in StandardIORTypes.Types)
                {
                    data[i] = value.Value;
                    i += 1;
                }

                _intIORCombo.Data =  data;
            }

            if (_extIORCombo == null)
            {
                var extIOR_section = AddUserInterfaceSection(typeof(MaterialCombo), "Exterior Index of Refraction", true, true);
                _extIORCombo = (MaterialCombo)extIOR_section.Window;

                var data = new string[StandardIORTypes.Types.Count];
                int i = 0;
                foreach (var value in StandardIORTypes.Types)
                {
                    data[i] = value.Value;
                    i += 1;
                }

                _extIORCombo.Data = data;

                //Air for default exterior IOR
                string default_value;
                if (StandardIORTypes.Types.TryGetValue("air", out default_value))
                    _extIORCombo.SelectedItem = default_value;
            }

            base.OnAddUserInterfaceSections();
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            if (_intIORCombo != null)
            {
                var myValue = StandardIORTypes.Types.FirstOrDefault(x => x.Value == _intIORCombo.SelectedItem).Key;
                IntIOR.SecondParameter = myValue;
            }

            if (_extIORCombo != null)
            {
                var myValue = StandardIORTypes.Types.FirstOrDefault(x => x.Value == _extIORCombo.SelectedItem).Key;
                ExtIOR.SecondParameter = myValue;
            }

            /*
            //Exterior Index of Refraction
            float extIOR;
            Fields.TryGetValue(EXTIOR_FIELD, out extIOR);
            ExtIOR.FirstParameter = extIOR;

            //Interior Index of Refraction
            float intIOR;
            if (Fields.TryGetValue(INTIOR_FIELD, out intIOR))
                IntIOR.FirstParameter = intIOR;
            else IntIOR.FirstParameter = -1;
            */
        }

        /// <summary>
        /// This method simulates the selected material in the Rhino viewport.
        /// </summary>
        /// <param name="simulation">Set the properties of the input basic material to provide the simulation for this material.</param>
        /// <param name="isForDataOnly">Called when only asking for a hash - don't write any textures to the disk - 
        /// just provide the filenames they will get.</param>
        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();
            // TODO que transparencia poner ??
            if (IntIOR.FirstParameter > 0) simulation.Transparency = 0.5D;
        }
    }
}