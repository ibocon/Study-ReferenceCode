using System;
using System.Windows.Forms;

namespace nemonic
{
    /// <summary>
    /// 새로 작성해서 반영하는 컨트롤에 공통적으로 필요한 함수들을 표현
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class CommonCtrl : UserControl
    {
        public CommonCtrl()
        {
            //InitializeComponent();

            // Enable double duffering to stop flickering.
            /*
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            */

            this.MouseDown += new MouseEventHandler(Menu_MouseDown);
            this.MouseUp += new MouseEventHandler(Menu_MouseUp);
            this.MouseMove += new MouseEventHandler(Menu_MouseMove);
            this.Leave += new EventHandler(Menu_Leave);
            this.KeyDown += new KeyEventHandler(Menu_KeyDown);
        }

        protected void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            (this.Parent as MenuCtrl).Menu_MouseUp(sender, e);
        }

        protected void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            (this.Parent as MenuCtrl).Menu_MouseDown(sender, e);
        }

        protected void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            (this.Parent as MenuCtrl).Menu_MouseMove(sender, e);
        }

        protected void Menu_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }

        /// <summary>
        /// Handles the Leave event of the Menu control.
        /// 마우스가 컨트롤을 벗어날 때, 메뉴가 변경될 수 있도록 세팅
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void Menu_Leave(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
        }

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="type">The type.</param>
        public virtual void ChangeColor(ColorType type)
        {
            Colors colors = NemonicApp.MemoColors[type];
            //Main
            this.BackColor = colors.Menu;

            //Button
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button)
                {
                    (ctrl as Button).FlatAppearance.MouseDownBackColor = colors.Hover;
                    (ctrl as Button).FlatAppearance.MouseOverBackColor = colors.Hover;
                }
            }
        }
    }
}
