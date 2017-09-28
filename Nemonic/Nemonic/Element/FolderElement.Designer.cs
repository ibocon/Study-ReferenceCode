namespace nemonic
{
    partial class FolderElement
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
            this.Button_Folder = new nemonic.ElementButton();
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
            this.Label_Title.Text = "Folder";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Button_Folder
            // 
            this.Button_Folder.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Button_Folder.BackgroundImage = global::nemonic.Properties.Resources.op_folder;
            this.Button_Folder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Folder.Dock = System.Windows.Forms.DockStyle.Top;
            this.Button_Folder.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Folder.FlatAppearance.BorderSize = 0;
            this.Button_Folder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Folder.Location = new System.Drawing.Point(0, 0);
            this.Button_Folder.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Folder.Name = "Button_Folder";
            this.Button_Folder.Size = new System.Drawing.Size(128, 128);
            this.Button_Folder.TabIndex = 3;
            this.Button_Folder.UseVisualStyleBackColor = false;
            // 
            // FolderElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.Button_Folder);
            this.Name = "FolderElement";
            this.Size = new System.Drawing.Size(128, 160);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label Label_Title;
        private ElementButton Button_Folder;
    }
}
