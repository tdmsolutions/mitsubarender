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

using MitsubaRender.UI;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;

namespace MitsubaRender.Materials
{
    /// <summary>
    ///   Parent material for all Mitsuba materials
    /// </summary>
    public abstract class MitsubaMaterial : RenderMaterial
    {
        #region Material constants

        public const string DISTRIBUTION_FIELD = "distribution";
        public const string ALPHA_FLOAT_FIELD = "alphaFloat";
        public const string ALPHA_TEXTURE_FIELD = "alphaTexture";
        public const string ALPHAU_FLOAT_FIELD = "alphaUFloat";
        public const string ALPHAU_TEXTURE_FIELD = "alphaUTexture";
        public const string ALPHAV_FLOAT_FIELD = "alphaVFloat";
        public const string ALPHAV_TEXTURE_FIELD = "alphaVTexture";
        public const string MATERIAL_FIELD = "material";
        public const string ETA_FIELD = "eta";
        public const string K_FIELD = "k";
        public const string EXT_ETA_FLOAT_FIELD = "extEtaFloat";
        public const string EXT_ETA_TEXTURE_FIELD = "extEtaTexture";
        public const string EXT_ETA_SPECTRUM_FIELD = "extEtaSpectrum";
        public const string REFLECTANCE_COLOR_FIELD = "reflectanceColor";
        public const string REFLECTANCE_TEXTURE_FIELD = "reflectanceTexture";
        public const string USE_FAST_APPROX_FIELD = "useFastApprox";
        public const string INTIOR_FIELD = "intIOR";
        public const string EXTIOR_FIELD = "extIOR";
        public const string NONLINEAR_FIELD = "nonlinear";
        public const string THICKNESS_FIELD = "thickness";
        public const string SIGMAA_COLOR_FIELD = "sigmaAColor";
        public const string SIGMAA_TEXTURE_FIELD = "sigmaATexture";

        //Texture Slots
        public const string ALPHA_TEXTURE_SLOT = "alphaTextureSlot";
        public const string ALPHAU_TEXTURE_SLOT = "alphaUTextureSlot";
        public const string ALPHAV_TEXTURE_SLOT = "alphaVTextureSlot";
        public const string EXT_ETA_TEXTURE_SLOT = "extEtaSlot";
        public const string REFLECTANCE_TEXTURE_SLOT = "reflectanceTextureSlot";
        public const string SIGMAA_TEXTURE_SLOT = "reflectanceTextureSlot";

        #endregion

        /// <summary>
        ///   The internal ID of the current material.
        /// </summary>
        protected string MaterialId;

        /// <summary>
        /// This method has to be implemented in each material. 
        /// </summary>
        /// <returns></returns>
        public abstract string GetMaterialId();

        /// <summary>
        /// This method creates the UI in Rhino for the current material.
        /// </summary>
        protected abstract void CreateUserInterface();

        /// <summary>
        /// This method reads the values introduced by the user and established class properties with them.
        /// </summary>
        protected abstract void ReadDataFromUI();

        /// <summary>
        /// This method simulates the selected material in the Rhino viewport.
        /// </summary>
        /// <param name="simulation">Set the properties of the input basic material to provide the simulation for this material.</param>
        /// <param name="isForDataOnly">Called when only asking for a hash - don't write any textures to the disk - 
        /// just provide the filenames they will get.</param>
        public abstract override void SimulateMaterial(ref Material simulation, bool isForDataOnly);

        /// <summary>
        /// This method creates a new section named "Parameters" in the material selector of the Rhino user interface.
        /// </summary>
        protected override void OnAddUserInterfaceSections()
        {
            AddAutomaticUserInterfaceSection("Mitsuba Parameters", 0);
        }

        /// <summary>
        /// This method creates a hexadecimal representation of a Color4f Rhino class.
        /// </summary>
        /// <param name="color">The color to cast</param>
        /// <returns>A string with the hexadecimal with # in the begin representation (without alpha channel)</returns>
        public static string GetColorHex(Color4f color)
        {
            var sysColor = color.AsSystemColor();
            return "#" + sysColor.R.ToString("X2") + sysColor.G.ToString("X2") + sysColor.B.ToString("X2");
        }
    }
}