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

using System.Linq;
using System.Runtime.InteropServices;
using MitsubaRender.Materials.Interfaces;
using MitsubaRender.Materials.Wrappers;
using MitsubaRender.UI;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Render;
using Rhino.Render.Fields;

namespace MitsubaRender.Materials
{
	/// <summary>
	///   TODO summary
	/// </summary>
	[Guid("213C1BE1-CE02-410b-801B-C947470F1796")]
	public sealed class SmoothPlasticMaterial : MitsubaMaterial, IPlastic
	{
		/// <summary>
		///   Static count of Smooth Diffuse Materials used to create unique ID's.
		/// </summary>
		private static uint _count;

		#region ComboBoxes

		/// <summary>
		/// This field handles the comboBox for the IntIOR property.
		/// </summary>
		private MaterialCombo _intIORCombo;

		/// <summary>
		/// This field handles the comboBox for the ExtIOR property.
		/// </summary>
		private MaterialCombo _extIORCombo;

		/// <summary>
		/// The IntIOR Rhino field for its comboBox.
		/// </summary>
		private StringField _intIORField;

		/// <summary>
		/// The ExtIOR Rhino field for its comboBox.
		/// </summary>
		private StringField _extIORField;

		#endregion

		#region Material parameters

		/// <summary>
		///   Interior index of refraction specified numerically or using a known material name.
		///   Default: polypropylene / 1.49
		/// </summary>
		public MitsubaType<float, string> IntIOR
		{
			get;
			set;
		}

		/// <summary>
		///   Exterior index of refraction specified numerically or using a known material name.
		///   Default: air / 1.000277
		/// </summary>
		public MitsubaType<float, string> ExtIOR
		{
			get;
			set;
		}

		//specularReflectance

		/// <summary>
		///   Optional factor used tomodulate the diffuse reflection component.
		///   Default: 0.5
		/// </summary>
		public MitsubaType<Color4f, string> DiffuseReflectance
		{
			get;
			set;
		}

		/// <summary>
		///   Account for nonlinear color shifts due to internal scattering?
		///   See the main text for details.
		///   Default: Don’t account for them and preserve the texture colors, i.e. false
		/// </summary>
		public bool Nonlinear
		{
			get;
			set;
		}

		#endregion

		public override string TypeName
		{
			get {
				return "Mitsuba Smooth Plastic Material";
			}
		}

		public override string TypeDescription
		{
			get {
				return "This material describes a smooth plastic-like material with internal scattering. " +
				       "It uses the Fresnel reflection and transmission coefficients to provide direction-dependent " +
				       "specular and diffuse components. Since it is simple, realistic, and fast, this model " +
				       "is often a better choice than the phong, ward, and roughplastic plugins when rendering " +
				       "smooth plastic-like materials.";
			}
		}

		public SmoothPlasticMaterial()
		{
			IntIOR = new MitsubaType<float, string>();
			ExtIOR = new MitsubaType<float, string>();
			DiffuseReflectance = new MitsubaType<Color4f, string>();
			CreateUserInterface();
		}

		/// <summary>
		/// </summary>
		/// <returns></returns>
		public override string GetMaterialId()
		{
			if (string.IsNullOrEmpty(MaterialId)) MaterialId = "__smoothplastic" + _count++;
			return MaterialId;
		}

		/// <summary>
		/// </summary>
		protected override void CreateUserInterface()
		{
			//var intIOR_field = Fields.Add(INTIOR_FIELD, IntIOR.FirstParameter, "Interior Index of Refraction");
			//var extIOR_field = Fields.Add(EXTIOR_FIELD, ExtIOR.FirstParameter, "Exterior Index of Refraction");
			var reflectance_field = Fields.Add(REFLECTANCE_COLOR_FIELD, DiffuseReflectance.FirstParameter,
			                                   "Diffuse Reflectance Color");
			var texture_field = Fields.AddTextured(REFLECTANCE_TEXTURE_FIELD, false, "Diffuse Reflectance Texture");
			var nonlinear_field = Fields.Add(NONLINEAR_FIELD, Nonlinear, "Nonlinear");

			//BindParameterToField(INTIOR_FIELD, intIOR_field, ChangeContexts.UI);
			//BindParameterToField(EXTIOR_FIELD, extIOR_field, ChangeContexts.UI);
			BindParameterToField(REFLECTANCE_COLOR_FIELD, reflectance_field, ChangeContexts.UI);
			BindParameterToField(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT, texture_field, ChangeContexts.UI);
			BindParameterToField(NONLINEAR_FIELD, nonlinear_field, ChangeContexts.UI);

			//The comboBoxes
			_intIORField = Fields.Add(INTIOR_FIELD, IntIOR.SecondParameter);
			_extIORField = Fields.Add(EXTIOR_FIELD, ExtIOR.SecondParameter);
			BindParameterToField(INTIOR_FIELD, _intIORField, ChangeContexts.UI);
			BindParameterToField(EXTIOR_FIELD, _extIORField, ChangeContexts.UI);
		}

		/// <summary>
		///
		/// </summary>
		protected override void OnAddUserInterfaceSections()
		{
			var intIOR_section = AddUserInterfaceSection(typeof(MaterialCombo), "Interior Index of Refraction", true, true);
			_intIORCombo = (MaterialCombo)intIOR_section.Window;

			var data = new string[StandardIORTypes.Types.Count];
			int i = 0;

			foreach (var value in StandardIORTypes.Types) {
				data[i] = value.Value;
				i += 1;
			}

			_intIORCombo.Data = data;

			var extIOR_section = AddUserInterfaceSection(typeof(MaterialCombo), "Exterior Index of Refraction", true, true);
			_extIORCombo = (MaterialCombo)extIOR_section.Window;

			data = new string[StandardIORTypes.Types.Count];
			i = 0;

			foreach (var value in StandardIORTypes.Types) {
				data[i] = value.Value;
				i += 1;
			}

			_extIORCombo.Data = data;

			_intIORCombo.SelectedItem = IntIOR.SecondParameter ?? _DEFAULT_INTIOR;
			_extIORCombo.SelectedItem = ExtIOR.SecondParameter ?? _DEFAULT_EXTIOR;
			_intIORCombo.OnChange += Combo_OnChange;
			_extIORCombo.OnChange += Combo_OnChange;

			base.OnAddUserInterfaceSections();
		}

		private void Combo_OnChange(object sender, System.EventArgs e)
		{
			ReadDataFromUI();
			_intIORField.Value = IntIOR.SecondParameter;
			_extIORField.Value = ExtIOR.SecondParameter;
		}

		/// <summary>
		/// </summary>
		protected override void ReadDataFromUI()
		{
			//Interior Index of Refraction
			if (_intIORCombo != null) {
				var myValue = StandardIORTypes.Types.FirstOrDefault(x => x.Value == _intIORCombo.SelectedItem).Key;
				IntIOR.SecondParameter = myValue;
			}
			else {
				//If we're reading a RMTL file, take the value from this file
				string intIOR_key;
				Fields.TryGetValue(INTIOR_FIELD, out intIOR_key);

				//Set the combobox value
				string combo_value;
				StandardIORTypes.Types.TryGetValue(intIOR_key, out combo_value);
				if (_extIORCombo != null) _extIORCombo.SelectedItem = combo_value;
				IntIOR.SecondParameter = combo_value;
			}

			//Exterior Index of Refraction
			if (_extIORCombo != null) {
				var myValue = StandardIORTypes.Types.FirstOrDefault(x => x.Value == _extIORCombo.SelectedItem).Key;
				ExtIOR.SecondParameter = myValue;
			}
			else {
				//If we're reading a RMTL file, take the value from this file
				string extIOR_key;
				Fields.TryGetValue(EXTIOR_FIELD, out extIOR_key);

				//Set the combobox value
				string combo_value;
				StandardIORTypes.Types.TryGetValue(extIOR_key, out combo_value);
				if (_extIORCombo != null) _extIORCombo.SelectedItem = combo_value;
				ExtIOR.SecondParameter = combo_value;
			}

			//Exterior Index of Refraction
			//float extIOR;
			//Fields.TryGetValue(EXTIOR_FIELD, out extIOR);
			//ExtIOR.FirstParameter = extIOR;

			//Interior Index of Refraction
			//float intIOR;
			//if (Fields.TryGetValue(INTIOR_FIELD, out intIOR)) IntIOR.FirstParameter = intIOR;

			//Diffuse Refectance
			bool hasTexture;
			var textureParam = GetChildSlotParameter(REFLECTANCE_TEXTURE_FIELD, REFLECTANCE_TEXTURE_SLOT);
			Fields.TryGetValue(REFLECTANCE_TEXTURE_FIELD, out hasTexture);

			if (hasTexture && textureParam != null) {
				//We have texture
				var tx = FindChild(REFLECTANCE_TEXTURE_FIELD);

				if (tx != null) {
					if (tx.Fields.ContainsField("filename")) {
						var fields = tx.Fields.GetField("filename");
						DiffuseReflectance.SecondParameter = fields.ValueAsObject().ToString();
					}
					else {
						var text = tx as RenderTexture;

						if (text != null) {
							var texSim = new SimulatedTexture();
							text.SimulateTexture(ref texSim, false);
							DiffuseReflectance.SecondParameter = texSim.Filename;
						}
					}
				}
				else DiffuseReflectance.SecondParameter = string.Empty;
			}
			else {
				//We have color
				Color4f color;
				Fields.TryGetValue(REFLECTANCE_COLOR_FIELD, out color);
				DiffuseReflectance.FirstParameter = color;
				DiffuseReflectance.SecondParameter = string.Empty;
			}

			//Nonlinear
			bool nonLinear;

			if (Fields.TryGetValue(NONLINEAR_FIELD, out nonLinear)) Nonlinear = nonLinear;
		}

		/// <summary>
		/// </summary>
		/// <param name="simulation"></param>
		/// <param name="isForDataOnly"></param>
		public override void SimulateMaterial(ref Material simulation, bool isForDataOnly)
		{
			ReadDataFromUI();
			//TODO simulate SmoothPlasticMaterial in Rhino!
		}
	}
}