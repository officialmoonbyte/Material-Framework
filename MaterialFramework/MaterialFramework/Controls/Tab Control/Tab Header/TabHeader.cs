using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Moonbyte.MaterialFramework.Controls
{

    #region Event Args

    public class TabDragOutEventArgs
    {
        public MaterialTabPage DraggedTab;
    }

    public class NewTabButtonClickedEventArgs
    {
        public MaterialTabPage NewTabPage;
    }
    
    #endregion Event Args

    public class TabHeader : UserControl
    {

        #region Vars

        MaterialTabControl basedTabControl;
        List<Rectangle> tabRectangles = new List<Rectangle>();

        enum Status { Default, Selected, MouseOver }

        bool _showArrowButton = false; // Internal value to keep track displaying the arrow button.
        bool _ArrowButtonEnable = false;
        bool _showCloseButt = false;
        bool _showNewTabButton = false;
        bool _limitStringLength = true;

        int _maxChars = 26;
        int _maxCharswithClose = 23;

        Color tabBackColor = Color.FromArgb(35, 35, 64);
        Color tabBorderColor = Color.FromArgb(75, 78, 101);
        Color topBorderColor = Color.FromArgb(35, 35, 64);

        Color s_tabBackColor = Color.FromArgb(249, 249, 250);
        Color s_topBorderColor = Color.FromArgb(10, 132, 255);
        Color s_TabBorderColor = Color.FromArgb(75, 78, 101);

        Color h_tabBackColor = Color.FromArgb(55, 57, 84);
        Color h_tabBorderColor = Color.FromArgb(75, 78, 101);
        Color h_topBorderColor = Color.FromArgb(164, 171, 182);

        int sizeX = 0;

        int _RectWidth = 230;

        public int ScrollInt
        {
            get { return sizeX; }
            set
            {
                sizeX = value;
                this.Invalidate();
            }
        }

        #endregion Vars

        #region Events

        //Custom event for triggering when the tab is dragged outside of bounds
        public event EventHandler<TabDragOutEventArgs> TabDragOut;
        public event EventHandler<NewTabButtonClickedEventArgs> NewTabButtonClick;
        public event EventHandler<EventArgs> TabDragComplete;

        #endregion Events

        #region Properties

        [Browsable(true)]
        [Description("Determines if it limits the string length to prevent visual bugs."), Category("Moonbyte Config")]
        public bool LimitTitleLength
        {
            get { return _limitStringLength; }
            set
            {
                _limitStringLength = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the max characters a tab can show. Do this if your text is off screen / wrong, only change this value for the close button!"), Category("Moonbyte Config")]
        public int MaxCharactersWithCloseButton
        {
            get { return _maxCharswithClose; }
            set
            {
                _maxCharswithClose = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the max characters a tab can show. Do this if your text is off screen / wrong"), Category("Moonbyte Config")]
        public int MaxCharacters
        {
            get { return _maxChars; }
            set
            {
                _maxChars = value;
                this.Invalidate();
            }
        }

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

        [Browsable(true)]
        [Description("Bool value to determine if we should draw the arrow button's"), Category("Moonbyte Config")]
        public bool EnableArrowButton
        {
            get { return _ArrowButtonEnable; }
            set
            {
                _ArrowButtonEnable = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Bool value to determine if we should draw the close button's"), Category("Moonbyte Config")]
        public bool EnableCloseButton
        {
            get { return _showCloseButt; }
            set
            {
                _showCloseButt = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Bool value to determine if we should draw the new tab button."), Category("Moonbyte Config")]
        public bool EnableNewTabButton
        {
            get { return _showNewTabButton; }
            set
            {
                _showNewTabButton = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Tab back color"), Category("Moonbyte Config")]
        public Color TabBackColor
        {
            get { return tabBackColor; }
            set
            {
                tabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Form default border color"), Category("Moonbyte Config")]
        public Color TabBorderColor
        {
            get { return tabBorderColor; }
            set
            {
                tabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The tab top border color"), Category("Moonbyte Config")]
        public Color TopBorderColor
        {
            get { return topBorderColor; }
            set
            {
                topBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Tab Seected Back Color"), Category("Moonbyte Config")]
        public Color TabSelectedBackColor
        {
            get { return s_tabBackColor; }
            set
            {
                s_tabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The selected tab border color"), Category("Moonbyte Config")]
        public Color TabSelectedBorderColor
        {
            get { return s_TabBorderColor; }
            set
            {
                s_TabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("The selected tab top color"), Category("Moonbyte Config")]
        public Color TabSelectedTopColor
        {
            get { return s_topBorderColor; }
            set
            {
                s_topBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Mouse Over Tab Back Color"), Category("Moonbyte Config")]
        public Color HoverTabBackColor
        {
            get { return h_tabBackColor; }
            set
            {
                h_tabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Mouse Over Tab border Color"), Category("Moonbyte Config")]
        public Color HoverTabBorderColor
        {
            get { return h_tabBorderColor; }
            set
            {
                h_tabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Mouse Over Tab Topbar Color"), Category("Moonbyte Config")]
        public Color HoverTabTopBarColor
        {
            get { return h_topBorderColor; }
            set
            {
                h_topBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion Properties

        #region Initialization

        public TabHeader()
        {
            if (ILogger.isInitialized != true) { ILogger.SetLoggingEvents(); }

            ILogger.AddToLog("INFO", "Initializing TabHeader");

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.DoubleBuffered = true;
            this.AllowDrop = true;
            this.Height = 32;

            ILogger.AddToLog("INFO", "Finished Initializing TabHeader");
            ILogger.AddWhiteSpace();
            ILogger.AddToLog("INFO", "Initialized Header Name : " + this.Name);
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
            if (_showNewTabButton) AllTabWidth += 32;
            if (AllTabWidth >= this.Width)
            { _showArrowButton = true; } else { _showArrowButton = false; sizeX = 0; }

            //Draw tabs
            tabRectangles = GetTabRectangles();
            foreach (Rectangle rect in tabRectangles)
            { PaintTab(rect, g); }

            Rectangle AddButtonRect = GetPlusRectangle();
            Color AddButtonBackColor = this.tabBackColor;
            Color AddButtonFontColor = Color.White;
            
            if (AddButtonRect.Contains(this.PointToClient(MousePosition)))
            { AddButtonBackColor = this.h_tabBackColor; }

            //Draw the plus button
            DrawPlusButton(g, AddButtonRect, AddButtonBackColor, AddButtonFontColor);

            //Draws the arrow buttons
            DrawArrowButton(g, GetLeftRectangle(), GetRightRectangle());

        }

        #endregion Override Paint

        #region Paint Tab

        #region Paint Tab Method

        #region Processing

        private void PaintTab(Rectangle TabRectangle, Graphics g)
        {
            Status tabStatus = Status.Default;

            MaterialTabPage tabPage = GetTabByPoint(TabRectangle);
            if (basedTabControl.SelectedTab == tabPage) { tabStatus = Status.Selected; }
            else if (TabRectangle.Contains(this.PointToClient(MousePosition)))
            { tabStatus = Status.MouseOver; }

            Color tabColor = Color.Red;
            Color p_TopBarColor = Color.Red;
            Color fontColor = Color.Red;

            if (tabStatus == Status.Default)
            {
                PaintTab(TabRectangle, tabPage, g, tabBackColor, tabBorderColor, topBorderColor, tabStatus);
                tabColor = tabBackColor; p_TopBarColor = topBorderColor; fontColor = Color.White;
            }
            else if (tabStatus == Status.MouseOver)
            {
                PaintTab(TabRectangle, tabPage, g, h_tabBackColor, h_tabBorderColor, h_topBorderColor, tabStatus);
                tabColor = h_tabBackColor; p_TopBarColor = h_topBorderColor; fontColor = Color.White;
            }
            else if (tabStatus == Status.Selected)
            {
                PaintTab(TabRectangle, tabPage, g, s_tabBackColor, s_TabBorderColor, s_topBorderColor, tabStatus);

                tabColor = s_tabBackColor; p_TopBarColor = s_topBorderColor; fontColor = Color.Black;
            }

            if (_showCloseButt)
            {
                int intY = TabRectangle.Height; Rectangle buttonRectangle = new Rectangle(new Point(TabRectangle.X + TabRectangle.Width - intY - 1, 3), new Size(intY, intY-3));
                if (buttonRectangle.Contains(this.PointToClient(MousePosition)))
                { DrawTabCloseButton(g, buttonRectangle, Color.FromArgb(255, 90, 90), Color.FromArgb(0, 0, 0));
                } else { DrawTabCloseButton(g, buttonRectangle, tabColor, fontColor); }
            }

            Rectangle drawRect = new Rectangle(new Point(TabRectangle.X, TabRectangle.Y), new Size(TabRectangle.Width, 3));
            DrawTopBar(g, drawRect, p_TopBarColor);
        }

        #endregion

        private void DrawTopBar(Graphics g, Rectangle TopBarRect, Color TopBarColor) { g.FillRectangle(new SolidBrush(TopBarColor), TopBarRect); }
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

            int drawIcon = 0;
            if (tabPage.icon != null)
            {
                int iconSize = 22; drawIcon = 22;
                Point IconLocation = new Point(TabRectangle.X + 4, TabRectangle.Y + 5);
                Rectangle iconRectangle = new Rectangle(IconLocation, new Size(iconSize, iconSize));
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(tabPage.icon, iconRectangle);
                g.InterpolationMode = InterpolationMode.Default;
            }

            string tabText = tabPage.Text;
            int maxChars = _maxChars;

            if (_limitStringLength)
            { 
                if (_showCloseButt == true) { maxChars = _maxCharswithClose; }

                if (tabText.Length > maxChars)
                {
                    tabText = tabText.Substring(0, Math.Min(tabText.Length, maxChars)) + "...";
                }
            }

            //Draws the text of the tab
            StringFormat sf = new StringFormat(); sf.Alignment = StringAlignment.Near; sf.LineAlignment = StringAlignment.Center;
            Rectangle tabTextRect = new Rectangle(TabRectangle.X + 6 + drawIcon, TabRectangle.Y, TabRectangle.Width - 6, TabRectangle.Height);
            g.DrawString(tabText, new Font("Segoe UI", 11), new SolidBrush(tabTextColor), tabTextRect, sf);

        }

        #endregion Paint Tab Method

        #region Draw Close Button

        private void DrawTabCloseButton(Graphics g, Rectangle ButtonRectangle, Color BackColor, Color FontColor)
        {
            g.FillRectangle(new SolidBrush(BackColor), ButtonRectangle);
            int margj = 16;
            Rectangle fontRect = new Rectangle(new Point(ButtonRectangle.X + ButtonRectangle.Width - (30 - margj) - 8, ButtonRectangle.Y + (ButtonRectangle.Height / 3) - 3), new Size(30 - margj, 30 - margj));

            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(FontColor, 1), new Point(fontRect.X, fontRect.Y), new Point(fontRect.X + fontRect.Width, fontRect.Y + fontRect.Height));
            g.DrawLine(new Pen(FontColor, 1), new Point(fontRect.X + fontRect.Width, fontRect.Y), new Point(fontRect.X, fontRect.Y + fontRect.Height));
            g.SmoothingMode = SmoothingMode.Default;
        }

        #endregion Draw Close Button

        #endregion Paint Tab

        #region Draw Plus Button

        private Rectangle GetPlusRectangle()
        {
            int start = _RectWidth * this.BasedTabControl.TabPages.Count -+ this.sizeX;
            if (_showNewTabButton && _showArrowButton)
            {
                return new Rectangle(new Point(start + 30, 0), new Size(32, 32));
            }
            else
            {
                return new Rectangle(new Point(start - 1, 0), new Size(32, 32));
            }
        }
        private void DrawPlusButton(Graphics g, Rectangle PlusRect, Color BackColor, Color FontColor)
        {
            if (_showNewTabButton)
            {
                g.FillRectangle(new SolidBrush(BackColor), PlusRect);
                g.DrawLine(new Pen(new SolidBrush(FontColor), 1),
                    new Point(PlusRect.X + (PlusRect.Width / 2), PlusRect.Y + (PlusRect.Height / 3)),
                    new Point(PlusRect.X + (PlusRect.Width / 2), PlusRect.Y + (PlusRect.Height - (PlusRect.Height / 3))));

                g.DrawLine(new Pen(new SolidBrush(FontColor), 1),
                    new Point(PlusRect.X + (PlusRect.Width / 3), PlusRect.Height / 2),
                    new Point(PlusRect.X + (PlusRect.Width - (PlusRect.Width / 3)), PlusRect.Y + (PlusRect.Height / 2)));
            }
        }

        #endregion Draw Plus Button

        #region MouseEvents

        #region OnMouseDown

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Console.WriteLine(1);
            MaterialTabPage tp = GetTabByPoint(MousePosition);
            MaterialTabPage currentSelectedTab = (MaterialTabPage)basedTabControl.SelectedTab;
            Console.WriteLine(2);
            Rectangle leftArrow = GetLeftRectangle();
            Rectangle rightArrow = GetRightRectangle();
            Rectangle plusRectangle = GetPlusRectangle();
            Console.WriteLine(3);
            bool continueMethod = true;
            Console.WriteLine(4);
            if (_showNewTabButton)
            {
                if (plusRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    MaterialTabPage NewTab = new MaterialTabPage();

                    NewTab.Text = "New Tab";

                    this.basedTabControl.TabPages.Add(NewTab);
                    this.basedTabControl.SelectedTab = NewTab;

                    continueMethod = false;
                    NewTabButtonClick?.Invoke(this, new NewTabButtonClickedEventArgs { NewTabPage = NewTab });
                }
            }
            Console.WriteLine(5);
            if (tp != null && continueMethod)
            {
                Console.WriteLine(6);
                this.tabRectangles = this.GetTabRectangles();
                int tabIndex = this.basedTabControl.TabPages.IndexOf(tp); Rectangle TabRectangle = this.tabRectangles[tabIndex];
                int intY = TabRectangle.Height; Rectangle closeButtonRect = new Rectangle(new Point(TabRectangle.X + TabRectangle.Width - intY - 1, 3), new Size(intY, intY - 3));

                if (leftArrow.Contains(this.PointToClient(MousePosition)))
                { ScrollLeft(); }
                else if (rightArrow.Contains(this.PointToClient(MousePosition)))
                { ScrollRight(); }
                else if (closeButtonRect.Contains(this.PointToClient(MousePosition)))
                {
                    Console.WriteLine(7);
                    if (this.basedTabControl.SelectedTab == tp && this.basedTabControl.TabPages.Count != 1)
                    {
                        if (this.basedTabControl.TabPages.Count - 1 == tabIndex)
                        {
                            this.basedTabControl.SelectedTab = this.basedTabControl.TabPages[tabIndex - 1];
                        }
                        else
                        {
                            this.basedTabControl.SelectedTab = this.basedTabControl.TabPages[tabIndex + 1];
                        }
                    }
                    else { basedTabControl.SelectedTab = currentSelectedTab; }

                    this.basedTabControl.TabPages.Remove(tp);
                    tp.Dispose();
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            MaterialTabPage tp = GetTabByPoint(MousePosition);
            if (tp != null)
            {
                this.tabRectangles = this.GetTabRectangles();
                int tabIndex = this.basedTabControl.TabPages.IndexOf(tp); Rectangle TabRectangle = this.tabRectangles[tabIndex];
                int intY = TabRectangle.Height; Rectangle closeButtonRect = new Rectangle(new Point(TabRectangle.X + TabRectangle.Width - intY - 1, 3), new Size(intY, intY - 3));

                this.basedTabControl.SelectedTab = tp;

                if (!closeButtonRect.Contains(this.PointToClient(MousePosition)))
                {
                    this.DoDragDrop(tp, DragDropEffects.All);
                }
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate(); 

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();

            base.OnMouseLeave(e);
        }

        #endregion OnMouseDown

        #endregion MouseEvents

        #region Draw Arrow Button's

        private void DrawArrowButton(Graphics g, Rectangle leftRectangle, Rectangle rightRectangle)
        {
            if (_ArrowButtonEnable && _showArrowButton)
            {
                Color buttonBorderColor = Color.FromArgb(164, 171, 182);
                Color buttonBackColor = Color.FromArgb(35, 35, 64);
                Color buttonFontColor = Color.FromArgb(250, 250, 250);

                //Draw BackColor
                g.FillRectangle(new SolidBrush(buttonBackColor), leftRectangle);
                g.FillRectangle(new SolidBrush(buttonBackColor), rightRectangle);

                //Draw the border's
                g.DrawRectangle(new Pen(new SolidBrush(buttonBorderColor), 1), leftRectangle);
                g.DrawRectangle(new Pen(new SolidBrush(buttonBorderColor), 1), rightRectangle);

                //Draw rect hiding border
                if (this.sizeX == 0)
                {
                    Rectangle rect = new Rectangle(new Point(leftRectangle.X + leftRectangle.Width, 0), new Size(1, this.Height));
                    g.FillRectangle(new SolidBrush(buttonBackColor), rect);
                }
                int AllTabWidth = this.basedTabControl.TabPages.Count * this.TabPageWidth;
                if ((AllTabWidth - ScrollInt) <= this.Width)
                {
                    Rectangle rect = new Rectangle(new Point(rightRectangle.X, rightRectangle.Y), new Size(1, this.Height + 1));
                    g.FillRectangle(new SolidBrush(buttonBackColor), rect);
                }

                Rectangle leftArrowRect = new Rectangle(new Point(leftRectangle.X + 2, leftRectangle.Y + 2), new Size(leftRectangle.Width - 3, leftRectangle.Height - 3));
                Rectangle rightArrowRect = new Rectangle(new Point(rightRectangle.X + 2, rightRectangle.Y + 2), new Size(leftRectangle.Width - 3, leftRectangle.Height - 3));

                g.SmoothingMode = SmoothingMode.HighQuality;

                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 2),
                    new Point(leftArrowRect.X + (leftArrowRect.Width / 3) - 4, leftArrowRect.Height / 2),
                    new Point(leftArrowRect.Width - (leftArrowRect.Width / 3), leftArrowRect.Height / 3 - 3));
                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 2),
                    new Point(leftArrowRect.X + (leftArrowRect.Width / 3) - 4, leftArrowRect.Height / 2),
                    new Point(leftArrowRect.Width - (leftArrowRect.Width / 3), leftArrowRect.Height - (leftArrowRect.Height / 3) + 3));

                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 2),
                    new Point(rightArrowRect.X + (rightArrowRect.Width - (rightArrowRect.Width / 3)), rightArrowRect.Y + (rightArrowRect.Height / 2)),
                    new Point(rightArrowRect.X + (rightArrowRect.Width / 3) - 4, rightArrowRect.Y + (rightArrowRect.Height / 3) - 3));
                g.DrawLine(new Pen(new SolidBrush(Color.FromArgb(255, 255, 255)), 2),
                    new Point(rightArrowRect.X + (rightArrowRect.Width - (rightArrowRect.Width / 3)), rightArrowRect.Y + (rightArrowRect.Height / 2)),
                    new Point(rightArrowRect.X + (rightArrowRect.Width / 3) - 4, rightArrowRect.Y + (rightArrowRect.Height - (rightArrowRect.Height / 3)) + 3));


                g.SmoothingMode = SmoothingMode.None;
            }
        }

        #endregion Draw Arrow Button's

        #region GetButtonRects

        private Rectangle GetLeftRectangle()
        { return new Rectangle(new Point(0, -1), new Size(32, 33)); }
        private Rectangle GetRightRectangle()
        { return new Rectangle(new Point(this.Width - 33, -1), new Size(32, 33)); }

        #endregion GetButtonRects

        #region Resize

        protected override void OnResize(EventArgs e)
        { base.OnResize(e); this.Invalidate(); }

        #endregion Resize

        #region GetTabRect

        private List<Rectangle> GetTabRectangles()
        {
            List<Rectangle> returnList = new List<Rectangle>();
            int arrowModifier = 0;

            //Set sizeX based if arrowEnable
            if (this._showArrowButton && this._ArrowButtonEnable) arrowModifier = 31;

            foreach (MaterialTabPage page in basedTabControl.TabPages)
            {
                int tabIndex = basedTabControl.TabPages.IndexOf(page);
                Rectangle tabRect = new Rectangle(new Point((this._RectWidth * tabIndex) +- sizeX + arrowModifier, 0), new Size(this._RectWidth, 32));
                returnList.Add(tabRect);
            }

            return returnList;
        }

        #endregion GetTabRect

        #region GetTabByPoint

        private MaterialTabPage GetTabByPoint(Point point)
        {
            MaterialTabPage returnTabPage = null;
            foreach (Rectangle rect in tabRectangles)
            {
                if (rect.Contains(this.PointToClient(point)))
                { returnTabPage = (MaterialTabPage)basedTabControl.TabPages[tabRectangles.IndexOf(rect)]; }
            }
            return returnTabPage;
        }
        private MaterialTabPage GetTabByPoint(Point pt, bool isDragnDrop)
        {
            MaterialTabPage tp = null; Point pt1 = this.PointToClient(pt);
            tabRectangles = GetTabRectangles();
            for (int i = 0; i < this.basedTabControl.TabPages.Count; i++)
            {
                if (tabRectangles[i].Contains(pt1))
                {
                    tp = (MaterialTabPage)this.basedTabControl.TabPages[i];
                    break;
                }
            }

            return tp;

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

        MaterialTabPage preDraggedTab;
        /// <summary>
        /// Occures when there is a Drag'n'Drop event over the control
        /// </summary>
        protected override void OnDragOver(DragEventArgs drgevent)
        {

            //Triggers base event
            base.OnDragOver(drgevent);

            //Get position of the Drag'n'Drop
            Point pt = new Point(drgevent.X, drgevent.Y);

            //Get the hover tab from TabPoint
            MaterialTabPage hoverTab = GetTabByPoint(pt, true);

            //Checks if the hover tab is is null
            if (hoverTab != null)
            {
                preDraggedTab = hoverTab;
                //Set drag events
                drgevent.Effect = DragDropEffects.Move;
                var dragTab = drgevent.Data.GetData(typeof(MaterialTabPage));

                //Setting index for Item Drag and Drop Location
                int item_drag_index = FindIndex((MaterialTabPage)dragTab);
                int drop_Location_Index = FindIndex(hoverTab);

                //Setting the PreDraggedTab
                preDraggedTab = (MaterialTabPage)dragTab;

                //Making sure the index is not equal to the origional index
                if (item_drag_index != drop_Location_Index)
                {
                    //Initializing the Array
                    ArrayList pages = new ArrayList();

                    //For each tab page
                    for (int i = 0; i < this.basedTabControl.TabPages.Count; i++)
                    {
                        if (i != item_drag_index) pages.Add(this.basedTabControl.TabPages[i]);
                    }

                    //Insert page into the Drop iNDEX
                    pages.Insert(drop_Location_Index, (MaterialTabPage)dragTab);

                    //Clearing TabPages from the tab control
                    this.basedTabControl.TabPages.Clear();

                    //Adding tab pages to the BasedTabControl
                    this.basedTabControl.TabPages.AddRange((MaterialTabPage[])pages.ToArray(typeof(MaterialTabPage)));

                    //Selecting the DragTab
                    this.basedTabControl.SelectedTab = (MaterialTabPage)dragTab;

                    //Triggers event
                    TabDragComplete?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                //Setting DragDropEffects to none
                drgevent.Effect = DragDropEffects.None;
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
        }

        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            base.OnQueryContinueDrag(qcdevent);

            try
            {
                //If this control does not contain the mouse position, trigger the event
                if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)) && qcdevent.KeyState != 1)
                {
                    //Checks if the tab is in the parent or in a diffrent tab control
                    if (preDraggedTab.Parent == this.BasedTabControl)
                    {
                        //Trigger the event
                        this.TabDragOut?.Invoke(this, new TabDragOutEventArgs { DraggedTab = preDraggedTab });
                    }
                }
            }
            catch { }
        }

        #endregion Drag'n'Drop

        #region FindIndex

        /// <summary>
        /// Gets the index of the Tab(arg0)
        /// </summary>
        private int FindIndex(TabPage tab)
        {
            //For each Tab, see if index is equal to I
            for (int i = 0; i < this.basedTabControl.TabPages.Count; i++)
            {
                //Checks if TabPages is equal to Tab
                if (this.basedTabControl.TabPages[i] == tab)
                {
                    //Returns the int I
                    return i;
                }
            }

            //Returns -1 as error
            return -1;
        }

        #endregion FindIndex

        #region MouseOverRect

        public bool MouseOverRect()
        {
            tabRectangles = this.GetTabRectangles();
            if (EnableNewTabButton) { tabRectangles.Add(GetPlusRectangle()); }
            foreach (Rectangle rect in tabRectangles) { if (rect.Contains(this.PointToClient(MousePosition))) { return true; } }
            return false;
        }

        #endregion 

        #region ScrollLeft

        //Vars for scrolling
        int endScrollInt = 0;
        int rightTimerTickStep = 0;
        int leftTimerTickStep = 0;

        bool isScrolling = false;

        public void ScrollLeft()
        {
            //Returns if scrolling right
            if (isScrolling) { return; }

            //Cancel if there is enough room
            int AllTabWidth = this.basedTabControl.TabPages.Count * this.TabPageWidth;
            if (_showNewTabButton) AllTabWidth += 32;
            if (ScrollInt == 0) { return; }

            //Initializing the timer
            Timer leftTimer = new Timer();
            leftTimer.Tick += ((obj, args) =>
            {

                //Check if animation is equal to 1.5 seconds long
                if (leftTimerTickStep == 30)
                {
                    //Set scroll int to EndScrollInt
                    ScrollInt = endScrollInt;

                    //Stop the timer
                    leftTimer.Stop();

                    //Set is scrolling to false
                    isScrolling = false;
                }
                else
                {

                    //Getting the Temp scroll int
                    int tmpScrollInt = ((31 - leftTimerTickStep) / 6) * ((31 - leftTimerTickStep) / 6);

                    //Check if the Scroll Int will be higher then the end scroll int
                    if ((tmpScrollInt - ScrollInt) == endScrollInt)
                    {
                        //Set the scroll int to the end scroll int
                        ScrollInt = endScrollInt;

                        //Stop the timer
                        leftTimer.Stop();

                        //Set is scroling to false
                        isScrolling = false;
                    }
                    else
                    {
                        //Add to the scroll int
                        ScrollInt -= Convert.ToInt32(tmpScrollInt);
                    }
                }

                //Increase TimerTickStep by one.
                leftTimerTickStep++;

            });

            //Setting based values
            endScrollInt = ScrollInt - this.TabPageWidth + 1;
            leftTimerTickStep = 0;
            isScrolling = true;

            //Setting the timer to 60Hz refresh rate
            leftTimer.Interval = 1;

            //Starting the timer
            leftTimer.Start();
        }

        #endregion ScrollLeft

        #region ScrollRight

        public void ScrollRight()
        {
            //Returns if scrolling right
            if (isScrolling) { return; }
            //Cancel if there is enough room
            int AllTabWidth = this.basedTabControl.TabPages.Count * this.TabPageWidth;
            if (_showNewTabButton) AllTabWidth += 32;
            if ((AllTabWidth - ScrollInt) <= this.Width) { return; }
            //Initializing the timer
            Timer rightTimer = new Timer();
            rightTimer.Tick += ((obj, args) =>
            {

                //Check if animation is equal to 1.5 seconds long
                if (rightTimerTickStep == 30)
                {
                    //Set scroll int to EndScrollInt
                    ScrollInt = endScrollInt;

                    //Stop the timer
                    rightTimer.Stop();

                    //Set is scrolling to false
                    isScrolling = false;
                }
                else
                {

                    //Getting the Temp scroll int
                    int tmpScrollInt = ((31 - rightTimerTickStep) / 6) * ((31 - rightTimerTickStep) / 6);

                    //Check if the Scroll Int will be higher then the end scroll int
                    if ((tmpScrollInt + ScrollInt) == endScrollInt)
                    {
                        //Set the scroll int to the end scroll int
                        ScrollInt = endScrollInt;

                        //Stop the timer
                        rightTimer.Stop();

                        //Set is scroling to false
                        isScrolling = false;
                    }
                    else
                    {
                        //Add to the scroll int
                        ScrollInt += Convert.ToInt32(tmpScrollInt);
                    }
                }

                //Increase TimerTickStep by one.
                rightTimerTickStep++;

            });

            //Setting based values
            endScrollInt = ScrollInt + this.TabPageWidth - 1;
            rightTimerTickStep = 0;
            isScrolling = true;

            //Setting the timer to 60Hz refresh rate
            rightTimer.Interval = 1;

            //Starting the timer
            rightTimer.Start();
        }

        #endregion ScrollRight

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            foreach(Control c in this.Controls)
            { c.Dispose(); }

            base.Dispose(disposing);
        }

        #endregion Dispose
    }
}
