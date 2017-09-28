using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nemonic
{
    public partial class SelectCtrl : CommonCtrl
    {
        public SelectCtrl()
        {
            InitializeComponent();
            ToolTip_Utility.SetToolTip(Button_Utility, nemonic.Properties.Messages.Tip_UtilityTab);
            ToolTip_Color.SetToolTip(Button_Color, nemonic.Properties.Messages.Tip_ColorTab);
            ToolTip_Text.SetToolTip(Button_Text, nemonic.Properties.Messages.Tip_ToggleText);
            ToolTip_ScreenShot.SetToolTip(Button_ScreenShot, nemonic.Properties.Messages.Tip_ScreenshotTab);
            ToolTip_Print.SetToolTip(Button_Print, nemonic.Properties.Messages.Tip_Print);
        }

        private void Button_ScreenShot_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.ScreenShot);
            (this.Parent as MenuCtrl).TakeScreenShot();
        }

        //TODO: Print Driver가 작동하는 방식을 이해한 뒤에, 작업해야 한다.
        private void Button_Print_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).Print();
        }

        private void Button_Color_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Color);
        }

        private void Button_Utility_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Utility);
        }

        protected override void Menu_Leave(object sender, EventArgs e)
        {
            //(this.Parent as MenuCtrl).ChangeSubCtrl(MenuCtrl.SubCtrl.Select);
        }

        private void SelectCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }

        private void Button_Utility_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }

        private void Button_Text_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ToggleText();
        }

        private void Button_ScreenShot_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }

        public void ChangeTextIcon(bool isText)
        {
            if (isText)
            {
                this.Button_Text.BackgroundImage = nemonic.Properties.Resources.icon_menu_editmode_t;
            }
            else
            {
                this.Button_Text.BackgroundImage = nemonic.Properties.Resources.icon_menu_editmode_i;
            }
        }
    }
}
