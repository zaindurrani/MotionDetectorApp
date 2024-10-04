namespace MotionDetectorApp
{
    partial class CameraSelectionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnOk = new System.Windows.Forms.Button();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.labelSelectCamera = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(343, 119);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(343, 70);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCameras.TabIndex = 2;
            this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.ComboBoxCameras_SelectedIndexChanged);
            // 
            // labelSelectCamera
            // 
            this.labelSelectCamera.AutoSize = true;
            this.labelSelectCamera.Location = new System.Drawing.Point(340, 34);
            this.labelSelectCamera.Name = "labelSelectCamera";
            this.labelSelectCamera.Size = new System.Drawing.Size(85, 13);
            this.labelSelectCamera.TabIndex = 3;
            this.labelSelectCamera.Text = "Select a Camera";
            // 
            // CameraSelectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.labelSelectCamera);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.btnOk);
            this.Name = "CameraSelectionForm";
            this.Text = "Camera Selection";
            this.Load += new System.EventHandler(this.CameraSelectionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Label labelSelectCamera;
    }
}
