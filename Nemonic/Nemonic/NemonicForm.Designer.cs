namespace nemonic
{
    partial class NemonicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NemonicForm));
            this.openImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveJsonDialog = new System.Windows.Forms.SaveFileDialog();
            this.openJsonDialog = new System.Windows.Forms.OpenFileDialog();
            this.LayersCtrl = new nemonic.LayersCtrl();
            this.MenuCtrl = new nemonic.MenuCtrl();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.Panel_Resize = new System.Windows.Forms.Panel();
            this.Label_ToastMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // openImageDialog
            // 
            this.openImageDialog.Filter = "image files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            // 
            // saveJsonDialog
            // 
            this.saveJsonDialog.Filter = "nemonic memo | *.nemo";
            // 
            // openJsonDialog
            // 
            this.openJsonDialog.Filter = "nemonic memo | *.nemo";
            // 
            // LayersCtrl
            // 
            this.LayersCtrl.AllowDrop = true;
            this.LayersCtrl.BackColor = System.Drawing.Color.White;
            this.LayersCtrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LayersCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LayersCtrl.Location = new System.Drawing.Point(1, 61);
            this.LayersCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.LayersCtrl.Name = "LayersCtrl";
            this.LayersCtrl.Padding = new System.Windows.Forms.Padding(12);
            this.LayersCtrl.Size = new System.Drawing.Size(312, 312);
            this.LayersCtrl.TabIndex = 1;
            // 
            // MenuCtrl
            // 
            this.MenuCtrl.BackColor = System.Drawing.Color.White;
            this.MenuCtrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.MenuCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.MenuCtrl.Location = new System.Drawing.Point(1, 1);
            this.MenuCtrl.Margin = new System.Windows.Forms.Padding(0);
            this.MenuCtrl.Name = "MenuCtrl";
            this.MenuCtrl.Size = new System.Drawing.Size(312, 60);
            this.MenuCtrl.TabIndex = 0;
            this.MenuCtrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MenuCtrl_KeyDown);
            // 
            // printDialog
            // 
            this.printDialog.UseEXDialog = true;
            // 
            // Panel_Resize
            // 
            this.Panel_Resize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Panel_Resize.BackColor = System.Drawing.Color.Gray;
            this.Panel_Resize.BackgroundImage = global::nemonic.Properties.Resources.icon_resize;
            this.Panel_Resize.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Panel_Resize.Enabled = false;
            this.Panel_Resize.Location = new System.Drawing.Point(301, 361);
            this.Panel_Resize.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Resize.Name = "Panel_Resize";
            this.Panel_Resize.Size = new System.Drawing.Size(12, 12);
            this.Panel_Resize.TabIndex = 2;
            this.Panel_Resize.Visible = false;
            this.Panel_Resize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Panel_Resize_MouseDown);
            this.Panel_Resize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Resize_MouseMove);
            this.Panel_Resize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Panel_Resize_MouseUp);
            // 
            // Label_ToastMessage
            // 
            this.Label_ToastMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Label_ToastMessage.AutoSize = true;
            this.Label_ToastMessage.BackColor = System.Drawing.Color.LightYellow;
            this.Label_ToastMessage.Enabled = false;
            this.Label_ToastMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Label_ToastMessage.Location = new System.Drawing.Point(1, 355);
            this.Label_ToastMessage.Margin = new System.Windows.Forms.Padding(0);
            this.Label_ToastMessage.Name = "Label_ToastMessage";
            this.Label_ToastMessage.Size = new System.Drawing.Size(90, 12);
            this.Label_ToastMessage.TabIndex = 3;
            this.Label_ToastMessage.Text = "ToastMessage";
            this.Label_ToastMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_ToastMessage.Visible = false;
            // 
            // NemonicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(314, 374);
            this.Controls.Add(this.Label_ToastMessage);
            this.Controls.Add(this.Panel_Resize);
            this.Controls.Add(this.LayersCtrl);
            this.Controls.Add(this.MenuCtrl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "NemonicForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Untitled";
            this.Activated += new System.EventHandler(this.NemonicForm_Activated);
            this.Deactivate += new System.EventHandler(this.NemonicForm_Deactivate);
            this.LocationChanged += new System.EventHandler(this.NemonicForm_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ControlKeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openImageDialog;
        private System.Windows.Forms.SaveFileDialog saveJsonDialog;
        private System.Windows.Forms.OpenFileDialog openJsonDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        public LayersCtrl LayersCtrl;
        public MenuCtrl MenuCtrl;
        private System.Windows.Forms.Panel Panel_Resize;
        private System.Windows.Forms.Label Label_ToastMessage;
    }
}