using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace nemonic
{
    public partial class ElementButton : Button
    {
        private static int BorderSize = 1;

        public ElementButton()
        {
            SetStyle(ControlStyles.StandardClick | ControlStyles.StandardDoubleClick, true);
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.FlatAppearance.BorderSize = ElementButton.BorderSize;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.FlatAppearance.BorderSize = 0;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (!this.Focused)
            {
                this.FlatAppearance.BorderSize = 0;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.FlatAppearance.BorderSize = ElementButton.BorderSize;
        }
    }
}
