using Moonbyte.MaterialFramework.Events;
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

namespace Moonbyte.MaterialFramework.Controls
{
    /// <summary>
    /// A custom user control, like a track bar,
    /// but functions diffrently and is fully custom.
    /// </summary>
    [ToolboxBitmap(typeof(TrackBar))]
    [DefaultEvent("ValueChanged")]
    public class MaterialTrackBar : UserControl
    {
        #region Vars

        private int _MinValue = 0;
        private int _MaxValue = 100;
        private int _Value = 0;
        private int _opacity = 100;

        //Panel vars
        private int _line_pntX = 0;
        private int _line_pntY = 11;
        private int _line_Height = 6;
        private int _line_gap = 10;

        private Panel pnl = new Panel();

        private bool isMouseOver = false;
        private bool isMouseClicked = false;

        private Rectangle trackRectangle;

        #region Color Vars

        private Color _LineColor = Color.FromArgb(250, 250, 250);
        private Color _LineBorderColor = Color.FromArgb(235, 235, 235);
        private Color _TractColor = Color.Gainsboro;
        private Color _TractBorderColor = Color.FromArgb(160, 160, 160);
        private Color _CLineColor = Color.FromArgb(0, 235, 127);
        private Color _CLineBorderColor = Color.FromArgb(0, 200, 119);
        private Color _TractColorMouseOver = Color.FromArgb(180, 180, 180);
        private Color _TractColorMouseClick = Color.FromArgb(240, 240, 240);
        private Color _TractColorBorderMouseOver = Color.FromArgb(140, 140, 140);
        private Color _TractColorBorderMouseClick = Color.FromArgb(200, 200, 200);

        #endregion

        #endregion

        #region Event's

        public event EventHandler<TrackBarValueChangedArgs> ValueChanged;

        #endregion

        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public int Value
        {
            get { return _Value; }
            set
            {
                _Value = value;

                //Trigger the ValueChange event args
                ValueChanged?.Invoke(this, new TrackBarValueChangedArgs { Value = value });
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public int MaxValue
        {
            get { return _MaxValue; }
            set
            {
                _MaxValue = value;
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

        #region Color Settings

        #region Line Color

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color CompleteBorderColor
        {
            get { return _LineBorderColor; }
            set
            {
                _LineBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color CompleteLineColor
        {
            get { return _CLineColor; }
            set
            {
                _CLineColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color TractBorderColor
        {
            get { return _TractBorderColor; }
            set
            {
                _TractBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color TractColor
        {
            get { return _TractColor; }
            set
            {
                _TractColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color LineBorderColor
        {
            get { return _LineBorderColor; }
            set
            {
                _LineBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("LineColor")]
        public Color LineColor
        {
            get { return _LineColor; }
            set
            {
                _LineColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region TractColorSettings

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Tract Color")]
        public Color TractColorBorderMouseClick
        {
            get { return _TractColorBorderMouseClick; }
            set
            {
                _TractColorBorderMouseClick = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Tract Color")]
        public Color TractColorMouseOverBorderColor
        {
            get { return _TractColorBorderMouseOver; }
            set
            {
                _TractColorBorderMouseOver = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Tract Color")]
        public Color TractColorMouseClick
        {
            get { return _TractColorMouseClick; }
            set
            {
                _TractColorMouseClick = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Tract Color")]
        public Color TractColorMouseOver
        {
            get { return _TractColorMouseOver; }
            set
            {
                _TractColorMouseOver = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #endregion

        #region Required

        /// <summary>
        /// Happens when the TrackBar was created.
        /// </summary>
        public MaterialTrackBar()
        {
            //Setting the size of the control
            this.Size = new Size(300, 24);

            //Setting the double buffered to true
            this.DoubleBuffered = true;
        }

        #endregion

        #region Paint

        Rectangle lineRectangle;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Setting the new graphics
            Graphics g = e.Graphics;

            //Get the mouse position
            Point mousePosition = Cursor.Position;

            //Get alpha int
            int alpha = (_opacity * 255) / 100;

            //Invalidating the LineRectangle
            lineRectangle = new Rectangle(_line_pntX + _line_gap, _line_pntY, this.Width - (_line_gap * 2), _line_Height);

            //Drawing the LineRectangle
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, _LineColor)), lineRectangle);
            g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(alpha, _LineBorderColor))), lineRectangle);

            //Drawing the track.
            DrawTrack(g);
        }

        #endregion

        #region Resize Events

        /// <summary>
        /// On resize, used to keep the height capped to 24.
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            //Set the height of the control
            this.Height = 24;
        }

        #endregion

        #region MouseClick Events

        /// <summary>
        /// Triggers when the mouse clicks the control
        /// </summary>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            //Get the mouse position
            Point mousePosition = Cursor.Position;

            //Check if the button was the LeftClick
            if (e.Button == MouseButtons.Left)
            {
                //Getting the new values
                decimal TakPercent = decimal.Divide(this.PointToClient(mousePosition).X - 6, lineRectangle.Width);
                decimal percentSizeRatio = decimal.Divide(_MaxValue, lineRectangle.Width);
                decimal sizeUnits = decimal.Multiply(TakPercent, lineRectangle.Width);

                //Setting the value
                this.Value = (int)Math.Round((sizeUnits * percentSizeRatio));
                this.Invalidate();
            }
        }

        /// <summary>
        /// Triggers when the mouse moves
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //Invalidate the control when the mouse is over the track
            if (trackRectangle.Contains(PointToClient(MousePosition)))
            {
                isMouseOver = true; this.Invalidate();
            }
            else
            {
                isMouseOver = false; this.Invalidate();
            }

            //Check if the button was LeftClick
            if (e.Button == MouseButtons.Left)
            {
                //invalidate the track color when it is clicked
                if (trackRectangle.Contains(PointToClient(MousePosition))) isMouseClicked = true;

                //Get the mouse position
                Point mousePosition = Cursor.Position;

                //Getting the new values
                decimal TakPercent = decimal.Divide(this.PointToClient(mousePosition).X - 6, lineRectangle.Width);
                decimal percentSizeRatio = decimal.Divide(_MaxValue, lineRectangle.Width);
                decimal sizeUnits = decimal.Multiply(TakPercent, lineRectangle.Width);

                //Setting the new value
                int tmpValue = (int)Math.Round((sizeUnits * percentSizeRatio));

                if (tmpValue > _MaxValue) { Value = MaxValue; }
                else if (tmpValue < 0) { Value = 0; }
                else { Value = tmpValue; }

                this.Invalidate();
            }
            else { isMouseClicked = false; }
        }

        /// <summary>
        /// Triggers when the mouse is up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            //Get the mouse position
            Point mousePosition = Cursor.Position;

            //Setting the MouseClick to false
            isMouseClicked = false;

            //Getting the new values
            decimal TakPercent = decimal.Divide(this.PointToClient(mousePosition).X - 6, lineRectangle.Width);
            decimal percentSizeRatio = decimal.Divide(_MaxValue, lineRectangle.Width);
            decimal sizeUnits = decimal.Multiply(TakPercent, lineRectangle.Width);

            //Setting the new value
            int tmpValue = (int)Math.Round((sizeUnits * percentSizeRatio));

            if (tmpValue > _MaxValue) { Value = MaxValue; }
            else if (tmpValue < 0) { Value = 0; }
            else { Value = tmpValue; }

            this.Invalidate();

            base.OnMouseUp(e);
        }

        #endregion

        #region Track

        private void DrawTrack(Graphics g)
        {
            //Get the percent of the control
            decimal TakPercent = decimal.Divide(Value, _MaxValue);

            //Getting the color for the track
            Color tmpTickColor;
            Color tmpTickBorderColor;

            //Setting all of the colors based off of mouse events
            if (isMouseClicked) { tmpTickColor = _TractColorMouseClick; tmpTickBorderColor = _TractColorBorderMouseClick; }
            else if (isMouseOver) { tmpTickColor = _TractColorMouseOver; tmpTickBorderColor = _TractColorBorderMouseOver; }
            else { tmpTickColor = _TractColor; tmpTickBorderColor = _TractBorderColor; }

            //Get the TrackRectangle based off of the percent
            trackRectangle = new Rectangle((int)decimal.Multiply(lineRectangle.Width, TakPercent), 6, 16, 16);
            Rectangle completeRectangle = new Rectangle(_line_pntX + _line_gap, _line_pntY, ((int)decimal.Multiply(lineRectangle.Width, TakPercent)), _line_Height);

            //Set the smoothing mode
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            //Get alpha int
            int alpha = (_opacity * 255) / 100;
            
            //Fill the rectangle for the completed line
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, _CLineColor)), completeRectangle);
            g.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(alpha, _CLineBorderColor))), completeRectangle);

            //Fill the eclpise for the completed line
            g.FillEllipse(new SolidBrush(Color.FromArgb(alpha, tmpTickColor)), trackRectangle);
            g.DrawEllipse(new Pen(new SolidBrush(Color.FromArgb(alpha, tmpTickBorderColor))), trackRectangle);

            //Set the smoothing mode back to default
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

        }

        #endregion

    }
}
