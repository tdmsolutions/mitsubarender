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
    [Guid("23159962-30a3-44bb-aec5-3d5017b2d639")]
    public class MitsubaPresetMaterial : MitsubaMaterial
    {
        public static uint _count;
        public string PresetName { get; set; }

        public MitsubaPresetMaterial()
        {
            var field = Fields.Add("presetName", PresetName, "Preset Name");
            BindParameterToField("presetName", field, ChangeContexts.UI);
        }

        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__preset" + _count++;
            return MaterialId;
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Preset material"; }
        }

        public override string TypeName
        {
            get { return "Mitsuba Preset material"; }
        }
    }
}
