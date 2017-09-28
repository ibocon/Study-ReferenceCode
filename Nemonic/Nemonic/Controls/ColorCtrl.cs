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
    public partial class ColorCtrl : CommonCtrl
    {
        public ColorCtrl()
        {
            InitializeComponent();

            ToolTip_White.SetToolTip(Button_White, "set memo to White theme");
            ToolTip_Yellow.SetToolTip(Button_Yellow, "set memo to Yellow theme");
            ToolTip_Pink.SetToolTip(Button_Pink, "set memo to Pink theme");
            ToolTip_Blue.SetToolTip(Button_Blue, "set memo to Blue theme");
            ToolTip_Green.SetToolTip(Button_Green, "set memo to Green theme");
        }

        private void Button_White_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeColor(ColorType.White);
        }

        private void Button_Yellow_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeColor(ColorType.Yellow);
        }

        private void Button_Pink_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeColor(ColorType.Pink);
        }

        private void Button_Blue_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeColor(ColorType.Blue);
        }

        private void Button_Green_Click(object sender, EventArgs e)
        {
            (this.Parent as MenuCtrl).ChangeColor(ColorType.Green);
        }

        private void Button_White_KeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as MenuCtrl).ControlKeyDown(sender, e);
        }
    }
}
