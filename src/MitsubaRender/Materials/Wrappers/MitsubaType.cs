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

namespace MitsubaRender.Materials.Wrappers
{
	/// <summary>
	///   This generic class handles the Mitsuba types "float or string" and "spectrum and string".
	/// </summary>
	/// <typeparam name="T">Must be spectrum or float</typeparam>
	/// <typeparam name="S">Must be a simple string represents a specific name or Texture (also a string)</typeparam>
	public class MitsubaType<T, S>
	{
		/// <summary>
		///   A spectrum (Color4f) or float
		/// </summary>
		public T FirstParameter
		{
			get;
			set;
		}

		/// <summary>
		///   A simple string that represents a specific name or Texture (also a string)
		/// </summary>
		public S SecondParameter
		{
			get;
			set;
		}

		/// <summary>
		///   Main ctor.
		/// </summary>
		public MitsubaType()
		{
			// Empty
		}

		/// <summary>
		///   Secondary ctor.
		/// </summary>
		/// <param name="first">Can be variable, float or string</param>
		/// <param name="second"></param>
		public MitsubaType(T first, S second)
		{
			FirstParameter = first;
			SecondParameter = second;
		}

		/// <summary>
		///   True if the SecondParameter is a not empty string. False otherwise.
		/// </summary>
		public bool HasTextureOrName
		{
			get {
				if (SecondParameter != null && SecondParameter.GetType() == typeof (string))
					return !string.IsNullOrEmpty((string) (object) SecondParameter);

				return false;
			}
		}

		/// <summary>
		///   This method returns the hexadecimal representation (with # in the begin) of the color (spectrum) property.
		/// </summary>
		/// <returns>A hexadecimal representation without alpha channel.</returns>
		public string GetColorHex()
		{
			if (FirstParameter != null && FirstParameter.GetType() == typeof (Color4f))
				return MitsubaMaterial.GetColorHex((Color4f) (object) FirstParameter);

			return null;
		}
	}
}