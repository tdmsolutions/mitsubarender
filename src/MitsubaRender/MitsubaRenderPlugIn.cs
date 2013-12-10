﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Rhino;
using Rhino.Commands;
using Rhino.Display;
using Rhino.FileIO;
using Rhino.PlugIns;
using Rhino.Render;
using Rhino.UI;
using MitsubaRender;

namespace MitsubaRender
{
    /// <summary>
    ///     <para>
    ///         Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    ///         class. DO NOT create instances of this class yourself. It is the
    ///         responsibility of Rhino to create an instance of this class.
    ///     </para>
    ///     <para>
    ///         To complete plug-in information, please also see all PlugInDescription
    ///         attributes in AssemblyInfo.cs (you might need to click "Project" ->
    ///         "Show All Files" to see it in the "Solution Explorer" window).
    ///     </para>
    /// </summary>
    public class MitsubaRenderPlugIn : RenderPlugIn
    {
        public MitsubaRenderPlugIn()
        {
            Instance = this;
        }

        /// <summary>Gets the only instance of the RhinoRenderPlugInPlugIn plug-in.</summary>
        public static MitsubaRenderPlugIn Instance { get; private set; }

        ///// Is called when the user calls the _Render command.
        /// <summary>
        /// </summary>
        /// <param name="doc">The document to be rendered.</param>
        /// <param name="mode">The run mode: interactive or scripted.</param>
        /// <param name="fastPreview">Whether the render is in preview-mode.</param>
        /// <returns>The result of the command.</returns>
        protected override Result Render(RhinoDoc doc, RunMode mode, bool fastPreview)
        {
            //TODO: Crear xml(mitsuba) de todo el documento
            string basePath = MitsubaRender.Settings.Settings.WorkingDirectory;
            string name;
            if (String.IsNullOrEmpty(doc.Path))
            {
                name = Path.GetFileNameWithoutExtension(Path.GetTempFileName());
            }
            else
            {
                name = Path.GetFileNameWithoutExtension(doc.Path);
            }
            basePath = createDirectory(basePath, name);
            string filename = name + ".xml";
            string sceneFile;
            try
            {
                var exporter = new MitsubaExporter(basePath, filename);
                sceneFile = exporter.Export(doc);
            }
            catch (Exception ex)
            {
                RhinoApp.WriteLine(ex.ToString());
                return Result.Failure;
            }

            //TODO: Añadirlo a un archivo temporal
            //TODO:llamar a mitsuba con el xml como argumento
            //Process proc = new Process();
            //proc.StartInfo.FileName = Path.Combine(RhinoRenderPlugIn.Settings.MitsubaRoot, "mtsgui.exe");
            //proc.StartInfo.Arguments = "\"" + sceneFile + "\"";
            //proc.StartInfo.WorkingDirectory = basePath;
            //proc.Start();
            //proc.Disposed += RenderEnd;

            return Result.Success;
            //throw new NotImplementedException("Render is not implemented in the RhinoRenderPlugIn.RhinoRenderPlugInPlugIn class.");
        }

        private string createDirectory(string basePath, string name)
        {
            string path = Path.Combine(basePath, name);
            if (!Directory.Exists(path))
            {
                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
            return path;
        }

        private void RenderEnd(object sender, EventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Is called when the user calls the _RenderWindow command.
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
            return Result.Success;
        }

        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and mantain plug-in wide options in a document.

        protected override void OptionsDialogPages(List<OptionsDialogPage> pages)
        {
            base.OptionsDialogPages(pages);
        }

        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            RenderContent.RegisterContent(this);

            return base.OnLoad(ref errorMessage);
        }

        protected override bool AllowChooseContent(RenderContent content)
        {
            return base.AllowChooseContent(content);
        }

        protected override void CreatePreview(CreatePreviewEventArgs args)
        {
            base.CreatePreview(args);
        }

        protected override bool ShouldCallWriteDocument(FileWriteOptions options)
        {
            return base.ShouldCallWriteDocument(options);
        }

        protected override List<FileType> SupportedOutputTypes()
        {
            return base.SupportedOutputTypes();
        }

        protected override void WriteDocument(RhinoDoc doc, BinaryArchiveWriter archive, FileWriteOptions options)
        {
            base.WriteDocument(doc, archive, options);
        }
    }
}