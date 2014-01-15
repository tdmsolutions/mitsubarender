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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MitsubaRender.Materials.Wrappers;
using Rhino.Geometry;

namespace MitsubaRender.Emitters
{
    public class SpotLightSource : MitsubaEmitter
    {
        /// <summary>
        ///   Static emitter count used to create unique ID's.
        /// </summary>
        private static uint _count;

        #region Mitsuba parameters

        /// <summary>
        /// Specifies an optional sensor-to-world transformation.
        /// Default: none (i.e. sensor space = world space)
        /// </summary>
        public MitsubaTransform ToWorld { get; set; }

        /// <summary>
        /// Specifies the maximum radiant intensity at the center in units of power per unit steradian.
        /// Default: 1
        /// </summary>
        public float Intensity { get; set; }

        /// <summary>
        /// Cutoff angle, beyond which the spot light is completely black.
        /// Default: 20 degrees
        /// </summary>
        public float CutOffAngle { get; set; }

        /// <summary>
        /// Subtended angle of the central beam portion.
        /// Default: cutoffAngle ⋅ 3/4
        /// </summary>
        //public float BeamWidth { get; set; }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SpotLightSource(Point3d origin, Vector3d target, float intensity = 100, float angle = 20)
        {
            EmitterId = "__spotlight" + _count++;
            ToWorld = new MitsubaTransform {Origin = origin, Target = target};
            Intensity = intensity;
            CutOffAngle = angle;
        }
    }
}
