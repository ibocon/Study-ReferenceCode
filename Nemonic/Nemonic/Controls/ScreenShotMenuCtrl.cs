using System;

namespace nemonic.Controls
{
    public partial class ScreenShotMenuCtrl : CommonCtrl
    {
        public ScreenShotMenuCtrl()
        {
            InitializeComponent();
            ToolTip_Back.SetToolTip(Button_Back, nemonic.Properties.Messages.Tip_Cancel);
            ToolTip_ScreenShot.SetToolTip(Button_ScreenShot, nemonic.Properties.Messages.Tip_Screenshot);
            ToolTip_Print.SetToolTip(Button_Print, nemonic.Properties.Messages.Tip_PrintInst);
        }

        protected override void Menu_Leave(object sender, EventArgs e)
        {
            //(this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
        }

        private void Button_ScreenShot_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
            (this.Parent as MenuCtrl).TakeScreenShot();
        }

        private void Button_Print_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
            this.Button_ScreenShot_Click(sender, e);
            (this.Parent as MenuCtrl).Print();
        }

        private void Button_Back_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
            (this.Parent as MenuCtrl).CancelScreenShot();
        }
    }
}
