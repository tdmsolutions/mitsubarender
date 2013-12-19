// This file is part of MitsubaRenderPlugin project.
//  
// This program is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the
// Free Software Foundation; either version 3 of the License, or (at your
// option) any later version. This program is distributed in the hope that
// it will be useful, but WITHOUT ANY WARRANTY; without even the implied
// warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details. 
// 
// You should have received a copy of the GNU General Public License
// along with MitsubaRenderPlugin.  If not, see <http://www.gnu.org/licenses/>.
// 
// Copyright 2014 TDM Solutions SL

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;
using MitsubaRender.Materials;
using Rhino;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace MitsubaRender.Exporter
{
    /// <summary>
    ///   This class handles the XML file.
    /// </summary>
    internal class MitsubaXml
    {
        #region Private fields

        /// <summary>
        /// </summary>
        private static XmlDocument _document;

        /// <summary>
        /// This fields contains the geometry ID with its Mitsuba material.
        /// </summary>
        public Dictionary<Guid, string> _mitsubaObjects;

        #endregion

        public MitsubaXml()
        {
            _document = new XmlDocument();
            _document.LoadXml("<?xml version=\"1.0\"?> <scene version=\"" + MitsubaScene.MITSUBA_SCENE_VERSION + "\"/>");
            _mitsubaObjects = new Dictionary<Guid, string>();
        }

        #region Public methods

        /// <summary>
        ///   TODO summary
        /// </summary>
        public void AddToXmlRoot(XmlElement element)
        {
            GetRootElement().AppendChild(element);
        }

        /// <summary>
        ///   Create an XML reference node
        /// </summary>
        /// <param name="name">Name of the referenced object</param>
        /// <returns>The created XML element</returns>
        public XmlElement MakeReference(string name)
        {
            var element = _document.CreateElement("ref");
            element.SetAttribute("id", name);
            return element;
        }

        /// <summary>
        ///   Create default Emitter
        /// </summary>
        /// <returns>XmlElement with a default emitter</returns>
        public XmlElement CreateDefaultEmitter()
        {
            var defaultEmitter = _document.CreateElement("emitter");
            var emitterTrafo = _document.CreateElement("transform");
            var rotateTrafo = _document.CreateElement("rotate");
            var extendProperty = _document.CreateElement("boolean");
            var sunScale = _document.CreateElement("float");
            defaultEmitter.SetAttribute("type", "sunsky");
            extendProperty.SetAttribute("name", "extend");
            extendProperty.SetAttribute("value", "true");
            sunScale.SetAttribute("name", "sunRadiusScale");
            sunScale.SetAttribute("value", "10");
            emitterTrafo.SetAttribute("name", "toWorld");
            rotateTrafo.SetAttribute("x", "1");
            rotateTrafo.SetAttribute("angle", "90");
            emitterTrafo.AppendChild(rotateTrafo);
            defaultEmitter.AppendChild(emitterTrafo);
            defaultEmitter.AppendChild(extendProperty);
            defaultEmitter.AppendChild(sunScale);

            return defaultEmitter;
        }

        /// <summary>
        ///   TODO summary
        /// </summary>
        public bool CreateDefaultIntegrator()
        {
            const string type = "photonmapper";

            var integrator = _document.CreateElement("integrator");
            integrator.SetAttribute("type", type);
            GetRootElement().AppendChild(integrator);

            return true;
        }

        /// <summary>
        ///   This method defines the default integrator. Must be Photon mapper ?!
        /// </summary>
        public void CreateMaterial(MitsubaMaterial material, Guid objId, bool isDuplicated)
        {
            XmlElement result = null;
            
            if (!isDuplicated)
            {
                if (material.HasTexture)
                {
                    //Copy the texture if the file does not exists
                    var tempRhinoName = Path.GetFileName(material.TextureFile);
                    if (String.IsNullOrEmpty(tempRhinoName)) return;
                    tempRhinoName = tempRhinoName.Replace('$', '-');
                    var destFile = Path.Combine(MitsubaScene.BasePath, tempRhinoName);
                    destFile = destFile.Replace('$', '-');
                    File.Copy(material.TextureFile, destFile, true);
                    material.TextureFile = destFile;

                    
                }

                if (material is MitsubaDiffuseMaterial)
                    result = CreateDiffuse((MitsubaDiffuseMaterial)material);
                else if (material is MitsubaRoughMaterial)
                    result = CreateRough((MitsubaRoughMaterial)material);
                else if (material is MitsubaDielectricMaterial)
                    result = CreateDielectric((MitsubaDielectricMaterial)material);

                if (result != null) AddToXmlRoot(result);
            }

            //Store the material with the object to create a reference later
            _mitsubaObjects.Add(objId, material.GetMaterialId());
        }

        /// <summary>
        ///   The method returns the root tag of the XML file (must be a "scene" tag).
        /// </summary>
        /// <returns>The root tag of the XML file</returns>
        public XmlElement GetRootElement()
        {
            return _document.DocumentElement;
        }

        /// <summary>
        ///   It creates a new element in the XML file.
        /// </summary>
        /// <param name="tag">tag for the xml element</param>
        /// <param name="name">name="input name"</param>
        /// <param name="value">value="input value"</param>
        /// <param name="type">type="input type" </param>
        /// <returns></returns>
        public static XmlElement AddElement(string tag, string name, string value = null, string type = null)
        {
            var element = _document.CreateElement(tag);
            element.SetAttribute("name", name);
            if (!String.IsNullOrEmpty(type)) element.SetAttribute("type", type);
            if (!String.IsNullOrEmpty(value)) element.SetAttribute("value", value);

            return element;
        }

        /// <summary>
        ///   Export an a perspective or orthographic sensor
        /// </summary>
        /// <returns>True if any content was exported</returns>
        public bool ExportSensor()
        {
            var view = RhinoDoc.ActiveDoc.Views.ActiveView.ActiveViewport;
            var perspective = view.IsPerspectiveProjection;
            var orthographic = view.IsParallelProjection;

            if (!perspective && !orthographic)
            {
                RhinoApp.WriteLine("Warning: camera type not supported -- ignoring.");
                return false;
            }

            double left, right, bottom, top, near, far;
            view.GetFrustum(out left, out right, out bottom, out top, out near, out far);
            var sensorElement = _document.CreateElement("sensor");
            sensorElement.SetAttribute("type", perspective ? "perspective" : "orthographic");
            var toWorld = view.GetTransform(CoordinateSystem.Camera, CoordinateSystem.World);
            toWorld = toWorld * Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, -1)));
            toWorld = toWorld * Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(-1, 0, 0)));
            var toWorldElement = MakeProperty("toWorld", toWorld);

            if (perspective)
            {
                var focusDistance = view.CameraLocation.DistanceTo(view.CameraTarget);
                double halfDiag, halfVert, halfHoriz;
                view.GetCameraAngle(out halfDiag, out halfVert, out halfHoriz);
                //toWorld = toWorld * Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, -1)));
                sensorElement.AppendChild(MakeProperty("fovAxis", "diagonal"));
                sensorElement.AppendChild(MakeProperty("fov", 2 * halfDiag * 180 / Math.PI));
                sensorElement.AppendChild(MakeProperty("focusDistance", focusDistance));
            }
            else
            {
                var scaleNode = _document.CreateElement("scale");
                var scale = (right - left) / 2;
                scaleNode.SetAttribute("x", ToStringHelper(scale));
                scaleNode.SetAttribute("y", ToStringHelper(scale));
                toWorldElement.PrependChild(scaleNode);
            }

            // Some extra room for nativating using the interactive viewer
            near /= 10;
            far *= 10;
            sensorElement.AppendChild(toWorldElement);
            sensorElement.AppendChild(MakeProperty("nearClip", near));
            sensorElement.AppendChild(MakeProperty("farClip", far));

            var filmElement = _document.CreateElement("film");
            filmElement.SetAttribute("type", "hdrfilm");
            //filmElement.AppendChild(MakeProperty("width", m_settings.xres));
            //filmElement.AppendChild(MakeProperty("height", m_settings.yres));
            filmElement.AppendChild(MakeProperty("width", 1024));
            filmElement.AppendChild(MakeProperty("height", 768));
            sensorElement.AppendChild(filmElement);

            var independentSampler = false;
            //if (m_settings.integrator == MitsubaSettings.Integrator.EAdjointParticleTracer ||
            //    m_settings.integrator == MitsubaSettings.Integrator.EKelemenMLT ||
            //    m_settings.integrator == MitsubaSettings.Integrator.EVeachMLT) {
            //    /* These integrators require the independent sampler */
            //    independentSampler = true;
            //}

            var samplerElement = _document.CreateElement("sampler");
            samplerElement.SetAttribute("type", independentSampler ? "independent" : "ldsampler");
            //samplerElement.AppendChild(MakeProperty("sampleCount", m_settings.samplesPerPixel));
            samplerElement.AppendChild(MakeProperty("sampleCount", 4));
            sensorElement.AppendChild(samplerElement);
            GetRootElement().AppendChild(sensorElement);

            return true;
        }

        /// <summary>
        ///   This method creates a new shape in the XML file. If the shape has a reference to any BSDF
        ///   (Bidirectional scattering distribution function) it creates the ref tag.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="obj"></param>
        /// <param name="meshIndex"></param>
        /// <param name="filename"></param>
        public void CreateShape(XmlElement parent, RhinoObject obj, int meshIndex, string filename)
        {
            //TODO references ! 
            var shapeElement = _document.CreateElement("shape");
            if (obj.Name.Length > 0)
                shapeElement.AppendChild(_document.CreateComment("Rhino object '" + obj.Name + "' "));
            //var doc = obj.Document;
            //var matIdx = -1;

            //switch (obj.Attributes.MaterialSource)
            //{
            //    case ObjectMaterialSource.MaterialFromLayer:
            //        matIdx = doc.Layers[obj.Attributes.LayerIndex].RenderMaterialIndex;
            //        break;
            //    case ObjectMaterialSource.MaterialFromObject:
            //        matIdx = obj.Attributes.MaterialIndex;
            //        break;
            //}

            shapeElement.AppendChild(MakeProperty("filename", filename));
            shapeElement.AppendChild(MakeProperty("shapeIndex", meshIndex));
            shapeElement.SetAttribute("type", "serialized");
            parent.AppendChild(shapeElement);

            //BSDF references
            if (_mitsubaObjects.ContainsKey(obj.Id))
            {
                //If the object has a Mitsuba object we have to reference it !
                string materialRef;
                if (_mitsubaObjects.TryGetValue(obj.Id, out materialRef))
                {
                    shapeElement.AppendChild(MakeReference(materialRef));
                }
            }

            //if (matIdx >= 0 && _xmlIdMap.ContainsKey(matIdx))
            //{
            //    //Referenciamos el material del modelo con el material mitsuba
            //    shapeElement.AppendChild(MakeReference(_xmlIdMap[matIdx]));
            //    /* Create an area emitter if requested */
            //    var mat = doc.Materials[matIdx];
            //    if (mat.EmissionColor.GetBrightness() > 0)
            //    {
            //        var emitterElement = _document.CreateElement("emitter");
            //        emitterElement.SetAttribute("type", "area");
            //        emitterElement.AppendChild(MakeProperty("radiance", mat.EmissionColor));
            //        shapeElement.AppendChild(emitterElement);
            //    }
            //}
        }

        /// <summary>
        ///   Create a XML property node
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="value">
        ///   Value of the property. An appropriate property tag
        ///   will be chosen based on the value's type.
        /// </param>
        /// <returns>The created XML element</returns>
        public XmlElement MakeProperty(string name, object value)
        {
            var type = value.GetType();
            string elementType;

            if (type == typeof(string)) elementType = "string";
            else if (type == typeof(int)) elementType = "integer";
            else if (type == typeof(string)) elementType = "string";
            else if (type == typeof(float) || type == typeof(double)) elementType = "float";
            else if (type == typeof(Transform)) elementType = "transform";
            else if (type == typeof(Color)) elementType = "srgb";
            else throw new Exception("Unknown element type!");

            var element = _document.CreateElement(elementType);
            element.SetAttribute("name", name);

            if (type == typeof(Transform))
            {
                var matrix = _document.CreateElement("matrix");
                var trafo = (Transform)value;
                var matStr = "";
                for (var i = 0; i < 4; ++i) for (var j = 0; j < 4; ++j) matStr += trafo[i, j].ToString(CultureInfo.InvariantCulture) + ", ";
                matrix.SetAttribute("value", matStr.Substring(0, matStr.Length - 2));
                element.AppendChild(matrix);
            }
            else if (type == typeof(Color))
            {
                var color = (Color)value;
                element.SetAttribute("value",
                                     ToStringHelper(color.R / 255.0f) + ", " + ToStringHelper(color.G / 255.0f) + ", " +
                                     ToStringHelper(color.B / 255.0f));
            }
            else if (type == typeof(float)) element.SetAttribute("value", ToStringHelper((float)value));
            else if (type == typeof(double)) element.SetAttribute("value", ToStringHelper((double)value));
            else if (type == typeof(int)) element.SetAttribute("value", ToStringHelper((int)value));
            else element.SetAttribute("value", value.ToString());

            return element;
        }

        /// <summary>
        ///   This method writes the XML scene file.
        /// </summary>
        /// <param name="sceneFile">The XML raw text.</param>
        public void WriteData(string sceneFile)
        {
            var output = new FileStream(sceneFile, FileMode.Create);
            var sw = new StreamWriter(output);
            var xmlWriter = new XmlTextWriter(sw) { Formatting = Formatting.Indented, Indentation = 4 };
            _document.WriteTo(xmlWriter);
            sw.Close();
            output.Close();
        }

        #endregion

        #region Private methods

        #region String helpers

        private static string ToStringHelper(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static string ToStringHelper(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        private static string ToStringHelper(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        #endregion

        #region Material static methods

        private static XmlElement CreateDiffuse(MitsubaDiffuseMaterial material)
        {
            var element = _document.CreateElement("bsdf");
            element.SetAttribute("type", "diffuse");
            element.SetAttribute("id", material.GetMaterialId());

            if (material.HasTexture)
            {
                var texture = AddElement("texture", "reflectance", null, "bitmap");
                texture.AppendChild(AddElement("string", "filename", Path.GetFileName(material.TextureFile)));
                element.AppendChild(texture);
            }
            else
            {
                var color = material.GetHexColor();
                element.AppendChild(AddElement("srgb", "reflectance", color));
            }

            return element;
        }

        private static XmlElement CreateRough(MitsubaRoughMaterial material)
        {
            //TODO
            return null;
        }

        private static XmlElement CreateDielectric(MitsubaDielectricMaterial material)
        {
            var element = _document.CreateElement("bsdf");
            element.SetAttribute("type", "dielectric");
            element.SetAttribute("id", material.GetMaterialId());
            element.AppendChild(AddElement("float", "intIOR", material.IntIOR + "")); //TODO fix me!
            element.AppendChild(AddElement("float", "extIOR", material.ExtIOR + ""));

            return element;
        }

        #endregion

        //TODO implement integrator
        //private XmlElement DefineIntegrator(bool softRays, double darkMatter)
        //{
        //    var toRet = _document.CreateElement("integrator");
        //    toRet.SetAttribute("type", "amazing");
        //    var boolean = _document.CreateElement("boolean");
        //    boolean.SetAttribute("name", "softerRays");
        //    boolean.SetAttribute("value", "true");
        //    var flo = _document.CreateElement("float");
        //    flo.SetAttribute("name", "darkMatter");
        //    flo.SetAttribute("value", "0.4");
        //    toRet.AppendChild(boolean);
        //    toRet.AppendChild(flo);
        //    return toRet;
        //}

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////

        //NOTA: El metodo ExportAllInstaceDefReference guarda las copias de los objetos del documento transformadas
        //con el objetivo de ser mas eficiente (en vez de usar copias de la geometría)

        ///// <summary>
        ///// Export all instance definitions and reference
        ///// </summary>
        ///// <param name="doc"></param>
        ///// <param name="docRoot"></param>
        //private void ExportAllInstaceDefReference(int index, string meshStoreFileName)
        //{
        //    foreach (InstanceDefinition idef in RhinoDoc.ActiveDoc.InstanceDefinitions)
        //    {
        //        RhinoApp.WriteLine("Exporting instance definition '" + idef.Name + "'");
        //        ExportInstanceDef(_document.DocumentElement, idef);
        //    }

        //    RhinoObject[] instanceRefs = RhinoDoc.ActiveDoc.Objects.FindByObjectType(ObjectType.InstanceReference);
        //    foreach (RhinoObject o in instanceRefs)
        //    {
        //        if (o.Name.Length > 0)
        //            RhinoApp.WriteLine("Exporting instance reference '" + o.Name + "'");
        //        ExportInstanceRef(_document.DocumentElement, (InstanceObject)o);
        //    }
        //}

        ///// <summary>
        ///// Export an a Rhino instance reference using the 'instance' plugin
        ///// </summary>
        ///// <param name="parent">The parent node in the output XML document</param>
        ///// <param name="inst">The InstanceObject instance to be exported</param>
        ///// <returns>true if any content was exported</returns>
        //private bool ExportInstanceRef(XmlElement parent, InstanceObject inst)
        //{
        //    Guid guid = inst.InstanceDefinition.Id;

        //    if (!_idMap.ContainsKey(guid))
        //    {
        //        RhinoApp.WriteLine("Warning: no content found -- perhaps the instance definition was empty?");
        //        return false;
        //    }

        //    XmlElement shapeElement = _document.CreateElement("shape");
        //    if (inst.Name.Length > 0)
        //        shapeElement.AppendChild(_document.CreateComment(" Rhino object '" + inst.Name + "' "));
        //    if (inst.InstanceDefinition.Name.Length > 0)
        //        shapeElement.AppendChild(_document.CreateComment(" (references '"
        //            + inst.InstanceDefinition.Name + "') "));

        //    shapeElement.SetAttribute("type", "instance");

        //    string id = _idMap[guid];
        //    shapeElement.AppendChild(MakeReference(id));
        //    parent.AppendChild(shapeElement);
        //    shapeElement.AppendChild(MakeProperty("toWorld", inst.InstanceXform));
        //    return true;
        //}

        ///// <summary>
        ///// Export an entire Rhino instance definition by turning it into a Mitsuba shapegroup
        ///// </summary>
        ///// <param name="parent">The parent node in the output XML document</param>
        ///// <param name="idef">The InstanceDefinition instance to be exported</param>
        ///// <returns>true if any content was exported</returns>
        //private bool ExportInstanceDef(XmlElement parent, InstanceDefinition idef)
        //{
        //    if (!idef.InUse(1))
        //        return false;

        //    string id = GetID("group");
        //    _idMap.Add(idef.Id, id);

        //    XmlElement shapeElement = _document.CreateElement("shape");
        //    if (idef.Name.Length > 0)
        //        shapeElement.AppendChild(_document.CreateComment(" Rhino object '" + idef.Name + "' "));

        //    shapeElement.SetAttribute("type", "shapegroup");
        //    shapeElement.SetAttribute("id", id);

        //    RhinoObject[] objects = idef.GetObjects();

        //    bool success = false;
        //    foreach (RhinoObject o in objects)
        //        success |= ExportRenderMesh(shapeElement, o);

        //    if (success)
        //        parent.AppendChild(shapeElement);
        //    return success;
        //}

        ///// <summary>
        ///// Export the render mesh associated with a certain object
        ///// </summary>
        ///// <param name="parent">The parent node in the output XML document</param>
        ///// <param name="obj">The RhinoObject instance to be exported</param>
        ///// <returns>true if any content was exported</returns>
        //private bool ExportRenderMesh(XmlElement parent, RhinoObject obj, int index, string meshStoreFileName)
        //{
        //    ObjectType type = obj.ObjectType;
        //    if (type != ObjectType.Surface && type != ObjectType.Brep &&
        //        type != ObjectType.Mesh && type != ObjectType.Extrusion)
        //    {
        //        RhinoApp.WriteLine("Not exporting object of type " + type);
        //        return false;
        //    }

        //    ObjRef[] meshes = RhinoObject.GetRenderMeshes(new[] { obj }, true, true);

        //    if (meshes == null)
        //        return false;

        //    foreach (ObjRef meshRef in meshes)
        //    {
        //        if (meshRef == null)
        //            continue;

        //        XmlElement shapeElement = _document.CreateElement("shape");
        //        if (obj.Name.Length > 0)
        //            shapeElement.AppendChild(_document.CreateComment(" Rhino object '" + obj.Name + "' "));

        //        RhinoDoc doc = obj.Document;
        //        Mesh mesh = meshRef.Mesh();

        //        int matIdx = -1;
        //        switch (obj.Attributes.MaterialSource)
        //        {
        //            case ObjectMaterialSource.MaterialFromLayer:
        //                matIdx = doc.Layers[obj.Attributes.LayerIndex].RenderMaterialIndex;
        //                break;
        //            case ObjectMaterialSource.MaterialFromObject:
        //                matIdx = obj.Attributes.MaterialIndex;
        //                break;

        //        }

        //        //int index = _meshStore.Store(mesh, obj.Name);
        //        shapeElement.AppendChild(MakeProperty("filename", meshStoreFileName)); //_meshStore.Filename
        //        shapeElement.AppendChild(MakeProperty("shapeIndex", index));
        //        shapeElement.SetAttribute("type", "serialized");
        //        parent.AppendChild(shapeElement);

        //        if (matIdx >= 0 && _xmlIdMap.ContainsKey(matIdx))
        //        {
        //            //Referenciamos el material del modelo con el material mitsuba
        //            shapeElement.AppendChild(MakeReference(_xmlIdMap[matIdx]));

        //            /* Create an area emitter if requested */
        //            Material mat = doc.Materials[matIdx];
        //            if (mat.EmissionColor.GetBrightness() > 0)
        //            {
        //                XmlElement emitterElement = _document.CreateElement("emitter");
        //                emitterElement.SetAttribute("type", "area");
        //                emitterElement.AppendChild(MakeProperty("radiance", mat.EmissionColor));
        //                shapeElement.AppendChild(emitterElement);
        //            }
        //        }
        //    }

        //    return meshes.Length > 0;
        //}

        ///// <summary>
        ///// Return an unique ID string for use in the generated XML document
        ///// </summary>
        //private string GetID(string prefix)
        //{
        //    return prefix + _idCounter++.ToString();
        //}
    }
}