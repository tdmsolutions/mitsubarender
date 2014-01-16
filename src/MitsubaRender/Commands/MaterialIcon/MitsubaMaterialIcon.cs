using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using MitsubaRender.Exporter;
using MitsubaRender.Materials;
using MitsubaRender.Properties;
using MitsubaRender.Settings;
using Rhino;
using Rhino.Commands;
using Rhino.Render;
using Rhino.UI;

namespace MitsubaRender.Commands.MaterialIcon
{
    [Guid("65d68d5d-7ac4-4386-a8e3-3bb7cb3d89f4")]
    public class MitsubaMaterialIcon : Command
    {
        private static MitsubaMaterialIcon _instance;

        public MitsubaMaterialIcon()
        {
            _instance = this;
        }

        /// <summary>The only instance of the MitsubaMaterialIcon command.</summary>
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
            // Allow to select one of these
            if (RhinoDoc.ActiveDoc.RenderMaterials.Count < 1)
            {
                MessageBox.Show(@"Please, apply at least one material.", @"Mitsuba Render", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return Result.Failure;
            }

            RenderMaterial renderMaterial = null;
            if (RhinoDoc.ActiveDoc.RenderMaterials.Count == 1)
            {
                renderMaterial = RhinoDoc.ActiveDoc.RenderMaterials[0];
            }
            else
            {
                var materialName = RhinoDoc.ActiveDoc.RenderMaterials.Select(material => material.Name).ToList();

                object userSelectedMaterial = Dialogs.ShowComboListBox("Materials", "Select one material", materialName);
                foreach (RenderMaterial material in RhinoDoc.ActiveDoc.RenderMaterials.Where(material => material.Name == userSelectedMaterial.ToString()))
                {
                    renderMaterial = material;
                }
            }

            if (renderMaterial == null) return Result.Failure;

            XmlElement materialXml;
            var mitsubaMaterial = renderMaterial as MitsubaMaterial;

            // Create the material XML
            if (mitsubaMaterial != null)
            {
                materialXml = MitsubaXml.CreateMaterialXml(mitsubaMaterial);
            }
            else
            {
                MessageBox.Show(@"Please, select a Mitsuba material", @"Mitsuba Render", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return Result.Failure;
            }

            // Create the files in a temp folder
            string tmpFolder = Path.Combine(Path.GetTempPath(), "Mitsuba");
            if (!Directory.Exists(tmpFolder)) Directory.CreateDirectory(tmpFolder);

            // Copy model
            string modelFileName = Path.Combine(tmpFolder, "matpreview.serialized");
            File.WriteAllBytes(modelFileName, Resources.matpreview);

            // Copy HDRI
            string envFileName = Path.Combine(tmpFolder, "envmap.exr");
            File.WriteAllBytes(envFileName, Resources.envmap);


            // Copy the project
            string projectFileName = Path.Combine(tmpFolder, "mitsubaproject.xml");
            string project = Resources.mitsubaproject;
            project = project.Replace("[MATERIAL]", materialXml.OuterXml);
            File.WriteAllText(projectFileName, project);


            var proc = new Process
            {
                StartInfo =
                {
                    FileName = MitsubaSettings.MitsubaPath,
                    Arguments = projectFileName,
                    WorkingDirectory = tmpFolder
                }
            };

            proc.Start();

            return Result.Success;
        }
    }
}