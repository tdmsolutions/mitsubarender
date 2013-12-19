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

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO MitsubaDielectricMaterial summary
    /// </summary>
    [Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4")]
    public class MitsubaDielectricMaterial : MitsubaMaterial
    {
        private static uint _count;

        //private float _intIOR;
        //private float _extIOR;

        public MitsubaDielectricMaterial()
        {
            var intIOR_field = Fields.Add("intIOR", IntIOR, "Interior Index of Refraction");
            var extIOR_field = Fields.Add("extIOR", ExtIOR, "Exterior Index of Refraction");
            BindParameterToField("intIOR", intIOR_field, ChangeContexts.UI);
            BindParameterToField("extIOR", extIOR_field, ChangeContexts.UI);
        }

        /// <summary>
        ///   Interior Index of Refraction.
        /// </summary>
        public float IntIOR { get; set; }

        /// <summary>
        ///   Exterior Index of Refraction.
        /// </summary>
        public float ExtIOR { get; set; }

        //TODO: specular Reflectance && specular Transmittance
        //Note that for physical realism, this parameter should never be touched. (Default: 1.0)

        public override string TypeName
        {
            get { return "Mitsuba Dielectric material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Dielectric material"; }
        }

        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__dielectric" + _count++;
            return MaterialId;
        }

        public override void SimulateMaterial(ref Rhino.DocObjects.Material simulation, bool isForDataOnly)
        {
            float extIOR;
            Fields.TryGetValue("extIOR", out extIOR);
            ExtIOR = extIOR;

            float intIOR;
            if (Fields.TryGetValue("intIOR", out intIOR))
            {
                IntIOR = intIOR;
                simulation.Transparency = 0.5D; // TODO que transparencia poner ??
            }
            else base.SimulateMaterial(ref simulation, isForDataOnly);
        }
    }
}