using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
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


    //This document is currently really unorganized! Will update later.

namespace Moonbyte.MaterialFramework.Controls
{
    /// <summary>
    /// A label with custom font
    /// </summary>
    public class MaterialLabel : Label
    {

        #region Vars

        int _Opacity = 0;
        Color _OpacityColor = Color.White;

        Font font = new Font("Segoe UI", 10f);

        #endregion Vars

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

        #endregion Properties

        #region Initialization

        public MaterialLabel()
        {
            this.Font = font;
            this.AutoSize = true;
            this.BackColor = Color.Transparent;
        }

        #endregion

        #region Antialiasing 

        private TextRenderingHint _textRenderingHint = TextRenderingHint.SystemDefault;

        public TextRenderingHint TextRenderingHint
        {
            get { return _textRenderingHint; }
            set { _textRenderingHint = value; }
        }

        #endregion Antialiasing

        #region Override Paint

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            pe.Graphics.TextRenderingHint = _textRenderingHint;

            //Draws the Opacity rectangle
            if (_Opacity > 0)
            { pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(_Opacity, _OpacityColor)), this.ClientRectangle); }
        }

        #endregion Override Paint

        #region UpdateOpacityColor

        /// <summary>
        /// Updates the OpacityColor var with the Parent Back Color
        /// </summary>
        public void UpdateOpacityColor()
        { _OpacityColor = this.Parent.BackColor; }

        #endregion UpdateOpacityColor
    }
}
