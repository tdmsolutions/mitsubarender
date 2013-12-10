//using Mitsuba.Tools;
using System;
using System.Runtime.InteropServices;
using System.Xml;
using Rhino.Display;
using MitsubaRender.MitsubaMaterials;

namespace MitsubaRender.Materials
{
    [Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4")]
    public class MitsubaDielectricMaterial: MitsubaMaterial
    {
        public static Guid MitsubaMaterialId = new Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4");

        private Color4f _color;
        private float _intIOR;
        private string _colorString;
        string _file = null;
        bool _textureOn;
        private string _idMat;

        public MitsubaDielectricMaterial()
        {
            string paramNameReflec = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            
        }

        public override string TypeName
        {
            get { throw new NotImplementedException(); }
        }

        public override string TypeDescription
        {
            get { throw new NotImplementedException(); }
        }

        public override XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile)
        {
            throw new NotImplementedException();
        }

        public override string GetIdMaterial()
        {
            throw new NotImplementedException();
        }

        public override string GetFileNameMaterial()
        {
            throw new NotImplementedException();
        }
    }
}
