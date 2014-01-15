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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabControlProperties = new System.Windows.Forms.TabControl();
            this.tabPageIntegrator = new System.Windows.Forms.TabPage();
            this.tabPageSampler = new System.Windows.Forms.TabPage();
            this.TabPageReconstruction = new System.Windows.Forms.TabPage();
            this.propertyGridSampler = new System.Windows.Forms.PropertyGrid();
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
            this.propertyGridIntegrator.Location = new System.Drawing.Point(0, 3);
            this.propertyGridIntegrator.Name = "propertyGridIntegrator";
            this.propertyGridIntegrator.Size = new System.Drawing.Size(373, 191);
            this.propertyGridIntegrator.TabIndex = 11;
            this.propertyGridIntegrator.ToolbarVisible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 482);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(324, 482);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 26);
            this.button2.TabIndex = 14;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // tabControlProperties
            // 
            this.tabControlProperties.Controls.Add(this.tabPageIntegrator);
            this.tabControlProperties.Controls.Add(this.tabPageSampler);
            this.tabControlProperties.Controls.Add(this.TabPageReconstruction);
            this.tabControlProperties.Location = new System.Drawing.Point(12, 123);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(387, 220);
            this.tabControlProperties.TabIndex = 15;
            // 
            // tabPageIntegrator
            // 
            this.tabPageIntegrator.Controls.Add(this.propertyGridIntegrator);
            this.tabPageIntegrator.Location = new System.Drawing.Point(4, 22);
            this.tabPageIntegrator.Name = "tabPageIntegrator";
            this.tabPageIntegrator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIntegrator.Size = new System.Drawing.Size(379, 194);
            this.tabPageIntegrator.TabIndex = 0;
            this.tabPageIntegrator.Text = "Integrator";
            this.tabPageIntegrator.UseVisualStyleBackColor = true;
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.propertyGridSampler);
            this.tabPageSampler.Location = new System.Drawing.Point(4, 22);
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSampler.Size = new System.Drawing.Size(379, 194);
            this.tabPageSampler.TabIndex = 1;
            this.tabPageSampler.Text = "Sampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
            // 
            // TabPageReconstruction
            // 
            this.TabPageReconstruction.Controls.Add(this.propertyGridReconstruction);
            this.TabPageReconstruction.Location = new System.Drawing.Point(4, 22);
            this.TabPageReconstruction.Name = "TabPageReconstruction";
            this.TabPageReconstruction.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageReconstruction.Size = new System.Drawing.Size(379, 194);
            this.TabPageReconstruction.TabIndex = 2;
            this.TabPageReconstruction.Text = "Reconstruction";
            this.TabPageReconstruction.UseVisualStyleBackColor = true;
            // 
            // propertyGridSampler
            // 
            this.propertyGridSampler.Location = new System.Drawing.Point(3, 2);
            this.propertyGridSampler.Name = "propertyGridSampler";
            this.propertyGridSampler.Size = new System.Drawing.Size(373, 191);
            this.propertyGridSampler.TabIndex = 12;
            this.propertyGridSampler.ToolbarVisible = false;
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
            this.ClientSize = new System.Drawing.Size(411, 514);
            this.Controls.Add(this.tabControlProperties);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TabControl tabControlProperties;
        private System.Windows.Forms.TabPage tabPageIntegrator;
        private System.Windows.Forms.TabPage tabPageSampler;
        private System.Windows.Forms.TabPage TabPageReconstruction;
        private System.Windows.Forms.PropertyGrid propertyGridSampler;
        private System.Windows.Forms.PropertyGrid propertyGridReconstruction;
    }
}