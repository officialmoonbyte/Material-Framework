namespace TestViewApplication
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
            this.genericTabHeader1 = new IndieGoat.MaterialFramework.Controls.GenericTabHeader();
            this.materialTabControl1 = new IndieGoat.MaterialFramework.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // genericTabHeader1
            // 
            this.genericTabHeader1.AddButtonBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            this.genericTabHeader1.AddButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.genericTabHeader1.AllowDrop = true;
            this.genericTabHeader1.BasedTabControl = this.materialTabControl1;
            this.genericTabHeader1.CloseButtonHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.genericTabHeader1.FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.genericTabHeader1.Hover_BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(57)))), ((int)(((byte)(84)))));
            this.genericTabHeader1.Hover_BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.genericTabHeader1.Hover_FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.genericTabHeader1.Hover_TopBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(164)))), ((int)(((byte)(171)))), ((int)(((byte)(182)))));
            this.genericTabHeader1.Location = new System.Drawing.Point(59, 169);
            this.genericTabHeader1.Name = "genericTabHeader1";
            this.genericTabHeader1.ScrollInt = 0;
            this.genericTabHeader1.Selected_BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.genericTabHeader1.Selected_BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.genericTabHeader1.Selected_FontColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.genericTabHeader1.Selected_TopBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.genericTabHeader1.ShowCloseButton = false;
            this.genericTabHeader1.Size = new System.Drawing.Size(562, 32);
            this.genericTabHeader1.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            this.genericTabHeader1.TabBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(78)))), ((int)(((byte)(101)))));
            this.genericTabHeader1.TabIndex = 2;
            this.genericTabHeader1.TopBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(64)))));
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Location = new System.Drawing.Point(487, 49);
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(200, 100);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(192, 74);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 261);
            this.Controls.Add(this.genericTabHeader1);
            this.Controls.Add(this.materialTabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.materialTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private IndieGoat.MaterialFramework.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private IndieGoat.MaterialFramework.Controls.GenericTabHeader genericTabHeader1;
    }
}

