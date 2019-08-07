using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Moonbyte.MaterialFramework.Controls
{
    /// <summary>
    /// Interaction logic for MaterialButton.xaml
    /// </summary>
    public partial class MaterialButton : UserControl
    {

        #region Vars



        #endregion

        #region Properties

        #region Dependencies

        public static DependencyProperty BackColorDependency = DependencyProperty.Register("BackColor", typeof(Color), typeof(MaterialButton), new PropertyMetadata(Color.FromArgb(250, 250, 250, 250)));

        #endregion Dependencies

        #region BackColor

        public Color BackColor
        {
            get { return (Color)base.GetValue(BackColorDependency); }
            set { base.SetValue(BackColorDependency, value); }
        }

        #endregion

        #endregion

        #region Initialization

        /// <summary>
        /// Used on creation of the control
        /// </summary>
        public MaterialButton()
        {
            Console.WriteLine(this.BackColor);
        }

        #endregion

        #region OnPaint



        #endregion OnPaint
    }
}
