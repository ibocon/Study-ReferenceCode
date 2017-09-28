using System;
using System.Drawing;
using System.Windows.Forms;

namespace nemonic
{
    public partial class StickyCtrl : TransparentImage
    {
        public Sticky CurrentSticky;

        public StickyCtrl() : base()
        {
            CurrentSticky = Sticky.Top;
        }

        /// <summary>
        /// Sticky의 위치를 전환한다.
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="defualtSticky">if set to <c>true</c> [defualt sticky].</param>
        public void ChangeSticky(Paper paper, bool defualtSticky = false)
        {
            //Sticky 결정
            Sticky sticky = this.CurrentSticky;
            if (defualtSticky)
            {
                switch (paper)
                {
                    case Paper.p80x80:
                    case Paper.p56x80:
                    case Paper.p104x80:
                        sticky = Sticky.Top;

                        break;
                    case Paper.p80x104:
                    case Paper.p80x56:
                        sticky = Sticky.Left;
                        break;
                }
            }
            else
            {
                sticky = (Sticky)(((int)sticky + 1) % Enum.GetNames(typeof(Sticky)).Length);

                while (!StickyCtrl.IsAllowSticky(paper, sticky))
                {
                    sticky = (Sticky)(((int)sticky + 1) % Enum.GetNames(typeof(Sticky)).Length);
                }
            }
            //Sticky 반영
            this.ChangeSticky(paper, sticky);
        }

        private const int Thickness = 31;
        /// <summary>
        /// Sticky의 위치를 전환한다.
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="sticky">The sticky.</param>
        public void ChangeSticky(Paper paper, Sticky sticky)
        {
            Size formSize = NemonicApp.FormSize[paper];
            formSize.Height -= MenuCtrl.MenuHeight;
            //formSize.Height -= MenuCtrl.MenuHeight;

            switch (sticky)
            {
                case Sticky.Top:
                    this.Location = new Point(0, 0);
                    this.Size = new Size(formSize.Width, Thickness);
                    break;
                case Sticky.Right:
                    this.Location = new Point(formSize.Width - Thickness, 0);
                    this.Size = new Size(Thickness, formSize.Height);
                    break;
                case Sticky.Bottom:
                    this.Location = new Point(0, formSize.Height - Thickness);
                    this.Size = new Size(formSize.Width, Thickness);
                    break;
                case Sticky.Left:
                    this.Location = new Point(0, 0);
                    this.Size = new Size(Thickness, formSize.Height);
                    break;
            }
            CurrentSticky = sticky;
            //this.Validate();
        }

        /// <summary>
        /// Determines whether [is allow sticky] [the specified paper].
        /// 메모 용지 크기에 맞는 Sticky 위치인지 체크
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="sticky">The sticky.</param>
        /// <returns>
        ///   <c>true</c> if [is allow sticky] [the specified paper]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAllowSticky(Paper paper, Sticky sticky)
        {
            bool rt = false;
            switch (paper)
            {
                case (Paper.p80x80):
                    {
                        if (sticky == Sticky.Top)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Right)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Bottom)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Left)
                        {
                            rt = true;
                        }
                        break;
                    }
                case (Paper.p104x80):
                    {
                        if (sticky == Sticky.Top)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Right)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Bottom)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Left)
                        {
                            rt = false;
                        }
                        break;
                    }

                case (Paper.p56x80):
                    {
                        if (sticky == Sticky.Top)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Right)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Bottom)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Left)
                        {
                            rt = false;
                        }
                        break;
                    }

                case (Paper.p80x104):
                    {
                        if (sticky == Sticky.Top)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Right)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Bottom)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Left)
                        {
                            rt = true;
                        }
                        break;
                    }

                case (Paper.p80x56):
                    {
                        if (sticky == Sticky.Top)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Right)
                        {
                            rt = true;
                        }
                        else if (sticky == Sticky.Bottom)
                        {
                            rt = false;
                        }
                        else if (sticky == Sticky.Left)
                        {
                            rt = true;
                        }
                        break;
                    }
            }
            return rt;
        }

        //Stretch
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle canvas = new Rectangle(0, 0, this.Width, this.Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(10, Color.Black)), canvas);
            
            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                //rc.Inflate(0, 0);
                ControlPaint.DrawFocusRectangle(e.Graphics, rc);
            }
        }

        /// <summary>
        /// Sticky 레이어가 클릭 이벤트를 먹지 않도록 설정
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x0084;
            const int HTTRANSPARENT = (-1);

            if (m.Msg == WM_NCHITTEST)
            {
                m.Result = (IntPtr)HTTRANSPARENT;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void StickyCtrl_DragDrop(object sender, DragEventArgs e)
        {
            (this.Parent as LayersCtrl).Image_DragDrop(e);
        }

        private void StickyCtrl_DragEnter(object sender, DragEventArgs e)
        {
            (this.Parent as LayersCtrl).Image_DragEnter(e);
        }
    }
}
