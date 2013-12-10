using System.Xml;
using Rhino.Render;

namespace MitsubaRender.MitsubaMaterials
{
    public abstract class MitsubaMaterial:RenderMaterial
    {
        public abstract XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile);

        public abstract string GetIdMaterial();

        public abstract string GetFileNameMaterial();

        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Parameters", 0);
        }
    }
}
