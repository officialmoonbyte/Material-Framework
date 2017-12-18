using System;
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
    /// A custom text box design used by Metro Framework with custom
    /// color properties.
    /// </summary>
    [DefaultEvent("CheckChange")]
    public class MaterialCheckBox : UserControl
    {
        #region Vars

        bool _Checked = false;
        bool IsMouseOver = false;
        bool MouseClicked = false;

        Color _BorderColor = Color.FromArgb(153, 153, 153);
        Color _OnMouseOverColor = Color.FromArgb(51, 51, 51);
        Color _MiddleColor = Color.FromArgb(0, 174, 219);
        Color _ClickColor = Color.FromArgb(0, 74, 74);

        int rect_x = 2;
        int rect_y = 2;
        int rect_width = 12;
        int rect_height = 12;
        int checkWidth_MinusBy = 4;
        int checkHeight_MinusBy = 4;

        #endregion

        #region Events

        public event EventHandler CheckChange;

        #endregion

        #region Properties

        #region Color

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
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color BorderColor
        {
            get
            {
                return _BorderColor;
            }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color OnMouseOverColor
        {
            get
            {
                return _OnMouseOverColor;
            }
            set
            {
                _OnMouseOverColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color MiddleColor
        {
            get
            {
                return _MiddleColor;
            }
            set
            {
                _MiddleColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public Color ClickedColor
        {
            get
            {
                return _ClickColor;
            }
            set
            {
                _ClickColor = value;
                this.Invalidate();
            }
        }

        #endregion

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public bool Checked
        {
            get
            {
                return _Checked;
            }
            set
            {
                _Checked = value;
                this.Invalidate();
                CheckChange?.Invoke(value, new EventArgs());
            }
        }

        #endregion

        #region Start up / Required

        /// <summary>
        /// Initialize the text box back color, and set the size of the check box
        /// </summary>
        public MaterialCheckBox()
        {
            //Set the back color and the size of the control
            this.BackColor = Color.Transparent;
            this.Size = new Size(16, 16);
        }

        #endregion

        #region Mouse Events

        // Mouse Events to set IsMouseOver and MouseIsClicked.
        protected override void OnMouseEnter(EventArgs e)
        {
            DrawBorder(true); IsMouseOver = true;
            DrawCheck(false); MouseClicked = false;
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            DrawBorder(false); IsMouseOver = false;
            DrawCheck(false); MouseClicked = false;
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            DrawBorder(true); IsMouseOver = true;
            DrawCheck(true); MouseClicked = true;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Check if the client rectangle contains the point to the mouse
            if (this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                DrawBorder(true); IsMouseOver = true;
                DrawCheck(false); MouseClicked = false;

                //Setting the bool IsChecked
                if (this.Checked)
                {
                    this.Checked = false;
                }
                else
                {
                    this.Checked = true;
                }
            }
            else
            {
                DrawBorder(false); IsMouseOver = false;
                DrawCheck(false); MouseClicked = false;
            }

            base.OnMouseUp(e);
        }

        #endregion

        #region Override Painting

        /// <summary>
        /// Custom painting for the Border and the Check
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Painting the border and the check
            DrawBorder(IsMouseOver);
            DrawCheck(MouseClicked);

            base.OnPaint(e);
        }

        #endregion

        #region Custom Painting 

        /// <summary>
        /// Draw a line around the Control.ClientRectangle. 
        /// </summary>
        /// <param name="MouseIsInControl">A bool used to detect if the mouse is 
        /// in the control or not.</param>
        private void DrawBorder(bool MouseIsInControl)
        {
            //If Mouse is in control
            if (MouseIsInControl == true)
            {
                //Draw the border with the OnMouseOver color
                ControlPaint.DrawBorder(this.CreateGraphics(), this.ClientRectangle, OnMouseOverColor, ButtonBorderStyle.Solid);
            } //Else if mouse is not in control
            else
            {
                //Draw the border with the default border color
                ControlPaint.DrawBorder(this.CreateGraphics(), this.ClientRectangle, BorderColor, ButtonBorderStyle.Solid);
            }

        }


        /// <summary>
        /// Draw the rectangle for the check. If mouse is down,
        /// draw the check in a diffrent color.
        /// </summary>
        /// <param name="MouseIsDown">A bool that states if the mouse is currently down or not.</param>
        private void DrawCheck(bool MouseIsDown)
        {
            //Return if the control check value is false.
            if (!_Checked) return;

            //Initializing the brush
            SolidBrush CheckBrush;

            //If mouse is down, draw the check with a custom color
            if (MouseIsDown)
            {
                //Setting the check brush to mouse click color
                CheckBrush = new SolidBrush(ClickedColor);
            }
            else
            {
                //Setting the check brush to the default middle color
                CheckBrush = new SolidBrush(MiddleColor);
            }

            //Initializing the graphics
            Graphics g = this.CreateGraphics();

            //Drawing the check rectangle
            g.FillRectangle(CheckBrush, new Rectangle(
                rect_x, rect_y, this.Width - checkWidth_MinusBy, this.Height - checkHeight_MinusBy));

            //Dispose of unused objects
            CheckBrush.Dispose();
            g.Dispose();
        }

        #endregion

    }
}
