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
    /// TODO MitsubaSmoothConductorMaterial summary
    /// </summary>
    [Guid("8D83D79D-0E66-49be-8264-CCD7F37AF074")]
    public class MitsubaSmoothConductorMaterial : MitsubaMaterial
    {
        public static uint _count;

        public MitsubaSmoothConductorMaterial()
        {
            
        }

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
    }
}