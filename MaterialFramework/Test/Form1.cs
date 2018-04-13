using IndieGoat.MaterialFramework.Controls;
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
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabHeader1.TabDragOut += (obj, args) =>
            {
                Console.WriteLine("OUT");
            };
            tabHeader2.TabDragOut += (obj, args) =>
            {
                Console.WriteLine("OUT");
            };
            materialTabControl1.ControlRemoved += (obj, args) =>
            {
                if (materialTabControl1.TabPages.Count == 0)
                {
                    Console.WriteLine("Closing");
                }
            };
            materialTabControl2.ControlRemoved += (obj, args) =>
            {
                if (materialTabControl2.TabPages.Count == 0)
                {
                    Console.WriteLine("Closing");
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            new Form1().Show();
        }
    }
}
