using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialFramework.Controls
{
    public class MaterialPictureBox : PictureBox
    {

        #region Vars

        int _Opacity = 0;
        Color _OpacityColor = Color.White;

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

        public MaterialPictureBox()
        { this.DoubleBuffered = true; }

        #endregion Initialization

        #region Override Paint

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

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
