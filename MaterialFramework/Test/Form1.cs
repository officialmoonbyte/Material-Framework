using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
