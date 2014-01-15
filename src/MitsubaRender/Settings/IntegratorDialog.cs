using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MitsubaRender.Integrators;

namespace MitsubaRender.Settings
{
    public partial class IntegratorDialog : Form
    {

        public IntegratorDialog()
        {
            InitializeComponent();
        }

        private void IntegratorDialogLoad(object sender, EventArgs e)
        {
            comboBoxIntegrator.DataSource = MitsubaSettings.IntegratorData;
            comboBoxSampler.DataSource = MitsubaSettings.SamplerData;
            comboBoxReconstruction.DataSource = MitsubaSettings.ReconstructionData;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {

        }

        private void ComboBoxIntegratorSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 0;
            switch (comboBoxIntegrator.SelectedIndex)
            {
                case 0:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.AmbientOclusion;
                    break;

            }
        }

        private void ComboBoxSamplerSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 1;
        }

        private void ComboBoxReconstructionSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 2;
        }


    }
}
