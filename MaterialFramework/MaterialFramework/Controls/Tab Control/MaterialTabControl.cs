using System;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
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
    #region TabControl

    public class MaterialTabCollection : CollectionEditor
    {
        public MaterialTabCollection(Type type)
            : base(type)
        {

        }

        protected override bool CanSelectMultipleInstances()
        {
            return false;
        }


        protected override Type CreateCollectionItemType()
        {
            return typeof(MaterialTabPage);
        }

        protected override object CreateInstance(Type itemType)
        {
            MaterialTabPage tabPage = (MaterialTabPage)itemType.Assembly.CreateInstance(itemType.FullName);

            IDesignerHost host = (IDesignerHost)this.GetService(typeof(IDesignerHost));
            host.Container.Add(tabPage);
            //this.Context.Container.Add(tabPage);

            tabPage.Text = tabPage.Name;
            return tabPage;
        }

    }

    /// <summary>
    /// The tab control used with the TabHeader(s)
    /// </summary>
    public class MaterialTabControl : TabControl
    {

        /// <summary>
        /// A CollectionEditor for new tab pages to be added as a MaterialTabPage
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [EditorAttribute(typeof(MaterialTabCollection), typeof(UITypeEditor))]
        [MergableProperty(false)]
        public new TabPageCollection TabPages
        {
            get
            {
                return base.TabPages;
            }
        }

        /// <summary>
        /// Initialize the TabControl
        /// </summary>
        public MaterialTabControl() : base()
        {

        }

        /// <summary>
        /// Used to change the new tab page to a custom MaterialTabPage
        /// </summary>
        /// <param name="e"></param>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            //A try event to detect if the tab page is a material tab page
            try
            {
                //If failed to convert to a MaterialTabPage, this is a refrence of TabPage
                MaterialTabPage tabPage = (MaterialTabPage)e.Control;
            }
            catch
            {
                //Initializing both TabPage and the new MaterialTabPage
                TabPage tabPage = (TabPage)e.Control;
                MaterialTabPage newTabPage = new MaterialTabPage();

                //Setting custom vars to the NewTabPage
                newTabPage.Text = tabPage.Text;
                newTabPage.BackColor = tabPage.BackColor;

                //Removing the old one and adding the new tab page
                this.TabPages.Remove(tabPage);
                this.TabPages.Add(newTabPage);
            }
        }

        protected override void Dispose(bool disposing)
        {
            foreach(MaterialTabPage tabPage in this.TabPages)
            { foreach (Control c in tabPage.Controls)
                { c.Dispose(); } tabPage.Dispose();
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// Overrides the designer and only draw's the TabPage
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode) m.Result = (IntPtr)1;
            else base.WndProc(ref m);
        }
    }

    #endregion
}
