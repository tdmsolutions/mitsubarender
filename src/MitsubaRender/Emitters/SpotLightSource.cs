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

            ToWorld.Origin = origin; //Point
            ToWorld.Target = target; //Direction vector
            Intensity = intensity;
            CutOffAngle = angle;
        }
    }
}
