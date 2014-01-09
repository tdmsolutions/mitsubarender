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
    internal interface ICoating : IDielectric
    {
        /// <summary>
        /// Denotes the thickness of the layer (to model absorption — should be specified in inverse units of sigmaA) 
        /// Default: 1
        /// </summary>
        float Thickness { get; set; }

        /// <summary>
        /// The absorption coefficient of the coating layer.
        /// Default: 0, i.e. there is no absorption
        /// </summary>
        MitsubaType<Color4f, string> SigmaA { get; set; }

        /// <summary>
        /// A nested BSDF model that should be coated.
        /// </summary>
        MitsubaMaterial NestedPlugin { get; set; }
    }
}