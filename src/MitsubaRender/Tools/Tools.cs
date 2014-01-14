using System;
using System.IO;
using MitsubaRender.Exporter;

namespace MitsubaRender.Tools
{
    public static class FileTools
    {
        /// <summary>
        /// This method copies the file passed in the current scene folder.
        /// </summary>
        /// <param name="file">The file to copy.</param>
        /// <returns>The new path of the file to be used in the Mitsuba XML.</returns>
        public static string CopyTextureToScenePath(string file)
        {
            if (file == null) return null;

            //Copy the texture if the file does not exists
            var tempRhinoName = Path.GetFileName(file);

            if (String.IsNullOrEmpty(tempRhinoName)) return null;

            tempRhinoName = tempRhinoName.Replace('$', '-');
            var destFile = Path.Combine(MitsubaScene.BasePath, tempRhinoName);
            destFile = destFile.Replace('$', '-');
            File.Copy(file, destFile, true);

            return destFile;
            //diffuse.ReflectanceTexture = destFile;
        }
    }
}
