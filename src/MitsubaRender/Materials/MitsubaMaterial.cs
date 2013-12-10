/*
 * This file is part of MitsubaRenderPlugin project.
 * 
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version. This program is distributed in the hope that
 * it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Copyright 2014 TDM Solutions SL
 */

using System.Xml;
using Rhino.Render;

namespace MitsubaRender.MitsubaMaterials
{
    /// <summary>
    /// Parent material for all Mitsuba materials
    /// </summary>
    public abstract class MitsubaMaterial:RenderMaterial
    {
        /// <summary>
        /// Generate the xml material for mitsuba render
        /// </summary>
        /// <param name="doc">Xml document</param>
        /// <param name="destFile"></param>
        /// <returns>the material xml element</returns>
        public abstract XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile);

        public abstract string GetIdMaterial();

        public abstract string GetFileNameMaterial();

        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Parameters", 0);
        }
    }
}
