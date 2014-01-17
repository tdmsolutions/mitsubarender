namespace MitsubaRender.Settings
{
    partial class MitsubaOptionsControl
    {
        /// <summary> 
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.listViewIntegrators = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteIntegrator = new System.Windows.Forms.Button();
            this.buttonAddIntegrator = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mitsuba";
            // 
            // listViewIntegrators
            // 
            this.listViewIntegrators.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewIntegrators.Location = new System.Drawing.Point(6, 16);
            this.listViewIntegrators.Name = "listViewIntegrators";
            this.listViewIntegrators.Size = new System.Drawing.Size(511, 326);
            this.listViewIntegrators.TabIndex = 1;
            this.listViewIntegrators.UseCompatibleStateImageBehavior = false;
            this.listViewIntegrators.View = System.Windows.Forms.View.Details;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonDeleteIntegrator);
            this.groupBox1.Controls.Add(this.buttonAddIntegrator);
            this.groupBox1.Controls.Add(this.listViewIntegrators);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(523, 375);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Integrators";
            // 
            // buttonDeleteIntegrator
            // 
            this.buttonDeleteIntegrator.Location = new System.Drawing.Point(36, 346);
            this.buttonDeleteIntegrator.Name = "buttonDeleteIntegrator";
            this.buttonDeleteIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonDeleteIntegrator.TabIndex = 3;
            this.buttonDeleteIntegrator.Text = "-";
            this.buttonDeleteIntegrator.UseVisualStyleBackColor = true;
            // 
            // buttonAddIntegrator
            // 
            this.buttonAddIntegrator.Location = new System.Drawing.Point(7, 346);
            this.buttonAddIntegrator.Name = "buttonAddIntegrator";
            this.buttonAddIntegrator.Size = new System.Drawing.Size(23, 23);
            this.buttonAddIntegrator.TabIndex = 2;
            this.buttonAddIntegrator.Text = "+";
            this.buttonAddIntegrator.UseVisualStyleBackColor = true;
            this.buttonAddIntegrator.Click += new System.EventHandler(this.buttonAddIntegrator_Click);
            // 
            // MitsubaOptionsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Name = "MitsubaOptionsControl";
            this.Size = new System.Drawing.Size(523, 420);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView listViewIntegrators;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonDeleteIntegrator;
        private System.Windows.Forms.Button buttonAddIntegrator;
    }
}
