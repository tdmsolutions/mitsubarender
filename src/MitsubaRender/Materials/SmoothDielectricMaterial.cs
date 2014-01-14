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
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO SmoothDielectricMaterial summary
    /// TODO this class it is NOT using the second parameter (strings) !!
    /// </summary>
    [Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4")]
    public sealed class SmoothDielectricMaterial : MitsubaMaterial, IDielectric
    {
        #region Field constants

        private const string INTIOR_FIELD = "intIOR";
        private const string EXTIOR_FIELD = "extIOR";

        #endregion

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
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            float extIOR;
            Fields.TryGetValue(EXTIOR_FIELD, out extIOR);
            ExtIOR.FirstParameter = extIOR;

            float intIOR;
            if (Fields.TryGetValue(INTIOR_FIELD, out intIOR))
                IntIOR.FirstParameter = intIOR;
            else IntIOR.FirstParameter = -1;
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