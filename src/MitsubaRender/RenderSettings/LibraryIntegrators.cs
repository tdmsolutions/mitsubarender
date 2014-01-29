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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using MitsubaRender.Settings;
using MitsubaRender.Tools;

namespace MitsubaRender.RenderSettings
{
	static class LibraryIntegrators
	{
		public const string Extension = ".Integrator";
		private const string FilePattern = "*" + Extension;
		private static List<string> _integrators;

		public static List<string> Integrators
		{
			get {
				if (_integrators == null)
					Init();

				return _integrators;
			}

		}

		public static void Init()
		{
			_integrators = new List<string>();

			if (FileTools.CheckOrCreateFolder(MitsubaSettings.FolderIntegratorsName) != 0 ||
			    !Directory.GetFiles(MitsubaSettings.FolderIntegratorsFolder, FilePattern).Any())
				MitsubaSettings.GenerateDefaultIntegrators();

			foreach (var file in Directory.GetFiles(MitsubaSettings.FolderIntegratorsFolder, FilePattern))
				_integrators.Add(Path.GetFileNameWithoutExtension(file));

		}

		public static object GetIntegrator(string name)
		{
			if (!Directory.Exists(MitsubaSettings.FolderIntegratorsFolder)) return null;

			if (_integrators == null || !_integrators.Any()) Init();

			foreach (var integratorName in _integrators) {
				if (integratorName == name) {
					var filePath = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, name + Extension);
					return FileTools.LoadObject(filePath);
				}
			}

			return null;
		}
	}
}
