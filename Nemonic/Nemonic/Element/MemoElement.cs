using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.ComponentModel;

namespace nemonic
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<MemoElement, Element>))]
    public partial class MemoElement : Element
    {
        private Action HideSettings;

        //TODO: Json File 정보를 조합하여, 이미지를 만들어 반영해야 한다.
        public MemoElement(TabCtrl control, string path, Action hide) : base(control, path)
        {
            InitializeComponent();

            this.Item = Button_Memo;
            this.Item.DoubleClick += Item_DoubleClick;
            this.Item.KeyDown += Item_KeyDown;

            this.Title = Label_Title;
            this.Title.Text = Path.GetFileNameWithoutExtension(this.RootPath);

            //Json 파일에서 Image 파일을 생성하는 과정

            //바쁜 대기라, 매우 나쁜 코드지만 더 나은 코드가 생각나지 않는다.
            bool isReady = false;
            string json = string.Empty;

            while (!isReady)
            {
                try
                {
                    json = File.ReadAllText(this.RootPath);
                    isReady = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Busy Wait Error!\n\t" + e.StackTrace);
                    //isReady = false;
                }
            }
            //Json 정보 해석
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            List<JsonObject> objects = JsonConvert.DeserializeObject<List<JsonObject>>(json, settings);

            foreach (JsonObject obj in objects)
            {
                if (obj is Thumbnail)
                {
                    //Thumbnail 정보를 찾아, Element의 이미지로 세팅
                    Thumbnail thumbnail = obj as Thumbnail;
                    this.Item.BackgroundImage = NemonicApp.ByteToImage(thumbnail.image);
                    this.Item.BackgroundImageLayout = ImageLayout.Zoom;
                }
            }

            this.HideSettings = hide;

        }

        private void Item_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Shift && e.KeyCode == Keys.Delete)
            {
                DialogResult ans = MessageBox.Show(nemonic.Properties.Messages.Msg_DeleteWarning, "Warning", MessageBoxButtons.OKCancel);
                if (ans == DialogResult.OK)
                {
                    File.Delete(this.RootPath);
                }
            }
            else if (e.KeyCode == Keys.Delete)
            {
                RecybleBin.Send(this.RootPath, RecybleBin.FileOperationFlags.FOF_SILENT);
            }
        }

        private void Item_DoubleClick(object sender, EventArgs e)
        {
            /* 
             * Handle이 파괴되어, RichTextBoxIME에서 SendMessage를 할 때 에러가 생긴다.
             * 
            if (SettingsForm.App.LayersCtrl.IsEmptyMemo())
            {
                SettingsForm.App.OpenJson(path: this.RootPath);
            }
            else
            {
                SettingsForm.App.OpenNew(path: this.RootPath);
            }
            */
            SettingsForm.App.OpenNew(path: this.RootPath);
            this.HideSettings();
        }
    }
}
