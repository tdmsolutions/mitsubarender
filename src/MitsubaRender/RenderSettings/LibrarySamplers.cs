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
	class LibrarySamplers
	{
		public const string Extension = ".Sampler";
		private const string FilePattern = "*" + Extension;
		private static List<string> _samplers;

		public static List<string> Samplers
		{
			get {
				if (_samplers == null)
					Init();

				return _samplers;
			}
		}

		public static void Init()
		{
			_samplers = new List<string>();

			if (FileTools.CheckOrCreateFolder(MitsubaSettings.FolderSamplersName) != 0 ||
			    !Directory.GetFiles(MitsubaSettings.FolderSamplersFolder, FilePattern).Any())
				MitsubaSettings.GenerateDefaultSamplers();

			foreach (var file in Directory.GetFiles(MitsubaSettings.FolderSamplersFolder, FilePattern))
				_samplers.Add(Path.GetFileNameWithoutExtension(file));

		}

		public static object GetSampler(string name)
		{
			if (!Directory.Exists(MitsubaSettings.FolderSamplersFolder)) return null;

			if (_samplers == null || !_samplers.Any()) Init();

			foreach (var samplerName in _samplers) {
				if (samplerName == name) {
					var filePath = Path.Combine(MitsubaSettings.FolderSamplersFolder, name + Extension);
					return FileTools.LoadObject(filePath);
				}
			}

			return null;
		}
	}
}
