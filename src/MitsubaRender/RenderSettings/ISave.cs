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

namespace MitsubaRender.RenderSettings
{
	public interface ISave
	{
		/// <summary>
		/// Save this object with the Type name.
		/// </summary>
		/// <returns>True if saved successfully</returns>
		bool Save();

		/// <summary>
		/// Save this object with the given name, if name is null or empty it will be .
		/// </summary>
		/// <param name="name"></param>
		/// <returns>True if saved successfully</returns>
		bool Save(String name);

	}
}
