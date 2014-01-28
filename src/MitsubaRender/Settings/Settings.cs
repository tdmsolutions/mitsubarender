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
using System.Linq;
using MitsubaRender.Integrators;
using MitsubaRender.RenderSettings;

namespace MitsubaRender.Settings
{

	internal static class IntegratorsDataSource
	{

	}

	public static class MitsubaSettings
	{
		public static string WorkingDirectory;
		public static string MitsubaPath;
		public static string ApplicationPath;

		public static string DefaultRenderSettingsPresetName;
		public static object Integrator = new IntegratorPhotonMapper();
		public static object Sampler = new SamplerLowDiscrepancy();
		public static object ReconstructionFilter = new ReconstructionFilterGaussianFilter();

		public const string FileConfigFileName = "Config.ini";

		//Folder Names
		public static string FolderIntegratorsName = "Integrators";
		public static string FolderSamplersName = "Samplers";
		public static string FolderReconstructionFiltersName = "Reconstruction Filters";
		public static string FolderRenderSettingsPresetsName = "Render Settings Presets";

		//Folders
		public static readonly string FolderUserFolder =
		  Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TDM Solutions", "Mitsuba Render");
		public static string FolderIntegratorsFolder = Path.Combine(new[] { FolderUserFolder, FolderIntegratorsName });
		public static string FolderSamplersFolder = Path.Combine(new[] { FolderUserFolder, FolderSamplersName });
		public static string FolderReconstructionFiltersFolder = Path.Combine(new[] { FolderUserFolder, FolderReconstructionFiltersName });
		public static string FolderRenderSettingsPresetsFolder = Path.Combine(new[] { FolderUserFolder, FolderRenderSettingsPresetsName });

		public static bool GenerateDefaultIntegrators()
		{
			Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderIntegratorsFolder));
			var success = true;

			foreach (var integrator in IntegratorObjectInstances.GetIntegratorDefaultInstances()) {
				if (!integrator.Save())
					success = false;
			}

			return success;
		}

		public static bool GenerateDefaultSamplers()
		{
			Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderSamplersFolder));
			var success = true;

			foreach (var sampler in SamplerObjectInstances.GetSamplersDefaultInstances()) {
				if (!sampler.Save())
					success = false;
			}

			return success;
		}

		public static bool GenerateDefaultReconstructionFilters()
		{
			Tools.FileTools.CheckOrCreateFolder(Path.GetDirectoryName(FolderReconstructionFiltersFolder));
			var success = true;

			foreach (var sampler in ReconstructionFilterObjectInstances.GetReconstructionFilterDefaultInstances()) {
				if (!sampler.Save())
					success = false;
			}

			return success;
		}

		public static bool SaveSettings()
		{
			if (Tools.FileTools.CheckOrCreateFolder(FolderUserFolder) == -1) return false;

			var configPath = Path.Combine(FolderUserFolder, FileConfigFileName);

			try {
				if (File.Exists(configPath))
					File.Delete(configPath);
			}
			catch {
				return false;
			}

			var settingValues = new[] {
				"FolderRenderSettingsPresetsFolder=" + FolderRenderSettingsPresetsFolder,
				"FolderIntegratorsFolder=" + FolderIntegratorsFolder,
				"FolderSamplersFolder=" + FolderSamplersFolder,
				"FolderReconstructionFiltersFolder=" + FolderReconstructionFiltersFolder,
				"DefaultRenderSettingsPreset=" + DefaultRenderSettingsPresetName
			};

			try {
				File.WriteAllLines(configPath, settingValues);
			}
			catch {
				return false;
			}

			return true;
		}

		public static bool LoadSettings()
		{
			if (Tools.FileTools.CheckOrCreateFolder(FolderUserFolder) == -1) return false;

			var configPath = Path.Combine(FolderUserFolder, FileConfigFileName);

			if (!File.Exists(configPath)) return false;

			try {
				var settings = File.ReadAllLines(configPath);

				if (!settings.Any()) return false;

				foreach (var line in settings) {
					var splited = line.Split(new[] { '=' });

					if (splited.Length != 2) continue;

					var key = splited[0];
					var value = splited[1];
					var folderName = Path.GetDirectoryName(value);

					switch (key) {
						case "FolderRenderSettingsPresetsFolder":
							if (!String.IsNullOrEmpty(folderName)) {
								FolderRenderSettingsPresetsFolder = value;
								FolderRenderSettingsPresetsName = folderName;
							}

							break;

						case "FolderIntegratorsFolder":

							if (!String.IsNullOrEmpty(folderName)) {
								FolderIntegratorsFolder = value;
								FolderIntegratorsName = folderName;
							}

							break;

						case "FolderSamplersFolder":
							if (!String.IsNullOrEmpty(folderName)) {
								FolderSamplersFolder = value;
								FolderSamplersName = folderName;
							}

							break;

						case "FolderReconstructionFiltersFolder":
							if (!String.IsNullOrEmpty(folderName)) {
								FolderReconstructionFiltersFolder = value;
								FolderReconstructionFiltersName = folderName;
							}

							break;

						case "DefaultRenderSettingsPreset": {
							if (!String.IsNullOrEmpty(value)) {
								DefaultRenderSettingsPresetName = value;
								var preset = LibraryPresets.GetPreset(value);

								if (preset != null) {
									var integrator = LibraryIntegrators.GetIntegrator(preset.IntegratorName);
									var sampler = LibrarySamplers.GetSampler(preset.SamplerName);
									var reconstructionFilter = LibraryReconstructionFilters.GetReconstructionFilter(preset.ReconstructionFilterName);

									if (integrator != null) Integrator = integrator;

									if (sampler != null) Sampler = sampler;

									if (reconstructionFilter != null) ReconstructionFilter = reconstructionFilter;
								}
							}
						}
						break;
					}
				}

				return true;
			}
			catch {
				return false;
			}
		}
	}
}