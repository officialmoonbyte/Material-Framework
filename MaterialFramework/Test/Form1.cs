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
            Panel panel = new Panel();
            panel.Location = new Point(1, 1);
            panel.Size = new Size(this.Width - 2, this.Height - 2);
            panel.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom);
            panel.MouseMove += (obj, args) =>
            {
                bool result; if (args.Button == MouseButtons.Left) { result = true; } else { result = false; }
                this.MouseMoveExternal(result);
            };
            panel.MouseDown += (obj, args) =>
            {
                bool result; if (args.Button == MouseButtons.Left) { result = true; } else { result = false; }
                this.MouseDownExternal(result);
            };
            panel.MouseUp += (obj, args) =>
            {
                this.MouseUpExternal();
            };
            //this.Controls.Add(panel);
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
           
        }
    }
}
