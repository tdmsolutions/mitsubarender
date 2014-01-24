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
            listViewIntegrators.Clear();
            listViewIntegrators.GridLines = true;
            listViewIntegrators.FullRowSelect = true;
            var column = listViewIntegrators.Columns.Add("Mitsuba Rendering Presets");
            column.Width = 376;

            LibraryPresets.Init();
            if (LibraryPresets.Presets != null && LibraryPresets.Presets.Any())
            {
                foreach (var preset in LibraryPresets.Presets)
                {
                    listViewIntegrators.Items.Add(preset);
                }
            }

            column.TextAlign = HorizontalAlignment.Center;

            if (!String.IsNullOrEmpty(MitsubaSettings.DefaultRenderSettingsPresetName))
            {
               
                for (int i = 0; i < listViewIntegrators.Items.Count; i++)
                {
                    if (listViewIntegrators.Items[i].Text == MitsubaSettings.DefaultRenderSettingsPresetName)
                    {
                        listViewIntegrators.Items[i].Selected = true;
                        listViewIntegrators.Items[i].Focused = true;
                        listViewIntegrators.FocusedItem = listViewIntegrators.Items[i];
             
                        break;
                    }
                }
            }
        }

        private void ButtonNewPresetClick(object sender, EventArgs e)
        {
            new IntegratorDialog(String.Empty).ShowDialog();
            LoadPresets();
        }

        private void ListViewIntegratorsDoubleClick(object sender, EventArgs e)
        {
            new IntegratorDialog(listViewIntegrators.SelectedItems[0].Text).ShowDialog();
        }

        private void ButtonDeleteIntegratorClick(object sender, EventArgs e)
        {
            if (listViewIntegrators.SelectedItems.Count < 1) return;
            bool success = true;

            for (int index = 0; index < listViewIntegrators.SelectedItems.Count; index++)
            {
                var name = listViewIntegrators.SelectedItems[index].Text;
                var path = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, name) + LibraryPresets.Extension;

                if (File.Exists(path))
                {
                    try
                    {
                        File.Delete(path);
                    }
                    catch 
                    {
                        //TODO Localize me
                        success = false;
                    }
                }
            }

            LoadPresets();

            if (!success)
                MessageBox.Show("There was a problem deleting the preset(s)");


        }

        private void ListViewIntegratorsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewIntegrators.SelectedItems.Count == 1)
            {
                MitsubaSettings.DefaultRenderSettingsPresetName = listViewIntegrators.SelectedItems[0].Text;

                var preset = LibraryPresets.GetPreset(MitsubaSettings.DefaultRenderSettingsPresetName);
                if (preset != null)
                {
                    var integrator = LibraryIntegrators.GetIntegrator(preset.IntegratorName);
                    var sampler = LibrarySamplers.GetSampler(preset.SamplerName);
                    var reconstructionFilter = LibraryReconstructionFilters.GetReconstructionFilter(preset.ReconstructionFilterName);

                    if (integrator != null) MitsubaSettings.Integrator = integrator;
                    if (sampler != null) MitsubaSettings.Sampler = sampler;
                    if (reconstructionFilter != null) MitsubaSettings.ReconstructionFilter = reconstructionFilter;
                }
            }
            MitsubaSettings.SaveSettings();
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
            MitsubaSettings.LoadSettings();
            _control.LoadPresets();
            _control.Focus();
            _control.listViewIntegrators.Focus();
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