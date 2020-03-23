using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Moonbyte.MaterialFramework.Controls
{
    public class MaterialForm : Form
    {

        #region Internal Vars

        Rectangle HeaderRectangle = new Rectangle(0, 0, 0, 0);

        Color borderColor = Color.FromArgb(0, 120, 220);
        Color headerColor = Color.FromArgb(250, 250, 250);
        Color titleColor = Color.FromArgb(12, 12, 12);

        bool _enableCloseButton = true;
        bool _enableMaxButton = true;
        bool _enableMinButton = true;
        bool _showTitle = true;
        bool _sizeAble = true;
        bool _snapAble = true;

        int _borderWidth = 2;
        int _headerHeight = 32;

        //
        // FormControl Buttons
        //
        public CloseButton closebutton = new CloseButton();
        public MaxButton maxbutton = new MaxButton();
        public MinButton minbutton = new MinButton();

        #endregion Internal Vars

        #region Form Properties

        #region Colours

        [Browsable(true)]
        [Description("Form Header Color"), Category("Moonbyte Config")]
        public Color HeaderColor
        {
            get { return this.headerColor; }
            set
            {
                this.headerColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Form default border color"), Category("Moonbyte Config")]
        public Color BorderColor
        {
            get { return this.borderColor; }
            set
            {
                this.borderColor = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Form default title color"), Category("Moonbyte Config")]
        public Color TitleColor
        {
            get { return this.titleColor; }
            set
            {
                this.titleColor = value;
                this.Invalidate();
            }
        }

        #endregion Colours

        #region Int's

        [Browsable(true)]
        [Description("Form default pen size."), Category("Moonbyte Config")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; this.Invalidate(); }
        }

        [Browsable(true)]
        [Description("Form default header height"), Category("Moonbyte Config")]
        public int HeaderHeight
        {
            get { return _headerHeight; }
            set { _headerHeight = value; this.Invalidate(); }
        }

        #endregion Int's

        #region Bool's

        [Browsable(true)]
        [Description("Enables or Disables the user ability to snap the form on the sides of the screen."), Category("Moonbyte Config")]
        public bool Snapable
        {
            get { return _snapAble; }
            set
            {
                _snapAble = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Enables or Disable the users ability to resize the form."), Category("Moonbyte Config")]
        public bool Sizeable
        {
            get { return _sizeAble; }
            set
            {
                _sizeAble = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the visibility of the close button"), Category("Moonbyte Config")]
        public bool EnableCloseButton
        {
            get { return _enableCloseButton; }
            set
            {
                _enableCloseButton = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the visibility of the max button"), Category("Moonbyte Config")]
        public bool EnableMaxButton
        {
            get { return _enableMaxButton; }
            set
            {
                _enableMaxButton = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the visibility of the min button"), Category("Moonbyte Config")]
        public bool EnableMinButton
        {
            get { return _enableMinButton; }
            set
            {
                _enableMinButton = value;
                this.Invalidate();
            }
        }

        [Browsable(true)]
        [Description("Changes the visibility of the Title"), Category("Moonbyte Config")]
        public bool ShowTitle
        {
            get { return _showTitle; }
            set
            {
                _showTitle = value;
                this.Invalidate();
            }
        }

        #endregion Bool's

        #endregion Form Properties

        #region Initialization / OnLoad

        #region Initialization

        public MaterialForm()
        {
            //
            // MaterialFrom
            //
            this.FormBorderStyle = FormBorderStyle.None;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Font = new Font("Segoe UI", 12f);
            this.DoubleBuffered = true;

            //
            // GlobalMouseHandler
            //
            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += new MouseEventHandler(GlobalMouseMove);
        }

        #endregion Initialization

        #region OnLoad

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            int constYValue = 1;
            int mar = 1;

            //
            // Close Button
            //
            if (_enableCloseButton)
            {
                this.closebutton.Location = new Point(this.Width - this.closebutton.Width - mar, constYValue);
                this.closebutton.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
                this.closebutton.MouseUp += (obj, args) =>
                {
                    if (closebutton.ClientRectangle.Contains(closebutton.PointToClient(MousePosition)))
                    { this.Close(); closebutton.Status = status.MouseOver; }
                    else { closebutton.Status = status.Default; }
                };
                this.Controls.Add(closebutton);
            }
            //
            // Max Button
            //
            if (_enableMaxButton)
            {
                Point controlLocation = new Point(this.Width - (this.maxbutton.Width * 2) - mar, constYValue);
                if (!_enableCloseButton) { controlLocation = new Point(this.Width - this.maxbutton.Width - mar, constYValue); }
                this.maxbutton.Location = controlLocation;
                this.maxbutton.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
                this.maxbutton.MouseUp += (obj, args) =>
                {
                    if (maxbutton.ClientRectangle.Contains(maxbutton.PointToClient(MousePosition)))
                    { MaxForm(); maxbutton.Status = status.MouseOver; }
                    else { maxbutton.Status = status.Default; }
                };
                this.Controls.Add(maxbutton);
            }
            //
            // Min Button
            //
            if (_enableMinButton)
            {
                Point controlLocation = new Point(this.Width - (this.minbutton.Width * 3) - mar, constYValue);
                if (!_enableMaxButton && !_enableMinButton)
                { controlLocation = new Point(this.Width - this.minbutton.Width - mar, constYValue); }
                if (!_enableCloseButton || !_enableMaxButton)
                { controlLocation = new Point(this.Width - (this.minbutton.Width * 2) - mar, constYValue); }
                this.minbutton.Location = controlLocation;
                this.minbutton.Anchor = (AnchorStyles.Right | AnchorStyles.Top);
                this.minbutton.MouseUp += (obj, args) =>
                {
                    if (minbutton.ClientRectangle.Contains(minbutton.PointToClient(MousePosition)))
                    { MinForm(); minbutton.Status = status.MouseOver; }
                    else { minbutton.Status = status.Default; }
                };
                this.Controls.Add(minbutton);
            }
        }

        #endregion OnLoad

        #endregion Initialization / OnLoad

        #region Button Controls

        public void MaxForm() { Screen screen = GetCurrentScreen();
            previousSize = this.Size; isSnapped = true;
            this.Location = new Point(screen.Bounds.X, screen.Bounds.Y);
            this.Size = new Size(screen.Bounds.Width, screen.Bounds.Height); }

        public void MinForm() { this.WindowState = FormWindowState.Minimized; }

        #endregion

        #region Startup Animation

        [Flags]
        enum AnimateWindowFlags
        { AW_HOR_POSITIVE = 0x0000000,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000 }

        [DllImport("user32.dll")]
        static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        protected override void SetVisibleCore(bool value)
        {
            if (!this.IsHandleCreated) { AnimateWindow(this.Handle, 100, AnimateWindowFlags.AW_BLEND); }
            base.SetVisibleCore(value);
        }

        protected override void OnShown(EventArgs e)
        { this.BringToFront(); base.OnShown(e); }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            HeaderRectangle = new Rectangle(new Point(0, 0), new Size(this.Width, _headerHeight));

            this.SuspendLayout();

            Graphics g = e.Graphics;

            //
            // Form Backcolor
            //
            g.FillRectangle(new SolidBrush(this.BackColor), this.ClientRectangle);

            //
            // Form Header
            //
            g.FillRectangle(new SolidBrush(this.headerColor), HeaderRectangle);

            //
            // Form Border
            //
            g.DrawRectangle(new Pen(new SolidBrush(this.borderColor), _borderWidth), this.ClientRectangle);

            //
            // Form Icon
            //
            if (this.ShowIcon)
            { Icon image = this.Icon;
                Rectangle iconRectangle = new Rectangle(new Point(6, 6), new Size(20, 20));
                g.DrawIcon(image, iconRectangle); }

            //
            // Form Title
            //
            if (_showTitle)
            { g.DrawString(this.Text, this.Font, new SolidBrush(titleColor), new Point(28, 6)); }

            this.ResumeLayout();
        }

        #endregion OnPaint

        #region Resize

        #region Vars

        Rectangle leftRectangle;
        Rectangle rightRectangle;
        Rectangle bottomRectangle;

        Rectangle BottomRightRectangle;
        Rectangle BottomLeftRectangle;

        int mouseSize = 10;

        private enum ResizeStatus { Bottom, Right, Left, BottomRight, BottomLeft, None}
        ResizeStatus resizeStatus = ResizeStatus.None;

        #endregion

        #region Update Rectangle Values

        private void UpdateBorderRectangles()
        {
            leftRectangle = new Rectangle(new Point(0, 32), new Size(mouseSize, this.Height - 32));
            rightRectangle = new Rectangle(new Point(this.Width - mouseSize, 32), new Size(mouseSize, this.Height - 32));
            bottomRectangle = new Rectangle(new Point(0, this.Height - mouseSize), new Size(this.Width, this.Height - mouseSize));
            BottomRightRectangle = new Rectangle(new Point(this.Width - mouseSize, this.Height - mouseSize), new Size(mouseSize, mouseSize));
            BottomLeftRectangle = new Rectangle(new Point(0, this.Height - mouseSize), new Size(mouseSize, mouseSize));
        }

        #endregion

        #region IsResizing

        private bool isResizing()
        {
            //Update the border rectangles
            UpdateBorderRectangles();

            //Calculate if mouse is in the rectangles
            Point mousePoint = this.PointToClient(MousePosition);

            if (!isFormResizing && this._sizeAble)
            {
                if (BottomRightRectangle.Contains(mousePoint)) { resizeStatus = ResizeStatus.BottomRight; return true; }
                if (BottomLeftRectangle.Contains(mousePoint)) { resizeStatus = ResizeStatus.BottomLeft; return true; }
                if (leftRectangle.Contains(mousePoint)) { resizeStatus = ResizeStatus.Left; return true; }
                if (rightRectangle.Contains(mousePoint)) { resizeStatus = ResizeStatus.Right; return true; }
                if (bottomRectangle.Contains(mousePoint)) { resizeStatus = ResizeStatus.Bottom; return true; }
            }
            return false;
        }

        private bool isInRectangle()
        {
            //Update the border rectangles
            UpdateBorderRectangles();

            //Calculate if mouse is in the rectangles
            Point mousePoint = this.PointToClient(MousePosition);

            if (BottomRightRectangle.Contains(mousePoint)) { return true; }
            if (leftRectangle.Contains(mousePoint)) { return true; }
            if (rightRectangle.Contains(mousePoint)) { return true; }
            if (bottomRectangle.Contains(mousePoint)) { return true; }
            return false;
        }

        #endregion

        #endregion Resize

        #region Mouse Controls

        #region Form Movement vars

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        bool isFormResizing = false;
        Point oldMouseLocation;

        #endregion Form Movement / Mouse Down

        #region Snapping Vars

        Size previousSize;
        bool isSnapped = false;

        #endregion

        #region OnMouseDown

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            bool result; if (e.Button == MouseButtons.Left) { result = true; } else { result = false; }
            MouseDownExternal(result);
        }

        #endregion OnMouseDown

        #region OnMouseMove

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            bool result; if (e.Button == MouseButtons.Left) { result = true; } else { result = false; }
            MouseMoveExternal(result);
        }

        #endregion OnMouseMove

        #region OnMouseUp

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            this.resizeStatus = ResizeStatus.None;
            MouseUpExternal();
        }

        #endregion OnMouseUp

        #region MouseMoveExternal

        public void MouseMoveExternal(bool button, bool ext = false)
        {
            if (isResizing())
            {
                if (resizeStatus == ResizeStatus.Left || resizeStatus == ResizeStatus.Right) { Cursor.Current = Cursors.SizeWE; }
                else if (resizeStatus == ResizeStatus.Bottom) { Cursor.Current = Cursors.SizeNS; }
                else if (resizeStatus == ResizeStatus.BottomRight) { Cursor.Current = Cursors.SizeNWSE; }
                else if (resizeStatus == ResizeStatus.BottomLeft) { Cursor.Current = Cursors.SizeNESW; }
            } else if (button && !isFormResizing)
            {
                if (HeaderRectangle.Contains(PointToClient(MousePosition)))
                {
                    CalculateMouseMovement();
                } else if (ext == true)
                {
                    CalculateMouseMovement();
                }
            }
        }

        #endregion MouseMoveExternal

        #region MouseDownExternal

        public void MouseDownExternal(bool button)
        {
            bool resize = isInRectangle();
            if (button)
            {
                if (!isFormResizing && resize)
                { isFormResizing = true;  }

                if (isFormResizing)
                {
                    if (resizeStatus == ResizeStatus.Left || resizeStatus == ResizeStatus.Right) { Cursor.Current = Cursors.SizeWE; }
                    else if (resizeStatus == ResizeStatus.Bottom) { Cursor.Current = Cursors.SizeNS; }
                    else if (resizeStatus == ResizeStatus.BottomRight) { Cursor.Current = Cursors.SizeNWSE; }
                    else if (resizeStatus == ResizeStatus.BottomLeft) { Cursor.Current = Cursors.SizeNESW; }
                    oldMouseLocation = Cursor.Position;
                }
            }
        }

        #endregion MouseDownExternal

        #region MouseUpExternal

        public void MouseUpExternal()
        {
            if (isFormResizing)
            { isFormResizing = false; }
            this.resizeStatus = ResizeStatus.None;
        }

        #endregion MouseUpExternal

        #region GlobalMouseMove

        private void GlobalMouseMove(object sender, MouseEventArgs e)
        {
            //
            // Resize for right
            //
            if (isFormResizing)
            {
                if (resizeStatus == ResizeStatus.Right)
                {
                    this.SuspendLayout();
                    Point currentMousePos = e.Location;
                    Point formLocation = this.Location;
                    int modifier = this.Width + formLocation.X;
                    this.Width = (currentMousePos.X - modifier) + this.Width;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    this.ResumeLayout();
                    this.Invalidate();
                }
                if (resizeStatus == ResizeStatus.Bottom)
                {
                    this.SuspendLayout();
                    Point currentMousePos = e.Location;
                    Point formLocation = this.Location;
                    int modifier = this.Height + formLocation.Y;
                    this.Height = (currentMousePos.Y - modifier) + this.Height;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    this.ResumeLayout();
                    this.Invalidate();
                }
                if (resizeStatus == ResizeStatus.Left)
                {
                    this.SuspendLayout();
                    Point currentMousePos = e.Location;
                    Point formLocation = this.Location;
                    int change = formLocation.X - currentMousePos.X;
                    this.Location = new Point(currentMousePos.X, formLocation.Y); this.Width = this.Width + change;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    this.ResumeLayout();
                    this.Invalidate();
                }
                if (resizeStatus == ResizeStatus.BottomRight)
                {
                    this.SuspendLayout();
                    Point currentMousePos = e.Location;
                    Point formLocation = this.Location;
                    int xModifier = this.Width + formLocation.X;
                    int yModifier = this.Height + formLocation.Y;
                    this.Width = (currentMousePos.X - xModifier) + this.Width;
                    this.Height = (currentMousePos.Y - yModifier) + this.Height;
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    this.ResumeLayout();
                    this.Invalidate();
                }
                if (resizeStatus == ResizeStatus.BottomLeft)
                {
                    this.SuspendLayout();

                    Point currentMousePos = e.Location;
                    Point formLocation = this.Location;
                    int xModifier = formLocation.X - currentMousePos.X;

                    this.Location = new Point(currentMousePos.X, formLocation.Y); this.Width = this.Width + xModifier;

                    int yModifier = this.Height + formLocation.Y;
                    this.Height = (currentMousePos.Y - yModifier) + this.Height;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    this.ResumeLayout();
                    this.Invalidate();
                }
            }
        }

        #endregion GlobalMouseMove

        #region CalculateMouseMovement / Snapping form

        /// <summary>
        /// Calculate the mouse movement on external processes (like TabHeader)
        /// Also used to organized code
        /// </summary>
        public void CalculateMouseMovement()
        {
            Screen currentScreen = GetCurrentScreen();
            Size screenWorkingArea = currentScreen.WorkingArea.Size;
            Point mouseLocation = this.PointToClient(Cursor.Position);

            //Detects if the form is snapped
            if (isSnapped)
            {
                if (previousSize != null)
                {
                    this.SuspendLayout();

                    decimal mousePercent = CalculateMousePercent();
                    int mouseX = decimal.ToInt32(decimal.Multiply(this.previousSize.Width, mousePercent));

                    this.Size = previousSize;

                    //Sets the location of the form
                    this.Location = new Point(MousePosition.X - mouseX, MousePosition.Y - mouseLocation.Y);
                    isSnapped = false;

                    this.ResumeLayout();
                }
            }

            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

            mouseLocation = Cursor.Position;

            this.SuspendLayout();
            currentScreen = Screen.FromPoint(MousePosition);
            screenWorkingArea = currentScreen.WorkingArea.Size;

            //
            // Snap to the left
            //
            if (mouseLocation.X == currentScreen.Bounds.Y)
            {
                if (!isSnapped) previousSize = this.Size;
                this.Location = new Point(currentScreen.Bounds.X, currentScreen.Bounds.Y);
                this.Size = new Size(screenWorkingArea.Width / 2, screenWorkingArea.Height);

                isSnapped = true;
            }

            if (_snapAble)
            {
                //
                // Snap to the top
                //
                if (mouseLocation.Y == currentScreen.Bounds.Y)
                {
                    if (!isSnapped) previousSize = this.Size;
                    this.Location = new Point(currentScreen.Bounds.X, currentScreen.Bounds.Y);
                    this.Size = new Size(screenWorkingArea.Width, screenWorkingArea.Height);

                    isSnapped = true;
                }

                //
                // Snap to the right
                //
                if (mouseLocation.X == (currentScreen.Bounds.Width - 1))
                {
                    if (!isSnapped) previousSize = this.Size;
                    this.Size = new Size(screenWorkingArea.Width / 2, screenWorkingArea.Height);
                    this.Location = new Point(currentScreen.Bounds.Width / 2, 0);

                    isSnapped = true;
                }

                //
                // Snap to top left cornor
                //
                if (mouseLocation.X == currentScreen.Bounds.Y && mouseLocation.Y == currentScreen.Bounds.Y)
                {
                    if (!isSnapped) previousSize = this.Size;
                    this.Location = new Point(0, 0);
                    this.Size = new Size(screenWorkingArea.Width / 2, screenWorkingArea.Height / 2);

                    isSnapped = true;
                }

                // 
                // Snap to top right cornor
                //
                if (mouseLocation.X == (currentScreen.Bounds.Width - 1) && mouseLocation.Y == currentScreen.Bounds.Y)
                {
                    if (!isSnapped) previousSize = this.Size;
                    this.Size = new Size(screenWorkingArea.Width / 2, screenWorkingArea.Height / 2);
                    this.Location = new Point(currentScreen.Bounds.Width / 2, 0);

                    isSnapped = true;
                }

                // 
                // Snap to bottom left cornor
                //


                //
                // Snap to bottom right cornor
                //
            }

            this.ResumeLayout();

        }

        #endregion CalculateMouseMovement

        #endregion MouseControls

        #region Drop Shadow

        private const int CS_DROPSHADOW = 0x20000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        #endregion Drop Shadow

        #region GetCurrentScreen

        private Screen GetCurrentScreen()
        { return Screen.FromPoint(new Point(Cursor.Position.X, Cursor.Position.Y)); }

        #endregion GetCurrentScreen

        #region Calculate Mouse Percent

        private decimal CalculateMousePercent()
        { return decimal.Divide(this.PointToClient(MousePosition).X, this.Width); }
        private Point CalculateMousePositon()
        {
            decimal mousePercent = CalculateMousePercent();
            Point currentMousePosition = this.PointToClient(Cursor.Position);
            int xPos = decimal.ToInt32(decimal.Multiply(currentMousePosition.X, mousePercent));
            return new Point(xPos, currentMousePosition.Y);
        }

        #endregion Calculate Mouse Percent

        #region SetButtonPositions

        private void SetButtonPositions(Size formSize)
        {
            int constYValue = 1;
            int mar = 1;
            this.closebutton.Location = new Point(formSize.Width - this.closebutton.Width - mar, constYValue);
            Point controlLocation = new Point(formSize.Width - (this.maxbutton.Width * 2) - mar, constYValue);
            if (!_enableCloseButton) { controlLocation = new Point(formSize.Width - this.maxbutton.Width - mar, constYValue); }
            Point _controlLocation = new Point(formSize.Width - (this.minbutton.Width * 3) - mar, constYValue);
            if (!_enableMaxButton && !_enableMinButton)
            { _controlLocation = new Point(formSize.Width - this.minbutton.Width - mar, constYValue); }
            if (!_enableCloseButton || !_enableMaxButton)
            { _controlLocation = new Point(formSize.Width - (this.minbutton.Width * 2) - mar, constYValue); }
            this.maxbutton.Location = controlLocation;
            this.minbutton.Location = _controlLocation;
        }

        #endregion

        #region Dispose

        protected override void OnClosed(EventArgs e)
        { base.OnClosed(e); this.Dispose(); }
        protected override void Dispose(bool disposing)
        {
            //
            // Disposing all of the contorls on the form
            //
            Control.ControlCollection coll = this.Controls;
            foreach(Control c in coll) { c.Dispose(); }

            base.Dispose(disposing);

            //GC Collection
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        #endregion

    }

    public enum status { Default, MouseOver, MouseDown }

    #region Close Button

    //[ToolboxItem(false)]
    [DefaultEvent("Click")]
    public class CloseButton : UserControl
    {

        #region Vars

        Color _backColor = Color.Transparent;
        Color _borderColor = Color.Transparent;
        Color _mouseOverBackColor = Color.FromArgb(255, 90, 90);
        Color _mouseOverBorderColor = Color.FromArgb(255, 90, 90);
        Color _mouseDownBackColor = Color.FromArgb(255, 130, 130);
        Color _mouseDownBorderColor = Color.FromArgb(255, 130, 130);
        Color _fontColor = Color.FromArgb(48, 48, 48);
        Color _mouseOverFontColor = Color.White;

        status _status = status.Default;

        int _borderSize = 2;

        #endregion Vars

        #region Properties

        #region Default

        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this.Invalidate();
            }
        }

        #endregion Default

        #region MouseOver

        public Color MouseOverBackColor
        {
            get { return this._mouseOverBackColor; }
            set
            {
                this._mouseOverBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseOverBorderColor
        {
            get { return this._mouseOverBorderColor; }
            set
            {
                this._mouseOverBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseOver

        #region MouseDown

        public Color MouseDownBackColor
        {
            get { return this._mouseDownBackColor; }
            set
            {
                this._mouseDownBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseDownBorderColor
        {
            get { return this._mouseDownBorderColor; }
            set
            {
                this._mouseDownBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseDown

        #region Other

        #region Colours

        public Color FontColor
        {
            get { return this._fontColor; }
            set
            {
                this._fontColor = value;
                this.Invalidate();
            }
        }

        #endregion 

        public int borderSize
        {
            get { return this._borderSize; }
            set
            {
                this._borderSize = value;
                this.Invalidate();
            }
        }

        public status Status
        {
            get { return this._status; }
            set
            {
                this._status = value;
                this.Invalidate();
            }
        }

        #endregion

        #endregion

        #region Initialization

        public CloseButton()
        {
            this.Size = new Size(48, 28);
            this.DoubleBuffered = true;
        }

        #endregion Initialization

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (this._status == status.Default)
            { DrawControl(g, _backColor, _borderColor, _fontColor); }
            if (this._status == status.MouseOver)
            { DrawControl(g, _mouseOverBackColor, _mouseOverBorderColor, _mouseOverFontColor); }
            if (this._status == status.MouseDown)
            { DrawControl(g, _mouseDownBackColor, _mouseDownBorderColor, _mouseOverFontColor); }
        }

        #endregion

        #region DrawControl

        private void DrawControl(Graphics g, Color P_BackColor, Color P_BorderColor, Color P_FontColor)
        {
            //
            // Background
            //
            g.FillRectangle(new SolidBrush(P_BackColor), this.ClientRectangle);

            //
            //Border Color
            //
            g.DrawRectangle(new Pen(new SolidBrush(P_BorderColor), _borderSize), this.ClientRectangle);

            //
            // Draws the X
            //
            int x = 18; int y = 9; int modif = 10;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.DrawLine(new Pen(P_FontColor, 1), y + modif, y, x + modif, x);
            g.DrawLine(new Pen(P_FontColor, 1), y + modif, x, x + modif, y);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        #endregion 

        #region MouseMovements

        protected override void OnMouseEnter(EventArgs e)
        { base.OnMouseEnter(e); this.Status = status.MouseOver; }
        protected override void OnMouseLeave(EventArgs e)
        { base.OnMouseLeave(e); this.Status = status.Default; }
        protected override void OnMouseDown(MouseEventArgs e)
        { base.OnMouseClick(e); this.Status = status.MouseDown; }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            this.MouseEnter += null;
            this.MouseLeave += null;
            this.MouseUp += null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.Dispose(disposing);
        }

        #endregion Dispose

    }

    #endregion Close Button

    #region Max Button

    [ToolboxItem(false)]
    [DefaultEvent("Click")]
    public class MaxButton : UserControl
    {

        #region Vars

        Color _backColor = Color.Transparent;
        Color _borderColor = Color.Transparent;
        Color _mouseOverBackColor = Color.FromArgb(229, 229, 229);
        Color _mouseOverBorderColor = Color.FromArgb(229, 229, 229);
        Color _mouseDownBackColor = Color.FromArgb(80, 80, 80);
        Color _mouseDownBorderColor = Color.FromArgb(80, 80, 80);

        status _status = status.Default;

        #endregion Vars

        #region Properties

        #region Default

        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this.Invalidate();
            }
        }

        #endregion Default

        #region MouseOver

        public Color MouseOverBackColor
        {
            get { return this._mouseOverBackColor; }
            set
            {
                this._mouseOverBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseOverBorderColor
        {
            get { return this._mouseOverBorderColor; }
            set
            {
                this._mouseOverBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseOver

        #region MouseDown

        public Color MouseDownBackColor
        {
            get { return this._mouseDownBackColor; }
            set
            {
                this._mouseDownBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseDownBorderColor
        {
            get { return this._mouseDownBorderColor; }
            set
            {
                this._mouseDownBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseDown

        #region Other

        public status Status
        {
            get { return this._status; }
            set
            {
                this._status = value;
                this.Invalidate();
            }
        }

        #endregion Other

        #endregion Properties

        #region Initialization

        public MaxButton()
        {
            this.Size = new Size(48, 28);
            this.DoubleBuffered = true;
        }

        #endregion Initialization

        #region Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this._status == status.Default)
            { DrawControl(e.Graphics, _backColor, _borderColor); }
            else if (this.Status == status.MouseOver)
            { DrawControl(e.Graphics, _mouseOverBackColor, _mouseOverBorderColor); }
            else if (this.Status == status.MouseDown)
            { DrawControl(e.Graphics, _mouseDownBackColor, _mouseDownBorderColor); }
        }

        #endregion Paint

        #region DrawControl

        private void DrawControl(Graphics g, Color P_BackColor, Color P_BorderColor)
        {
            int rectSize = 9; int penSize = 1;
            Color rectColor = Color.FromArgb(120, 120, 120);

            g.SmoothingMode = SmoothingMode.HighQuality;

            //
            // Draws back color and border
            //
            g.FillRectangle(new SolidBrush(P_BackColor), this.ClientRectangle);
            g.DrawRectangle(new Pen(P_BorderColor, 1), this.ClientRectangle);

            //
            // Draws max button
            //
            g.DrawRectangle(new Pen(new SolidBrush(rectColor), penSize), new Rectangle(new Point(rectSize + 10, rectSize), new Size(this.Height - (rectSize * 2), this.Height - (rectSize * 2))));

            g.SmoothingMode = SmoothingMode.None;
        }

        #endregion DrawControl

        #region MouseMovements

        protected override void OnMouseEnter(EventArgs e)
        { base.OnMouseEnter(e); this.Status = status.MouseOver; }
        protected override void OnMouseLeave(EventArgs e)
        { base.OnMouseLeave(e); this.Status = status.Default; }
        protected override void OnMouseDown(MouseEventArgs e)
        { base.OnMouseClick(e); this.Status = status.MouseDown; }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            this.MouseEnter += null;
            this.MouseLeave += null;
            this.MouseUp += null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.Dispose(disposing);
        }

        #endregion Dispose
    }

    #endregion Max Button

    #region Min Button

    [ToolboxItem(false)]
    [DefaultEvent("Click")]
    public class MinButton : UserControl
    {

        #region Vars

        Color _backColor = Color.Transparent;
        Color _borderColor = Color.Transparent;
        Color _mouseOverBackColor = Color.FromArgb(229, 229, 229);
        Color _mouseOverBorderColor = Color.FromArgb(229, 229, 229);
        Color _mouseDownBackColor = Color.FromArgb(80, 80, 80);
        Color _mouseDownBorderColor = Color.FromArgb(80, 80, 80);

        status _status = status.Default;

        #endregion Vars

        #region Properties

        #region Default

        public override Color BackColor
        {
            get { return this._backColor; }
            set
            {
                this._backColor = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return this._borderColor; }
            set
            {
                this._borderColor = value;
                this.Invalidate();
            }
        }

        #endregion Default

        #region MouseOver

        public Color MouseOverBackColor
        {
            get { return this._mouseOverBackColor; }
            set
            {
                this._mouseOverBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseOverBorderColor
        {
            get { return this._mouseOverBorderColor; }
            set
            {
                this._mouseOverBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseOver

        #region MouseDown

        public Color MouseDownBackColor
        {
            get { return this._mouseDownBackColor; }
            set
            {
                this._mouseDownBackColor = value;
                this.Invalidate();
            }
        }

        public Color MouseDownBorderColor
        {
            get { return this._mouseDownBorderColor; }
            set
            {
                this._mouseDownBorderColor = value;
                this.Invalidate();
            }
        }

        #endregion MouseDown

        #region Other

        public status Status
        {
            get { return _status; }
            set
            {
                _status = value;
                this.Invalidate();
            }
        }

        #endregion Other

        #endregion

        #region Initialization

        public MinButton()
        {
            this.Size = new Size(48, 28);
            this.DoubleBuffered = true;
        }

        #endregion Initialization

        #region Override Paint

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this._status == status.Default)
            { DrawControl(e.Graphics, this._backColor, this._borderColor); }
            else if (this._status == status.MouseOver)
            { DrawControl(e.Graphics, this._mouseOverBackColor, this._mouseOverBorderColor); }
            else if (this._status == status.MouseDown)
            { DrawControl(e.Graphics, this._mouseDownBackColor, this._mouseDownBorderColor); }
        }

        #endregion Override Paint

        #region DrawControl

        private void DrawControl(Graphics g, Color P_BackColor, Color P_BorderColor)
        {
            //
            // Draw Vars
            //
            Color minusColor = Color.FromArgb(120, 120, 120);

            int xMod = 18;
            int rectHeight = 1;

            Rectangle minus_Rect = new Rectangle(new Point(this.Width - ((this.Width - (xMod * 2) / 2)), this.Height - ((this.Height - rectHeight) / 2) - 1), new Size(this.Width - (xMod*2), rectHeight));

            //
            // Draw control border and back color
            //
            g.FillRectangle(new SolidBrush(P_BackColor), this.ClientRectangle);
            g.DrawRectangle(new Pen(P_BorderColor, 1), this.ClientRectangle);

            //
            // Draw minus symbol
            //
            g.FillRectangle(new SolidBrush(minusColor), minus_Rect);

        }

        #endregion DrawControl

        #region MouseMovements

        protected override void OnMouseEnter(EventArgs e)
        { base.OnMouseEnter(e); this.Status = status.MouseOver; }
        protected override void OnMouseLeave(EventArgs e)
        { base.OnMouseLeave(e); this.Status = status.Default; }
        protected override void OnMouseDown(MouseEventArgs e)
        { base.OnMouseClick(e); this.Status = status.MouseDown; }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            this.MouseEnter += null;
            this.MouseLeave += null;
            this.MouseUp += null;

            GC.Collect();
            GC.WaitForPendingFinalizers();

            base.Dispose(disposing);
        }

        #endregion Dispose

    }

    #endregion Min Button

    #region Global Mouse Hander

    class MouseMessageFilter : IMessageFilter
    {
        public static event MouseEventHandler MouseMove = delegate { };
        const int WM_MOUSEMOVE = 0x0200;

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg == WM_MOUSEMOVE)
            {

                Point mousePosition = Control.MousePosition;

                MouseMove(null, new MouseEventArgs(
                    MouseButtons.None, 0, mousePosition.X, mousePosition.Y, 0));
            }
            return false;
        }
    }

    #endregion

}
