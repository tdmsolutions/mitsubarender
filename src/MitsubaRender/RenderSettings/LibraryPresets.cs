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
	class LibraryPresets
	{
		public const string Extension = ".Preset";
		private const string FilePattern = "*" + Extension;
		private static List<string> _presets;

		public static List<string> Presets
		{
			get {
				if (_presets == null)
					Init();

				return _presets;
			}

		}

		public static void Init()
		{
			_presets = new List<string>();

			if (FileTools.CheckOrCreateFolder(MitsubaSettings.FolderRenderSettingsPresetsName) == -1) return;

			var files = Directory.GetFiles(MitsubaSettings.FolderRenderSettingsPresetsFolder, FilePattern);

			if (files.Any())
				foreach (var file in files)
					_presets.Add(Path.GetFileNameWithoutExtension(file));

		}

		public static RenderSettingsPreset GetPreset(string name)
		{
			if (!Directory.Exists(MitsubaSettings.FolderRenderSettingsPresetsFolder)) return null;

			if (_presets == null || !_presets.Any()) Init();

			foreach (var presetName in _presets) {
				if (presetName == name) {
					var filePath = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, name + Extension);
					var obj = FileTools.LoadObject(filePath);
					var preset = obj as RenderSettingsPreset;

					if (preset != null) return preset;
				}
			}

			return null;
		}
	}
}
