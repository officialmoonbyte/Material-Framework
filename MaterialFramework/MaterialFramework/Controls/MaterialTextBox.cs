using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

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
    public class MaterialTextBox : UserControl
    {

        #region Variables

        //The based control for the text box
        public TextBox baseControl = new TextBox();

        bool focc = false;

        //All of the color properties for the control
        Color _borderColor = Color.FromArgb(204, 204, 204);
        Color _borderSelectedColor = Color.FromArgb(0, 135, 250);
        Color _bottomBorderColor = Color.FromArgb(204, 204, 204);

        //Private vars for the BaseControl
        Font _baseControl_Font = new Font("Segoe UI", 11);
        string _baseControl_String = "MaterialTextBox";

        int _opacity = 100;

        #endregion

        #region Events

        public event EventHandler TextBoxClick { add { baseControl.Click += value; } remove { baseControl.Click -= value; } }
        public event EventHandler TextChange { add { baseControl.TextChanged += value; } remove { baseControl.TextChanged -= value; } }

        #endregion

        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return baseControl.UseSystemPasswordChar;
            }
            set
            {
                baseControl.UseSystemPasswordChar = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override string Text
        {
            get
            {
                return _baseControl_String;
            }
            set
            {
                _baseControl_String = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override Font Font
        {
            get
            {
                return _baseControl_Font;
            }
            set
            {
                _baseControl_Font = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public int Opacity
        {
            get { return _opacity; }
            set
            {
                if (value > 100) _opacity = 100;
                else if (value < 1) _opacity = 1;
                else { _opacity = value; }

                this.Invalidate();
            }
        }

        #region Color Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                baseControl.BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color FontColor
        {
            get
            {
                return baseControl.ForeColor;
            }
            set
            {
                baseControl.ForeColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color BottomBorderColor
        {
            get
            {
                return _bottomBorderColor;
            }
            set
            {
                _bottomBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color SelectedBottomBorderColor
        {
            get
            {
                return _borderSelectedColor;
            }
            set
            {
                _borderSelectedColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region Required

        /// <summary>
        /// Start of the control
        /// </summary>
        public MaterialTextBox()
        {
            //Set the default size and back color of the control
            this.Size = new Size(200, 23);
            this.BackColor = Color.FromArgb(238, 238, 238);

            //Set the base control default settings
            baseControl.BackColor = Color.FromArgb(238, 238, 238);
            baseControl.UseSystemPasswordChar = false;
            baseControl.Font = _baseControl_Font;
            baseControl.Text = _baseControl_String;

            //Basecontrol events
            baseControl.TextChanged += BaseControl_TextChanged;

            //Setting the support transparent styles to true
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            //More based setting controls
            baseControl.Multiline = false;
            baseControl.BorderStyle = BorderStyle.None;
            this.Controls.Add(baseControl);

            //Invalidate the size.
            InvalidateSize();
        }

        #endregion

        #region Resize / On Resize

        /// <summary>
        /// Used to set the default size for both base control
        /// and the control.
        /// </summary>
        protected override void OnResize(EventArgs e)
        { 
            //Set the BaseControl Width and location and then set the height of the control.
            baseControl.Location = new Point(4, 1);
            baseControl.Width = this.Width - 5;
            this.Height = baseControl.Height + 2;

            //Update the background
            UpdateBackground();

            base.OnResize(e);
        }

        /// <summary>
        /// Used to invalidate the size of the base control
        /// </summary>
        private void InvalidateSize()
        {
            //Invalidate the base control
            baseControl.Size = new Size(this.Width - 10, this.Height - 2);
            baseControl.Location = new Point(3, 3);

            //Invalidate the height of the TextBox
            this.Height = 24;
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            //Set the BaseControl Width, and location and then set the height of the control.
            baseControl.Location = new Point(4, 1);
            baseControl.Width = this.Width - 5;
            this.Height = baseControl.Height + 2;

            //Setting the font and the string for the base control
            baseControl.Text = _baseControl_String;
            baseControl.Font = _baseControl_Font;

            base.OnInvalidated(e);
        }

        #endregion

        #region Title settings

        /// <summary>
        /// Changes the private var when the contorl text has changed.
        /// </summary>
        /// <param name="sender">Ojbect to trigger the event</param>
        /// <param name="e">The event args</param>
        private void BaseControl_TextChanged(object sender, EventArgs e)
        {
            //Changes the private var on the base control
            _baseControl_String = baseControl.Text;
        }

        #endregion

        #region Paint and Background


        /// <summary>
        /// Used to draw the border of the control
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Get alpha int
            int alpha = (_opacity * 255) / 100;

            //Draw the broder of the control
            ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.FromArgb(alpha, _borderColor), 1, 
                ButtonBorderStyle.Solid, Color.FromArgb(alpha,_borderColor), 1, 
                ButtonBorderStyle.Solid, Color.FromArgb(alpha, _borderColor), 1, 
                ButtonBorderStyle.Solid, Color.FromArgb(alpha, _bottomBorderColor), 1, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// Used to  the back color for the control
        /// when the back color is out of sync.
        /// </summary>
        private void UpdateBackground()
        {
            //Get alpha int
            int alpha = (_opacity * 255) / 100;

            //Set the back color of the Background
            this.BackColor = Color.FromArgb(alpha, BackColor);
        }

        #endregion

    }
}
