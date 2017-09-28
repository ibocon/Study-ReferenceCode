namespace nemonic
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.TabControl = new System.Windows.Forms.TabControl();
            this.Panel_Dock = new System.Windows.Forms.Panel();
            this.Panel_Select = new System.Windows.Forms.Panel();
            this.OpenMemo = new System.Windows.Forms.RadioButton();
            this.ImportTemplate = new System.Windows.Forms.RadioButton();
            this.Settings = new System.Windows.Forms.RadioButton();
            this.Help = new System.Windows.Forms.RadioButton();
            this.PictureBox_Logo = new System.Windows.Forms.PictureBox();
            this.Button_Close = new System.Windows.Forms.Button();
            this.Panel_All = new System.Windows.Forms.Panel();
            this.Panel_Dock.SuspendLayout();
            this.Panel_Select.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).BeginInit();
            this.Panel_All.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Location = new System.Drawing.Point(270, 100);
            this.TabControl.Margin = new System.Windows.Forms.Padding(0);
            this.TabControl.Name = "TabControl";
            this.TabControl.Padding = new System.Drawing.Point(0, 0);
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(774, 649);
            this.TabControl.TabIndex = 1;
            this.TabControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TabControl_KeyDown);
            // 
            // Panel_Dock
            // 
            this.Panel_Dock.BackColor = System.Drawing.Color.DimGray;
            this.Panel_Dock.Controls.Add(this.Panel_Select);
            this.Panel_Dock.Controls.Add(this.PictureBox_Logo);
            this.Panel_Dock.Dock = System.Windows.Forms.DockStyle.Left;
            this.Panel_Dock.ForeColor = System.Drawing.Color.Black;
            this.Panel_Dock.Location = new System.Drawing.Point(0, 0);
            this.Panel_Dock.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Dock.Name = "Panel_Dock";
            this.Panel_Dock.Size = new System.Drawing.Size(256, 762);
            this.Panel_Dock.TabIndex = 2;
            this.Panel_Dock.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_Dock_MouseMove);
            // 
            // Panel_Select
            // 
            this.Panel_Select.Controls.Add(this.OpenMemo);
            this.Panel_Select.Controls.Add(this.ImportTemplate);
            this.Panel_Select.Controls.Add(this.Settings);
            this.Panel_Select.Controls.Add(this.Help);
            this.Panel_Select.Location = new System.Drawing.Point(1, 100);
            this.Panel_Select.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_Select.Name = "Panel_Select";
            this.Panel_Select.Size = new System.Drawing.Size(256, 192);
            this.Panel_Select.TabIndex = 2;
            // 
            // OpenMemo
            // 
            this.OpenMemo.Appearance = System.Windows.Forms.Appearance.Button;
            this.OpenMemo.FlatAppearance.BorderSize = 0;
            this.OpenMemo.FlatAppearance.CheckedBackColor = System.Drawing.Color.Snow;
            this.OpenMemo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenMemo.Image = global::nemonic.Properties.Resources.op_menu_meno_1;
            this.OpenMemo.Location = new System.Drawing.Point(0, 0);
            this.OpenMemo.Margin = new System.Windows.Forms.Padding(0);
            this.OpenMemo.Name = "OpenMemo";
            this.OpenMemo.Size = new System.Drawing.Size(256, 48);
            this.OpenMemo.TabIndex = 0;
            this.OpenMemo.UseVisualStyleBackColor = true;
            this.OpenMemo.CheckedChanged += new System.EventHandler(this.OpenMemo_CheckedChanged);
            // 
            // ImportTemplate
            // 
            this.ImportTemplate.Appearance = System.Windows.Forms.Appearance.Button;
            this.ImportTemplate.FlatAppearance.BorderSize = 0;
            this.ImportTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImportTemplate.Image = global::nemonic.Properties.Resources.op_menu_temp_1;
            this.ImportTemplate.Location = new System.Drawing.Point(0, 48);
            this.ImportTemplate.Margin = new System.Windows.Forms.Padding(0);
            this.ImportTemplate.Name = "ImportTemplate";
            this.ImportTemplate.Size = new System.Drawing.Size(256, 48);
            this.ImportTemplate.TabIndex = 0;
            this.ImportTemplate.UseVisualStyleBackColor = true;
            this.ImportTemplate.CheckedChanged += new System.EventHandler(this.ImportTemplate_CheckedChanged);
            // 
            // Settings
            // 
            this.Settings.Appearance = System.Windows.Forms.Appearance.Button;
            this.Settings.FlatAppearance.BorderSize = 0;
            this.Settings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Settings.Image = global::nemonic.Properties.Resources.op_menu_set_1;
            this.Settings.Location = new System.Drawing.Point(0, 96);
            this.Settings.Margin = new System.Windows.Forms.Padding(0);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(256, 48);
            this.Settings.TabIndex = 0;
            this.Settings.UseVisualStyleBackColor = true;
            this.Settings.Visible = false;
            this.Settings.CheckedChanged += new System.EventHandler(this.Settings_CheckedChanged);
            // 
            // Help
            // 
            this.Help.Appearance = System.Windows.Forms.Appearance.Button;
            this.Help.FlatAppearance.BorderSize = 0;
            this.Help.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Help.Image = global::nemonic.Properties.Resources.op_menu_help_1;
            this.Help.Location = new System.Drawing.Point(0, 144);
            this.Help.Margin = new System.Windows.Forms.Padding(0);
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(256, 48);
            this.Help.TabIndex = 0;
            this.Help.UseVisualStyleBackColor = true;
            this.Help.Visible = false;
            this.Help.CheckedChanged += new System.EventHandler(this.Help_CheckedChanged);
            // 
            // PictureBox_Logo
            // 
            this.PictureBox_Logo.BackColor = System.Drawing.Color.DimGray;
            this.PictureBox_Logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBox_Logo.Image = global::nemonic.Properties.Resources.op_logo;
            this.PictureBox_Logo.Location = new System.Drawing.Point(0, 0);
            this.PictureBox_Logo.Margin = new System.Windows.Forms.Padding(0);
            this.PictureBox_Logo.Name = "PictureBox_Logo";
            this.PictureBox_Logo.Size = new System.Drawing.Size(256, 100);
            this.PictureBox_Logo.TabIndex = 1;
            this.PictureBox_Logo.TabStop = false;
            this.PictureBox_Logo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictureBox_Logo_MouseMove);
            // 
            // Button_Close
            // 
            this.Button_Close.BackColor = System.Drawing.Color.White;
            this.Button_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Close.FlatAppearance.BorderSize = 0;
            this.Button_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Close.Image = global::nemonic.Properties.Resources.op_close;
            this.Button_Close.Location = new System.Drawing.Point(1013, 11);
            this.Button_Close.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Close.Name = "Button_Close";
            this.Button_Close.Size = new System.Drawing.Size(31, 31);
            this.Button_Close.TabIndex = 0;
            this.Button_Close.UseVisualStyleBackColor = false;
            this.Button_Close.Click += new System.EventHandler(this.Button_Close_Click);
            this.Button_Close.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Button_Close_KeyDown);
            // 
            // Panel_All
            // 
            this.Panel_All.BackColor = System.Drawing.Color.White;
            this.Panel_All.Controls.Add(this.Panel_Dock);
            this.Panel_All.Controls.Add(this.TabControl);
            this.Panel_All.Controls.Add(this.Button_Close);
            this.Panel_All.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel_All.Location = new System.Drawing.Point(1, 1);
            this.Panel_All.Margin = new System.Windows.Forms.Padding(0);
            this.Panel_All.Name = "Panel_All";
            this.Panel_All.Size = new System.Drawing.Size(1056, 762);
            this.Panel_All.TabIndex = 3;
            this.Panel_All.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Panel_All_MouseMove);
            // 
            // SettingsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Black;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1058, 764);
            this.Controls.Add(this.Panel_All);
            this.Font = new System.Drawing.Font("Gulim", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Panel_Dock.ResumeLayout(false);
            this.Panel_Select.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox_Logo)).EndInit();
            this.Panel_All.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Button_Close;
        private System.Windows.Forms.TabControl TabControl;
        private System.Windows.Forms.Panel Panel_Dock;
        private System.Windows.Forms.RadioButton OpenMemo;
        private System.Windows.Forms.RadioButton Help;
        private System.Windows.Forms.RadioButton Settings;
        private System.Windows.Forms.RadioButton ImportTemplate;
        private System.Windows.Forms.PictureBox PictureBox_Logo;
        private System.Windows.Forms.Panel Panel_Select;
        private System.Windows.Forms.Panel Panel_All;
    }
}