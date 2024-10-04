using System;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace MotionDetectorApp
{
    public partial class Form1 : Form
    {
        private VideoCaptureDevice _videoSource;
        private Bitmap _currentFrame;
        private Bitmap _previousFrame;
        private const int _motionThreshold = 30; // Adjust sensitivity

        public Form1()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            var cameraSelection = new CameraSelectionForm();
            if (cameraSelection.ShowDialog() == DialogResult.OK)
            {
                _videoSource = new VideoCaptureDevice(cameraSelection.SelectedCamera);
                _videoSource.NewFrame += new NewFrameEventHandler(VideoSource_NewFrame);
                _videoSource.Start();
                lblStatus.Text = "Motion Detection Started.";
            }
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            _previousFrame?.Dispose();
            _previousFrame = _currentFrame;
            _currentFrame = (Bitmap)eventArgs.Frame.Clone();

            // Display the current frame in the PictureBox
            pictureBoxCamera.Image = _currentFrame;

            if (_previousFrame != null)
            {
                DetectMotion();
            }
        }

        private void DetectMotion()
        {
            var grayCurrent = Grayscale.CommonAlgorithms.BT709.Apply(_currentFrame);
            var grayPrevious = Grayscale.CommonAlgorithms.BT709.Apply(_previousFrame);

            var difference = new Bitmap(grayCurrent.Width, grayCurrent.Height);
            for (int y = 0; y < grayCurrent.Height; y++)
            {
                for (int x = 0; x < grayCurrent.Width; x++)
                {
                    var currentPixel = grayCurrent.GetPixel(x, y).R;
                    var previousPixel = grayPrevious.GetPixel(x, y).R;
                    var diff = Math.Abs(currentPixel - previousPixel);
                    difference.SetPixel(x, y, diff > _motionThreshold ? Color.White : Color.Black);
                }
            }

            if (IsMotionDetected(difference))
            {
                CaptureAndEmail();
            }
        }

        private bool IsMotionDetected(Bitmap difference)
        {
            for (int y = 0; y < difference.Height; y++)
            {
                for (int x = 0; x < difference.Width; x++)
                {
                    if (difference.GetPixel(x, y).R == 255)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CaptureAndEmail()
        {
            string imagePath = "motion_capture.jpg";
            _currentFrame.Save(imagePath);
            SendEmail(imagePath);
        }

        private void SendEmail(string attachmentPath)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("your_email@example.com");
                    mail.To.Add("recipient_email@example.com");
                    mail.Subject = "Motion Detected!";
                    mail.Body = "Motion has been detected. See the attached image.";
                    mail.Attachments.Add(new Attachment(attachmentPath));

                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential("your_email@example.com", "your_password");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to send email: {ex.Message}");
            }
        }

        private void LblStatus_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Ready to start.";
        }
    }
}
