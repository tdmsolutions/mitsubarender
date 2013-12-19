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
        ///   If there aren't any texture (TextureFile == null) it's used a simple colour.
        /// </summary>
        public Color4f MaterialColor { get; set; }

        /// <summary>
        ///   True if it's using a texture instead of a color. NO ES NECESARIO...
        /// </summary>
        public bool HasTexture { get; set; }

        /// <summary>
        ///   The fisical path of the texture file. Usually we copy the texture file to the scene folder.
        /// </summary>
        public string TextureFile { get; set; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public abstract string GetMaterialId();

        /// <summary>
        ///   This method returns the hexadecimal representation (with # in the begin) of the MaterialColor field.
        /// </summary>
        /// <returns>A hexadecimal representation without alpha channel.</returns>
        public string GetHexColor()
        {
            var sysColor = MaterialColor.AsSystemColor();
            return "#" + sysColor.R.ToString("X2") + sysColor.G.ToString("X2") + sysColor.B.ToString("X2");
        }

        /// <summary>
        /// This method creates a new section named "Parameters" in the material selector of the Rhino user interface.
        /// </summary>
        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Parameters", 0);
        }
    }
}