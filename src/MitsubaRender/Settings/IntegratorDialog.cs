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
using MitsubaRender.RenderSettings;
using MitsubaRender.UI;

namespace MitsubaRender.Settings
{
    public partial class IntegratorDialog : Form
    {
        private readonly String _editingPreset;

        //Constructor
        public IntegratorDialog(string presetName)
        {
            InitializeComponent();
            _editingPreset = presetName;
        }

        private void IntegratorDialogLoad(object sender, EventArgs e)
        {
            Text = _editingPreset;
            LibraryIntegrators.Init();
            LibrarySamplers.Init();
            LibraryReconstructionFilters.Init();

            if (LibraryIntegrators.Integrators == null || !LibraryIntegrators.Integrators.Any())
                MitsubaSettings.GenerateDefaultIntegrators();

            if (LibrarySamplers.Samplers == null || !LibrarySamplers.Samplers.Any())
                MitsubaSettings.GenerateDefaultSamplers();

            if (LibraryReconstructionFilters.ReconstructionFilters == null || !LibraryReconstructionFilters.ReconstructionFilters.Any())
                MitsubaSettings.GenerateDefaultReconstructionFilters();


            if (LibraryIntegrators.Integrators != null) comboBoxIntegrator.DataSource = LibraryIntegrators.Integrators.ToArray();
            if (LibrarySamplers.Samplers != null) comboBoxSampler.DataSource = LibrarySamplers.Samplers.ToArray();
            if (LibraryReconstructionFilters.ReconstructionFilters != null) comboBoxReconstruction.DataSource = LibraryReconstructionFilters.ReconstructionFilters.ToArray();
        }

        private void ComboBoxIntegratorSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 0;

            var integratorName = comboBoxIntegrator.SelectedItem.ToString();
            propertyGridIntegrator.SelectedObject = LibraryIntegrators.GetIntegrator(integratorName);

        }
        private void ComboBoxSamplerSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 1;
            var samplerName = comboBoxSampler.SelectedItem.ToString();
            propertyGridSampler.SelectedObject = LibrarySamplers.GetSampler(samplerName);
        }
        private void ComboBoxReconstructionSelectedIndexChanged(object sender, EventArgs e)
        {
            tabControlProperties.SelectedIndex = 2;

            var filterName = comboBoxReconstruction.SelectedItem.ToString();
            propertyGridReconstruction.SelectedObject = LibraryReconstructionFilters.GetReconstructionFilter(filterName);
        }


        private void ButtonDuplicateIntegratorClick(object sender, EventArgs e)
        {
            var name = comboBoxIntegrator.SelectedItem.ToString();
            var finalName = name;
            var integrator = LibraryIntegrators.GetIntegrator(name);
            var isaveObj = integrator as ISave;

            int num = 1;
            var path = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, name) + LibraryIntegrators.Extension; ;

            while (File.Exists(path))
            {
                num++;
                finalName = name + " (" + num + ")";
                path = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, finalName);
                path += LibraryIntegrators.Extension;

            }

            if (isaveObj != null)
            {

                var input = new InputBoxDlg
                    {
                        Titul = "Mistuba Render Integrator",
                        TopicText = "Please Type a Name",
                        InputText = finalName
                    };

                input.ShowDialog();
                if (input.DialogResult == DialogResult.OK)
                {
                    finalName = input.InputText;
                    path = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, finalName);
                    if (File.Exists(path))
                    {
                        //TODO Localize me
                        if (MessageBox.Show("This file already exist, do you want to overwrite it?",
                                            "Mistuba Render Integrator", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    isaveObj.Save(finalName);
                    LibraryIntegrators.Init();
                    comboBoxIntegrator.DataSource = LibraryIntegrators.Integrators.ToArray();
                }


            }
        }
        private void ButtonDeleteIntegratorClick(object sender, EventArgs e)
        {
            var name = comboBoxIntegrator.SelectedItem.ToString();
            //TODO Localize me 
            {
                if (MessageBox.Show(String.Format("Are you sure to delete {0}?", name), "Mistuba Render Integrator", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var path = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, name) + LibraryIntegrators.Extension;
                    try
                    {
                        File.Delete(path);
                        LibraryIntegrators.Init();
                        comboBoxIntegrator.DataSource = LibraryIntegrators.Integrators.ToArray();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("A problem ocurred deleting the file");
                    }

                }
            }
        }
        private void ButtonResetIntegratorClick(object sender, EventArgs e)
        {
            var name = comboBoxIntegrator.SelectedItem.ToString();
            var path = Path.Combine(MitsubaSettings.FolderIntegratorsFolder, name) + LibraryIntegrators.Extension;

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                //TODO Localize me 
                MessageBox.Show("A problem ocurred reseting the Integrator");
                return;

            }

            var obj = propertyGridIntegrator.SelectedObject;

            if (obj is IntegratorAdaptativeIntegrator)
            {
                var integrator = new IntegratorAdaptativeIntegrator();
                integrator.Save(name);
            }

            else if (obj is IntegratorAdjointParticleTracer)
            {
                var integrator = new IntegratorAdjointParticleTracer();
                integrator.Save(name);
            }
            else if (obj is IntegratorAmbientOclusion)
            {
                var integrator = new IntegratorAmbientOclusion();
                integrator.Save(name);
            }
            else if (obj is IntegratorBidirectionalPathTracer)
            {
                var integrator = new IntegratorBidirectionalPathTracer();
                integrator.Save(name);
            }
            else if (obj is IntegratorDirectIlumination)
            {
                var integrator = new IntegratorDirectIlumination();
                integrator.Save(name);
            }
            else if (obj is IntegratorEnergyRedisributionPathTracing)
            {
                var integrator = new IntegratorEnergyRedisributionPathTracing();
                integrator.Save(name);
            }
            else if (obj is IntegratorIrradianceCaching)
            {
                var integrator = new IntegratorIrradianceCaching();
                integrator.Save(name);
            }
            else if (obj is IntegratorPathTracer)
            {
                var integrator = new IntegratorPathTracer();
                integrator.Save(name);
            }
            else if (obj is IntegratorPhotonMapper)
            {
                var integrator = new IntegratorPhotonMapper();
                integrator.Save(name);
            }
            else if (obj is IntegratorPrimarySampleSpaceMLT)
            {
                var integrator = new IntegratorPrimarySampleSpaceMLT();
                integrator.Save(name);
            }
            else if (obj is IntegratorProgressivePhotonMapper)
            {
                var integrator = new IntegratorProgressivePhotonMapper();
                integrator.Save(name);
            }
            else if (obj is IntegratorSampleSpaceMLT)
            {
                var integrator = new IntegratorSampleSpaceMLT();
                integrator.Save(name);
            }
            else if (obj is IntegratorStochasticProgressivePhotonMapper)
            {
                var integrator = new IntegratorStochasticProgressivePhotonMapper();
                integrator.Save(name);
            }
            else if (obj is IntegratorVirtualPointLightRenderer)
            {
                var integrator = new IntegratorVirtualPointLightRenderer();
                integrator.Save(name);
            }
            else if (obj is IntegratorVolumetricPathTracerExtended)
            {
                var integrator = new IntegratorVolumetricPathTracerExtended();
                integrator.Save(name);
            }
            else if (obj is IntegratorVolumetricPathTracerSimple)
            {
                var integrator = new IntegratorVolumetricPathTracerSimple();
                integrator.Save(name);
            }

            var integratorName = comboBoxIntegrator.SelectedItem.ToString();
            propertyGridIntegrator.SelectedObject = LibraryIntegrators.GetIntegrator(integratorName);
        }

        private void ButtonDuplicateSamplerClick(object sender, EventArgs e)
        {
            var name = comboBoxSampler.SelectedItem.ToString();
            var finalName = name;
            var sampler = LibraryIntegrators.GetIntegrator(name);
            var isaveObj = sampler as ISave;

            int num = 1;
            var path = Path.Combine(MitsubaSettings.FolderSamplersFolder, name) + LibrarySamplers.Extension; ;

            while (File.Exists(path))
            {
                num++;
                finalName = name + " (" + num + ")";
                path = Path.Combine(MitsubaSettings.FolderSamplersFolder, finalName);
                path += LibraryIntegrators.Extension;

            }

            if (isaveObj != null)
            {

                var input = new InputBoxDlg
                {
                    Titul = "Mistuba Render Sampler",
                    TopicText = "Please Type a Name",
                    InputText = finalName
                };

                input.ShowDialog();
                if (input.DialogResult == DialogResult.OK)
                {
                    finalName = input.InputText;
                    path = Path.Combine(MitsubaSettings.FolderSamplersFolder, finalName);
                    if (File.Exists(path))
                    {
                        //TODO Localize me
                        if (MessageBox.Show("This file already exist, do you want to overwrite it?",
                                            "Mistuba Render Sampler", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    isaveObj.Save(finalName);
                    LibrarySamplers.Init();
                    comboBoxSampler.DataSource = LibrarySamplers.Samplers.ToArray();
                }
            }
        }
        private void ButtonDeleteSamplerClick(object sender, EventArgs e)
        {
            var name = comboBoxSampler.SelectedItem.ToString();
            //TODO Localize me 
            {
                if (MessageBox.Show(String.Format("Are you sure to delete {0}?", name), "Mistuba Render Sampler", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var path = Path.Combine(MitsubaSettings.FolderSamplersFolder, name) + LibrarySamplers.Extension;
                    try
                    {
                        File.Delete(path);
                        LibrarySamplers.Init();
                        comboBoxSampler.DataSource = LibrarySamplers.Samplers.ToArray();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("A problem ocurred deleting the file");
                    }

                }
            }
        }
        private void ButtonResetSamplerClick(object sender, EventArgs e)
        {
            var name = comboBoxSampler.SelectedItem.ToString();
            var path = Path.Combine(MitsubaSettings.FolderSamplersFolder, name) + LibrarySamplers.Extension;

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                //TODO Localize me 
                MessageBox.Show("A problem ocurred reseting the Sampler");
                return;

            }

            var obj = propertyGridSampler.SelectedObject;

            if (obj is SamplerHaltonQMC)
            {
                var sampler = new SamplerHaltonQMC();
                sampler.Save(name);
            }

            else if (obj is SamplerHammersleyQMC)
            {
                var sampler = new SamplerHammersleyQMC();
                sampler.Save(name);
            }
            else if (obj is SamplerIndependent)
            {
                var sampler = new SamplerIndependent();
                sampler.Save(name);
            }
            else if (obj is SamplerLowDiscrepancy)
            {
                var sampler = new SamplerLowDiscrepancy();
                sampler.Save(name);
            }

            else if (obj is SamplerSobolQMC)
            {
                var sampler = new SamplerSobolQMC();
                sampler.Save(name);
            }
            else if (obj is SamplerStraitfield)
            {
                var sampler = new SamplerStraitfield();
                sampler.Save(name);
            }

            var samplerName = comboBoxSampler.SelectedItem.ToString();
            propertyGridSampler.SelectedObject = LibrarySamplers.GetSampler(samplerName);
        }

        private void ButtonDuplicateFilterClick(object sender, EventArgs e)
        {
            var name = comboBoxReconstruction.SelectedItem.ToString();
            var finalName = name;
            var sampler = LibraryReconstructionFilters.GetReconstructionFilter(name);
            var isaveObj = sampler as ISave;

            int num = 1;
            var path = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension; ;

            while (File.Exists(path))
            {
                num++;
                finalName = name + " (" + num + ")";
                path = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, finalName);
                path += LibraryReconstructionFilters.Extension;

            }

            if (isaveObj != null)
            {

                var input = new InputBoxDlg
                {
                    Titul = "Mistuba Render Reconstruction Filter",
                    TopicText = "Please Type a Name",
                    InputText = finalName
                };

                input.ShowDialog();
                if (input.DialogResult == DialogResult.OK)
                {
                    finalName = input.InputText;
                    path = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, finalName);
                    if (File.Exists(path))
                    {
                        //TODO Localize me
                        if (MessageBox.Show("This file already exist, do you want to overwrite it?",
                                            "Mistuba Render Reconstruction Filter", MessageBoxButtons.YesNo) == DialogResult.No)
                            return;
                    }
                    isaveObj.Save(finalName);
                    LibraryReconstructionFilters.Init();
                    comboBoxReconstruction.DataSource = LibraryReconstructionFilters.ReconstructionFilters.ToArray();
                }
            }
        }
        private void ButtonDeleteFilterClick(object sender, EventArgs e)
        {
            var name = comboBoxReconstruction.SelectedItem.ToString();
            //TODO Localize me 
            {
                if (MessageBox.Show(String.Format("Are you sure to delete {0}?", name), "Mistuba Render Sampler", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var path = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension;
                    try
                    {
                        File.Delete(path);
                        LibraryReconstructionFilters.Init();
                        comboBoxReconstruction.DataSource = LibraryReconstructionFilters.ReconstructionFilters.ToArray();
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("A problem ocurred deleting the file");
                    }

                }
            }
        }
        private void ButtonResetFilterClick(object sender, EventArgs e)
        {
            var name = comboBoxReconstruction.SelectedItem.ToString();
            var path = Path.Combine(MitsubaSettings.FolderReconstructionFiltersFolder, name) + LibraryReconstructionFilters.Extension;

            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                //TODO Localize me 
                MessageBox.Show("A problem ocurred reseting the Sampler");
                return;

            }

            var obj = propertyGridReconstruction.SelectedObject;

            if (obj is ReconstructionFilterGaussianFilter)
            {
                var sampler = new ReconstructionFilterGaussianFilter();
                sampler.Save(name);
            }

            else if (obj is ReconstructionFilterLanczosSincFilter)
            {
                var sampler = new ReconstructionFilterLanczosSincFilter();
                sampler.Save(name);
            }
            else if (obj is ReconstructionFilterMitchellNetravaliFilter)
            {
                var sampler = new ReconstructionFilterMitchellNetravaliFilter();
                sampler.Save(name);
            }
           

            var filterName = comboBoxReconstruction.SelectedItem.ToString();
            propertyGridReconstruction.SelectedObject = LibraryReconstructionFilters.GetReconstructionFilter(filterName);
        }

        private void PropertyGridIntegratorPropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            var obj = propertyGridIntegrator.SelectedObject as ISave;
            if (obj != null)
                obj.Save(comboBoxIntegrator.SelectedItem.ToString());
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_editingPreset))
            {

                var input = new InputBoxDlg
                    {
                        Titul = "Mistuba Render Settings Preset",
                        TopicText = "Please Type a Name",
                        InputText = "New Render Preset"
                    };

                input.ShowDialog();
                if (input.DialogResult == DialogResult.OK)
                {
                    var finalName = input.InputText;
                    //var path = Path.Combine(MitsubaSettings.FolderRenderSettingsPresetsFolder, finalName);

                    var presetObj = new RenderSettingsPreset()
                        {
                            IntegratorName = comboBoxIntegrator.SelectedItem.ToString(),
                            SamplerName = comboBoxSampler.SelectedItem.ToString(),
                            ReconstructionFilterName = comboBoxReconstruction.SelectedItem.ToString(),
                        };


                    presetObj.Save(finalName);
                    LibraryPresets.Init();
                }

                Close();
            }

            else
            {
                //TODO: Localize me
                MessageBox.Show("Please Type a name");
            }



        }
        private void ButtonCancelClick(object sender, EventArgs e)
        {
            Close();
        }


      


    }
}
