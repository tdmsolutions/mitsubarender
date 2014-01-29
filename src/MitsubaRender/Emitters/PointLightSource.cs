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

namespace MitsubaRender.Emitters
{
	/// <summary>
	/// A simple point light source, which uniformly radiates illumination into all directions.
	/// </summary>
	public class PointLightSource : MitsubaEmitter
	{
		/// <summary>
		///   Static emitter count used to create unique ID's.
		/// </summary>
		private static uint _count;

		#region Mitsuba parameters

		/// <summary>
		/// Specifies an optional sensor-to-world transformation.
		/// Default:none (i.e. sensor space = world space)
		/// </summary>
		//public MitsubaType<MitsubaTransform, MitsubaAnimation> ToWorld { get; set; }

		/// <summary>
		/// Alternative parameter for specifying the light source position.
		/// Note that only one of the parameters ToWorld and position can be used at a time.
		/// </summary>
		public Point3d Position
		{
			get;
			set;
		}

		/// <summary>
		/// Specifies the radiant intensity in units of power per unit steradian.
		/// Default: 1
		/// </summary>
		public float Intensity
		{
			get;
			set;
		}

		/// <summary>
		/// Specifies the relative amount of samples allocated to this emitter.
		/// Default: 1
		/// </summary>
		//public float SamplingWeight { get; set; }

		#endregion

		/// <summary>
		/// Main ctor.
		/// </summary>
		public PointLightSource(Point3d pos, float intensity = 1)
		{
			Position = pos;
			Intensity = intensity;
			EmitterId = "__pointlight" + _count++;
		}
	}
}