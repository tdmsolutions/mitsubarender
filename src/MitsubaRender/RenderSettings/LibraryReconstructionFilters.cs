using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MitsubaRender.Settings;

namespace MitsubaRender.Integrators
{
    class LibraryReconstructionFilters
    {
        public const string Extension = ".ReconstructionFilter";
        private const string FilePattern = "*" + Extension;
        private static List<string> _reconstructionFilters;

        public static List<string> ReconstructionFilters
        {
            get
            {
                if (_reconstructionFilters == null)
                    Init();

                return _reconstructionFilters;
            }

        }

        public static void Init()
        {
            _reconstructionFilters = new List<string>();

            if (Tools.FileTools.CheckOrCreateFolder(MitsubaSettings.FolderReconstructionFiltersName) != 0 || !Directory.GetFiles(MitsubaSettings.FolderReconstructionFiltersFolder, FilePattern).Any())
            {
                MitsubaSettings.GenerateDefaultReconstructionFilters();
            }

            foreach (var file in Directory.GetFiles(MitsubaSettings.FolderReconstructionFiltersFolder, FilePattern))
            {
                _reconstructionFilters.Add(Path.GetFileNameWithoutExtension(file));
            }

        }

        public static object GetReconstructionFilter(string name)
        {
            if (!Directory.Exists(MitsubaSettings.FolderReconstructionFiltersFolder)) return null;

            if (_reconstructionFilters == null || !_reconstructionFilters.Any()) Init();

            foreach (var reconstructionFiltername in _reconstructionFilters)
            {
                if (reconstructionFiltername == name)
                {
                    var filePath = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name + Extension);
                    return Tools.FileTools.LoadObject(filePath);
                }
            }

            return null;
        }
    }
}
