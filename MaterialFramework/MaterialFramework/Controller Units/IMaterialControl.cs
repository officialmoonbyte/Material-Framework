using IndieGoat.MaterialDesign.Animations;
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

namespace IndieGoat.MaterialFramework.Controller_Units
{
    /// <summary>
    /// Custom class, used for a easier method to interact
    /// with the Animation Manager, designed by Ignace Maes
    /// </summary>
    public class IMaterialControl
    {
        #region Vars

        //Animation vars, used to calculate the size and position of the eclipse
        private readonly AnimationManager _AnimationManager = new AnimationManager(false)
        {
            Increment = 0.03,
            AnimationType = AnimationType.EaseOut
        };
        private readonly AnimationManager _HoverAnimation = new AnimationManager()
        {
            Increment = 0.07,
            AnimationType = AnimationType.Linear
        };

        #endregion

        #region Required / On Startup

        /// <summary>
        /// Used to set the Animation Progress to the
        /// Invalidate event of the BaseUserControl
        /// </summary>
        /// <param name="baseUserControl">UserControl you want to have this animation on</param>
        /// <param name="WaveColor">The color of the wave</param>
        /// <param name="hoverAnimation">Depends on if you want to calculate HoverAnimation - Currently not supported</param>
        public IMaterialControl(UserControl baseUserControl, Color WaveColor, bool hoverAnimation = false)
        {
            //Set the hover animation to the base control Invalidate event
            if (hoverAnimation) _HoverAnimation.OnAnimationProgress += sender => baseUserControl.Invalidate();

            //Run the InitializeAnimation Method
            InitializeAnimation(baseUserControl, WaveColor);
        }

        #endregion

        #region Animation

        //Use to setup the animation with the BaseControl
        private void InitializeAnimation(UserControl _baseUserControl, Color _WaveColor)
        {
            //Setting the AnimationManager progress to invalidate the base control
            _AnimationManager.OnAnimationProgress += sender => _baseUserControl.Invalidate();

            //Setting the BaseMouseDown event for the control
            _baseUserControl.MouseDown += (sender, args) =>
            {
                //Check if the mouse button was Left
                if (args.Button == MouseButtons.Left)
                {
                    //Start a new animation and invalidate the control
                    _AnimationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    _baseUserControl.Invalidate();
                }
            };

            //Setting the BaseMouseUp event for the control
            _baseUserControl.MouseUp += (sender, args) =>
            {
                //Invalidate the control
                _baseUserControl.Invalidate();
            };

            //Overriding the Paint method of the control
            _baseUserControl.Paint += (sender, args) =>
            {
                //Getting the graphics for the paint method
                Graphics graphics = args.Graphics;

                //Check if the AnimationManager is animating
                if (_AnimationManager.IsAnimating())
                {
                    //Set smoothing mode
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    //For each animation count
                    for (int i = 0; i < _AnimationManager.GetAnimationCount(); i++)
                    {
                        //Getting Animation source and Value
                        var animationValue = _AnimationManager.GetProgress(i);
                        var animationSource = _AnimationManager.GetSource(i);

                        //Draw the eclipse for the animation
                        using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), _WaveColor)))
                        {
                            var rippleSize = (int)(animationValue * _baseUserControl.Width * 2);
                            graphics.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                        }
                    }

                    //Reset the Smoothing mode
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }
            };
        }

        #endregion
    }
}
