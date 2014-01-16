using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MitsubaRender.Exporter;
using MitsubaRender.Materials;
using Rhino;
using Rhino.Commands;

namespace MitsubaRender.Commands.MaterialIcon
{
    [System.Runtime.InteropServices.Guid("65d68d5d-7ac4-4386-a8e3-3bb7cb3d89f4")]
    public class MitsubaMaterialIcon : Command
    {
        static MitsubaMaterialIcon _instance;
        public MitsubaMaterialIcon()
        {
            _instance = this;
        }

        ///<summary>The only instance of the MitsubaMaterialIcon command.</summary>
        public static MitsubaMaterialIcon Instance
        {
            get { return _instance; }
        }

        public override string EnglishName
        {
            get { return "MitsubaMaterialIcon"; }
        }

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            
            ////// Allow to select one of these
            ////if (RhinoDoc.ActiveDoc.Materials.Count < 1)
            ////{
            ////    MessageBox.Show("Please, apply at least one material.", "Mitsuba Render", MessageBoxButtons.OK,
            ////        MessageBoxIcon.Error);
            ////    return Result.Failure;
            ////}

            ////List<string> materialName = new List<string>();
            ////foreach (var material in RhinoDoc.ActiveDoc.Materials)
            ////{
            ////    materialName.Add(material.Name);
            ////}

            ////Rhino.UI.Dialogs.ShowComboListBox("Materials", "Select one material",materialName);



            // Create the files in a temp folder
            string tmpFolder = Path.GetTempPath();
            using (Stream input = GetType().Assembly.GetManifestResourceStream("MitsubaRender.matpreview"))
            using (Stream output = File.Create(Path.Combine(tmpFolder,"Mat")))
            {
                CopyStream(input, output);
            }


            //var material = Rhino.RhinoDoc.ActiveDoc.Materials[0];
            //var type = material.GetType();
            //if (type == typeof (MitsubaMaterial))
            //{
            //    MitsubaXml.CreateMaterialXml(material);
            //}

            //
            //// Copy geometry file

            //try
            //{

            //    using (Stream resource = GetType().Assembly
            //                         .GetManifestResourceStream("MitsubaRender.matpreview.serialized"))
            //    {

            //        File.Copy()
            //        if (resource == null)
            //        {
            //            throw new ArgumentException("No such resource", "resourceName");
            //        }
            //        using (Stream output = File.OpenWrite(file))
            //        {
            //            resource.CopyTo(output);
            //        }
            //    }

            //    var _assembly = Assembly.GetExecutingAssembly();
            //   var  _imageStream = _assembly.GetManifestResourceStream("MitsubaRender.matpreview.serialized");
            //    _textStreamReader = new StreamReader(_assembly.GetManifestResourceStream("MyNamespace.MyTextFile.txt"));
            //}
            //catch
            //{
            //    MessageBox.Show("Error accessing resources!");
            //}



            // 

            return Result.Success;
        }


        public static void CopyStream(Stream input, Stream output)
        {
            // Insert null checking here for production
            byte[] buffer = new byte[8192];

            int bytesRead;
            while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, bytesRead);
            }
        }

    }
}
