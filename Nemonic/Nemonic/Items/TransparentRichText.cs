using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace nemonic
{
    public partial class TransparentRichText : RichTextBoxIME
    {
        private FontStyle style;

        public class RichTextLayer : Layers
        {
            public string text;
            public Font font;
            public HorizontalAlignment align;
        }

        public TransparentRichText()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.ResizeRedraw | ControlStyles.Selectable, true);
            BackColor = Color.Transparent;
            this.AllowDrop = true;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //This makes the control's background transparent
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= 0x20;
                return CP;
            }
        }
        
        protected override void OnMouseClick(MouseEventArgs e)
        {
            (this.Parent as LayersCtrl).ControlMouseEvent(this, e);
        }
        
        protected override void OnKeyDown(KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.B)
            {
                e.Handled = true;

                if (this.Font.Bold)
                {
                    style ^= FontStyle.Bold;
                }
                else
                {
                    style |= FontStyle.Bold;
                }
                this.Font = new Font(this.Font, style);
            }

            /*
            else if (e.Control && e.KeyCode == Keys.I)
            {
                if (this.Font.Italic)
                {
                    style ^= FontStyle.Italic;
                }
                else
                {
                    style |= FontStyle.Italic;
                }
                this.Font = new Font(this.Font, style);
                
            }
            else if (e.Control && e.KeyCode == Keys.U)
            {

                if (this.Font.Underline)
                {
                    style ^= FontStyle.Underline;
                }
                else
                {
                    style |= FontStyle.Underline;
                }
                this.Font = new Font(this.Font, style);
            }
            else if (e.Control && e.KeyCode == Keys.T)
            {
                if (this.Font.Strikeout)
                {
                    style ^= FontStyle.Strikeout;
                }
                else
                {
                    style |= FontStyle.Strikeout;
                }
                this.Font = new Font(this.Font, style);
            }
            else if (e.Control && e.KeyCode == Keys.R)
            {
                this.SelectAll();
                this.SelectionAlignment = HorizontalAlignment.Right;
                this.DeselectAll();
            }
            else if (e.Control && e.KeyCode == Keys.E)
            {
                this.SelectAll();
                this.SelectionAlignment = HorizontalAlignment.Center;
                this.DeselectAll();
            }
            else if (e.Control && e.KeyCode == Keys.L)
            {
                this.SelectAll();
                this.SelectionAlignment = HorizontalAlignment.Left;
                this.DeselectAll();
            }
            */
            else if (e.Control && e.Shift && e.KeyCode == Keys.Oemcomma)
            {
                float size = this.Font.Size - 1;
                if (size >= 8)
                {
                    this.Font = new Font(this.Font.FontFamily, size);
                }
                e.Handled = true;
            }
            else if (e.Control && e.Shift && e.KeyCode == Keys.OemPeriod)
            {
                
                float size = this.Font.Size + 1;
                this.Font = new Font(this.Font.FontFamily, size);
                e.Handled = true;
            }
            

            //프린트
            else if (e.Control && e.KeyCode == Keys.P)
            {
                ((this.Parent as LayersCtrl).Parent as NemonicForm).Print();
            }
            //저장
            else if (e.Control && e.KeyCode == Keys.S)
            {
                ((this.Parent as LayersCtrl).Parent as NemonicForm).SaveJson(false);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                ((this.Parent as LayersCtrl).Parent as NemonicForm).OpenSettings(SettingsForm.Tab.Memo);
            }
            else if (e.Control && e.KeyCode == Keys.V)
            {
                // suspend layout to avoid blinking
                this.SuspendLayout();

                // get insertion point
                int insPt = this.SelectionStart;

                // preserve text from after insertion pont to end of RTF content
                string postRTFContent = this.Text.Substring(insPt);

                // remove the content after the insertion point
                this.Text = this.Text.Substring(0, insPt);

                // add the clipboard content and then the preserved postRTF content
                this.Text += (string)Clipboard.GetData("Text") + postRTFContent;

                // adjust the insertion point to just after the inserted text
                this.SelectionStart = this.TextLength - postRTFContent.Length;

                // restore layout
                this.ResumeLayout();

                // cancel the paste
                e.Handled = true;
            }
        }
        
        #region ImageDragEvent
        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            (this.Parent as LayersCtrl).Image_DragEnter(drgevent);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            (this.Parent as LayersCtrl).Image_DragDrop(drgevent);
        }
        #endregion
        
        #region FixScroll
        private const UInt32 SB_TOP = 0x6;
        private const UInt32 WM_VSCROLL = 0x115;
        private const UInt32 SB_LEFT = 0x06;
        private const UInt32 WM_HSCROLL = 0x0114;

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        
        protected override void OnTextChanged(EventArgs e)
        {
            //FixScroll();
            base.OnTextChanged(e);
        }
        
        protected override void OnVScroll(EventArgs e)
        {
            base.OnVScroll(e);
            //FixScroll();
        }

        private void FixScroll()
        {
            PostMessage(this.Handle, WM_VSCROLL, (IntPtr)SB_TOP, IntPtr.Zero);
            PostMessage(this.Handle, WM_HSCROLL, (IntPtr)SB_LEFT, IntPtr.Zero);
        }
        #endregion

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            //Height을 넘어가면 e.Handled = true 시전!
            /*
            Graphics g = this.CreateGraphics();
            SizeF size = g.MeasureString(this.Text, this.Font, this.Size);

            if (size.Height > this.Height)
            {
                e.Handled = true;
            }
            */
            
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FixScroll();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FixScroll();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            //this.SelectionStart = this.Text.Length;
            this.Focus();
        }
    }
}
