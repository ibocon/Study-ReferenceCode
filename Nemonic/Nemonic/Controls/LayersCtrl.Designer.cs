namespace nemonic
{
    partial class LayersCtrl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Layer_Template = new System.Windows.Forms.PictureBox();
            this.Layer_Screenshot = new nemonic.ScreenshotCtrl();
            this.Layer_TextField = new nemonic.TransparentRichText();
            this.Layer_Sticky = new nemonic.StickyCtrl();
            ((System.ComponentModel.ISupportInitialize)(this.Layer_Template)).BeginInit();
            this.SuspendLayout();
            // 
            // Layer_Template
            // 
            this.Layer_Template.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layer_Template.Cursor = System.Windows.Forms.Cursors.Default;
            this.Layer_Template.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layer_Template.Location = new System.Drawing.Point(12, 12);
            this.Layer_Template.Margin = new System.Windows.Forms.Padding(0);
            this.Layer_Template.Name = "Layer_Template";
            this.Layer_Template.Size = new System.Drawing.Size(288, 288);
            this.Layer_Template.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.Layer_Template.TabIndex = 1;
            this.Layer_Template.TabStop = false;
            this.Layer_Template.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ControlMouseEvent);
            // 
            // Layer_Screenshot
            // 
            this.Layer_Screenshot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Layer_Screenshot.BackColor = System.Drawing.Color.Transparent;
            this.Layer_Screenshot.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layer_Screenshot.Location = new System.Drawing.Point(12, 12);
            this.Layer_Screenshot.Margin = new System.Windows.Forms.Padding(0);
            this.Layer_Screenshot.Name = "Layer_Screenshot";
            this.Layer_Screenshot.Size = new System.Drawing.Size(288, 288);
            this.Layer_Screenshot.TabIndex = 4;
            this.Layer_Screenshot.Visible = false;
            // 
            // Layer_TextField
            // 
            this.Layer_TextField.AllowDrop = true;
            this.Layer_TextField.BackColor = System.Drawing.Color.Transparent;
            this.Layer_TextField.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Layer_TextField.DetectUrls = false;
            this.Layer_TextField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Layer_TextField.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Layer_TextField.Location = new System.Drawing.Point(12, 12);
            this.Layer_TextField.Margin = new System.Windows.Forms.Padding(0);
            this.Layer_TextField.MaxLength = 0;
            this.Layer_TextField.Name = "Layer_TextField";
            this.Layer_TextField.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.Layer_TextField.Size = new System.Drawing.Size(288, 288);
            this.Layer_TextField.TabIndex = 3;
            this.Layer_TextField.Text = "";
            this.Layer_TextField.TextChanged += new System.EventHandler(this.Layer_TextField_TextChanged);
            // 
            // Layer_Sticky
            // 
            this.Layer_Sticky.AllowDrop = true;
            this.Layer_Sticky.BackColor = System.Drawing.Color.Transparent;
            this.Layer_Sticky.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Layer_Sticky.Image = null;
            this.Layer_Sticky.Location = new System.Drawing.Point(0, 0);
            this.Layer_Sticky.Margin = new System.Windows.Forms.Padding(0);
            this.Layer_Sticky.Name = "Layer_Sticky";
            this.Layer_Sticky.Size = new System.Drawing.Size(312, 30);
            this.Layer_Sticky.TabIndex = 2;
            this.Layer_Sticky.TabStop = false;
            // 
            // LayersCtrl
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.Layer_Screenshot);
            this.Controls.Add(this.Layer_TextField);
            this.Controls.Add(this.Layer_Sticky);
            this.Controls.Add(this.Layer_Template);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LayersCtrl";
            this.Padding = new System.Windows.Forms.Padding(12);
            this.Size = new System.Drawing.Size(312, 312);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.LayersCtrl_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.LayersCtrl_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.Layer_Template)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StickyCtrl Layer_Sticky;
        private TransparentRichText Layer_TextField;
        private ScreenshotCtrl Layer_Screenshot;
        public System.Windows.Forms.PictureBox Layer_Template;
    }
}
