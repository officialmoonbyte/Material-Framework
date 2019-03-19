using IndieGoat.MaterialFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialFramework.Controls
{
    public class TabHeader : UserControl
    {
        #region Vars

        MaterialTabControl basedTabControl;
        List<Rectangle> tabRectangles = new List<Rectangle>();

        enum Status { Default, Selected, MouseOver }

        bool arrowEnable = false;

        Color tabBackColor = Color.FromArgb(35, 35, 64);
        Color tabBorderColor = Color.FromArgb(75, 78, 101);
        Color topBorderColor = Color.FromArgb(35, 35, 64);

        Color s_tabBackColor = Color.FromArgb(249, 249, 250);
        Color s_topBorderColor = Color.FromArgb(10, 132, 255);
        Color s_TabBorderColor = Color.FromArgb(75, 78, 101);

        Color h_tabBackColor = Color.FromArgb(55, 57, 84);
        Color h_tabBorderColor = Color.FromArgb(75, 78, 101);
        Color h_topBorderColor = Color.FromArgb(164, 171, 182);

        int _RectWidth = 230;

        #endregion Vars

        #region Events



        #endregion Events

        #region Properties

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Moonbyte")]
        public MaterialTabControl BasedTabControl
        {
            get { return this.basedTabControl; }
            set
            {
                this.basedTabControl = value;
                if (basedTabControl == null) return;
                basedTabControl.Deselected += (obj, args) =>
                {
                    this.Invalidate();
                };
                basedTabControl.ControlAdded += (obj, args) =>
                {
                    foreach (MaterialTabPage page in basedTabControl.TabPages)
                    {
                        //Setting the events of the tab page
                        page.TabIconChange += ((ss, sss) =>
                        {
                            this.Invalidate();
                        });
                        page.TabTextChanged += ((ss, sss) =>
                        {
                            this.Invalidate();
                        });
                    }
                };
                basedTabControl.ControlRemoved += (obj, args) =>
                {
                    this.Invalidate();
                };
                if (basedTabControl.TabPages.Count != 0)
                {
                    foreach (MaterialTabPage page in basedTabControl.TabPages)
                    {
                        //Setting the events of the tab page
                        page.TabIconChange += ((sender, args) =>
                        {
                            this.Invalidate();
                        });
                        page.TabTextChanged += ((sender, args) =>
                        {
                            this.Invalidate();
                        });
                    }
                }
            }
        }

        public int TabPageWidth
        {
            get { return this._RectWidth; }
            set { this._RectWidth = value; this.Invalidate(); }
        }

        #endregion Properties

        #region Initialization

        public TabHeader()
        {
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.Height = 32;
        }

        #endregion Initialization

        #region Override Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //Returns if there is no tabs
            if (basedTabControl == null) return;

            Graphics g = e.Graphics;
            

            // Draws Background
            g.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            //Gets a list of rectangles
            tabRectangles = new List<Rectangle>();

            //Check if we should draw the arrow buttons
            int AllTabWidth = basedTabControl.TabPages.Count * this._RectWidth;
            if (AllTabWidth >= this.Width)
            { arrowEnable = true; } else { arrowEnable = false; }

            //Draw tabs
            tabRectangles = GetTabRectangles();
            foreach (Rectangle rect in tabRectangles)
            { PaintTab(rect, g); }

            //Draws the arrow buttons
            DrawArrowButton(g);

        }

        #endregion Override Paint

        #region Paint Tab

        private void PaintTab(Rectangle TabRectangle, Graphics g)
        {
            Status tabStatus = Status.Default;

            MaterialTabPage tabPage = GetTabByPoint(TabRectangle);
            if (basedTabControl.SelectedTab == tabPage) { tabStatus = Status.Selected; }
            else if (TabRectangle.Contains(this.PointToClient(MousePosition)))
            { tabStatus = Status.MouseOver; }

            if (tabStatus == Status.Default)
            {
                PaintTab(TabRectangle, tabPage, g, tabBackColor, tabBorderColor, topBorderColor, tabStatus);
            }
            else if (tabStatus == Status.MouseOver)
            {
                PaintTab(TabRectangle, tabPage, g, h_tabBackColor, h_tabBorderColor, h_topBorderColor, tabStatus);
            }
            else if (tabStatus == Status.Selected)
            {
                PaintTab(TabRectangle, tabPage, g, s_tabBackColor, s_TabBorderColor, s_topBorderColor, tabStatus);
            }
        }

        private void PaintTab(Rectangle TabRectangle, MaterialTabPage tabPage, Graphics g, Color BackColor, Color BorderColor, Color TopBorderColor, Status tabStatus)
        {

            int tabIndex = basedTabControl.TabPages.IndexOf(tabPage);

            Color tabTextColor = Color.White;

            //Draw background of the tab
            g.FillRectangle(new SolidBrush(BackColor), TabRectangle);

            //Draw borders of the tab
            if ((tabIndex + 1) != basedTabControl.TabPages.Count)
            {
                ControlPaint.DrawBorder(g, TabRectangle, BackColor, 1, ButtonBorderStyle.Solid,
                    BackColor, 1, ButtonBorderStyle.Solid, BorderColor, 1, ButtonBorderStyle.Solid,
                    BackColor, 1, ButtonBorderStyle.Solid);
            }

            if (tabStatus == Status.Selected) { tabTextColor = Color.Black; }

            //Draws the top border color
            Rectangle drawRect = new Rectangle(new Point(TabRectangle.X, TabRectangle.Y), new Size(TabRectangle.Width, 3));
            g.FillRectangle(new SolidBrush(TopBorderColor), drawRect);

            int drawIcon = 0;
            if (tabPage.icon != null)
            {
                int iconSize = 16; drawIcon = 22;
                int y = (TabRectangle.Y + iconSize) / 2;
                Rectangle iconRectangle = new Rectangle(new Point(y, y), new Size(iconSize, iconSize));
                if (arrowEnable) { iconRectangle.X += 32; }
                g.DrawImage(tabPage.icon, iconRectangle);
            }

            //Draws the text of the tab
            StringFormat sf = new StringFormat(); sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Center;
            Rectangle tabTextRect = new Rectangle(TabRectangle.X + 6 + drawIcon, TabRectangle.Y, TabRectangle.Width - 6, TabRectangle.Height);
            g.DrawString(tabPage.Text, new Font("Segoe UI", 11), new SolidBrush(tabTextColor), tabTextRect, sf);

        }

        #endregion Paint Tab

        #region MouseEvents

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Invalidate();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            this.Invalidate();
        }

        #endregion MouseEvents

        #region Draw Arrow Button's

        private void DrawArrowButton(Graphics g)
        {
            if (!arrowEnable) return;
        }

        #endregion Draw Arrow Button's

        #region GetTabRect

        private List<Rectangle> GetTabRectangles()
        {
            List<Rectangle> returnList = new List<Rectangle>();
            int startX = 0;
            if (this.arrowEnable) startX = 32;

            foreach(MaterialTabPage page in basedTabControl.TabPages)
            {
                int tabIndex = basedTabControl.TabPages.IndexOf(page);
                Rectangle tabRect = new Rectangle(new Point((this._RectWidth * tabIndex) + startX, 0), new Size(this._RectWidth, 32));
                returnList.Add(tabRect);
            }

            return returnList;
        }

        #endregion GetTabRect

        #region GetTabByPoint

        private MaterialTabPage GetTabByPoint(Point point)
        {
            MaterialTabPage returnTabPage = null;
            foreach(Rectangle rect in tabRectangles)
            {
                if (rect.Contains(this.PointToClient(point)))
                { returnTabPage = (MaterialTabPage)basedTabControl.TabPages[tabRectangles.IndexOf(rect)]; }
            }
            return returnTabPage;
        }
        private MaterialTabPage GetTabByPoint(Rectangle rect)
        {
            MaterialTabPage returnTab = null;
            for (int i = 0; i < tabRectangles.Count; i++)
            {
                if (tabRectangles[i].Contains(rect))
                { returnTab = (MaterialTabPage)basedTabControl.TabPages[i]; }
            }
            return returnTab;
        }

        #endregion

        #region Drag'n'Drop



        #endregion Drag'n'Drop

        #region Dispose



        #endregion Dispose
    }
}
