using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace nemonic
{
    public partial class TitleCtrl : CommonCtrl
    {
        public TitleCtrl()
        {
            InitializeComponent();
            ToolTip_New.SetToolTip(this.Button_New, nemonic.Properties.Messages.Tip_New);
            ToolTip_Close.SetToolTip(this.Button_Close, nemonic.Properties.Messages.Tip_Close);
        }

        protected override void Menu_Leave(object sender, EventArgs e)
        {
            //(this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
        }

        private void Button_New_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).OpenNew();
        }

        private void Button_Close_Click(object sender, EventArgs e)
        {
            //접근 방식이 고정적인 방법이라, 조금만 변경되도 에러를 일으킬 확률이 높다.
            (this.Parent as MenuCtrl).Close();
        }

        private void Button_New_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }

        private void TitleCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }
    }
}
