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

namespace MitsubaRender.Materials.Interfaces
{
    /// <summary>
    /// TODO summary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal interface IRough<T>
    {
        /// <summary>
        /// Specifies the roughness of the unresolved surface microgeometry using the root mean square (RMS) slope of the microfacets. 
        /// Default: 0.2
        /// </summary>
        MitsubaType<T, string> Alpha { get; set; }
    }
}