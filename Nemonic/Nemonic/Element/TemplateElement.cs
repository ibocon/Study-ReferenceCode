using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace nemonic
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<TemplateElement, Element>))]
    public partial class TemplateElement : Element
    {
        private Action HideSettings;

        public TemplateElement(TabCtrl control, string path, Action hide) : base(control, path)
        {
            InitializeComponent();
            this.Item = Button_Template;

            this.Item.DoubleClick += Item_DoubleClick;
            this.Item.KeyDown += Item_KeyDown;
            this.Item.BackgroundImage = NemonicApp.GetImage(this.RootPath);
            this.Item.BackgroundImageLayout = ImageLayout.Zoom;

            this.Title = Label_Title;
            this.Title.Text = Path.GetFileNameWithoutExtension(this.RootPath);

            this.HideSettings = hide;
        }

        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            //삭제기능
            /*
            if (e.KeyCode == Keys.Delete)
            {
                File.Delete(this.RootPath);
            }
            */
        }

        private void Item_DoubleClick(object sender, EventArgs e)
        {
            SettingsForm.App.LayersCtrl.ChangeTemplate(new Bitmap(this.Item.BackgroundImage));
            this.HideSettings();
        }
    }
}
