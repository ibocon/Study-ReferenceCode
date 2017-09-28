namespace nemonic
{
    partial class TitleCtrl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TitleCtrl));
            this.Button_Close = new System.Windows.Forms.Button();
            this.Button_New = new System.Windows.Forms.Button();
            this.ToolTip_New = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTip_Close = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Button_Close
            // 
            this.Button_Close.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Close.BackColor = System.Drawing.Color.Transparent;
            this.Button_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_Close.BackgroundImage")));
            this.Button_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Close.FlatAppearance.BorderSize = 0;
            this.Button_Close.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Close.Location = new System.Drawing.Point(281, 0);
            this.Button_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(31, 31);
            this.Button_Close.TabIndex = 1;
            this.Button_Close.UseVisualStyleBackColor = false;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            // 
            // Button_New
            // 
            this.Button_New.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Button_New.BackColor = System.Drawing.Color.Transparent;
            this.Button_New.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Button_New.BackgroundImage")));
            this.Button_New.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_New.FlatAppearance.BorderSize = 0;
            this.Button_New.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.Button_New.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_New.Location = new System.Drawing.Point(0, 0);
            this.Button_New.Margin = new System.Windows.Forms.Padding(0);
            this.Button_New.Name = "Button_New";
            this.Button_New.Size = new System.Drawing.Size(31, 31);
            this.Button_New.TabIndex = 0;
            this.Button_New.UseVisualStyleBackColor = false;
            this.Button_New.Click += new System.EventHandler(this.Button_New_Click);
            this.Button_New.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Button_New_KeyDown);
            // 
            // ToolTip_New
            // 
            this.ToolTip_New.Tag = "";
            // 
            // ToolTip_Close
            // 
            this.ToolTip_Close.Tag = "";
            // 
            // TitleCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.Controls.Add(this.Button_Close);
            this.Controls.Add(this.Button_New);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "TitleCtrl";
            this.Size = new System.Drawing.Size(312, 31);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TitleCtrl_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_New;
        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.ToolTip ToolTip_New;
        private System.Windows.Forms.ToolTip ToolTip_Close;
    }
}
