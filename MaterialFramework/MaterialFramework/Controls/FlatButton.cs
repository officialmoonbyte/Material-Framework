using IndieGoat.MaterialFramework.Controller_Units;
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

namespace IndieGoat.MaterialFramework.Controls
{
    /// <summary>
    /// A classical button but with a more 
    /// modern skin attached to it
    /// </summary>
    [DefaultEvent("Click")]
    public class FlatButton : UserControl
    {
        #region Vars

        //Border width with the button
        int _borderWidth = 0;

        //Bools for mouse events
        bool isMouseOver = false;
        bool isMouseClicked = false;

        //Text of the button
        string _Text = "FlatButton";

        #region Color's

        Color _BorderColor = Color.FromArgb(238, 238, 238);
        Color _MouseOverColor = Color.FromArgb(102, 102, 102);
        Color _BackgroundColor = Color.FromArgb(238, 238, 238);
        Color _BackColorClicked = Color.FromArgb(51, 51, 51);
        Color _WaveColor = Color.Black;
        Color _TextColor = Color.Black;

        #endregion

        #endregion

        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override Font Font
        {
            get { return base.Font; }
            set
            {
                base.Font = value;
                this.Invalidate();
            }

        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.Invalidate();
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Category("IndieGoat Control Settings")]
        public string text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                this.Invalidate();
            }
        }

        #region Color Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color WaveColor
        {
            get { return _WaveColor; }
            set
            {
                _WaveColor = value;
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
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color MouseOverColor
        {
            get { return _MouseOverColor; }
            set
            {
                _MouseOverColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color BackgroundColorClicked
        {
            get { return _BackColorClicked; }
            set
            {
                _BackColorClicked = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color TextColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region Required Method's

        /// <summary>
        /// Used on the creation on the control,
        /// set some varables and events.
        /// </summary>
        public FlatButton()
        {
            // FlatButton //
            this.DoubleBuffered = true;
            this.BackColor = _BackgroundColor;
            this.Size = new Size(75, 23);
            this.Font = new Font("Segoe UI", 8f, FontStyle.Bold);

            //Mouse Events
            this.MouseEnter += (sender, args) =>
            {
                isMouseOver = true;
                isMouseClicked = false;

                this.Invalidate();
            };
            this.MouseLeave += (sender, args) =>
            {
                isMouseOver = false;
                isMouseClicked = false;

                this.Invalidate();
            };
            this.MouseDown += (sender, args) =>
            {
                isMouseOver = false;
                isMouseClicked = true;

                this.Invalidate();
            };
            this.MouseUp += (sender, args) =>
            {
                isMouseOver = true;
                isMouseOver = false;

                this.Invalidate();
            };
        }

        /// <summary>
        /// Set the custom IMaterialControl to this
        /// control
        /// </summary>
        protected override void OnCreateControl()
        {
            //Set the Material Animation to this control
            new IMaterialControl(this, _WaveColor);

            base.OnCreateControl();
        }

        #endregion

        #region Over-ride Paint

        /// <summary>
        /// Custom painting for the control.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Invalidating the Graphics from the event args
            Graphics g = e.Graphics;

            //Drawing back color
            _BackgroundColor = this.BackColor;
            g.Clear(_BackgroundColor);

            //Initializing new Client Rectangle
            Rectangle b = new Rectangle(); b = this.ClientRectangle;

            //Drawing border
            ControlPaint.DrawBorder(g, this.ClientRectangle, _BorderColor, ButtonBorderStyle.Solid);

            //Setting font options
            StringFormat stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;

            //Set antialias on String
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Draw the text over the button
            g.DrawString(_Text, this.Font, new SolidBrush(_TextColor), b, stringFormat);

            base.OnPaint(e);
        }

        #endregion
    }
}
