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

using Rhino.Geometry;

namespace MitsubaRender.Materials.Wrappers
{
    /// <summary>
    ///   TODO summaries MitsubaTransform !
    /// </summary>
    public class MitsubaTransform
    {
        /// <summary>
        ///   The origin point.
        /// </summary>
        public Point3d Origin { get; set; }

        /// <summary>
        ///   The directional vector.
        /// </summary>
        public Vector3d Target { get; set; }

        /// <summary>
        ///   Main and empty ctor.
        /// </summary>
        public MitsubaTransform()
        {
            Origin = new Point3d();
            Target = new Vector3d();
        }

        /// <summary>
        ///   Secondary ctor.
        /// </summary>
        /// <param name="oX">The X component of the origin.</param>
        /// <param name="oY">The Y component of the origin.</param>
        /// <param name="oZ">The Z component of the origin.</param>
        /// <param name="tX">The X component of the target.</param>
        /// <param name="tY">The Y component of the target.</param>
        /// <param name="tZ">The Z component of the target.</param>
        public MitsubaTransform(float oX, float oY, float oZ, float tX, float tY, float tZ)
        {
            Origin = new Point3d(oX, oY, oZ);
            Target = new Vector3d(tX, tY, tZ);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetOriginForMitsuba()
        {
            return Origin.X + ", " + Origin.Y + ", " + Origin.Z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetTargetForMitsuba()
        {
            return Target.X + ", " + Target.Y + ", " + Target.Z;
        }
    }
}