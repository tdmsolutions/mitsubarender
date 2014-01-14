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

using System.Runtime.InteropServices;
using Rhino.Render;

namespace MitsubaRender.Emitters
{
    /// <summary>
    /// TODO EnvironmentEmitter
    /// </summary>
    [Guid("c71c946e-c538-4162-a0c4-8a0eefd18490")]
    public class EnvironmentEmitter : RenderEnvironment
    {
        /// <summary>
        /// 
        /// </summary>
        private static int _count;

        /// <summary>
        ///   The internal ID of the current emitter.
        /// </summary>
        protected string EmitterId;

        /// <summary>
        /// 
        /// </summary>
        public string Filename { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Scale { get; set; }

        //public MitsubaTransform ToWorld { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float Gamma { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool Cache { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public float SamplingWeight { get; set; }

        /// <summary>
        /// Main ctor.
        /// </summary>
        public EnvironmentEmitter()
        {
            CreateUserInterface();
        }

        /// <summary>
        ///   This method creates the UI in Rhino for the current material.
        /// </summary>
        protected void CreateUserInterface()
        {
            //var field = Fields.Add("test", Cache, "Testing");
            var env_field = Fields.AddTextured("envfield", false, "Image HDR");
            
            //BindParameterToField("test", field, ChangeContexts.FieldInit);
            BindParameterToField("envfield", env_field, ChangeContexts.FieldInit);
        }

        public override string TypeName
        {
            get { return "Mitsuba Environment"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Environment"; }
        }

        public override void SimulateEnvironment(ref SimulatedEnvironment simualation, bool isForDataOnly)
        {
            //ReadDataFromUI();
            //simualation.BackgroundImage = new SimulatedTexture {Filename = Filename};
        }

        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Mitsuba Environment Parameters", 0);
        }

        /// <summary>
        /// This method has to be implemented in each emitter.
        /// </summary>
        /// <returns></returns>
        public string GetEmitterId()
        {
            if (string.IsNullOrEmpty(EmitterId)) EmitterId = "__envmap" + _count++;
            return EmitterId;
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected void ReadDataFromUI()
        {
            //We have texture
            var tx = FindChild("envfield");
            if (tx != null)
            {
                if (tx.Fields.ContainsField("filename"))
                {
                    var fields = tx.Fields.GetField("filename");
                    Filename = fields.ValueAsObject().ToString();
                }
                else
                {
                    var text = tx as RenderTexture;
                    if (text != null)
                    {
                        var texSim = new SimulatedTexture();
                        text.SimulateTexture(ref texSim, false);
                        Filename = texSim.Filename;
                    }
                }
            }
        }
    }
}