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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MitsubaRender.Integrators;
using Rhino.UI;

namespace MitsubaRender.Settings
{
    [Guid("c204ea50-7b2a-493e-bf83-74aa5f57f7af")]
    public partial class MitsubaOptionsControl : UserControl
    {
        public MitsubaOptionsControl()
        {
            InitializeComponent();
        }

        public void LoadPresets()
        {
            if (LibraryPresets.Presets != null && LibraryPresets.Presets.Any())
            {
                foreach (var preset in LibraryPresets.Presets)
                {
                    listViewIntegrators.Items.Add(preset);
                }
            }
            //TODO: Carregar els integradors
        }

        //public void SavePresets()
        //{
        //    //TODO: Guardar els integradors
        //}

        private void ButtonNewPresetClick(object sender, EventArgs e)
        {
            new IntegratorDialog(String.Empty).ShowDialog();
        }

        private void ListViewIntegratorsDoubleClick(object sender, EventArgs e)
        {
             new IntegratorDialog(listViewIntegrators.SelectedItems[0].ToString()).Show();
        }
    }

    internal class MainOptionPage : OptionsDialogPage
    {
        private readonly MitsubaOptionsControl _control = new MitsubaOptionsControl();

        public MainOptionPage()
            : base("Mitsuba")
        {
        }

        public override Control PageControl
        {
            get { return _control; }
        }


        public override bool ShowDefaultsButton
        {
            get { return true; }
        }

        public override void OnCreateParent(IntPtr hwndParent)
        {
            _control.LoadPresets();
        }

        public override void OnSizeParent(int cx, int cy)
        {
            _control.Height = cx;
            _control.Width = cy;
        }

        //public override bool OnApply()
        //{
        //    _control.SavePresets();
        //    return base.OnApply();
        //}

        //public override void OnDefaults()
        //{
        //    _control.SetDefaults();
        //    base.OnDefaults();
        //}
    }

}