using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using MitsubaRender.Integrators;

namespace MitsubaRender.Settings
{
    public partial class IntegratorDialog : Form
    {
        private readonly String _editingPreset;
        public IntegratorDialog(string presetName)
        {
            InitializeComponent();
            _editingPreset = presetName;
        }

        private void IntegratorDialogLoad(object sender, EventArgs e)
        {
            Text = _editingPreset;

            comboBoxIntegrator.DataSource = LibraryIntegrators.Integrators.ToArray();
            comboBoxSampler.DataSource = IntegratorsDataSource.SamplerData;
            comboBoxReconstruction.DataSource = IntegratorsDataSource.ReconstructionData;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {

        }

        private void ComboBoxIntegratorSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 0;

            var integratorName = comboBoxIntegrator.SelectedItem.ToString();
            propertyGridIntegrator.SelectedObject = LibraryIntegrators.GetIntegrator(integratorName);

            //switch (comboBoxIntegrator.SelectedIndex)
            //{

            //case (int)IntegratorType.AdjointParticleTracer:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.AdjointParticleTracer;
            //    break;
            //case (int)IntegratorType.Ambientoclusion:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.AmbientOclusion;
            //    break;
            //case (int)IntegratorType.BidirectionalPathTracer:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.BidirectionalPathTracer;
            //    break;
            //case (int)IntegratorType.DirectIlumination:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.DirectIlumination;
            //    break;
            //case (int)IntegratorType.EnergyRedisributionPathTracing:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.EnergyRedisributionPathTracing;
            //    break;
            //case (int)IntegratorType.PathTracer:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PathTracer;
            //    break;
            //case (int)IntegratorType.PhotonMapper:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PhotonMapper;
            //    break;
            //case (int)IntegratorType.PrimarySampleSpaceMLT:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PrimarySampleSpaceMLT;
            //    break;
            //case (int)IntegratorType.ProgressivePhotonMapper:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.ProgressivePhotonMapper;
            //    break;
            //case (int)IntegratorType.SampleSpaceMLT:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.SampleSpaceMLT;
            //    break;
            //case (int)IntegratorType.StochasticProgressivePhotonMapper:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.StochasticProgressivePhotonMapper;
            //    break;
            //case (int)IntegratorType.VirtualPointLightRenderer:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VirtualPointLightRenderer;
            //    break;
            //case (int)IntegratorType.VolumetricPathTracerExtended:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VolumetricPathTracerExtended;
            //    break;
            //case (int)IntegratorType.VolumetricPathTracerSimple:
            //    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VolumetricPathTracerSimple;
            //    break;
            //}
        }

        private void ComboBoxSamplerSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 1;
            //switch (comboBoxSampler.SelectedIndex)
            //{
            //    case (int)SamplerType.HaltonQMCSampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerHaltonQMC;
            //        break;
            //    case (int)SamplerType.HammersleyQMCSampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerHammersleyQMC;
            //        break;
            //    case (int)SamplerType.IndependentSampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerIndependent;
            //        break;
            //    case (int)SamplerType.LowDiscrepancySampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerLowDiscrepancy;
            //        break;
            //    case (int)SamplerType.SobolQMCSampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerSobolQMC;
            //        break;
            //    case (int)SamplerType.StraitfieldSampler:
            //        propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerStraitfield;
            //        break;
            //}
        }

        private void ComboBoxReconstructionSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 2;
            //switch (comboBoxReconstruction.SelectedIndex)
            //{
            //    case (int)ReconstructionFilterType.BoxFilter:
            //        // propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.;
            //        break;
            //    case (int)ReconstructionFilterType.CatmullRomFilter:
            //        //propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.
            //        break;
            //    case (int)ReconstructionFilterType.GaussianFilter:
            //        propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.IntegratorGaussianFilter;
            //        break;
            //    case (int)ReconstructionFilterType.LanczosSincFilter:
            //        propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.IntegratorLanczosSincFilter;
            //        break;
            //    case (int)ReconstructionFilterType.MitchellNetravaliFilter:
            //        propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.IntegratorMitchellNetravaliFilter;
            //        break;
            //    case (int)ReconstructionFilterType.TentFilter:
            //        //propertyGridReconstruction.SelectedObject = ReconstructionFilterObjectInstances.;
            //        break;
            //}

        }


        private void tabControlProperties_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
