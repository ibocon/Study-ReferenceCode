namespace nemonic
{
    partial class MemoElement
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
            this.Button_Memo = new nemonic.ElementButton();
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
            this.Label_Title.TabIndex = 0;
            this.Label_Title.Text = "Memo";
            this.Label_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Label_Title.Visible = false;
            // 
            // Button_Memo
            // 
            this.Button_Memo.BackColor = System.Drawing.Color.White;
            this.Button_Memo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Button_Memo.Dock = System.Windows.Forms.DockStyle.Top;
            this.Button_Memo.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.Button_Memo.FlatAppearance.BorderSize = 0;
            this.Button_Memo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_Memo.Location = new System.Drawing.Point(0, 0);
            this.Button_Memo.Margin = new System.Windows.Forms.Padding(0);
            this.Button_Memo.Name = "Button_Memo";
            this.Button_Memo.Size = new System.Drawing.Size(128, 128);
            this.Button_Memo.TabIndex = 2;
            this.Button_Memo.UseVisualStyleBackColor = false;
            // 
            // MemoElement
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label_Title);
            this.Controls.Add(this.Button_Memo);
            this.DoubleBuffered = true;
            this.Name = "MemoElement";
            this.Size = new System.Drawing.Size(128, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label Label_Title;
        private ElementButton Button_Memo;
    }
}
