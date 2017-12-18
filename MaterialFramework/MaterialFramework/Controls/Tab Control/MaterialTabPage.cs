using IndieGoat.MaterialFramework.Events;
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
    /// Custom tab page, used to handle the Icon for the tab.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false)]
    [DesignTimeVisible(true)]
    public class MaterialTabPage : TabPage
    {

        //Private local icon
        public Image icon;

        #region Custom Events

        public event EventHandler<TabIconChangeArgs> TabIconChange;

        #endregion

        #region Custom Method's

        /// <summary>
        /// Used to change the private var icon
        /// </summary>
        /// <param name="ico">Image var, used to chage the private var icon</param>
        public void ChangeTabIcon(Image ico)
        {
            //Invoke the TabIconChangeArgs if not null with the attached icon
            TabIconChange?.Invoke(this, new TabIconChangeArgs { icon = ico });

            //Change the local Icon to the image
            icon = ico;
        }

        #endregion
    }
}
