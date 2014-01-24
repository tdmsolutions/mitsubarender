using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MitsubaRender.Settings;

namespace MitsubaRender.Integrators
{
    class LibrarySamplers
    {
        public const string Extension = ".Sampler";
        private const string FilePattern = "*" + Extension;
        private static List<string> _samplers;

        public static List<string> Samplers
        {
            get
            {
                if (_samplers == null)
                    Init();

                return _samplers;
            }
        }

        public static void Init()
        {
            _samplers = new List<string>();

            if (Tools.FileTools.CheckOrCreateFolder(MitsubaSettings.FolderSamplersName) != 0 || !Directory.GetFiles(MitsubaSettings.FolderSamplersFolder, FilePattern).Any())
            {
                MitsubaSettings.GenerateDefaultSamplers();
            }

            foreach (var file in Directory.GetFiles(MitsubaSettings.FolderSamplersFolder, FilePattern))
            {
                _samplers.Add(Path.GetFileNameWithoutExtension(file));
            }

        }

        public static object GetSampler(string name)
        {
            if (!Directory.Exists(MitsubaSettings.FolderSamplersFolder)) return null;

            if (_samplers == null || !_samplers.Any()) Init();

            foreach (var samplerName in _samplers)
            {
                if (samplerName == name)
                {
                    var filePath = Path.Combine(MitsubaSettings.FolderSamplersFolder, name + Extension);
                    return Tools.FileTools.LoadObject(filePath);
                }
            }

            return null;
        }
    }
}
