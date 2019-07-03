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


            tabHeader1.MouseMove += (obj, args) => {
                if (tabHeader1.MouseOverRect() == false && args.Button == MouseButtons.Left)
                { this.MouseMoveExternal(true, true); }
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
