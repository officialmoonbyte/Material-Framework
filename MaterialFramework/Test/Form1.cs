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
            materialTabPage1.icon = Image.FromFile(@"C:\Users\Alexander Ritter\source\repos\protection.ico");
            materialTabPage2.icon = Image.FromFile(@"C:\Users\Alexander Ritter\source\repos\protection.ico");
            materialTabPage3.icon = Image.FromFile(@"C:\Users\Alexander Ritter\source\repos\protection.ico");

            tabHeader1.MouseMove += (obj, args) => { if (tabHeader1.MouseOverRect() == false)
                { this.MouseMoveExternal(true, true); } };
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
