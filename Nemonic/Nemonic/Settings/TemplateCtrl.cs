using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.IO;
using System.Collections;
using System.Windows.Forms;

namespace nemonic
{
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<TemplateCtrl, TabCtrl>))]
    public partial class TemplateCtrl : TabCtrl
    {
        private Action HideSettings;

        public TemplateCtrl(string path, Action hide) : base(path)
        {
            InitializeComponent();
            this.HideSettings = hide;
        }

        //최적화되지 않은 해결방안이다.
        protected override void CreateElement(object sender, FileSystemEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Template: CreateElement\t" + e.FullPath);
#endif
            this.InitializeElements();
        }

        protected override void DeleteElement(object sender, FileSystemEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Template: DeleteElement\t" + e.FullPath);
#endif
            this.InitializeElements();
        }

        protected override void RenamedElement(object sender, FileSystemEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Template: RenamedElement\t" + e.FullPath);
#endif
            this.InitializeElements();
        }

        protected override void ChangedElement(object sender, FileSystemEventArgs e)
        {
#if DEBUG
            Console.WriteLine("Template: ChangedElement\t" + e.FullPath);
#endif
            this.InitializeElements();
        }

        public override void InitializeElements(string location = "")
        {
            //Thread-Safe 코드를 작성하기 위해, Invoke를 활용
            if (this.ContainerPanel.InvokeRequired)
            {
                this.ContainerPanel.Invoke((MethodInvoker)delegate
                {
                    InitializeElements(location);
                });
            }
            else
            {
                this.ClearElements();

                string path = location;
                if (String.IsNullOrEmpty(location))
                {
                    path = this.Path;
                }
                //1. 템플릿 폴더의 존재 유무확인, 없다면 생성.
                /*
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                */
                
                //2. 템플릿 폴더 내부 자료 조사.
                if (!this.Path.Equals(path))
                {
                    this.ContainerPanel.Controls.Add(new FolderElement(this, Directory.GetParent(path).FullName));
                }

                //하위폴더 검색 및 추가
                foreach (string d in Directory.GetDirectories(path))
                {
                    //폴더 이미지 FlowLayout에 추가
                    //Console.WriteLine("Dictionary : " + d);

                    this.ContainerPanel.Controls.Add(new FolderElement(this, d));
                }

                //현재 폴더에 있는 파일목록
                IEnumerable files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly).Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));
                foreach (string f in files)
                {
                    //파일의 이미지 bitmap으로 전환해서, FlowLayout에 추가
                    //Console.WriteLine("Template : " + f);
                    TemplateElement element = new TemplateElement(this, f, this.HideSettings);
                    this.ContainerPanel.Controls.Add(element);
                }
            }
        }

        public override void ClearElements()
        {
            this.ContainerPanel.Controls.Clear();
        }

    }
}
