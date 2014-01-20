namespace MitsubaRender.Settings
{
    partial class IntegratorDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IntegratorDialog));
            this.labelIntegrator = new System.Windows.Forms.Label();
            this.comboBoxIntegrator = new System.Windows.Forms.ComboBox();
            this.labelSampler = new System.Windows.Forms.Label();
            this.comboBoxSampler = new System.Windows.Forms.ComboBox();
            this.comboBoxReconstruction = new System.Windows.Forms.ComboBox();
            this.labelReconstruction = new System.Windows.Forms.Label();
            this.labelOtherFeatures = new System.Windows.Forms.Label();
            this.checkBoxIrradianceCache = new System.Windows.Forms.CheckBox();
            this.checkBoxAdaptiveIntegration = new System.Windows.Forms.CheckBox();
            this.propertyGridIntegrator = new System.Windows.Forms.PropertyGrid();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageIntegrator = new System.Windows.Forms.TabPage();
            this.buttonResetIntegrator = new System.Windows.Forms.Button();
            this.buttonDeleteIntegrator = new System.Windows.Forms.Button();
            this.buttonDuplicateIntegrator = new System.Windows.Forms.Button();
            this.tabPageSampler = new System.Windows.Forms.TabPage();
            this.buttonResetSampler = new System.Windows.Forms.Button();
            this.buttonDeleteSampler = new System.Windows.Forms.Button();
            this.buttonDuplicateSampler = new System.Windows.Forms.Button();
            this.propertyGridSampler = new System.Windows.Forms.PropertyGrid();
            this.TabPageReconstruction = new System.Windows.Forms.TabPage();
            this.buttonResetFilter = new System.Windows.Forms.Button();
            this.buttonDeleteFilter = new System.Windows.Forms.Button();
            this.buttonDuplicateFilter = new System.Windows.Forms.Button();
            this.propertyGridReconstruction = new System.Windows.Forms.PropertyGrid();
            this.tabControlProperties.SuspendLayout();
            this.tabPageIntegrator.SuspendLayout();
            this.tabPageSampler.SuspendLayout();
            this.TabPageReconstruction.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelIntegrator
            // 
            this.labelIntegrator.AutoSize = true;
            this.labelIntegrator.Location = new System.Drawing.Point(12, 15);
            this.labelIntegrator.Name = "labelIntegrator";
            this.labelIntegrator.Size = new System.Drawing.Size(55, 13);
            this.labelIntegrator.TabIndex = 0;
            this.labelIntegrator.Text = "Integrator:";
            // 
            // comboBoxIntegrator
            // 
            this.comboBoxIntegrator.AutoCompleteCustomSource.AddRange(new string[] {
            "Ambient occlusion",
            "Direct illumination",
            "Path tracer",
            "Volumetric path tracer (Simple)",
            "Volumetric path tracer  (Extender)",
            "Adjoint particle tracer",
            "Virtual point light renderer",
            "Photon mapper",
            "Progressive photon mapper",
            "Stochastic progressive photon mapper"});
            this.comboBoxIntegrator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIntegrator.FormattingEnabled = true;
            this.comboBoxIntegrator.Location = new System.Drawing.Point(108, 12);
            this.comboBoxIntegrator.Name = "comboBoxIntegrator";
            this.comboBoxIntegrator.Size = new System.Drawing.Size(291, 21);
            this.comboBoxIntegrator.TabIndex = 1;
            this.comboBoxIntegrator.SelectedIndexChanged += new System.EventHandler(this.ComboBoxIntegratorSelectedIndexChanged);
            // 
            // labelSampler
            // 
            this.labelSampler.AutoSize = true;
            this.labelSampler.Location = new System.Drawing.Point(12, 42);
            this.labelSampler.Name = "labelSampler";
            this.labelSampler.Size = new System.Drawing.Size(48, 13);
            this.labelSampler.TabIndex = 2;
            this.labelSampler.Text = "Sampler:";
            // 
            // comboBoxSampler
            // 
            this.comboBoxSampler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSampler.FormattingEnabled = true;
            this.comboBoxSampler.Items.AddRange(new object[] {
            "Independent sampler",
            "Stratied sampler",
            "Low discrepancy sampler",
            "Hammersley QMC sampler",
            "Halton QMC sampler",
            "Sobol QMC sampler"});
            this.comboBoxSampler.Location = new System.Drawing.Point(108, 39);
            this.comboBoxSampler.Name = "comboBoxSampler";
            this.comboBoxSampler.Size = new System.Drawing.Size(291, 21);
            this.comboBoxSampler.TabIndex = 3;
            this.comboBoxSampler.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSamplerSelectedIndexChanged);
            // 
            // comboBoxReconstruction
            // 
            this.comboBoxReconstruction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReconstruction.FormattingEnabled = true;
            this.comboBoxReconstruction.Items.AddRange(new object[] {
            "Box filter",
            "Tent filter",
            "Gaussian filter",
            "Mitchen-Netravali filter",
            "Catmull-Rom filter",
            "Lanczos Sinc filter"});
            this.comboBoxReconstruction.Location = new System.Drawing.Point(108, 67);
            this.comboBoxReconstruction.Name = "comboBoxReconstruction";
            this.comboBoxReconstruction.Size = new System.Drawing.Size(291, 21);
            this.comboBoxReconstruction.TabIndex = 6;
            this.comboBoxReconstruction.SelectedIndexChanged += new System.EventHandler(this.ComboBoxReconstructionSelectedIndexChanged);
            // 
            // labelReconstruction
            // 
            this.labelReconstruction.AutoSize = true;
            this.labelReconstruction.Location = new System.Drawing.Point(12, 70);
            this.labelReconstruction.Name = "labelReconstruction";
            this.labelReconstruction.Size = new System.Drawing.Size(82, 13);
            this.labelReconstruction.TabIndex = 7;
            this.labelReconstruction.Text = "Reconstruction:";
            // 
            // labelOtherFeatures
            // 
            this.labelOtherFeatures.AutoSize = true;
            this.labelOtherFeatures.Location = new System.Drawing.Point(12, 101);
            this.labelOtherFeatures.Name = "labelOtherFeatures";
            this.labelOtherFeatures.Size = new System.Drawing.Size(74, 13);
            this.labelOtherFeatures.TabIndex = 8;
            this.labelOtherFeatures.Text = "Other features";
            // 
            // checkBoxIrradianceCache
            // 
            this.checkBoxIrradianceCache.AutoSize = true;
            this.checkBoxIrradianceCache.Location = new System.Drawing.Point(108, 100);
            this.checkBoxIrradianceCache.Name = "checkBoxIrradianceCache";
            this.checkBoxIrradianceCache.Size = new System.Drawing.Size(107, 17);
            this.checkBoxIrradianceCache.TabIndex = 9;
            this.checkBoxIrradianceCache.Text = "Irradiance Cache";
            this.checkBoxIrradianceCache.UseVisualStyleBackColor = true;
            // 
            // checkBoxAdaptiveIntegration
            // 
            this.checkBoxAdaptiveIntegration.AutoSize = true;
            this.checkBoxAdaptiveIntegration.Location = new System.Drawing.Point(221, 100);
            this.checkBoxAdaptiveIntegration.Name = "checkBoxAdaptiveIntegration";
            this.checkBoxAdaptiveIntegration.Size = new System.Drawing.Size(121, 17);
            this.checkBoxAdaptiveIntegration.TabIndex = 10;
            this.checkBoxAdaptiveIntegration.Text = "Adaptive Integration";
            this.checkBoxAdaptiveIntegration.UseVisualStyleBackColor = true;
            // 
            // propertyGridIntegrator
            // 
            this.propertyGridIntegrator.Location = new System.Drawing.Point(3, 2);
            this.propertyGridIntegrator.Name = "propertyGridIntegrator";
            this.propertyGridIntegrator.Size = new System.Drawing.Size(373, 191);
            this.propertyGridIntegrator.TabIndex = 11;
            this.propertyGridIntegrator.ToolbarVisible = false;
            this.propertyGridIntegrator.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyGridIntegratorPropertyValueChanged);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(251, 491);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 26);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.ButtonSaveClick);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(332, 491);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 26);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.ButtonCancelClick);
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabPageIntegrator);
            this.tabControlProperties.Controls.Add(this.tabPageSampler);
            this.tabControlProperties.Controls.Add(this.TabPageReconstruction);
            this.tabControlProperties.Location = new System.Drawing.Point(11, 123);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(387, 242);
            this.tabControlProperties.TabIndex = 15;
            // 
            // tabPageIntegrator
            // 
            this.tabPageIntegrator.Controls.Add(this.buttonResetIntegrator);
            this.tabPageIntegrator.Controls.Add(this.buttonDeleteIntegrator);
            this.tabPageIntegrator.Controls.Add(this.buttonDuplicateIntegrator);
            this.tabPageIntegrator.Controls.Add(this.propertyGridIntegrator);
            this.tabPageIntegrator.Location = new System.Drawing.Point(4, 22);
            this.tabPageIntegrator.Name = "tabPageIntegrator";
            this.tabPageIntegrator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIntegrator.Size = new System.Drawing.Size(379, 216);
            this.tabPageIntegrator.TabIndex = 0;
            this.tabPageIntegrator.Text = "Integrator";
            this.tabPageIntegrator.UseVisualStyleBackColor = true;
            // 
            // buttonResetIntegrator
            // 
            this.buttonResetIntegrator.Location = new System.Drawing.Point(57, 193);
            this.buttonResetIntegrator.Name = "buttonResetIntegrator";
            this.buttonResetIntegrator.Size = new System.Drawing.Size(48, 23);
            this.buttonResetIntegrator.TabIndex = 19;
            this.buttonResetIntegrator.Text = "Reset";
            this.buttonResetIntegrator.UseVisualStyleBackColor = true;
            this.buttonResetIntegrator.Click += new System.EventHandler(this.ButtonResetIntegratorClick);
            // 
            // buttonDeleteIntegrator
            // 
            this.buttonDeleteIntegrator.Location = new System.Drawing.Point(29, 193);
            this.buttonDeleteIntegrator.Name = "buttonDeleteIntegrator";
            this.buttonDeleteIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteIntegrator.TabIndex = 20;
            this.buttonDeleteIntegrator.Text = "-";
            this.buttonDeleteIntegrator.UseVisualStyleBackColor = true;
            this.buttonDeleteIntegrator.Click += new System.EventHandler(this.ButtonDeleteIntegratorClick);
            // 
            // buttonDuplicateIntegrator
            // 
            this.buttonDuplicateIntegrator.Location = new System.Drawing.Point(2, 193);
            this.buttonDuplicateIntegrator.Name = "buttonDuplicateIntegrator";
            this.buttonDuplicateIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonDuplicateIntegrator.TabIndex = 18;
            this.buttonDuplicateIntegrator.Text = "+";
            this.buttonDuplicateIntegrator.UseVisualStyleBackColor = true;
            this.buttonDuplicateIntegrator.Click += new System.EventHandler(this.ButtonDuplicateIntegratorClick);
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.buttonResetSampler);
            this.tabPageSampler.Controls.Add(this.buttonDeleteSampler);
            this.tabPageSampler.Controls.Add(this.buttonDuplicateSampler);
            this.tabPageSampler.Controls.Add(this.propertyGridSampler);
            this.tabPageSampler.Location = new System.Drawing.Point(4, 22);
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSampler.Size = new System.Drawing.Size(379, 216);
            this.tabPageSampler.TabIndex = 1;
            this.tabPageSampler.Text = "Sampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
            // 
            // buttonResetSampler
            // 
            this.buttonResetSampler.Location = new System.Drawing.Point(57, 193);
            this.buttonResetSampler.Name = "buttonResetSampler";
            this.buttonResetSampler.Size = new System.Drawing.Size(48, 23);
            this.buttonResetSampler.TabIndex = 17;
            this.buttonResetSampler.Text = "Reset";
            this.buttonResetSampler.UseVisualStyleBackColor = true;
            this.buttonResetSampler.Click += new System.EventHandler(this.ButtonResetSamplerClick);
            // 
            // buttonDeleteSampler
            // 
            this.buttonDeleteSampler.Location = new System.Drawing.Point(29, 193);
            this.buttonDeleteSampler.Name = "buttonDeleteSampler";
            this.buttonDeleteSampler.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteSampler.TabIndex = 17;
            this.buttonDeleteSampler.Text = "-";
            this.buttonDeleteSampler.UseVisualStyleBackColor = true;
            this.buttonDeleteSampler.Click += new System.EventHandler(this.ButtonDeleteSamplerClick);
            // 
            // buttonDuplicateSampler
            // 
            this.buttonDuplicateSampler.Location = new System.Drawing.Point(2, 193);
            this.buttonDuplicateSampler.Name = "buttonDuplicateSampler";
            this.buttonDuplicateSampler.Size = new System.Drawing.Size(23, 23);
            this.buttonDuplicateSampler.TabIndex = 16;
            this.buttonDuplicateSampler.Text = "+";
            this.buttonDuplicateSampler.UseVisualStyleBackColor = true;
            this.buttonDuplicateSampler.Click += new System.EventHandler(this.ButtonDuplicateSamplerClick);
            // 
            // propertyGridSampler
            // 
            this.propertyGridSampler.Location = new System.Drawing.Point(3, 2);
            this.propertyGridSampler.Name = "propertyGridSampler";
            this.propertyGridSampler.Size = new System.Drawing.Size(373, 191);
            this.propertyGridSampler.TabIndex = 12;
            this.propertyGridSampler.ToolbarVisible = false;
            // 
            // TabPageReconstruction
            // 
            this.TabPageReconstruction.Controls.Add(this.buttonResetFilter);
            this.TabPageReconstruction.Controls.Add(this.buttonDeleteFilter);
            this.TabPageReconstruction.Controls.Add(this.buttonDuplicateFilter);
            this.TabPageReconstruction.Controls.Add(this.propertyGridReconstruction);
            this.TabPageReconstruction.Location = new System.Drawing.Point(4, 22);
            this.TabPageReconstruction.Name = "TabPageReconstruction";
            this.TabPageReconstruction.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageReconstruction.Size = new System.Drawing.Size(379, 216);
            this.TabPageReconstruction.TabIndex = 2;
            this.TabPageReconstruction.Text = "Reconstruction";
            this.TabPageReconstruction.UseVisualStyleBackColor = true;
            // 
            // buttonResetFilter
            // 
            this.buttonResetFilter.Location = new System.Drawing.Point(57, 193);
            this.buttonResetFilter.Name = "buttonResetFilter";
            this.buttonResetFilter.Size = new System.Drawing.Size(48, 23);
            this.buttonResetFilter.TabIndex = 19;
            this.buttonResetFilter.Text = "Reset";
            this.buttonResetFilter.UseVisualStyleBackColor = true;
            this.buttonResetFilter.Click += new System.EventHandler(this.ButtonResetFilterClick);
            // 
            // buttonDeleteFilter
            // 
            this.buttonDeleteFilter.Location = new System.Drawing.Point(29, 193);
            this.buttonDeleteFilter.Name = "buttonDeleteFilter";
            this.buttonDeleteFilter.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteFilter.TabIndex = 20;
            this.buttonDeleteFilter.Text = "-";
            this.buttonDeleteFilter.UseVisualStyleBackColor = true;
            this.buttonDeleteFilter.Click += new System.EventHandler(this.ButtonDeleteFilterClick);
            // 
            // buttonDuplicateFilter
            // 
            this.buttonDuplicateFilter.Location = new System.Drawing.Point(2, 193);
            this.buttonDuplicateFilter.Name = "buttonDuplicateFilter";
            this.buttonDuplicateFilter.Size = new System.Drawing.Size(23, 23);
            this.buttonDuplicateFilter.TabIndex = 18;
            this.buttonDuplicateFilter.Text = "+";
            this.buttonDuplicateFilter.UseVisualStyleBackColor = true;
            this.buttonDuplicateFilter.Click += new System.EventHandler(this.ButtonDuplicateFilterClick);
            // 
            // propertyGridReconstruction
            // 
            this.propertyGridReconstruction.Location = new System.Drawing.Point(3, 2);
            this.propertyGridReconstruction.Name = "propertyGridReconstruction";
            this.propertyGridReconstruction.Size = new System.Drawing.Size(373, 191);
            this.propertyGridReconstruction.TabIndex = 12;
            this.propertyGridReconstruction.ToolbarVisible = false;
            // 
            // IntegratorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 529);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.checkBoxAdaptiveIntegration);
            this.Controls.Add(this.checkBoxIrradianceCache);
            this.Controls.Add(this.labelOtherFeatures);
            this.Controls.Add(this.labelReconstruction);
            this.Controls.Add(this.comboBoxReconstruction);
            this.Controls.Add(this.comboBoxSampler);
            this.Controls.Add(this.labelSampler);
            this.Controls.Add(this.comboBoxIntegrator);
            this.Controls.Add(this.labelIntegrator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "IntegratorDialog";
            this.Text = "Render Settings";
            this.Load += new System.EventHandler(this.IntegratorDialogLoad);
            this.tabControlProperties.ResumeLayout(false);
            this.tabPageIntegrator.ResumeLayout(false);
            this.tabPageSampler.ResumeLayout(false);
            this.TabPageReconstruction.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelIntegrator;
        private System.Windows.Forms.ComboBox comboBoxIntegrator;
        private System.Windows.Forms.Label labelSampler;
        private System.Windows.Forms.ComboBox comboBoxSampler;
        private System.Windows.Forms.ComboBox comboBoxReconstruction;
        private System.Windows.Forms.Label labelReconstruction;
        private System.Windows.Forms.Label labelOtherFeatures;
        private System.Windows.Forms.CheckBox checkBoxIrradianceCache;
        private System.Windows.Forms.CheckBox checkBoxAdaptiveIntegration;
        private System.Windows.Forms.PropertyGrid propertyGridIntegrator;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageIntegrator;
        private System.Windows.Forms.TabPage tabPageSampler;
        private System.Windows.Forms.TabPage TabPageReconstruction;
        private System.Windows.Forms.PropertyGrid propertyGridSampler;
        private System.Windows.Forms.PropertyGrid propertyGridReconstruction;
        private System.Windows.Forms.Button buttonResetIntegrator;
        private System.Windows.Forms.Button buttonDeleteIntegrator;
        private System.Windows.Forms.Button buttonDuplicateIntegrator;
        private System.Windows.Forms.Button buttonResetSampler;
        private System.Windows.Forms.Button buttonDeleteSampler;
        private System.Windows.Forms.Button buttonDuplicateSampler;
        private System.Windows.Forms.Button buttonResetFilter;
        private System.Windows.Forms.Button buttonDeleteFilter;
        private System.Windows.Forms.Button buttonDuplicateFilter;
    }
}