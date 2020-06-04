using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

#region Future Information

/* We will want to add a smooth animation in between changing the value of the progress bar. */

#endregion

namespace Moonbyte.MaterialFramework.Controls
{
    [DefaultEvent("PercentChange")]
    public class MaterialProgressBar : UserControl
    {
        #region Vars

        int _min = 0;
        int _max = 100;
        int _value = 0;

        int _Opacity = 0;
        Color _OpacityColor = Color.White;

        #region Colors

        Color _BarColor = Color.FromArgb(6, 176, 37);
        Color _BorderColor = Color.FromArgb(188, 188, 188);
        Color _BackColor = Color.FromArgb(230, 230, 230);

        #endregion

        #endregion

        #region Properties

        #region Opacity Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Moonbyte Control Settings")]
        public int Opacity
        {
            get
            { return _Opacity; }
            set
            {
                _Opacity = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Moonbyte Control Settings")]
        public Color OpacityColor
        {
            get
            { return _OpacityColor; }
            set
            {
                _OpacityColor = value;
                this.Invalidate();
            }
        }

        #endregion Opacity Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Void Settings")]
        public int Minimum
        {
            get { return _min; }
            set
            {
                if (value < 0) { _min = 0; }
                if (value > _min) { _min = value; _min = value; }
                if (_value < _min) { _value = _min; }
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Void Settings")]
        public int Maximum
        {
            get { return _max; }
            set
            {
                if (value < _min) { _min = value; }
                _max = value;
                if (_value > _max) { _value = _max; }
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Void Settings")]
        public int Value
        {
            get { return _value; }
            set
            {
                int oldValue = _value;
                if (value < _min) { _value = _min; }
                else if (value > _max) { _value = _max; }
                else { _value = value; }

                float percent;
                Rectangle newValueRect = this.ClientRectangle;
                Rectangle oldValueRect = this.ClientRectangle;

                percent = (float)(_value - _min) / (float)(_max - _min);
                newValueRect.Width = (int)((float)newValueRect.Width * percent);
                percent = (float)(oldValue - _min) / (float)(_max - _min);
                oldValueRect.Width = (int)((float)oldValueRect.Width * percent);
                Rectangle updateRect = new Rectangle();

                if (newValueRect.Width > oldValueRect.Width)
                {
                    updateRect.X = oldValueRect.Size.Width;
                    updateRect.Width = newValueRect.Width - oldValueRect.Width;
                }
                else
                {
                    updateRect.X = newValueRect.Size.Width;
                    updateRect.Width = oldValueRect.Width - newValueRect.Width;
                }

                updateRect.Height = this.Height;

                this.Invalidate(updateRect);

                PercentChange?.Invoke(value, new EventArgs());
            }
        }

        #region Color Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Indie Goat Settings")]
        public Color ProgressBarColor
        {
            get { return _BarColor; }
            set
            {
                _BarColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Indie Goat Settings")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set
            {
                _BorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Indie Goat Settings")]
        public override Color BackColor
        {
            get { return _BackColor; }
            set
            {
                this._BackColor = value;
                this.Invalidate();
            }
        }


        #endregion

        #endregion

        #region Event's

        public event EventHandler PercentChange;

        #endregion

        #region Required

        //Set the style and the size of the progress bar
        public MaterialProgressBar()
        {
            this.SetStyle(ControlStyles.DoubleBuffer |
   ControlStyles.UserPaint |
   ControlStyles.AllPaintingInWmPaint,
   true);
            this.Size = new Size(120, 23);
        }

        #endregion 

        #region Override Method's

        /// <summary>
        /// On resize, invalidate the control
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            this.Invalidate();
        }

        /// <summary>
        /// Overide the paint for custom painting
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Initialize the graphics and brush
            Graphics g = e.Graphics;
            SolidBrush brush = new SolidBrush(_BarColor);

            //Get the percent of the value filled up
            float percent = (float)(_value - _min) / (float)(_max - _min);

            //Initialize the rectangle
            Rectangle rect = this.ClientRectangle;
            rect.Width = (int)((float)rect.Width * percent);

            //Fill the percentage rectangle
            g.FillRectangle(brush, rect);

            //Draw the border with the paint graphics
            DrawBorder(g);

            //Dispose of the brush
            brush.Dispose();

            //Draws the Opacity rectangle
            if (_Opacity > 0)
            { e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(_Opacity, _OpacityColor)), this.ClientRectangle); }
        }

        //Draw the border 
        private void DrawBorder(Graphics g)
        {
            ControlPaint.DrawBorder(g, this.ClientRectangle, _BorderColor, ButtonBorderStyle.Solid);
        }

        #endregion

        #region UpdateOpacityColor

        /// <summary>
        /// Updates the OpacityColor var with the Parent Back Color
        /// </summary>
        public void UpdateOpacityColor()
        { _OpacityColor = this.Parent.BackColor; }

        #endregion UpdateOpacityColor
    }
}
