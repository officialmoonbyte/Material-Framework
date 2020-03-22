using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#region Legal Stuff

/*
 
MIT License

Copyright (c) 2015 - 2016 Vortex Studio (Inactive), 2015 - 2017 Indie Goat (Current Holder)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

    Support us! https://www.patreon.com/vortexstudio
    our website : https://vortexstudio.us

*/

#endregion

namespace Moonbyte.MaterialFramework.Controls
{
    public class MaterialComboBox : ComboBox
    {

        #region Vars

        private FlatButton _b = new FlatButton();
        private Color _BorderColor = Color.FromArgb(204, 204, 204);

        Font _bigFont = new Font("Segoe UI", 9f, FontStyle.Regular);
        Font _smallFont = new Font("Segoe UI", 4f, FontStyle.Bold);

        Size _comb_btn_Size = new Size(19, 21);

        int _comb_btn_Location_y = 1;
        int _comb_btn_Location_x = 23;
        int _comb_Border_Brush = 2;

        AnchorStyles _comb_btn_Anchor = (AnchorStyles.Top | AnchorStyles.Right);

        #endregion

        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color ButtonBackColor
        {
            get { return _b.BackColor; }
            set
            {
                _b.BackColor = value;
                _b.BorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color ButtonColorClicked
        {
            get { return _b.BackgroundColorClicked; }
            set
            {
                _b.BackgroundColorClicked = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color ButtonMouseOverColor
        {
            get { return _b.MouseOverColor; }
            set
            {
                _b.MouseOverColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color BorderColor
        {
            get { return this._BorderColor; }
            set
            {
                this._BorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set
            {
                base.BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color FontColor
        {
            get { return this.ForeColor; }
            set
            {
                this.ForeColor = value;
                this._b.ForeColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Required

        public MaterialComboBox()
        {
            /* Fonts */
            Font drawfont = _bigFont;
            //Font drawFont = smallFont;

            /* VoidCombobox */
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.Font = drawfont;

            /* B (Combobox Button) */
            this._b.Size = _comb_btn_Size;
            this._b.Location = new Point(this.Width - _comb_btn_Location_x, _comb_btn_Location_y);
            this._b.Text = "V";
            this._b.BorderColor = Color.FromArgb(214, 214, 214);
            this._b.BackColor = Color.FromArgb(214, 214, 214);
            this._b.Click += ((obj, args) =>
            {
                this.DroppedDown = true;
            });
            this._b.Anchor = _comb_btn_Anchor;
            this._b.Font = drawfont;
            this.Controls.Add(_b);

            /* Default Color Settings */
            this.ButtonColorClicked = Color.FromArgb(204, 204, 204);
            this.ButtonColorClicked = Color.FromArgb(51, 51, 51);
            this.ButtonMouseOverColor = Color.FromArgb(102, 102, 102);
            this.FontColor = Color.Black;
            this.BackColor = Color.FromArgb(238, 238, 238);
            this.BorderColor = Color.FromArgb(204, 204, 204);
        }

        #endregion

        #region Paint

        /// <summary>
        /// Used to prevent the control to paint the default stuff
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs pevent) { }

        /// <summary>
        /// Used to draw the border and the background of the control
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                //Drawing the back ground and the border.
                case 0xf:
                    Graphics g = this.CreateGraphics();
                    Pen pp = new Pen(this._BorderColor, _comb_Border_Brush);
                    Brush borderBrush = new SolidBrush(this.BackColor);
                    g.FillRectangle(borderBrush, this.ClientRectangle);
                    g.DrawRectangle(pp, this.ClientRectangle);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    break;
            }
        }

        #endregion

    }
}
