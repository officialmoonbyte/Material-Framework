using Moonbyte.MaterialFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            materialTrackBar1.ValueChanged += (obj, args) =>
            {
                materialPictureBox1.Opacity = materialTrackBar1.Value;
                materialLabel1.Opacity = materialTrackBar1.Value;
                flatButton1.Opacity = materialTrackBar1.Value;
                materialTextBox1.Opacity = materialTrackBar1.Value;
                materialCheckBox1.Opacity = materialTrackBar1.Value;
                materialProgressBar1.Opacity = materialTrackBar1.Value;
            };
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void TabHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
