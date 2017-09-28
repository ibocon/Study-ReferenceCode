using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace nemonic
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<Element, UserControl>))]
    public abstract class Element : UserControl
    {
        protected TabCtrl TabControl;
        protected Button Item;

        protected Label Title;

        protected string RootPath;

        public Element(TabCtrl control, string path)
        {
            this.TabControl = control;
            this.RootPath = path;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Element
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "Element";
            this.ResumeLayout(false);

        }
    }
}
