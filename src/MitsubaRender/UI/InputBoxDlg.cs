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
using System.Windows.Forms;

namespace MitsubaRender.UI
{
	public partial class InputBoxDlg : Form
	{

		public string InputText = String.Empty;
		public string TopicText = String.Empty;
		public string Titul = String.Empty;


		public InputBoxDlg()
		{
			InitializeComponent();
		}

		private void ButtonOkClick(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(InputText) || String.IsNullOrWhiteSpace(InputText)) {
				//TODO Localize me
				MessageBox.Show("Please type a name");
			}

			else {
				DialogResult = DialogResult.OK;
				Close();
			}

		}

		private void InputBoxDlgLoad(object sender, EventArgs e)
		{
			Text = Titul;
			label1.Text = TopicText;

			if (!String.IsNullOrEmpty(InputText)) textBox1.Text = InputText;

			textBox1.Focus();
		}

		private void TextBoxX1TextChanged(object sender, EventArgs e)
		{
			InputText = textBox1.Text;
		}

		private void ButtonCancelClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void TextBoxX1KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter) {
				if (String.IsNullOrEmpty(InputText) || String.IsNullOrWhiteSpace(InputText)) {
					//TODO Localize me
					MessageBox.Show("Please type a name");
				}

				else {
					DialogResult = DialogResult.OK;
					Close();
				}
			}
		}
	}
}