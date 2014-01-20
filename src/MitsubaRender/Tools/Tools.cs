using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MitsubaRender.Exporter;
using MitsubaRender.Settings;

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
        /// <summary>
        /// Check if a folder exists, if don't, go back until UserFolder's root and create the nested directories
        /// </summary>
        /// <param name="folderName">Folder to create</param>
        /// <returns>Number of created folders or -1 if there is an error</returns>
        public static int CheckOrCreateFolder(String folderName)
        {
            var ret = 0;
            byte goBackCount = 3;
            var currentCheckingPath = Path.Combine(MitsubaSettings.FolderUserFolder, folderName);
            try
            {
                if (!String.IsNullOrEmpty(currentCheckingPath))
                {
                    while (!Directory.Exists(currentCheckingPath) && goBackCount > 0)
                    {
                        currentCheckingPath = Path.GetDirectoryName(currentCheckingPath);
                        goBackCount--;
                    }
                }

                if (goBackCount == 0)
                {
                    Directory.CreateDirectory(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                                     "TDM Solutions"));
                    ret++;
                    goBackCount++;
                }
                if (goBackCount == 1)
                {
                    Directory.CreateDirectory(
                        Path.Combine(new[]
                            {
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TDM Solutions",
                                "Mitsuba Render"
                            }));
                    ret++;
                    goBackCount++;
                }
                if (goBackCount == 2)
                {
                    Directory.CreateDirectory(
                        Path.Combine(new[]
                            {
                                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TDM Solutions",
                                "Mitsuba Render", folderName
                            }));
                    ret++;
                }
                return ret;
            }
            catch
            {
                return -1;
            }
        }
        public static bool SaveObject(string path, object obj)
        {
            var formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Create(path);
                formatter.Serialize(stream, obj);

            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                    stream.Dispose();
                }

            }
            return true;
        }
        public static Object LoadObject(string path)
        {
            if (!File.Exists(path)) return null;
            var formatter = new BinaryFormatter();
            FileStream fileStream = null;
            try
            {
                fileStream = new FileStream(path, FileMode.Open);
                var obj = formatter.Deserialize(fileStream);

                fileStream.Close();
                fileStream.Dispose();
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }

        }
    }

    public class Objects
    {

        private object DuplicateObject(object obj)
        {
            var stream = new MemoryStream();
            var bin = new BinaryFormatter();
            bin.Serialize(stream, obj);
            return bin.Deserialize(stream);
        }
    }
}
