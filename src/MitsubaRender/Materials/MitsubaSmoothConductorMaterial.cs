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
using Rhino.Render.Fields;

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO MitsubaSmoothConductorMaterial summary
    /// </summary>
    [Guid("8D83D79D-0E66-49be-8264-CCD7F37AF074")]
    public class MitsubaSmoothConductorMaterial : MitsubaMaterial
    {

        // 
        public static uint _count;

        public string PresetName { get; set; }

        public MitsubaSmoothConductorMaterial()
        {
            var intIOR_field = Fields.Add("intIOR", IntIOR, "Interior Index of Refraction");
            var extIOR_field = Fields.Add("extIOR", ExtIOR, "Exterior Index of Refraction");
            BindParameterToField("intIOR", intIOR_field, ChangeContexts.UI);
            BindParameterToField("extIOR", extIOR_field, ChangeContexts.UI);
            var field = Fields.Add("presetName", PresetName, "Preset Name");
            BindParameterToField("presetName", field, ChangeContexts.UI);
        }
        /// <summary>
        ///   Interior Index of Refraction.
        /// </summary>
        public float IntIOR { get; set; }

        /// <summary>
        ///   Exterior Index of Refraction.
        /// </summary>
        public float ExtIOR { get; set; }
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__smoothconductor" + _count++;
            return MaterialId;
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
            string d;
            Fields.TryGetValue("presetName", out d);
            base.SimulateMaterial(ref simulation, isForDataOnly);
        }
    }
}