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
	/// TODO summary RoughDiffuseMaterial
	/// </summary>
	[Guid("2dbf06cd-e57b-43b2-b990-ce70a8d86eed")]
	public sealed class RoughDiffuseMaterial : MitsubaMaterial, IDiffuse, IRough<float>
	{
		/// <summary>
		///   Static count of Smooth Diffuse Materials used to create unique ID's.
		/// </summary>
		private static uint _count;

		public override string TypeName
		{
			get {
				return "Mitsuba Rough Diffuse material";
			}
		}

		public override string TypeDescription
		{
			get {
				return "This reflectance model describes the interaction of light with a " +
				       "rough diffuse material, such as plaster, sand, clay, or concrete, or “powdery” surfaces.";
			}
		}

		#region Material Parameters

		/// <summary>
		/// Specifies the diffuse albedo of the material.
		/// Default: 0.5
		/// </summary>
		public MitsubaType<Color4f, string> Reflectance
		{
			get;
			set;
		}

		/// <summary>
		/// Specifies the roughness of the unresolved surface microgeometry using the root mean square (RMS) slope of the microfacets.
		/// Default: 0.2
		/// </summary>
		public MitsubaType<float, string> Alpha
		{
			get;
			set;
		}

		/// <summary>
		/// This parameter selects between the full version of themodel or a fast approximation that still retainsmost qualitative features.
		/// Default: false, i.e. use the high-quality version
		/// </summary>
		public bool UseFastApprox
		{
			get;
			set;
		}

		#endregion

		/// <summary>
		///
		/// </summary>
		public RoughDiffuseMaterial()
		{
			Reflectance = new MitsubaType<Color4f, string>();
			Alpha = new MitsubaType<float, string>();
			CreateUserInterface();
		}

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
			var reflectance_field = Fields.Add(REFLECTANCE_COLOR_FIELD, Reflectance.FirstParameter, "Reflectance Color");
			var texture_field = Fields.AddTextured(REFLECTANCE_TEXTURE_FIELD, false, "Reflectance Texture");
			var alpha_float_field = Fields.Add(ALPHA_FLOAT_FIELD, Alpha.FirstParameter, "Alpha Float");
			var alpha_texture_field = Fields.AddTextured(ALPHA_TEXTURE_FIELD, false, "Alpha Texture");
			var use_fast_approx_field = Fields.Add(USE_FAST_APPROX_FIELD, UseFastApprox, "Use fast approximation");

			BindParameterToField(REFLECTANCE_COLOR_FIELD, reflectance_field, ChangeContexts.UI);
			BindParameterToField(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT, texture_field, ChangeContexts.UI);
			BindParameterToField(ALPHA_FLOAT_FIELD, alpha_float_field, ChangeContexts.UI);
			BindParameterToField(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT, alpha_texture_field, ChangeContexts.UI);
			BindParameterToField(USE_FAST_APPROX_FIELD, use_fast_approx_field, ChangeContexts.UI);
		}

		/// <summary>
		/// This method reads the values introduced by the user and established class properties with them.
		/// </summary>
		protected override void ReadDataFromUI()
		{
			bool hasTexture;
			var textureParam = GetChildSlotParameter(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT);
			Fields.TryGetValue(REFLECTANCE_TEXTURE_FIELD, out hasTexture);

			//Reflectance
			if (hasTexture && textureParam != null) {
				//We have texture
				var tx = FindChild(REFLECTANCE_TEXTURE_FIELD);

				if (tx != null) {
					if (tx.Fields.ContainsField("filename")) {
						var fields = tx.Fields.GetField("filename");
						Reflectance.SecondParameter = fields.ValueAsObject().ToString();
					}
					else {
						var text = tx as RenderTexture;

						if (text != null) {
							var texSim = new SimulatedTexture();
							text.SimulateTexture(ref texSim, false);
							Reflectance.SecondParameter = texSim.Filename;
						}
					}
				}
				else Reflectance.SecondParameter = string.Empty;
			}
			else {
				//We have color
				Color4f color;
				Fields.TryGetValue(REFLECTANCE_COLOR_FIELD, out color);
				Reflectance.FirstParameter = color;
				Reflectance.SecondParameter = string.Empty;
			}

			//Alpha
			textureParam = GetChildSlotParameter(ALPHA_TEXTURE_FIELD, ALPHA_TEXTURE_SLOT);
			Fields.TryGetValue(ALPHA_TEXTURE_FIELD, out hasTexture);

			if (hasTexture && textureParam != null) {
				//We have texture
				var tx = FindChild(ALPHA_TEXTURE_FIELD);

				if (tx != null) {
					if (tx.Fields.ContainsField("filename")) {
						var fields = tx.Fields.GetField("filename");
						Alpha.SecondParameter = fields.ValueAsObject().ToString();
					}
					else {
						var text = tx as RenderTexture;

						if (text != null) {
							var texSim = new SimulatedTexture();
							text.SimulateTexture(ref texSim, false);
							Alpha.SecondParameter = texSim.Filename;
						}
					}
				}
				else Alpha.SecondParameter = string.Empty;
			}
			else {
				float alpha;
				Fields.TryGetValue(ALPHA_FLOAT_FIELD, out alpha);
				Alpha.FirstParameter = alpha;
				Reflectance.SecondParameter = string.Empty;
			}

			//Use fast approximation?
			bool useFastApprox;
			Fields.TryGetValue(USE_FAST_APPROX_FIELD, out useFastApprox);
			UseFastApprox = useFastApprox;
		}

		/// <summary>
		/// This method simulates the selected material in the Rhino viewport.
		/// </summary>
		/// <param name="simulation">Set the properties of the input basic material to provide the simulation for this material.</param>
		/// <param name="isForDataOnly">Called when only asking for a hash - don't write any textures to the disk -
		/// just provide the filenames they will get.</param>
		public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
		{
			ReadDataFromUI();
			//TODO simulate RoughDiffuse
		}
	}
}