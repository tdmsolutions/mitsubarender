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
    internal interface IPlastic : IDielectric
    {
        /// <summary>
        ///   Interior index of refraction specified numerically or using a known material name.
        ///   Default: polypropylene / 1.49
        /// </summary>
        new MitsubaType<float, string> IntIOR { get; set; }

        /// <summary>
        /// Optional factor used to modulate the diffuse reflection component.
        /// Default: 0.5
        /// </summary>
        MitsubaType<Color4f, string> DiffuseReflectance { get; set; }

        /// <summary>
        /// Account for nonlinear color shifts due to internal scattering? See the main text for details. 
        /// Default: Don’t account for them and preserve the texture colors, i.e. false
        /// </summary>
        bool Nonlinear { get; set; }
    }
}