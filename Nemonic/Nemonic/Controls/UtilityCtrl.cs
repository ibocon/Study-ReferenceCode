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
    public partial class UtilityCtrl : CommonCtrl
    {

        public UtilityCtrl()
        {
            InitializeComponent();
            ToolTip_Save.SetToolTip(Button_Save, nemonic.Properties.Messages.Tip_Save);
            ToolTip_Load.SetToolTip(Button_Load, nemonic.Properties.Messages.Tip_LoadMemo);
            ToolTip_Image.SetToolTip(Button_Image, nemonic.Properties.Messages.Tip_LoadImage);
            ToolTip_Template.SetToolTip(Button_Template, nemonic.Properties.Messages.Tip_ChangeTemplate);
            ToolTip_Paper.SetToolTip(Button_Paper, nemonic.Properties.Messages.Tip_ChangePaper);
            ToolTip_Sticky.SetToolTip(Button_Sticky, nemonic.Properties.Messages.Tip_ChangeSticky);
        }

        private void Button_Save_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).SaveJson();
        }

        private void Button_Open_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).OpenJson();
        }

        private void Button_Image_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).SpawnImage();
        }

        private void Button_Template_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeTemplate();
        }

        private void Button_Paper_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangePaper();
        }

        private void Button_Sticky_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeSticky();
        }
        
        private void Button_Settings_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).OpenSettings();
        }

        public void ChangePaperIcon(Paper paper)
        {
            switch (paper)
            {
                case Paper.p80x80:
                    this.Button_Paper.BackgroundImage = nemonic.Properties.Resources.icon_menu_paper_regular;
                    break;
                case Paper.p80x56:
                case Paper.p56x80:
                    this.Button_Paper.BackgroundImage = nemonic.Properties.Resources.icon_menu_paper_small;
                    break;
                case Paper.p80x104:
                case Paper.p104x80:
                    this.Button_Paper.BackgroundImage = nemonic.Properties.Resources.icon_menu_paper_large;
                    break;
            }
        }

        public void ChangeStickyIcon(Sticky sticky)
        {
            switch (sticky)
            {
                case Sticky.Top:
                    this.Button_Sticky.BackgroundImage = nemonic.Properties.Resources.icon_menu_sticky_top;
                    break;
                case Sticky.Left:
                    this.Button_Sticky.BackgroundImage = nemonic.Properties.Resources.icon_menu_sticky_left;
                    break;
                case Sticky.Bottom:
                    this.Button_Sticky.BackgroundImage = nemonic.Properties.Resources.icon_menu_sticky_bottom;
                    break;
                case Sticky.Right:
                    this.Button_Sticky.BackgroundImage = nemonic.Properties.Resources.icon_menu_sticky_right;
                    break;
            }
        }

        private void Button_Save_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }
    }
}
