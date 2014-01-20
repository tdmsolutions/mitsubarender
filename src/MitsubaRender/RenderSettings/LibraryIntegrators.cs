using System.Collections.Generic;
using System.IO;
using System.Linq;
using MitsubaRender.Settings;

namespace MitsubaRender.Integrators
{
    static class LibraryIntegrators
    {
        public const string Extension = ".Integrator";
        private const string FilePattern = "*" + Extension;
        private static List<string> _integrators;

        public static List<string> Integrators
        {
            get
            {
                if (_integrators == null)
                    Init();

                return _integrators;
            }
           
        }

        public static void Init()
        {
            _integrators = new List<string>();

            if (Tools.FileTools.CheckOrCreateFolder(MitsubaSettings.FolderIntegratorsName) != 0 || !Directory.GetFiles(MitsubaSettings.FolderIntegratorsFolder, FilePattern).Any())
            {
                MitsubaSettings.GenerateDefaultIntegrators();
            }

            foreach (var file in Directory.GetFiles(MitsubaSettings.FolderIntegratorsFolder, FilePattern))
            {
                _integrators.Add(Path.GetFileNameWithoutExtension(file));
            }

        }

        public static object GetIntegrator(string name)
        {
            if (!Directory.Exists(MitsubaSettings.FolderIntegratorsFolder)) return null;

            if (_integrators == null || !_integrators.Any()) Init();

            foreach (var integratorName in _integrators)
            {
                if (integratorName == name)
                {
                    var filePath = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, name + Extension);
                    return Tools.FileTools.LoadObject(filePath);
                }
            }

            return null;
        }
    }
}
