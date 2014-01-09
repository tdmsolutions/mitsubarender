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

using Rhino.Display;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    ///   Parent material for all Mitsuba materials
    /// </summary>
    public abstract class MitsubaMaterial : RenderMaterial
    {
        /// <summary>
        ///   The internal ID of the current material.
        /// </summary>
        protected string MaterialId;

        /// <summary>
        /// This method has to be implemented in each material with 
        /// </summary>
        /// <returns></returns>
        public abstract string GetMaterialId();

        /// <summary>
        /// This method creates the UI in Rhino for the current material.
        /// </summary>
        protected abstract void CreateUserInterface();

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected abstract void ReadDataFromUI();

        /// <summary>
        /// This method creates a new section named "Parameters" in the material selector of the Rhino user interface.
        /// </summary>
        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Mitsuba Parameters", 0);
        }

        /// <summary>
        /// This method creates a hexadecimal representation of a Color4f Rhino class.
        /// </summary>
        /// <param name="color">The color to cast</param>
        /// <returns>A string with the hexadecimal with # in the begin representation (without alpha channel)</returns>
        public static string GetColorHex(Color4f color)
        {
            var sysColor = color.AsSystemColor();
            return "#" + sysColor.R.ToString("X2") + sysColor.G.ToString("X2") + sysColor.B.ToString("X2");
        }
    }
}