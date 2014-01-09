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
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using MitsubaRender.Exporter;
using MitsubaRender.Settings;
using Rhino;
using Rhino.Commands;
using Rhino.Display;
using Rhino.PlugIns;
using Rhino.Render;

namespace MitsubaRender
{
    /// <summary>
    ///   <para>
    ///     Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    ///     class. DO NOT create instances of this class yourself. It is the
    ///     responsibility of Rhino to create an instance of this class.
    ///   </para>
    ///   <para>
    ///     To complete plug-in information, please also see all PlugInDescription
    ///     attributes in AssemblyInfo.cs (you might need to click "Project" ->
    ///     "Show All Files" to see it in the "Solution Explorer" window).
    ///   </para>
    /// </summary>
    public class MitsubaRenderPlugIn : RenderPlugIn
    {
        public MitsubaRenderPlugIn()
        {
            Instance = this;
        }

        /// <summary>Gets the only instance of the RhinoRenderPlugInPlugIn plug-in.</summary>
        public static MitsubaRenderPlugIn Instance { get; private set; }

        /// <summary>
        ///   Is called when the user calls the _Render command.
        /// </summary>
        /// <param name="doc">The document to be rendered.</param>
        /// <param name="mode">The run mode: interactive or scripted.</param>
        /// <param name="fastPreview">Whether the render is in preview-mode.</param>
        /// <returns>The result of the command.</returns>
        protected override Result Render(RhinoDoc doc, RunMode mode, bool fastPreview)
        {
            //TODO: Rhino doc ...
            //TODO: Crear xml(mitsuba) de todo el documento
            var basePath = MitsubaSettings.WorkingDirectory;
            var name = Path.GetFileNameWithoutExtension(String.IsNullOrEmpty(RhinoDoc.ActiveDoc.Path)
                                                            ? Path.GetTempFileName()
                                                            : RhinoDoc.ActiveDoc.Path);
            name = name.Replace(" ", "_");
            basePath = CreateDirectory(basePath, name);

            var filename = name + ".xml";
            var scene = new MitsubaScene(basePath, filename);
            var sceneFile = scene.ExportSceneFile();

            //try
            //{
                
            //}
            //catch (Exception ex)
            //{
            //    RhinoApp.WriteLine(ex.ToString());
            //    return Result.Failure;
            //}

            //TODO: Añadirlo a un archivo temporal
            //TODO:llamar a mitsuba con el xml como argumento
            var proc = new Process
            {
                StartInfo = {FileName = MitsubaSettings.MitsubaPath, Arguments = sceneFile, WorkingDirectory = basePath}
            };

            proc.Start();
            //proc.Disposed += RenderEnd;
            return Result.Success;
        }

        /// <summary>
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private string CreateDirectory(string basePath, string name)
        {
            var path = Path.Combine(basePath, name);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                Directory.CreateDirectory(path);
            }

            return path;
        }

        //private void RenderEnd(object sender, EventArgs eventArgs)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        ///   Is called when the user calls the _RenderWindow command.
        /// </summary>
        /// <param name="doc">The document to be rendered.</param>
        /// <param name="mode">The run mode: interactive or scripted.</param>
        /// <param name="fastPreview">Whether the render is in preview-mode.</param>
        /// <param name="view">The view being rendered.</param>
        /// <param name="rect">The rendering rectangle.</param>
        /// <param name="inWindow">Whether rendering should appear in the window.</param>
        /// <returns>The result of the command.</returns>
        protected override Result RenderWindow(RhinoDoc doc, RunMode mode, bool fastPreview, RhinoView view,
                                               Rectangle rect, bool inWindow)
        {
            throw new NotImplementedException();
            //return Result.Success;
        }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            RenderContent.RegisterContent(this);
            var pluginPath = Assembly.GetExecutingAssembly().Location;

            MitsubaSettings.ApplicationPath = Path.GetDirectoryName(pluginPath);
            MitsubaSettings.MitsubaPath = Path.Combine(MitsubaSettings.ApplicationPath, "Mitsuba\\mtsgui.exe");

            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            MitsubaSettings.WorkingDirectory = Path.Combine(docFolder, "Mitsuba");

            if (!Directory.Exists(MitsubaSettings.WorkingDirectory)) 
                Directory.CreateDirectory(MitsubaSettings.WorkingDirectory);

            return base.OnLoad(ref errorMessage);
        }

        protected override bool AllowChooseContent(RenderContent content)
        {
            return base.AllowChooseContent(content);
        }
    }
}