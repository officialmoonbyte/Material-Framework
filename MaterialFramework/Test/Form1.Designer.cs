namespace Test
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.materialTabControl1 = new IndieGoat.MaterialFramework.Controls.MaterialTabControl();
            this.materialTabPage2 = new IndieGoat.MaterialFramework.Controls.MaterialTabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabHeader1 = new MaterialFramework.Controls.TabHeader();
            this.materialTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // closebutton
            // 
            this.closebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closebutton.Location = new System.Drawing.Point(938, 1);
            // 
            // maxbutton
            // 
            this.maxbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.maxbutton.Location = new System.Drawing.Point(890, 1);
            this.maxbutton.TabIndex = 1;
            // 
            // minbutton
            // 
            this.minbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minbutton.Location = new System.Drawing.Point(842, 1);
            this.minbutton.TabIndex = 2;
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.materialTabPage2);
            this.materialTabControl1.Location = new System.Drawing.Point(301, 201);
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(537, 100);
            this.materialTabControl1.TabIndex = 4;
            // 
            // materialTabPage2
            // 
            this.materialTabPage2.BackColor = System.Drawing.Color.Transparent;
            this.materialTabPage2.Location = new System.Drawing.Point(4, 30);
            this.materialTabPage2.Name = "materialTabPage2";
            this.materialTabPage2.Size = new System.Drawing.Size(529, 66);
            this.materialTabPage2.TabIndex = 1;
            this.materialTabPage2.Text = "tabPage2";
            this.materialTabPage2.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 30);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 66);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 30);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 66);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabHeader1
            // 
            this.tabHeader1.AllowDrop = true;
            this.tabHeader1.BackColor = System.Drawing.Color.Transparent;
            this.tabHeader1.BasedTabControl = this.materialTabControl1;
            this.tabHeader1.EnableArrowButton = true;
            this.tabHeader1.EnableCloseButton = true;
            this.tabHeader1.EnableNewTabButton = true;
            this.tabHeader1.HoverTabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.tabHeader1.HoverTabBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.tabHeader1.HoverTabTopBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(171)))), ((int)(((byte)(182)))));
            this.tabHeader1.Location = new System.Drawing.Point(110, 0);
            this.tabHeader1.Name = "tabHeader1";
            this.tabHeader1.ScrollInt = 0;
            this.tabHeader1.Size = new System.Drawing.Size(670, 32);
            this.tabHeader1.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            this.tabHeader1.TabBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.tabHeader1.TabIndex = 5;
            this.tabHeader1.TabPageWidth = 230;
            this.tabHeader1.TabSelectedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.tabHeader1.TabSelectedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.tabHeader1.TabSelectedTopColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabHeader1.TopBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            this.tabHeader1.Load += new System.EventHandler(this.TabHeader1_Load);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(1213, 423);
            this.Controls.Add(this.tabHeader1);
            this.Controls.Add(this.materialTabControl1);
            this.Controls.Add(this.closebutton);
            this.Controls.Add(this.maxbutton);
            this.Controls.Add(this.minbutton);
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Controls.SetChildIndex(this.minbutton, 0);
            this.Controls.SetChildIndex(this.maxbutton, 0);
            this.Controls.SetChildIndex(this.closebutton, 0);
            this.Controls.SetChildIndex(this.materialTabControl1, 0);
            this.Controls.SetChildIndex(this.tabHeader1, 0);
            this.materialTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private IndieGoat.MaterialFramework.Controls.MaterialTabControl materialTabControl1;
        private IndieGoat.MaterialFramework.Controls.MaterialTabPage materialTabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialFramework.Controls.TabHeader tabHeader1;
    }
}