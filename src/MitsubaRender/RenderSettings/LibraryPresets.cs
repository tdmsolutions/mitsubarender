using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MitsubaRender.RenderSettings;
using MitsubaRender.Settings;

namespace MitsubaRender.Integrators
{
    class LibraryPresets
    {
        public const string Extension = ".Preset";
        private const string FilePattern = "*" + Extension;
        private static List<string> _presets;

        public static List<string> Presets
        {
            get
            {
                if (_presets == null)
                    Init();

                return _presets;
            }

        }

        public static void Init()
        {
            _presets = new List<string>();

            if (Tools.FileTools.CheckOrCreateFolder(MitsubaSettings.FolderRenderSettingsPresetsName) == -1) return;

            var files = Directory.GetFiles(MitsubaSettings.FolderRenderSettingsPresetsFolder, FilePattern);
            
            if (files.Any())
                foreach (var file in files)
                {
                    _presets.Add(Path.GetFileNameWithoutExtension(file));
                }

        }

        public static RenderSettingsPreset GetPreset(string name)
        {
            if (!Directory.Exists(MitsubaSettings.FolderRenderSettingsPresetsFolder)) return null;

            if (_presets == null || !_presets.Any()) Init();

            foreach (var presetName in _presets)
            {
                if (presetName == name)
                {
                    var filePath = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, name + Extension);
                    var obj = Tools.FileTools.LoadObject(filePath);
                    var preset = obj as RenderSettingsPreset;

                    if (preset != null) return preset;
                }
            }

            return null;
        }
    }
}
