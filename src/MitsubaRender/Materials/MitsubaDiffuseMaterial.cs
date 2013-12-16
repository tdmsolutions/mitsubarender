using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using MitsubaRender.Exporter;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    ///   Mitsuba material parser, create a new material for each Mitsuba material type.
    ///   See Documantation
    /// </summary>
    [Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa")]
    public class MitsubaDiffuseMaterial : MitsubaMaterial
    {
        public static Guid MitsubaMaterialId = new Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa");

        public MitsubaDiffuseMaterial()
        {
            var paramName = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            var field = Fields.Add("reflectance", MaterialColor, "Diffuse Color");
            var bfield = Fields.AddTextured("texture", false, "Texture");

            BindParameterToField("reflectance", field, ChangeContexts.UI);
            BindParameterToField(paramName, StandardChildSlots.Diffuse.ToString(), bfield, ChangeContexts.UI);
        }

        public override string TypeName
        {
            get { return "Mitsuba Diffuse material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Diffuse material"; } //TODO better description maybe ?
        }

        protected override void OnAddUserInterfaceSections()
        {
            base.OnAddUserInterfaceSections();
        }

        public override void SimulateMaterial(ref Material simulatedMaterial, bool forDataOnly)
        {
            bool hasTexture;

            if (Fields.TryGetValue("texture", out hasTexture))
            {
                HasTexture = hasTexture;

                if (HasTexture)
                {
                    var textureParam = GetChildSlotParameter(StandardChildSlots.Diffuse.ToString(),
                                                             StandardChildSlots.Diffuse.ToString());
                    if (textureParam != null)
                    {
                        var tx = FindChild("texture");
                        if (tx == null) return;
                        if (tx.Fields.ContainsField("filename"))
                        {
                            var fields = tx.Fields.GetField("filename");
                            TextureFile = fields.ValueAsObject().ToString();
                        }
                        else
                        {
                            var text = tx as RenderTexture;
                            if (text != null)
                            {
                                var texSim = new SimulatedTexture();
                                text.SimulateTexture(ref texSim, false);
                                TextureFile = texSim.Filename;
                            }
                        }
                        simulatedMaterial.SetBitmapTexture(TextureFile);
                        return;
                    }
                }
            }
            Color4f color;
            if (Fields.TryGetValue("reflectance", out color))
            {
                MaterialColor = color;
                simulatedMaterial.DiffuseColor = color.AsSystemColor();
                //_colorString = color.R.ToString() + " ," + color.G.ToString() + " ," + color.B.ToString();
            }
            else base.SimulateMaterial(ref simulatedMaterial, forDataOnly);
        }

        public override XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile)
        {
            /* Copy texture image to destination folder */
            if (TextureFile != null)
            {
                destFile = Path.Combine(destFile, Path.GetFileName(TextureFile));
                File.Copy(TextureFile, destFile.Replace('$', '-'), true);
            }

            MaterialId = Id.ToString();
            var material = doc.CreateElement("bsdf");
            material.SetAttribute("type", "diffuse");
            material.SetAttribute("id", Id.ToString());

            if (HasTexture)
            {
                var texture = XmlMitsuba.AddElement(doc, "texture", "reflectance", null, "bitmap");
                texture.AppendChild(XmlMitsuba.AddElement(doc, "string", "filename", destFile.Replace('$', '-')));
                material.AppendChild(texture);
            }
            else
            {
                string colorString = MaterialColor.R + MaterialColor.G + MaterialColor.B + "";
                material.AppendChild(XmlMitsuba.AddElement(doc, "srgb", "reflectance", colorString));
            }

            return material;
        }
    }
}