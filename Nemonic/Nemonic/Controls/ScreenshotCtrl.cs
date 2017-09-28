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
    /// <summary>
    /// 스크린샷 모드일 때, 메모의 컨트롤
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ScreenshotCtrl : UserControl
    {
        public ScreenshotCtrl()
        {
            InitializeComponent();

            // Enable double duffering to stop flickering.
            /*
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Opaque, false);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            */
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                //cp.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return cp;
            }
        }

        private void Button_ScreenShot_Click(object sender, EventArgs e)
        {
            (this.Parent as LayersCtrl).ChangeTemplateFromScreenShot();
        }

        private void Button_Print_Click(object sender, EventArgs e)
        {
            this.Button_ScreenShot_Click(sender, e);
            (this.Parent as LayersCtrl).Print();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            (this.Parent as LayersCtrl).DoTransparent(false);
        }

        private void ScreenshotCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Button_Cancel_Click(sender, e);
            }
        }
    }
}
