namespace nemonic
{
    partial class MenuCtrl
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
            this.UtilityCtrl = new nemonic.UtilityCtrl();
            this.ColorCtrl = new nemonic.ColorCtrl();
            this.SelectCtrl = new nemonic.SelectCtrl();
            this.TitleCtrl = new nemonic.TitleCtrl();
            this.ScreenShotMenuCtrl = new nemonic.Controls.ScreenShotMenuCtrl();
            this.SuspendLayout();
            // 
            // UtilityCtrl
            // 
            this.UtilityCtrl.BackColor = System.Drawing.Color.DarkGray;
            this.UtilityCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.UtilityCtrl.Enabled = false;
            this.UtilityCtrl.Location = new System.Drawing.Point(0, 93);
            this.UtilityCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.UtilityCtrl.Name = "UtilityCtrl";
            this.UtilityCtrl.Size = new System.Drawing.Size(312, 31);
            this.UtilityCtrl.TabIndex = 3;
            this.UtilityCtrl.Visible = false;
            // 
            // ColorCtrl
            // 
            this.ColorCtrl.BackColor = System.Drawing.Color.DarkGray;
            this.ColorCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ColorCtrl.Enabled = false;
            this.ColorCtrl.Location = new System.Drawing.Point(0, 62);
            this.ColorCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.ColorCtrl.Name = "ColorCtrl";
            this.ColorCtrl.Size = new System.Drawing.Size(312, 31);
            this.ColorCtrl.TabIndex = 2;
            this.ColorCtrl.Visible = false;
            // 
            // SelectCtrl
            // 
            this.SelectCtrl.BackColor = System.Drawing.Color.DarkGray;
            this.SelectCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.SelectCtrl.Location = new System.Drawing.Point(0, 31);
            this.SelectCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.SelectCtrl.Name = "SelectCtrl";
            this.SelectCtrl.Size = new System.Drawing.Size(312, 31);
            this.SelectCtrl.TabIndex = 1;
            // 
            // TitleCtrl
            // 
            this.TitleCtrl.BackColor = System.Drawing.Color.DarkGray;
            this.TitleCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.TitleCtrl.Location = new System.Drawing.Point(0, 0);
            this.TitleCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.TitleCtrl.Name = "TitleCtrl";
            this.TitleCtrl.Size = new System.Drawing.Size(312, 31);
            this.TitleCtrl.TabIndex = 0;
            // 
            // ScreenShotMenuCtrl
            // 
            this.ScreenShotMenuCtrl.BackColor = System.Drawing.Color.DarkGray;
            this.ScreenShotMenuCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.ScreenShotMenuCtrl.Enabled = false;
            this.ScreenShotMenuCtrl.Location = new System.Drawing.Point(0, 124);
            this.ScreenShotMenuCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.ScreenShotMenuCtrl.Name = "ScreenShotMenuCtrl";
            this.ScreenShotMenuCtrl.Size = new System.Drawing.Size(312, 31);
            this.ScreenShotMenuCtrl.TabIndex = 4;
            this.ScreenShotMenuCtrl.Visible = false;
            // 
            // MenuCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.ScreenShotMenuCtrl);
            this.Controls.Add(this.UtilityCtrl);
            this.Controls.Add(this.ColorCtrl);
            this.Controls.Add(this.SelectCtrl);
            this.Controls.Add(this.TitleCtrl);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MenuCtrl";
            this.Size = new System.Drawing.Size(312, 60);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuCtrl_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private TitleCtrl TitleCtrl;
        private SelectCtrl SelectCtrl;
        private ColorCtrl ColorCtrl;
        private UtilityCtrl UtilityCtrl;
        private Controls.ScreenShotMenuCtrl ScreenShotMenuCtrl;
    }
}
