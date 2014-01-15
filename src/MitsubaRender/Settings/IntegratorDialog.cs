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
            comboBoxIntegrator.DataSource = IntegratorsDataSource.IntegratorData;
            comboBoxSampler.DataSource = IntegratorsDataSource.SamplerData;
            comboBoxReconstruction.DataSource = IntegratorsDataSource.ReconstructionData;
        }

        private void ButtonOkClick(object sender, EventArgs e)
        {

        }

        private void ComboBoxIntegratorSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 0;
            switch (comboBoxIntegrator.SelectedIndex)
            {

                case (int)IntegratorType.AdjointParticleTracer:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.AdjointParticleTracer;
                    break;
                case (int)IntegratorType.Ambientoclusion:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.AmbientOclusion;
                    break;
                case (int)IntegratorType.BidirectionalPathTracer:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.BidirectionalPathTracer;
                    break;
                case (int)IntegratorType.DirectIlumination:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.DirectIlumination;
                    break;
                case (int)IntegratorType.EnergyRedisributionPathTracing:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.EnergyRedisributionPathTracing;
                    break;
                case (int)IntegratorType.PathTracer:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PathTracer;
                    break;
                case (int)IntegratorType.PhotonMapper:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PhotonMapper;
                    break;
                case (int)IntegratorType.PrimarySampleSpaceMLT:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.PrimarySampleSpaceMLT;
                    break;
                case (int)IntegratorType.ProgressivePhotonMapper:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.ProgressivePhotonMapper;
                    break;
                case (int)IntegratorType.SampleSpaceMLT:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.SampleSpaceMLT;
                    break;
                case (int)IntegratorType.StochasticProgressivePhotonMapper:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.StochasticProgressivePhotonMapper;
                    break;
                case (int)IntegratorType.VirtualPointLightRenderer:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VirtualPointLightRenderer;
                    break;
                case (int)IntegratorType.VolumetricPathTracerExtended:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VolumetricPathTracerExtended;
                    break;
                case (int)IntegratorType.VolumetricPathTracerSimple:
                    propertyGridIntegrator.SelectedObject = IntegratorObjectInstances.VolumetricPathTracerSimple;
                    break;
            }
        }

        private void ComboBoxSamplerSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 1;
            switch (comboBoxSampler.SelectedIndex)
            {
                case (int)SamplerType.HaltonQMCSampler:
                    propertyGridSampler.SelectedObject = SamplerObjectInstances.SamplerHaltonQMC;
                    break;
                case (int)SamplerType.HammersleyQMCSampler:
                    break;
                case (int)SamplerType.IndependentSampler:
                    break;
                case (int)SamplerType.LowDiscrepancySampler:
                    break;
                case (int)SamplerType.SobolQMCSampler:
                    break;
                case (int)SamplerType.StraitfieldSampler:
                    break;


            }


        }

        private void ComboBoxReconstructionSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 2;
        }


    }
}
