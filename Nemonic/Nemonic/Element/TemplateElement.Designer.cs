namespace nemonic
{
    partial class TemplateElement
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
            this.Label_Title = new System.Windows.Forms.Label();
            this.Button_Template = new nemonic.ElementButton();
            this.SuspendLayout();
            // 
            // Label_Title
            // 
            this.Label_Title.BackColor = System.Drawing.Color.Transparent;
            this.Label_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.Label_Title.Font = new System.Drawing.Font("Gulim", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Label_Title.Location = new System.Drawing.Point(0, 128);
            this.Label_Title.Margin = new System.Windows.Forms.Padding(0);
            this.Label_Title.Name = "Label_Title";
            this.Label_Title.Padding = new System.Windows.Forms.Padding(3);
            this.Label_Title.Size = new System.Drawing.Size(128, 30);
            this.Label_Title.TabIndex = 1;
            this.Label_Title.Text = "Template";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Title.Visible = false;
            // 
            // Button_Template
            // 
            this.Button_Template.BackColor = System.Drawing.Color.White;
            this.Button_Template.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Template.Dock = System.Windows.Forms.DockStyle.Top;
            this.Button_Template.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Template.FlatAppearance.BorderSize = 0;
            this.Button_Template.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Template.Location = new System.Drawing.Point(0, 0);
            this.Button_Template.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Template.Name = "Button_Template";
            this.Button_Template.Size = new System.Drawing.Size(128, 128);
            this.Button_Template.TabIndex = 2;
            this.Button_Template.UseVisualStyleBackColor = false;
            // 
            // TemplateElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.Button_Template);
            this.Name = "TemplateElement";
            this.Size = new System.Drawing.Size(128, 160);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private ElementButton Button_Template;
    }
}
