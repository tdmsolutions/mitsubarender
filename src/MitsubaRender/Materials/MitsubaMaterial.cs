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

using System.Collections.Generic;
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

		public const string _DEFAULT_MATERIAL = "Copper";
		public const string _DEFAULT_DISTRIBUTION = "beckmann";
		public const string _DEFAULT_INTIOR = "Diamond";
		public const string _DEFAULT_EXTIOR = "Air";

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

	/// <summary>
	/// These material names can be used with the material dielectric, roughdielectric, plastic, roughplastic, as well as coating.
	///
	/// There are 23 different types.
	/// </summary>
	public static class StandardIORTypes
	{
		/// <summary>
		/// The string values that are used in the comboBox (the TValue) and in the Mitsuba XML (TKey).
		/// The second value of the Dictionary (TValue) can be localized.
		/// </summary>
		public static readonly Dictionary<string, string> Types = new Dictionary<string, string>();

		/// <summary>
		/// Static ctor. for the values initialization
		/// </summary>
		static StandardIORTypes()
		{
			Types.Add("vacuum", "Vacuum");
			Types.Add("helium", "Helium");
			Types.Add("hydrogen", "Hydrogen");
			Types.Add("air", "Air");
			Types.Add("carbon dioxide", "Carbon dioxide");
			Types.Add("water", "Water");
			Types.Add("acetone", "Acetone");
			Types.Add("ethanol", "Ethanol");
			Types.Add("carbon tetrachloride", "Carbon tetrachloride");
			Types.Add("glycerol", "Glycerol"); //10
			Types.Add("benzene", "Benzene");
			Types.Add("silicone oil", "Silicone oil");
			Types.Add("bromine", "Bromine");
			Types.Add("water ice", "Water ice");
			Types.Add("fused quartz", "Fused quartz");
			Types.Add("pyrex", "Acrylic glass");
			Types.Add("polypropylene", "Polypropylene");
			Types.Add("bk7", "bk7");
			Types.Add("sodium chloride", "Sodium chloride");
			Types.Add("amber", "Amber"); //20
			Types.Add("pet", "Pet");
			Types.Add("diamond", "Diamond");
		}
	}

	/// <summary>
	/// This table lists all supported materials that can be passed into the conductor and roughconductor plugins.
	/// Note that some of them are not actually conductors—this is not a problem, they can be used regardless (though only the reflection
	/// component and no transmission will be simulated). In most cases, there are multiple entries for each material, which represent
	/// measurements by different authors.
	///
	/// There are 37 different types.
	/// </summary>
	public static class StandardConductorTypes
	{
		/// <summary>
		/// The string values that are used in the comboBox (the TValue) and in the Mitsuba XML (TKey).
		/// The second value of the Dictionary (TValue) can be localized.
		/// </summary>
		public static readonly Dictionary<string, string> Types = new Dictionary<string, string>();

		/// <summary>
		/// Static ctor. for the values initialization
		/// </summary>
		static StandardConductorTypes()
		{
			Types.Add("a-C", "Amorphous carbon");
			Types.Add("Ag", "Silver");
			Types.Add("Al", "Aluminium");
			Types.Add("AlAs", "Cubic aluminium arsenide");
			Types.Add("AlSb", "Cubic aluminium antimonide");
			Types.Add("Au", "Gold");
			Types.Add("Be", "Polycrystalline beryllium");
			Types.Add("Cr", "Chromium");
			Types.Add("CsI", "Cubic caesium iodide");
			Types.Add("Cu", "Copper"); //10
			Types.Add("Cu2O", "Copper (I) oxide");
			Types.Add("CuO", "Copper (II) oxide");
			Types.Add("d-C", "Cubic diamond");
			Types.Add("Hg", "Mercury");
			Types.Add("HgTe", "Mercury telluride");
			Types.Add("Ir", "Iridium");
			Types.Add("K", "Polycrystalline potassium");
			Types.Add("Li", "Lithium");
			Types.Add("MgO", "Magnesium oxide");
			Types.Add("Mo", "Molybdenum"); //20
			Types.Add("Na_palik", "Sodium");
			Types.Add("Nb", "Niobium");
			Types.Add("Ni_palik", "Nickel");
			Types.Add("Rh", "Rhodium");
			Types.Add("Se", "Selenium");
			Types.Add("SiC", "Hexagonal silicon carbide");
			Types.Add("SnTe", "Tin telluride");
			Types.Add("Ta", "Tantalum");
			Types.Add("Te", "Trigonal tellurium");
			Types.Add("ThF4", "Polycryst. thorium (IV) fluoride"); //30
			Types.Add("TiC", "Polycrystalline titanium carbide");
			Types.Add("TiN", "Titanium nitride");
			Types.Add("TiO2", "Tetragonal titan. dioxide");
			Types.Add("VC", "Vanadium carbide");
			Types.Add("V_palik", "Vanadium");
			Types.Add("VN", "Vanadium nitride");
			Types.Add("W", "Tungsten"); //37
			Types.Add("none", "None (100% reflecting mirror)");
		}
	}
}