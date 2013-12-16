/*
 * This file is part of MitsubaRenderPlugin project.
 * 
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 3 of the License, or (at your
 * option) any later version. This program is distributed in the hope that
 * it will be useful, but WITHOUT ANY WARRANTY; without even the implied
 * warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
 * 
 * Copyright 2014 TDM Solutions SL
 */

//using Mitsuba.Tools;
using System;
using System.Runtime.InteropServices;
using System.Xml;
using Rhino.Display;

namespace MitsubaRender.Materials
{
    [Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4")]
    public class MitsubaDielectricMaterial: MitsubaMaterial
    {
        public static Guid MitsubaMaterialId = new Guid("b7d7e743-edce-429a-a5ce-326cb37a3cc4");

        ////////////////
        // COMUNES
        private Color4f _color;
        private string _colorString;
        bool _textureOn;
        string _file;
        private string _idMat;
        ////////////////

        private float _intIOR;

        public MitsubaDielectricMaterial()
        {
            string paramNameReflec = ParamNameFromChildSlotName(StandardChildSlots.Diffuse.ToString());
            
        }

        public override string TypeName
        {
            get { return "Mitsuba Dielectric material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Dielectric material"; }
        }

        public override XmlElement GetMitsubaMaterialXml(XmlDocument doc, string destFile)
        {
            throw new NotImplementedException();
        }

        //public override string GetIdMaterial()
        //{
        //    throw new NotImplementedException();
        //}

        //public override string GetFileNameMaterial()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
