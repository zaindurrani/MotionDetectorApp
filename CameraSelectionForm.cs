using System;
using System.Windows.Forms;
using AForge.Video.DirectShow;

namespace MotionDetectorApp
{
    public partial class CameraSelectionForm : Form
    {
        public string SelectedCamera { get; private set; }
        private FilterInfoCollection _videoDevices;

        public CameraSelectionForm()
        {
            InitializeComponent();
        }

        private void CameraSelectionForm_Load(object sender, EventArgs e)
        {
            _videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in _videoDevices) // Explicitly cast to FilterInfo
            {
                comboBoxCameras.Items.Add(device.Name);
            }
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            if (comboBoxCameras.SelectedIndex >= 0)
            {
                SelectedCamera = _videoDevices[comboBoxCameras.SelectedIndex].MonikerString;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Please select a camera.");
            }
        }

        private void ComboBoxCameras_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}
