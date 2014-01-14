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

namespace MitsubaRender.Materials
{
    /// <summary>
    /// TODO summary RoughDiffuseMaterial
    /// </summary>
    [Guid("2dbf06cd-e57b-43b2-b990-ce70a8d86eed")]
    public class RoughDiffuseMaterial : MitsubaMaterial, IDiffuse, IRough<Color4f>
    {
        #region Field constants

        private const string REFLECTANCE_COLOR_FIELD = "reflectanceColor";
        private const string REFLECTANCE_TEXTURE_FIELD = "reflectanceTexture";
        private const string ALPHA_FLOAT_FIELD = "alphaFloat";
        private const string ALPHA_TEXTURE_FIELD = "alphaTexture";
        private const string USE_FAST_APPROX_FIELD = "useFastApprox";

        private const string REFLECTANCE_TEXTURE_SLOT = "reflectanceTextureSlot";
        private const string ALPHA_TEXTURE_SLOT = "alphaTextureSlot";

        #endregion

        /// <summary>
        ///   Static count of Smooth Diffuse Materials used to create unique ID's.
        /// </summary>
        private static uint _count;

        public override string TypeName
        {
            get { return "TODO not working ! ---- Mitsuba Rough Diffuse material"; }
        }

        public override string TypeDescription
        {
            get { return "Mitsuba Rough Diffuse material"; } //TODO better TypeDescription
        }

        #region Material Parameters

        /// <summary>
        /// Specifies the diffuse albedo of the material. 
        /// Default: 0.5
        /// </summary>
        public MitsubaType<Color4f, string> Reflectance { get; set; }

        /// <summary>
        /// Specifies the roughness of the unresolved surface microgeometry using the root mean square (RMS) slope of the microfacets. 
        /// Default: 0.2
        /// </summary>
        public MitsubaType<Color4f, string> Alpha { get; set; }

        /// <summary>
        /// This parameter selects between the full version of themodel or a fast approximation that still retainsmost qualitative features. 
        /// Default: false, i.e. use the high-quality version
        /// </summary>
        public bool UseFastApprox { get; set; }

        #endregion

        /// <summary>
        /// This method has to be implemented in each material with 
        /// </summary>
        /// <returns></returns>
        public override string GetMaterialId()
        {
            if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__roughdiffuse" + _count++;
            return MaterialId;
        }

        /// <summary>
        /// This method creates the UI in Rhino for the current material.
        /// </summary>
        protected override void CreateUserInterface()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected override void ReadDataFromUI()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// This method simulates the selected material in the Rhino viewport.
        /// </summary>
        /// <param name="simulation">Set the properties of the input basic material to provide the simulation for this material.</param>
        /// <param name="isForDataOnly">Called when only asking for a hash - don't write any textures to the disk - 
        /// just provide the filenames they will get.</param>
        public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
        {
            throw new System.NotImplementedException();
        }
    }
}