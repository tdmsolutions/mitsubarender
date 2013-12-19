using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;
using MitsubaRender.Materials;
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;
using Rhino.Render;

//using Mitsuba.Tools;

namespace MitsubaRender.Exporter
{
    internal class MitsubaExporter
    {
        //private MitsubaSettings m_settings;
        
        public MitsubaExporter(string basePath, string filename)
        {
            //m_settings = settings;
            _basePath = basePath;
            _filename = filename;
            _meshStore = new MeshStore(basePath);
        }

        public string Export(RhinoDoc doc)
        {
            /* Measure the time spent in Export() */
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            /* Create an empty scene DOM representation */
            _xmlDocument = new XmlDocument();
            _xmlDocument.LoadXml("<?xml version=\"1.0\"?> <scene version=\"" + MTS_VERSION + "\"/>");

            var docRoot = _xmlDocument.DocumentElement;
            _meshStore.Create(Path.GetFileNameWithoutExtension(_filename) + ".serialized");

            var sceneFile = Path.Combine(_basePath, _filename);
            try
            {
                _idMap = new Dictionary<Guid, string>();
                _xmlIdMap = new Dictionary<int, string>();
                _materialNames = new HashSet<string>();

                /* Export the integrator */
                ExportIntegrator(docRoot);

                /* Export materials */
                ExportAllMaterials(doc, docRoot);

                /* Export all instance defitions and references */
                ExportAllInstaceDefReference(doc, docRoot);

                /* Export the remaining geometry */
                ExportGeometry(doc, docRoot);

                /* Create a sensor for the current view */

                ExportSensor(docRoot, doc.Views.ActiveView.ActiveViewport);
                if (doc.RenderEnvironments.Count > 0) ExportEnvironment(doc, docRoot);
                else
                {
                    /* Create default emitter */
                    docRoot.AppendChild(CreateEmitter());
                }

                /* Write the XML scene document */
                WriteSceneDoc(sceneFile, stopwatch, m_settings);
                //WriteSceneDoc(sceneFile, stopwatch);
                var output = new FileStream(sceneFile, FileMode.Create);
                var sw = new StreamWriter(output);
                var xmlWriter = new XmlTextWriter(sw);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.Indentation = 4;
                _xmlDocument.WriteTo(xmlWriter);
                sw.Close();
                output.Close();
                stopwatch.Stop();
                Log("Export is done (took " + stopwatch.ElapsedMilliseconds + " ms)");
            }
            finally
            {
                _meshStore.Close();
            }

            return sceneFile;
        }

        

        

        private bool ExportIntegrator(XmlElement parent)
        {
            var type = "direct";
            //switch (m_settings.integrator) {
            //    case MitsubaSettings.Integrator.EVeachMLT: type = "mlt"; break;
            //    case MitsubaSettings.Integrator.EKelemenMLT: type = "pssmlt"; break;
            //    case MitsubaSettings.Integrator.EPathTracer: type = "path"; break;
            //    case MitsubaSettings.Integrator.EBidirectional: type = "bdpt"; break;
            //    case MitsubaSettings.Integrator.EDirectIllumination: type = "direct"; break;
            //    case MitsubaSettings.Integrator.EAdjointParticleTracer: type = "ptracer"; break;
            //    case MitsubaSettings.Integrator.EERPT: type = "erpt"; break;
            //    default:
            //        throw new Exception("Unknown integrator type!");
            //}
            var integrator = _xmlDocument.CreateElement("integrator");
            integrator.SetAttribute("type", type);
            //if (m_settings.integrator != MitsubaSettings.Integrator.EDirectIllumination)
            //    integrator.AppendChild(MakeProperty("maxDepth", m_settings.pathLength));
            parent.AppendChild(integrator);

            return true;
        }


        /// <summary>
        ///   Export all the materials
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="docRoot"></param>
        private void ExportEnvironment(RhinoDoc doc, XmlElement docRoot)
        {
            var environment = doc.RenderEnvironments[0];
            var simulation = new SimulatedEnvironment();
            environment.SimulateEnvironment(ref simulation, false);
            //simulation.BackgroundImage.Filename
            var fileNamefield = simulation.BackgroundImage.Filename;
            //if (environment.Fields.ContainsField("fileName"))
            //{
            //    fileNamefield = environment.Fields.GetField("filename").ToString();
            //}
            //creamos el xml element para el enviorment y lo Ñdimos al root
            var env = _xmlDocument.CreateElement("emitter");
            env.SetAttribute("type", "envmap");
            var el = XmlMitsuba.AddElement(_xmlDocument, "string", "filename", fileNamefield);
            env.AppendChild(el);
            docRoot.AppendChild(env);
        }

        /// <summary>
        /// </summary>
        /// <param name="parent">The parent node in the output XML document</param>
        /// <param name="doc"></param>
        /// <param name="idx"></param>
        /// <returns>true if any content was exported</returns>
        private bool ExportMaterial(XmlElement parent, RhinoDoc doc, int idx)
        {
            var document = new XmlDocument();
            XmlElement bsdfElement = null;
            if (doc.RenderMaterials.Count > 0)
            {
                if (doc.RenderMaterials[idx] is MitsubaDiffuseMaterial)
                {
                    var diffuseMat = doc.RenderMaterials[idx] as MitsubaDiffuseMaterial;
                    //bsdfElement = diffuseMat.GetMitsubaMaterialXml(document);
                }
            }
            else
            {
                var mat = doc.Materials[idx];
                if (mat.UseCount == 0) return false;
                /* Super-lame material exporter for now */
                var matType = "diffuse";
                if (mat.Transparency == 1.0) matType = "dielectric";
                else if (mat.Shine > 0) matType = "conductor";
                Log(" -> type = " + matType);
                var materialName = mat.Name.Length > 0 ? mat.Name : "Unnamed material";
                var counter = 1;
                while (_materialNames.Contains(materialName)) materialName = mat.Name + "_" + counter++;
                _materialNames.Add(materialName);
                _xmlIdMap[idx] = materialName;
                bsdfElement = _xmlDocument.CreateElement("bsdf");
                bsdfElement.SetAttribute("type", matType);
                bsdfElement.SetAttribute("id", materialName);
            }

            parent.AppendChild(bsdfElement);
            return true;
        }

        /// <summary>
        ///   Export an a perspective or orthographic sensor
        /// </summary>
        /// <param name="parent">The parent node in the output XML document</param>
        /// <param name="view">The RhinoViewport instance to be exported</param>
        /// <returns>true if any content was exported</returns>
        private bool ExportSensor(XmlElement parent, RhinoViewport view)
        {
            var perspective = view.IsPerspectiveProjection;
            var orthographic = view.IsParallelProjection;
            if (!perspective && !orthographic)
            {
                Log("Warning: camera type not supported -- ignoring.");
                return false;
            }
            double left, right, bottom, top, near, far;
            view.GetFrustum(out left, out right, out bottom, out top, out near, out far);
            var sensorElement = _xmlDocument.CreateElement("sensor");
            sensorElement.SetAttribute("type", perspective ? "perspective" : "orthographic");
            var toWorld = view.GetTransform(CoordinateSystem.Camera, CoordinateSystem.World);
            toWorld = toWorld*Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, -1)));
            toWorld = toWorld*Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(-1, 0, 0)));
            var toWorldElement = MakeProperty("toWorld", toWorld);
            if (perspective)
            {
                var focusDistance = view.CameraLocation.DistanceTo(view.CameraTarget);
                double halfDiag, halfVert, halfHoriz;
                view.GetCameraAngle(out halfDiag, out halfVert, out halfHoriz);
                toWorld = toWorld*Transform.Mirror(new Plane(new Point3d(0, 0, 0), new Vector3d(0, 0, -1)));
                sensorElement.AppendChild(MakeProperty("fovAxis", "diagonal"));
                sensorElement.AppendChild(MakeProperty("fov", 2*halfDiag*180/Math.PI));
                sensorElement.AppendChild(MakeProperty("focusDistance", focusDistance));
            }
            else
            {
                var scaleNode = _xmlDocument.CreateElement("scale");
                var scale = (right - left)/2;
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
            var filmElement = _xmlDocument.CreateElement("film");
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
            var samplerElement = _xmlDocument.CreateElement("sampler");
            samplerElement.SetAttribute("type", independentSampler ? "independent" : "ldsampler");
            //samplerElement.AppendChild(MakeProperty("sampleCount", m_settings.samplesPerPixel));
            samplerElement.AppendChild(MakeProperty("sampleCount", 4));
            sensorElement.AppendChild(samplerElement);
            parent.AppendChild(sensorElement);
            return true;
        }

        /// <summary>
        /// Export an entire Rhino instance definition by turning it into a Mitsuba shapegroup
        /// </summary>
        /// <param name="parent">The parent node in the output XML document</param>
        /// <param name="idef">The InstanceDefinition instance to be exported</param>
        /// <returns>true if any content was exported</returns>
        private void ExportInstanceDef(XmlElement parent, InstanceDefinition idef)
        {
            if (!idef.InUse(1)) return;

            var id = GetID("group");
            _idMap.Add(idef.Id, id);
            var shapeElement = _xmlDocument.CreateElement("shape");

            if (idef.Name.Length > 0) shapeElement.AppendChild(_xmlDocument.CreateComment(" Rhino object '" + idef.Name + "' "));
            shapeElement.SetAttribute("type", "shapegroup");
            shapeElement.SetAttribute("id", id);

            var objects = idef.GetObjects();
            var success = false;
            foreach (var o in objects) 
                success |= ExportRenderMesh(shapeElement, o);

            if (success) parent.AppendChild(shapeElement);
        }

        /// <summary>
        ///   Export an a Rhino instance reference using the 'instance' plugin
        /// </summary>
        /// <param name="parent">The parent node in the output XML document</param>
        /// <param name="inst">The InstanceObject instance to be exported</param>
        /// <returns>true if any content was exported</returns>
        private void ExportInstanceRef(XmlElement parent, InstanceObject inst)
        {
            var guid = inst.InstanceDefinition.Id;
            if (!_idMap.ContainsKey(guid))
            {
                Log("Warning: no content found -- perhaps the instance definition was empty?");
                return;
            }

            var shapeElement = _xmlDocument.CreateElement("shape");
            if (inst.Name.Length > 0) shapeElement.AppendChild(_xmlDocument.CreateComment(" Rhino object '" + inst.Name + "' "));
            if (inst.InstanceDefinition.Name.Length > 0)
                shapeElement.AppendChild(
                    _xmlDocument.CreateComment(" (references '" + inst.InstanceDefinition.Name + "') "));
            shapeElement.SetAttribute("type", "instance");
            var id = _idMap[guid];
            shapeElement.AppendChild(MakeReference(id));
            parent.AppendChild(shapeElement);
            shapeElement.AppendChild(MakeProperty("toWorld", inst.InstanceXform));
        }

        /// <summary>
        ///   Export the render mesh associated with a certain object
        /// </summary>
        /// <param name="parent">The parent node in the output XML document</param>
        /// <param name="obj">The RhinoObject instance to be exported</param>
        /// <returns>true if any content was exported</returns>
        private bool ExportRenderMesh(XmlElement parent, RhinoObject obj)
        {
            var type = obj.ObjectType;
            if (type != ObjectType.Surface && type != ObjectType.Brep && type != ObjectType.Mesh &&
                type != ObjectType.Extrusion)
            {
                Log("Not exporting object of type " + type);
                return false;
            }
            var meshes = RhinoObject.GetRenderMeshes(new[] {obj}, true, true);
            if (meshes == null) return false;
            foreach (var meshRef in meshes)
            {
                if (meshRef == null) continue;
                var shapeElement = _xmlDocument.CreateElement("shape");
                if (obj.Name.Length > 0) shapeElement.AppendChild(_xmlDocument.CreateComment(" Rhino object '" + obj.Name + "' "));
                var doc = obj.Document;
                var mesh = meshRef.Mesh();
                var matIdx = -1;
                switch (obj.Attributes.MaterialSource)
                {
                    case ObjectMaterialSource.MaterialFromLayer:
                        matIdx = doc.Layers[obj.Attributes.LayerIndex].RenderMaterialIndex;
                        break;
                    case ObjectMaterialSource.MaterialFromObject:
                        matIdx = obj.Attributes.MaterialIndex;
                        break;
                }

                var index = _meshStore.Store(mesh, obj.Name);
                shapeElement.AppendChild(MakeProperty("filename", _meshStore.Filename));
                shapeElement.AppendChild(MakeProperty("shapeIndex", index));
                shapeElement.SetAttribute("type", "serialized");
                parent.AppendChild(shapeElement);
                if (matIdx >= 0 && _xmlIdMap.ContainsKey(matIdx))
                {
                    //Referenciamos el material del modelo con el material mitsuba
                    shapeElement.AppendChild(MakeReference(_xmlIdMap[matIdx]));
                    /* Create an area emitter if requested */
                    var mat = doc.Materials[matIdx];
                    if (mat.EmissionColor.GetBrightness() > 0)
                    {
                        var emitterElement = _xmlDocument.CreateElement("emitter");
                        emitterElement.SetAttribute("type", "area");
                        emitterElement.AppendChild(MakeProperty("radiance", mat.EmissionColor));
                        shapeElement.AppendChild(emitterElement);
                    }
                }
            }

            return meshes.Length > 0;
        }

        /// <summary>
        ///   Create a XML property node
        /// </summary>
        /// <param name="name">Name of the property</param>
        /// <param name="value">
        ///   Value of the property. An appropriate property
        ///   tag will be chosen based on the value's type.
        /// </param>
        /// <returns>The created XML element</returns>
        private XmlElement MakeProperty(string name, object value)
        {
            var type = value.GetType();
            string elementType;
            if (type == typeof (string)) elementType = "string";
            else if (type == typeof (int)) elementType = "integer";
            else if (type == typeof (string)) elementType = "string";
            else if (type == typeof (float) || type == typeof (double)) elementType = "float";
            else if (type == typeof (Transform)) elementType = "transform";
            else if (type == typeof (Color)) elementType = "srgb";
            else throw new Exception("Unknown element type!");
            var element = _xmlDocument.CreateElement(elementType);
            element.SetAttribute("name", name);

            if (type == typeof (Transform))
            {
                var matrix = _xmlDocument.CreateElement("matrix");
                var trafo = (Transform) value;
                var matStr = "";
                for (var i = 0; i < 4; ++i) for (var j = 0; j < 4; ++j) matStr += trafo[i, j].ToString(CultureInfo.InvariantCulture) + ", ";
                matrix.SetAttribute("value", matStr.Substring(0, matStr.Length - 2));
                element.AppendChild(matrix);
            }
            else if (type == typeof (Color))
            {
                var color = (Color) value;
                element.SetAttribute("value",
                                     ToStringHelper(color.R/255.0f) + ", " + ToStringHelper(color.G/255.0f) + ", " +
                                     ToStringHelper(color.B/255.0f));
            }
            else if (type == typeof (float)) element.SetAttribute("value", ToStringHelper((float) value));
            else if (type == typeof (double)) element.SetAttribute("value", ToStringHelper((double) value));
            else if (type == typeof (int)) element.SetAttribute("value", ToStringHelper((int) value));
            else element.SetAttribute("value", value.ToString());

            return element;
        }

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

        


        /// <summary>
        /// Return an unique ID string for use in the generated XML document
        /// </summary>
        private string GetID(string prefix)
        {
            return prefix + _idCounter++.ToString();
        }

        /// <summary>
        /// Export all instance definitions and reference
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="docRoot"></param>
        private void ExportAllInstaceDefReference(RhinoDoc doc, XmlElement docRoot)
        {
            foreach (var idef in doc.InstanceDefinitions)
            {
                Log("Exporting instance definition '" + idef.Name + "'");
                ExportInstanceDef(docRoot, idef);
            }
            var instanceRefs = doc.Objects.FindByObjectType(ObjectType.InstanceReference);
            foreach (var o in instanceRefs)
            {
                if (o.Name.Length > 0) Log("Exporting instance reference '" + o.Name + "'");
                ExportInstanceRef(docRoot, (InstanceObject) o);
            }
        }

        /// <summary>
        /// Export the geometry
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="docRoot"></param>
        private void ExportGeometry(RhinoDoc doc, XmlElement docRoot)
        {
            var objects = new List<RhinoObject>(doc.Objects.FindByObjectType(ObjectType.Brep));
            objects.AddRange(doc.Objects.FindByObjectType(ObjectType.Extrusion));
            objects.AddRange(doc.Objects.FindByObjectType(ObjectType.Mesh));
            foreach (var o in objects) ExportRenderMesh(docRoot, o);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sceneFile"></param>
        /// <param name="stopwatch"></param>
        private void WriteSceneDoc(string sceneFile, Stopwatch stopwatch)
        {
            var output = new FileStream(sceneFile, FileMode.Create);
            var sw = new StreamWriter(output);
            var xmlWriter = new XmlTextWriter(sw)
            {
                Formatting = Formatting.Indented, 
                Indentation = 4
            };

            _xmlDocument.WriteTo(xmlWriter);

            sw.Close();
            output.Close();
            stopwatch.Stop();

            Log("Export is done (took " + stopwatch.ElapsedMilliseconds + " ms)");

            /*
            if (m_settings.rendererDirectory.Length > 0)
            {
                Process proc = new Process();
                proc.StartInfo.FileName = Path.Combine(m_settings.rendererDirectory, "mtsgui.exe");
                proc.StartInfo.Arguments = "\"" + sceneFile + "\"";
                proc.StartInfo.WorkingDirectory = m_basePath;
                proc.Start();
            }
            else
            {
                Log("Warning: Unable to launch Mitsuba: please set the renderer directory in the Rhino options.");
            }*/
        }
    }
}