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
using System.IO;
using MitsubaRender.Settings;
using MitsubaRender.Tools;

namespace MitsubaRender.RenderSettings
{
	[Serializable]
	class RenderSettingsPreset
	{
		public string IntegratorName
		{
			get;
			set;
		}
		public string SamplerName
		{
			get;
			set;
		}
		public string ReconstructionFilterName
		{
			get;
			set;
		}

		public bool Save(string name)
		{
			if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
				name = GetType().Name;

			if (!Directory.Exists(MitsubaSettings.FolderRenderSettingsPresetsFolder))
				return false;

			var filePath = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, name) + LibraryPresets.Extension;

			return FileTools.SaveObject(filePath, this);
		}
	}
}
