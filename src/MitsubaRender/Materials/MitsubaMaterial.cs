using System.Xml;
using Rhino.Render;

namespace MitsubaRender.MitsubaMaterials
{
    /// <summary>
    /// Parent material for all Mitsuba materials
    /// </summary>
    public abstract class MitsubaMaterial:RenderMaterial
    {
        /// <summary>
        /// Generate the xml material for mitsuba render
        /// </summary>
        /// <param name="doc">Xml document</param>
        /// <param name="destFile"></param>
        /// <returns>the material xml element</returns>
        public abstract XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile);

        public abstract string GetIdMaterial();

        public abstract string GetFileNameMaterial();

        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Parameters", 0);
        }
    }
}
