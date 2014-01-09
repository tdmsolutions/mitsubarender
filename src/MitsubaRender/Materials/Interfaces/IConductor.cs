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

using MitsubaRender.Materials.Wrappers;
using Rhino.Display;

namespace MitsubaRender.Materials.Interfaces
{
    /// <summary>
    /// TODO summary
    /// </summary>
    internal interface IConductor
    {
        /// <summary>
        /// Name of material preset.
        /// Default: Cu / copper
        /// </summary>
        string Material { get; set; }

        /// <summary>
        /// Spectrum. Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material.
        /// </summary>
        Color4f Eta { get; set; }

        /// <summary>
        /// Spectrum. Real and imaginary components of the material’s index of refraction.
        /// Default: based on the value of material.
        /// </summary>
        Color4f K { get; set; }

        /// <summary>
        /// Real-valued index of refraction of the surrounding dielectric, or a material name of a dielectric.
        /// Default: air
        /// </summary>
        MitsubaType<float, string> ExtEta { get; set; }
    }
}