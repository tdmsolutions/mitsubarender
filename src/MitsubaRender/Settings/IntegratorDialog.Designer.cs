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
            this.propertyGridSampler = new System.Windows.Forms.PropertyGrid();
            this.TabPageReconstruction = new System.Windows.Forms.TabPage();
            this.propertyGridReconstruction = new System.Windows.Forms.PropertyGrid();
            this.buttonAddIntegrator = new System.Windows.Forms.Button();
            this.buttonDeleteIntegrator = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
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
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(251, 491);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 26);
            this.button1.TabIndex = 13;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(332, 491);
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
            this.tabControlProperties.Location = new System.Drawing.Point(11, 123);
            this.tabControlProperties.Name = "tabControlProperties";
            this.tabControlProperties.SelectedIndex = 0;
            this.tabControlProperties.Size = new System.Drawing.Size(387, 242);
            this.tabControlProperties.TabIndex = 15;
            this.tabControlProperties.SelectedIndexChanged += new System.EventHandler(this.tabControlProperties_SelectedIndexChanged);
            // 
            // tabPageIntegrator
            // 
            this.tabPageIntegrator.Controls.Add(this.button3);
            this.tabPageIntegrator.Controls.Add(this.button4);
            this.tabPageIntegrator.Controls.Add(this.button5);
            this.tabPageIntegrator.Controls.Add(this.propertyGridIntegrator);
            this.tabPageIntegrator.Location = new System.Drawing.Point(4, 22);
            this.tabPageIntegrator.Name = "tabPageIntegrator";
            this.tabPageIntegrator.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIntegrator.Size = new System.Drawing.Size(379, 216);
            this.tabPageIntegrator.TabIndex = 0;
            this.tabPageIntegrator.Text = "Integrator";
            this.tabPageIntegrator.UseVisualStyleBackColor = true;
            // 
            // tabPageSampler
            // 
            this.tabPageSampler.Controls.Add(this.buttonReset);
            this.tabPageSampler.Controls.Add(this.buttonDeleteIntegrator);
            this.tabPageSampler.Controls.Add(this.buttonAddIntegrator);
            this.tabPageSampler.Controls.Add(this.propertyGridSampler);
            this.tabPageSampler.Location = new System.Drawing.Point(4, 22);
            this.tabPageSampler.Name = "tabPageSampler";
            this.tabPageSampler.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSampler.Size = new System.Drawing.Size(379, 336);
            this.tabPageSampler.TabIndex = 1;
            this.tabPageSampler.Text = "Sampler";
            this.tabPageSampler.UseVisualStyleBackColor = true;
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
            this.TabPageReconstruction.Controls.Add(this.button6);
            this.TabPageReconstruction.Controls.Add(this.button7);
            this.TabPageReconstruction.Controls.Add(this.button8);
            this.TabPageReconstruction.Controls.Add(this.propertyGridReconstruction);
            this.TabPageReconstruction.Location = new System.Drawing.Point(4, 22);
            this.TabPageReconstruction.Name = "TabPageReconstruction";
            this.TabPageReconstruction.Padding = new System.Windows.Forms.Padding(3);
            this.TabPageReconstruction.Size = new System.Drawing.Size(379, 336);
            this.TabPageReconstruction.TabIndex = 2;
            this.TabPageReconstruction.Text = "Reconstruction";
            this.TabPageReconstruction.UseVisualStyleBackColor = true;
            // 
            // propertyGridReconstruction
            // 
            this.propertyGridReconstruction.Location = new System.Drawing.Point(3, 2);
            this.propertyGridReconstruction.Name = "propertyGridReconstruction";
            this.propertyGridReconstruction.Size = new System.Drawing.Size(373, 191);
            this.propertyGridReconstruction.TabIndex = 12;
            this.propertyGridReconstruction.ToolbarVisible = false;
            // 
            // buttonAddIntegrator
            // 
            this.buttonAddIntegrator.Location = new System.Drawing.Point(2, 193);
            this.buttonAddIntegrator.Name = "buttonAddIntegrator";
            this.buttonAddIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonAddIntegrator.TabIndex = 16;
            this.buttonAddIntegrator.Text = "+";
            this.buttonAddIntegrator.UseVisualStyleBackColor = true;
            // 
            // buttonDeleteIntegrator
            // 
            this.buttonDeleteIntegrator.Location = new System.Drawing.Point(29, 193);
            this.buttonDeleteIntegrator.Name = "buttonDeleteIntegrator";
            this.buttonDeleteIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteIntegrator.TabIndex = 17;
            this.buttonDeleteIntegrator.Text = "-";
            this.buttonDeleteIntegrator.UseVisualStyleBackColor = true;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(57, 193);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(48, 23);
            this.buttonReset.TabIndex = 17;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(57, 193);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(48, 23);
            this.button3.TabIndex = 19;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(29, 193);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(23, 23);
            this.button4.TabIndex = 20;
            this.button4.Text = "-";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(2, 193);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(23, 23);
            this.button5.TabIndex = 18;
            this.button5.Text = "+";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(57, 193);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(48, 23);
            this.button6.TabIndex = 19;
            this.button6.Text = "Reset";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(29, 193);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(23, 23);
            this.button7.TabIndex = 20;
            this.button7.Text = "-";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(2, 193);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(23, 23);
            this.button8.TabIndex = 18;
            this.button8.Text = "+";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // IntegratorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(411, 529);
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
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonDeleteIntegrator;
        private System.Windows.Forms.Button buttonAddIntegrator;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
    }
}