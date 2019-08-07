using Moonbyte.MaterialFramework.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : MaterialFramework.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaterialTextBox textBox = new MaterialTextBox();
            textBox.Text = "";
            textBox.Width = 300;
            textBox.Location = new Point(40, 40);

            this.Controls.Add(textBox);
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
           
        }

        private void TabHeader1_Load(object sender, EventArgs e)
        {

        }
    }
}
