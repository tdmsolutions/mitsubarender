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

using System.Runtime.InteropServices;
using Rhino;
using Rhino.Commands;

namespace MitsubaRender.Commands.About
{
	[Guid("7bb46650-13ad-4430-b3a6-cf6c99cf12fd")]
	public class MitsubaAbout : Command
	{
		private static MitsubaAbout _instance;

		public MitsubaAbout()
		{
			_instance = this;
		}

		/// <summary>The only instance of the MitsubaAbout command.</summary>
		public static MitsubaAbout Instance
		{
			get {
				return _instance;
			}
		}

		public override string EnglishName
		{
			get {
				return "MitsubaAbout";
			}
		}

		protected override Result RunCommand(RhinoDoc doc, RunMode mode)
		{
			var dlg = new AboutDialog();
			dlg.Show();
			return Result.Success;
		}
	}
}