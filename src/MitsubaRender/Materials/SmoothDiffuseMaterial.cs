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
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    ///   Mitsuba material parser, create a new material for each Mitsuba material type.
    ///   See Documantation
    /// </summary>
    [Guid("90f1187e-06c6-4f3d-bfc9-62e64bc632aa")]
    public sealed class SmoothDiffuseMaterial : MitsubaMaterial, IDiffuse
    {
        /// <summary>
        ///   Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static uint _count;

        #region Material Parameters

        /// <summary>
        ///   Spectrum or texture
        /// </summary>
        public MitsubaType<Color4f, string> Reflectance { get; set; }

        #endregion

        /// <summary>
        ///   Main ctor.
        /// </summary>
        public SmoothDiffuseMaterial()
        {
            Reflectance = new MitsubaType<Color4f, string>();
            CreateUserInterface();
        }

        /// <summary>
        ///   This method creates the UI in Rhino for the current material.
        /// </summary>
        protected override void CreateUserInterface()
        {
            var reflectance_field = Fields.Add(REFLECTANCE_COLOR_FIELD, Reflectance.FirstParameter, "Reflectance Color");
            var texture_field = Fields.AddTextured(REFLECTANCE_TEXTURE_FIELD, false, "Reflectance Texture");

            BindParameterToField(REFLECTANCE_COLOR_FIELD, reflectance_field, ChangeContexts.UI);
            BindParameterToField(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT, texture_field, ChangeContexts.UI);
        }

        /// <summary>
        ///   The name to show in the Rhino UI.
        /// </summary>
        public override string TypeName
        {
            get { return "Mitsuba Smooth Diffuse material"; }
        }

        /// <summary>
        /// </summary>
        public override string TypeDescription
        {
            get
            {
                return "The smooth diffuse material (also referred to as “Lambertian”) " +
                       "represents an ideally diffuse material with a user-specified " +
                       "amount of reflectance.";
            } //TODO better description maybe ?
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__diffuse" + _count++;
            return MaterialId;
        }

        /// <summary>
        ///   This method reads the values introduced by the user and established class properties with them.
        /// </summary> 
        protected override void ReadDataFromUI()
        {
            bool hasTexture;
            var textureParam = GetChildSlotParameter(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT);
            Fields.TryGetValue(REFLECTANCE_TEXTURE_FIELD, out hasTexture);

            if (hasTexture && textureParam != null)
            {
                //We have texture
                var tx = FindChild(REFLECTANCE_TEXTURE_FIELD);
                if (tx != null)
                {
                    if (tx.Fields.ContainsField("filename"))
                    {
                        var fields = tx.Fields.GetField("filename");
                        Reflectance.SecondParameter = fields.ValueAsObject().ToString();
                    }
                    else
                    {
                        var text = tx as RenderTexture;
                        if (text != null)
                        {
                            var texSim = new SimulatedTexture();
                            text.SimulateTexture(ref texSim, false);
                            Reflectance.SecondParameter = texSim.Filename;
                        }
                    }
                }
                else Reflectance.SecondParameter = string.Empty;
            }
            else
            {
                //We have color
                Color4f color;
                Fields.TryGetValue(REFLECTANCE_COLOR_FIELD, out color);
                Reflectance.FirstParameter = color;
                Reflectance.SecondParameter = string.Empty;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="simulation"></param>
        /// <param name="isForDataOnly"></param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            ReadDataFromUI();

            if (Reflectance.HasTextureOrName) 
                simulation.SetBitmapTexture(Reflectance.SecondParameter);
            else simulation.DiffuseColor = Reflectance.FirstParameter.AsSystemColor();
        }
    }
}