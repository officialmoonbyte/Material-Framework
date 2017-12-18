﻿using IndieGoat.MaterialFramework.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
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
    /// Used with the generic TabPage
    /// </summary>
    public class GenericTabHeader : UserControl
    {

        #region Vars

        //All customize color
        private Color _BackColor = Color.White;
        private Color _TextColor = Color.FromArgb(200, 200, 200);
        private Color _TabBackColor = Color.FromArgb(35, 35, 64);
        private Color _TabBorderColor = Color.FromArgb(75, 78, 101);
        private Color _TabTopBarColor = Color.FromArgb(35, 35, 64);

        //Selected Color
        private Color s_TabBackColor = Color.FromArgb(249, 249, 250);
        private Color s_TextColor = Color.FromArgb(100, 100, 100);
        private Color s_TabBorderColor = Color.FromArgb(75, 78, 101);
        private Color s_TabTopBarColor = Color.FromArgb(10, 132, 255);

        //Hover Color
        private Color h_TabBackColor = Color.FromArgb(55, 57, 84);
        private Color h_TextColor = Color.FromArgb(200, 200, 200);
        private Color h_TabBorderColor = Color.FromArgb(75, 78, 101);
        private Color h_TabTopBarColor = Color.FromArgb(164, 171, 182);

        //Close Button Color
        private Color _CloseButtonColor = Color.FromArgb(0, 0, 0);
        private bool _EnableCloseButton = false;

        //All bools for customization
        private bool _ShowTabTopBarColor = true;

        //Header for the tab and tab indicator height 
        private const int TAB_HEADER_PADDING = 24;
        private const int TAB_INDICATOR_HEIGHT = 2;

        //Rectangle for all of the tabs
        private List<Rectangle> _TabRects = new List<Rectangle>();

        //Set the Previous Selected Index int
        private int _previousSelectedTabIndex;

        //Base tab control
        MaterialTabControl _basedTabControl;

        //DragDrop predropTab
        TabPage preDraggedTab;

        //Scroll Int
        public int scrollInt = 0;

        //Width of the Rect
        int rect_Width = 230;

        int StartX = 0;

        #endregion

        #region Event's

        //Custom event for triggering when the tab is dragged outside of bounds
        public event EventHandler<GenericTabDragOutArgs> TabDragOut;

        #endregion

        #region Properties

        #region Based TabControl

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("IndieGoat Control Settings")]
        public MaterialTabControl BasedTabControl
        {
            get
            {
                return _basedTabControl;
            }
            set
            {
                //Set the BasedTabControl
                _basedTabControl = value;

                //Checks if the based tab control is set to null
                if (_basedTabControl == null) return;

                // Setting tab control event's //
                _basedTabControl.Deselected += ((sender, args) =>
                {
                    this.Invalidate();
                });
                _basedTabControl.ControlAdded += delegate
                {
                    this.Invalidate();
                };
                _basedTabControl.ControlRemoved += delegate
                {
                    this.Invalidate();
                };
            }
        }

        #endregion

        #region Color Properties

        #region Normal

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Normal Color's")]
        public override Color BackColor
        {
            get { return _BackColor; }
            set
            {
                _BackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Normal Color's")]
        public Color TabBackColor
        {
            get { return _TabBackColor; }
            set
            {
                _TabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Normal Color's")]
        public Color FontColor
        {
            get { return _TextColor; }
            set
            {
                _TextColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Normal Color's")]
        public Color TabBorderColor
        {
            get { return _TabBorderColor; }
            set
            {
                _TabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Normal Color's")]
        public Color TopBarColor
        {
            get { return _TabTopBarColor; }
            set
            {
                _TabTopBarColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Selected

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Selected Color")]
        public Color Selected_BackColor
        {
            get { return s_TabBackColor; }
            set
            {
                s_TabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Selected Color")]
        public Color Selected_FontColor
        {
            get { return s_TextColor; }
            set
            {
                s_TextColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Selected Color")]
        public Color Selected_BorderColor
        {
            get { return s_TabBorderColor; }
            set
            {
                s_TabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Selected Color")]
        public Color Selected_TopBarColor
        {
            get { return s_TabTopBarColor; }
            set
            {
                s_TabTopBarColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Hover

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Hover Color")]
        public Color Hover_BackColor
        {
            get { return h_TabBackColor; }
            set
            {
                h_TabBackColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Hover Color")]
        public Color Hover_FontColor
        {
            get { return h_TextColor; }
            set
            {
                h_TextColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Hover Color")]
        public Color Hover_BorderColor
        {
            get { return h_TabBorderColor; }
            set
            {
                h_TabBorderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Hover Color")]
        public Color Hover_TopBarColor
        {
            get { return h_TabTopBarColor; }
            set
            {
                h_TabTopBarColor = value;
                this.Invalidate();
            }
        }

        #endregion

        #region Close Button

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Close Button Color(s)")]
        public Color CloseButtonHoverColor
        {
            get { return _CloseButtonColor; }
            set
            {
                _CloseButtonColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true), EditorBrowsable(EditorBrowsableState.Always), Category("Close Button Color(s)")]
        public bool ShowCloseButton
        {
            get { return _EnableCloseButton; }
            set
            {
                _EnableCloseButton = value;
                this.Invalidate();
            }
        }

        #endregion

        public int ScrollInt
        {
            get { return scrollInt; }
            set
            {
                scrollInt = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region Required / Startup

        /// <summary>
        /// Start of the GenericTabHeader initialization process
        /// </summary>
        public GenericTabHeader()
        {
            // MaterialTabHeader //
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.DoubleBuffered = true;
            this.AllowDrop = true;
            this.Height = 32;
        }

        #endregion

        #region Override Paint

        /// <summary>
        /// Painting the control
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            //Vars used during the paint process
            Graphics g = e.Graphics;

            //Setting graphics method
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            //Draw the background
            g.Clear(_BackColor);

            //Checks if the base control is null, if it is returns.
            if (_basedTabControl == null) return;

            //Reinvalidating the TabRectangles
            _TabRects = new List<Rectangle>();

            //Initializing the StringFormat for drawing the string
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Near;
            stringFormat.LineAlignment = StringAlignment.Center;

            //Initializing the Font of the string
            Font tabPageFont = new Font("Segoe UI", 11);

            //Check if we should draw the Arrow Buttons
            int AllTabWidth = _basedTabControl.TabPages.Count * rect_Width;
            if (AllTabWidth >= this.Width)
            {
                //If StartX of Rectangle is equal to 0, set StartX to 32
                if (StartX == 0)
                {
                    StartX = 32;
                }
            }
            else
            {
                //Set StartX to 0 since we no longer need that button
                StartX = 0;
            }

            //Initializing Left and Right move buttons
            Rectangle leftMoveButton = new Rectangle(0, 0, 32, 32);
            Rectangle rightMoveButton = new Rectangle(this.Width - 32, 0, 32, 32);

            // Tab Headers //
            for (int i = 0; i < _basedTabControl.TabPages.Count; i++)
            {

                //Modifier for the text
                int textModifier = 6;

                //Icon box size
                int iconSize = 18;

                //Getting the rectangle of the Tab
                Point tabRectLocation = new Point(rect_Width * i - (i + scrollInt) + StartX, 0);
                Size tabRectSize = new Size(rect_Width, 32);
                Rectangle tmpRectangle = new Rectangle(tabRectLocation, tabRectSize);

                //Initializing the CloseButton Rectangle
                Rectangle CloseButtonRectangle = new Rectangle(tmpRectangle.X + tmpRectangle.Width - 32, tmpRectangle.Y, 32, 32);

                //Change FontRectangle position based on Icon
                MaterialTabPage tmpTabPage = (MaterialTabPage)_basedTabControl.TabPages[i];
                if (tmpTabPage.icon != null) textModifier += iconSize;

                //Font Rectangle
                Rectangle fontRect = new Rectangle(tmpRectangle.X + textModifier, tmpRectangle.Y,
                    tmpRectangle.Width - textModifier, tmpRectangle.Height);

                //Getting the tab page text
                string tabText = _basedTabControl.TabPages[i].Text;

                //Check if the tab is selected
                bool tabSelected = false;
                if (_basedTabControl.SelectedTab == _basedTabControl.TabPages[i]) tabSelected = true;

                //Close Button Hover Over
                bool closeHover = false;
                if (CloseButtonRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    closeHover = true;
                }

                //Draw tab background
                if (tabSelected)
                {
                    //Initializing the TabPageBrush
                    Brush tabStringBrush = new SolidBrush(s_TextColor);

                    // TabBackground //
                    g.FillRectangle(new SolidBrush(s_TabBackColor), tmpRectangle);

                    //Draw the tab border
                    ControlPaint.DrawBorder(g, tmpRectangle, s_TabBorderColor, 1, ButtonBorderStyle.Solid,
                        s_TabBorderColor, 1, ButtonBorderStyle.Solid,
                        s_TabBorderColor, 1, ButtonBorderStyle.Solid,
                        s_TabBorderColor, 0, ButtonBorderStyle.Solid);

                    //Drawing CloseButton
                    if (_EnableCloseButton) { DrawCloseButton(CloseButtonRectangle, g, closeHover); }

                    // TopBar //
                    Rectangle topbarRect = new Rectangle(tmpRectangle.X, tmpRectangle.Y, tmpRectangle.Width, 3);
                    g.FillRectangle(new SolidBrush(s_TabTopBarColor), topbarRect);

                    //Setting alias for text
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Font //
                    g.DrawString(tabText, tabPageFont, tabStringBrush, fontRect, stringFormat);

                    //Resetting smoothing mode
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

                }
                else if (StartX != 0 && leftMoveButton.Contains(this.PointToClient(MousePosition)) || rightMoveButton.Contains(this.PointToClient(MousePosition)))
                {
                    //Draw the default colors, will be oragnized later

                    //Initializing the TabPageBrush
                    Brush tabStringBrush = new SolidBrush(_TextColor);

                    // TabBackground //
                    g.FillRectangle(new SolidBrush(_TabBackColor), tmpRectangle);

                    //Draw the tab border
                    ControlPaint.DrawBorder(g, tmpRectangle, _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 0, ButtonBorderStyle.Solid);

                    //Drawing close button rectangle
                    if (_EnableCloseButton) { DrawCloseButton(CloseButtonRectangle, g, closeHover); }

                    // TopBar //
                    Rectangle topbarRect = new Rectangle(tmpRectangle.X, tmpRectangle.Y, tmpRectangle.Width, 3);
                    g.FillRectangle(new SolidBrush(_TabTopBarColor), topbarRect);

                    //Setting alias for text
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Font //
                    g.DrawString(tabText, tabPageFont, tabStringBrush, fontRect, stringFormat);

                    //Resetting smoothing mode
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }
                else if (tmpRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    //Initializng the TabPageBrush
                    Brush tabStringBrush = new SolidBrush(h_TextColor);

                    // Tab Background //
                    g.FillRectangle(new SolidBrush(h_TabBackColor), tmpRectangle);

                    //Draw the tab border
                    ControlPaint.DrawBorder(g, tmpRectangle, h_TabBackColor, 1, ButtonBorderStyle.Solid,
                        h_TabBorderColor, 1, ButtonBorderStyle.Solid,
                        h_TabBorderColor, 1, ButtonBorderStyle.Solid,
                        h_TabBorderColor, 0, ButtonBorderStyle.Solid);

                    //Draw close button rectangle
                    if (_EnableCloseButton) { DrawCloseButton(CloseButtonRectangle, g, closeHover); }

                    // TopBar //
                    Rectangle topBarRect = new Rectangle(tmpRectangle.X, tmpRectangle.Y, tmpRectangle.Width, 3);
                    g.FillRectangle(new SolidBrush(h_TabTopBarColor), topBarRect);

                    //Setting alias for text
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Font //
                    g.DrawString(tabText, tabPageFont, tabStringBrush, fontRect, stringFormat);

                    //Resetting smoothing mode
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }
                else
                {
                    //Initializing the TabPageBrush
                    Brush tabStringBrush = new SolidBrush(_TextColor);

                    // TabBackground //
                    g.FillRectangle(new SolidBrush(_TabBackColor), tmpRectangle);

                    //Draw the tab border
                    ControlPaint.DrawBorder(g, tmpRectangle, _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 1, ButtonBorderStyle.Solid,
                        _TabBorderColor, 0, ButtonBorderStyle.Solid);

                    // TopBar //
                    Rectangle topbarRect = new Rectangle(tmpRectangle.X, tmpRectangle.Y, tmpRectangle.Width, 3);
                    g.FillRectangle(new SolidBrush(_TabTopBarColor), topbarRect);

                    //Drawing CloseButton
                    if (_EnableCloseButton) { DrawCloseButton(CloseButtonRectangle, g, closeHover); }

                    //Setting alias for text
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                    // Font //
                    g.DrawString(tabText, tabPageFont, tabStringBrush, fontRect, stringFormat);

                    //Resetting smoothing mode
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
                }

                //Add the rectangle to the TabRects array
                _TabRects.Add(tmpRectangle);
            }

            //Draw the MoveButtons
            if (StartX != 0)
            {

                //Initializing the StringFormat for drawing the string
                StringFormat moveStringFormat = new StringFormat();
                moveStringFormat.Alignment = StringAlignment.Center;
                moveStringFormat.LineAlignment = StringAlignment.Center;

                //Initializing the Font of the string
                Font moveButtonFont = new Font("Segoe UI", 18);

                //Draw left move button
                g.FillRectangle(new SolidBrush(_TabBackColor), leftMoveButton);
                ControlPaint.DrawBorder(g, leftMoveButton, _TabBorderColor, ButtonBorderStyle.Solid);
                g.DrawString("<", moveButtonFont, new SolidBrush(_TextColor), leftMoveButton, moveStringFormat);

                //Draw right move button
                g.FillRectangle(new SolidBrush(_TabBackColor), rightMoveButton);
                ControlPaint.DrawBorder(g, rightMoveButton, _TabBorderColor, ButtonBorderStyle.Solid);
                g.DrawString(">", moveButtonFont, new SolidBrush(_TextColor), rightMoveButton, moveStringFormat);
            }

        }

        #endregion

        #region Drag'n'Drop

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
            TabPage hoverTab = GetTabByPoint(this.PointToClient(pt));

            //Checks if the hover tab is is null
            if (hoverTab != null)
            {
                //Set drag events
                drgevent.Effect = DragDropEffects.Move;

                //Set the DragTab
                TabPage dragTab = (TabPage)drgevent.Data.GetData(typeof(TabPage));

                //Setting index for Item Drag and Drop Location
                int item_drag_index = FindIndex(dragTab);
                int drop_Location_Index = FindIndex(hoverTab);

                //Setting the PreDraggedTab
                preDraggedTab = dragTab;

                //Making sure the index is not equal to the origional index
                if (item_drag_index != drop_Location_Index)
                {
                    //Initializing the Array
                    ArrayList pages = new ArrayList();

                    //For each tab page
                    for (int i = 0; i < _basedTabControl.TabPages.Count; i++)
                    {
                        if (i != item_drag_index) pages.Add(_basedTabControl.TabPages[i]);
                    }

                    //Insert page into the Drop iNDEX
                    pages.Insert(drop_Location_Index, dragTab);

                    //Clearing TabPages from the tab control
                    _basedTabControl.TabPages.Clear();

                    //Adding tab pages to the BasedTabControl
                    _basedTabControl.TabPages.AddRange((TabPage[])pages.ToArray(typeof(TabPage)));

                    //Selecting the DragTab
                    _basedTabControl.SelectedTab = dragTab;
                }
            }
            else
            {
                //Setting DragDropEffects to none
                drgevent.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Processes if the drag has occured out of the control
        /// </summary>
        protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
        {
            base.OnQueryContinueDrag(qcdevent);

            try
            {
                //If this control does not contain the mouse position, trigger the event
                if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)) && qcdevent.KeyState != 1)
                {
                    //Trigger the event
                    TabDragOut?.Invoke(this, new GenericTabDragOutArgs { DraggedTab = preDraggedTab });
                }
            }
            catch { }
        }

        #region Drag'n'Drop Method's

        /// <summary>
        /// Get the TabPage based on the Point of the mouse
        /// </summary>
        private TabPage GetTabByPoint(Point mousePoint)
        {
            //The return value of this method
            TabPage returnTabPage = null;

            //For loop for each tab
            for (int i = 0; i < _TabRects.Count(); i++)
            {
                //Checks if the point is located in the rectangle
                if (_TabRects[i].Contains(mousePoint))
                {
                    //Set the tab page to the rectangle tab page
                    returnTabPage = _basedTabControl.TabPages[i];

                    //Exit out of the for loop
                    break;
                }
            }

            //Returns the method
            return returnTabPage;
        }

        /// <summary>
        /// Gets the index of the Tab(arg0)
        /// </summary>
        private int FindIndex(TabPage tab)
        {
            //For each Tab, see if index is equal to I
            for (int i = 0; i < _basedTabControl.TabPages.Count; i++)
            {
                //Checks if TabPages is equal to Tab
                if (_basedTabControl.TabPages[i] == tab)
                {
                    //Returns the int I
                    return i;
                }
            }

            //Returns -1 as error
            return -1;
        }

        #endregion

        #endregion

        #region Mouse Events / Drag'n'Drop Mouse Events

        //Saved hover index
        int hoverOverIndex = 0;

        /// <summary>
        /// Detects if we need to redraw or not.
        /// 
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            //Invalidate the control
            this.Invalidate();
        }

        /// <summary>
        /// Invalidates the control
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            //Invalidate the control
            this.Invalidate();
        }

        /// <summary>
        /// Used to show a new tab / drag event
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            //Private vars
            TabPage tp = GetTabByPoint(new Point(e.X, e.Y));

            //Handle Left and Right buttons
            if (StartX != 0)
            {
                //Rectangle for the LeftButton
                Rectangle leftButtonRect = new Rectangle(0, 0, 32, 32);
                Rectangle rightButtonRect = new Rectangle(this.Width - 32, 0, 32, 32);

                //Check if the mouse is in the rectangle for the left button
                if (leftButtonRect.Contains(this.PointToClient(MousePosition)))
                {
                    //Activate the LeftScrollingMethod
                    ScrollLeft();

                    return;
                }

                //Check if the mouse is in the rectangle for the Right Button
                if (rightButtonRect.Contains(this.PointToClient(MousePosition)))
                {
                    //Activate the RightScrollingMethod
                    ScrollRight();

                    return;
                }
            }

            //Check if you click the close button
            if (_EnableCloseButton)
            {
                //Getting the based rectangle
                Rectangle tp_rect = GetTabRect(tp);

                Console.WriteLine(tp_rect);

                //Initializing the CloseButton Rectangle
                Rectangle CloseButtonRectangle = new Rectangle(tp_rect.X + tp_rect.Width - 32, tp_rect.Y, 32, 32);

                //Checking if mouse is over this rectangle
                if (CloseButtonRectangle.Contains(this.PointToClient(MousePosition)))
                {
                    //Dispose of tab page
                    _basedTabControl.TabPages.Remove(tp);

                    //Remove all of the controls from the tab page
                    foreach (Control control in tp.Controls)
                    {
                        //Remove and dispose of control
                        tp.Controls.Remove(control);
                        control.Dispose();
                    }

                    //Dispose of tab page
                    tp.Dispose();

                    return;

                }
            }

            //Select tab if not selected
            if (_basedTabControl.SelectedTab != tp) _basedTabControl.SelectedTab = tp;

            //Drag Drop Event
            if (tp != null)
            {
                this.DoDragDrop(tp, DragDropEffects.All);
            }
        }

        #endregion

        #region Paint Method's

        /// <summary>
        /// Used to draw the CloseRectangle
        /// </summary>
        /// <param name="CloseRectangle">The rectangle for drawing</param>
        /// <param name="g">Paint graphics used to draw the rectangle</param>
        /// <param name="IsHovered">If the control is hovered</param>
        private void DrawCloseButton(Rectangle CloseRectangle, Graphics g, bool IsHovered)
        {

            //Initializing the StringFormat for drawing the string
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            //Initializing the Font of the string
            Font closeButtonFont = new Font("Segoe UI", 18);

            //Modifier for the Text
            int Modifier = 1;

            //Check if the control is hovered over.
            if (IsHovered)
            {
                g.FillRectangle(new SolidBrush(_CloseButtonColor), CloseRectangle);
            }

            //Draw the string 
            g.DrawString("X", closeButtonFont, new SolidBrush(_TextColor), new Rectangle(
                CloseRectangle.X,
                CloseRectangle.Y + Modifier,
                CloseRectangle.Width,
                CloseRectangle.Height)
            , stringFormat);


        }

        #endregion

        #region GetTabRect

        /// <summary>
        /// yGet the tab rectangle based on the tab
        /// </summary>
        private Rectangle GetTabRect(TabPage tab)
        {
            Rectangle returnRect = new Rectangle(0, 0, 0, 0);

            //For each tab page
            for (int i = 0; i < _basedTabControl.TabPages.Count; i++)
            {
                if (_basedTabControl.TabPages[i] == tab)
                {
                    returnRect = _TabRects[i];
                }
            }

            //Returns the rectangle
            return returnRect;
        }

        #endregion

        #region Scrolling

        //Vars for scrolling
        int endScrollInt = 0;
        int rightTimerTickStep = 0;
        int leftTimerTickStep = 0;

        bool isScrolling = false;

        /// <summary>
        /// Used to scroll left with a smooth animation
        /// </summary>
        public void ScrollLeft()
        {
            //Returns if scrolling right
            if (isScrolling) { return; }

            //Cancel if there is enough room
            int AllTabWidth = _basedTabControl.TabPages.Count * rect_Width;
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
            endScrollInt = ScrollInt - rect_Width + 1;
            leftTimerTickStep = 0;
            isScrolling = true;

            //Setting the timer to 60Hz refresh rate
            leftTimer.Interval = 1;

            //Starting the timer
            leftTimer.Start();
        }

        /// <summary>
        /// Used to scroll right with a smooth animation
        /// </summary>
        public void ScrollRight()
        {
            //Returns if scrolling right
            if (isScrolling) { return; }

            //Cancel if there is enough room
            int AllTabWidth = _basedTabControl.TabPages.Count * rect_Width;
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
            endScrollInt = ScrollInt + rect_Width - 1;
            rightTimerTickStep = 0;
            isScrolling = true;

            //Setting the timer to 60Hz refresh rate
            rightTimer.Interval = 1;

            //Starting the timer
            rightTimer.Start();
        }

        #endregion

    }
}