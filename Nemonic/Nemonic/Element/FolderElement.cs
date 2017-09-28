using System.Windows.Forms;
using System.IO;
using System.ComponentModel;

namespace nemonic
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<FolderElement, Element>))]
    public partial class FolderElement : Element
    {
        public FolderElement(TabCtrl control, string path) : base(control, path)
        {
            InitializeComponent();
            this.Item = Button_Folder;

            this.Item.DoubleClick += Item_DoubleClick;

            this.Title = Label_Title;
            if (this.RootPath.Equals(NemonicApp.TemplatePath) || this.RootPath.Equals(NemonicApp.MemoPath))
            {
                this.Title.Text = "Show all";
            }
            else
            {
                this.Title.Text = new DirectoryInfo(this.RootPath).Name;
            }
            
        }

        private void Item_DoubleClick(object sender, System.EventArgs e)
        {
            this.TabControl.ClearElements();
            this.TabControl.InitializeElements(this.RootPath);
        }
    }
}
