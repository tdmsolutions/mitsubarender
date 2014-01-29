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
	class LibraryReconstructionFilters
	{
		public const string Extension = ".ReconstructionFilter";
		private const string FilePattern = "*" + Extension;
		private static List<string> _reconstructionFilters;

		public static List<string> ReconstructionFilters
		{
			get {
				if (_reconstructionFilters == null)
					Init();

				return _reconstructionFilters;
			}

		}

		public static void Init()
		{
			_reconstructionFilters = new List<string>();

			if (FileTools.CheckOrCreateFolder(MitsubaSettings.FolderReconstructionFiltersName) != 0 ||
			    !Directory.GetFiles(MitsubaSettings.FolderReconstructionFiltersFolder, FilePattern).Any())
				MitsubaSettings.GenerateDefaultReconstructionFilters();

			foreach (var file in Directory.GetFiles(MitsubaSettings.FolderReconstructionFiltersFolder, FilePattern))
				_reconstructionFilters.Add(Path.GetFileNameWithoutExtension(file));

		}

		public static object GetReconstructionFilter(string name)
		{
			if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder)) return null;

			if (_reconstructionFilters == null || !_reconstructionFilters.Any()) Init();

			foreach (var reconstructionFiltername in _reconstructionFilters) {
				if (reconstructionFiltername == name) {
					var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name + Extension);
					return FileTools.LoadObject(filePath);
				}
			}

			return null;
		}
	}
}
