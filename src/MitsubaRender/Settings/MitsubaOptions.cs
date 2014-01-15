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
using System.Runtime.InteropServices;
using System.Windows.Forms;
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

        public void LoadIntegrators()
        {
            //TODO: Carregar els integradors
        }

        public void SetDefaults()
        {
            //TODO: Definir els integradors per defecte
        }

        public void SaveIntegrators()
        {
            //TODO: Guardar els integradors
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
            _control.LoadIntegrators();
        }

        public override void OnSizeParent(int cx, int cy)
        {
            _control.Height = cx;
            _control.Width = cy;
        }

        public override bool OnApply()
        {
            _control.SaveIntegrators();
            return base.OnApply();
        }

        public override void OnDefaults()
        {
            _control.SetDefaults();
            base.OnDefaults();
        }
    }

}