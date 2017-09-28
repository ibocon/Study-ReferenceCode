namespace nemonic.Controls
{
    partial class ScreenShotMenuCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScreenShotMenuCtrl));
            this.Button_Back = new System.Windows.Forms.Button();
            this.Button_Print = new System.Windows.Forms.Button();
            this.Button_ScreenShot = new System.Windows.Forms.Button();
            this.ToolTip_Back = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_ScreenShot = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_Print = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Button_Back
            // 
            this.Button_Back.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button_Back.BackColor = System.Drawing.Color.Transparent;
            this.Button_Back.BackgroundImage = global::nemonic.Properties.Resources.icon_menu_back;
            this.Button_Back.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Back.FlatAppearance.BorderSize = 0;
            this.Button_Back.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Button_Back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Back.Location = new System.Drawing.Point(113, 0);
            this.Button_Back.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Back.Name = "Button_Back";
            this.Button_Back.Size = new System.Drawing.Size(31, 31);
            this.Button_Back.TabIndex = 0;
            this.Button_Back.UseVisualStyleBackColor = false;
            this.Button_Back.Click += new System.EventHandler(this.Button_Back_Click);
            // 
            // Button_Print
            // 
            this.Button_Print.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button_Print.BackColor = System.Drawing.Color.Transparent;
            this.Button_Print.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Print.BackgroundImage")));
            this.Button_Print.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Print.FlatAppearance.BorderSize = 0;
            this.Button_Print.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Button_Print.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Print.Location = new System.Drawing.Point(175, 0);
            this.Button_Print.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Print.Name = "Button_Print";
            this.Button_Print.Size = new System.Drawing.Size(31, 31);
            this.Button_Print.TabIndex = 2;
            this.Button_Print.UseVisualStyleBackColor = false;
            this.Button_Print.Click += new System.EventHandler(this.Button_Print_Click);
            // 
            // Button_ScreenShot
            // 
            this.Button_ScreenShot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Button_ScreenShot.BackColor = System.Drawing.Color.Transparent;
            this.Button_ScreenShot.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_ScreenShot.BackgroundImage")));
            this.Button_ScreenShot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_ScreenShot.FlatAppearance.BorderSize = 0;
            this.Button_ScreenShot.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Button_ScreenShot.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_ScreenShot.Location = new System.Drawing.Point(144, 0);
            this.Button_ScreenShot.Margin = new System.Windows.Forms.Padding(0);
            this.Button_ScreenShot.Name = "Button_ScreenShot";
            this.Button_ScreenShot.Size = new System.Drawing.Size(31, 31);
            this.Button_ScreenShot.TabIndex = 1;
            this.Button_ScreenShot.UseVisualStyleBackColor = false;
            this.Button_ScreenShot.Click += new System.EventHandler(this.Button_ScreenShot_Click);
            // 
            // ToolTip_Back
            // 
            this.ToolTip_Back.Tag = "";
            // 
            // ToolTip_ScreenShot
            // 
            this.ToolTip_ScreenShot.Tag = "";
            // 
            // ToolTip_Print
            // 
            this.ToolTip_Print.Tag = "";
            // 
            // ScreenShotMenuCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.Button_Back);
            this.Controls.Add(this.Button_Print);
            this.Controls.Add(this.Button_ScreenShot);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ScreenShotMenuCtrl";
            this.Size = new System.Drawing.Size(312, 31);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Print;
        private System.Windows.Forms.Button Button_ScreenShot;
        private System.Windows.Forms.Button Button_Back;
        private System.Windows.Forms.ToolTip ToolTip_Back;
        private System.Windows.Forms.ToolTip ToolTip_ScreenShot;
        private System.Windows.Forms.ToolTip ToolTip_Print;
    }
}
