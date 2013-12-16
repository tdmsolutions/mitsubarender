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

using System;
using System.Xml;

namespace MitsubaRender.Exporter
{
    public class XmlMitsuba
    {
        private readonly XmlDocument _document;
        private XmlElement _root;

        public XmlMitsuba(string version)
        {
            _document = new XmlDocument();
            _document.LoadXml("<?xml version=\"1.0\"?> <scene version=\"" + version + "\"/>");
            _root = _document.DocumentElement;
        }

        public void AddBoolean(XmlElement root, string name, bool value)
        {
            XmlElement boolean = _document.CreateElement("boolean");
            boolean.SetAttribute("name", name);
            boolean.SetAttribute("value", value.ToString().ToLower());
            root.AppendChild(boolean);
        }

        /// <summary>
        /// create a new element.
        /// </summary>
        /// <param name="doc">Xml document</param>
        /// <param name="tag">tag for the xml element</param>
        /// <param name="name">name="input name"</param>
        /// <param name="value">value="input value"</param>
        /// <param name="type">type="input type" </param>
        /// <returns></returns>
        public static XmlElement AddElement(XmlDocument doc, string tag, string name, string value = null, string type = null)
        {
            XmlElement element = doc.CreateElement(tag);
            element.SetAttribute("name", name);
            if (!String.IsNullOrEmpty(type))
                element.SetAttribute("type", type);
            if (!String.IsNullOrEmpty(value))
                element.SetAttribute("value", value);
            return element;
        }


        /// <summary>
        /// Export the Rhino material to the Mitsuba material
        /// </summary>
        public void ExportRhinoMaterial(Rhino.DocObjects.Material material)
        {
            XmlElement bsfdElement = _document.CreateElement("bsfd");
            //bsfdElement.SetAttribute("type", materialType);
        }
    }
}
