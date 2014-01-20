using System;
using System.IO;
using MitsubaRender.Integrators;
using MitsubaRender.Settings;

namespace MitsubaRender.RenderSettings
{
     [Serializable]
    class RenderSettingsPreset
    {
        public string IntegratorName { get; set; }
        public string SamplerName { get; set; }
        public string ReconstructionFilterName { get; set; }

         public bool Save(string name)
         {
             if (String.IsNullOrEmpty(name) || String.IsNullOrWhiteSpace(name))
                 name = GetType().Name;

             if (!Directory.Exists(MitsubaSettings.FolderRenderSettingsPresetsFolder))
                 return false;

             var filePath = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, name) + LibraryPresets.Extension;

             return Tools.FileTools.SaveObject(filePath, this);
         }
    }
}
