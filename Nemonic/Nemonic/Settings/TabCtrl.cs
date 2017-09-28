using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace nemonic
{
    /// <summary>
    /// MemoCtrl과 TemplateCtrl처럼 Tab 컨트롤이 필요한 함수를 정의
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    [TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<TabCtrl, UserControl>))]
    public abstract class TabCtrl : UserControl
    {
        protected string Path;
        protected FileSystemWatcher Watcher;

        /// <summary>
        /// Initializes a new instance of the <see cref="TabCtrl"/> class.
        /// 컨트롤이 체크하는 폴더 위치에 FileSystemWatcher를 설정하고, Event Handler를 등록.
        /// </summary>
        /// <param name="path">The path.</param>
        protected TabCtrl(string path)
        {
            this.Path = path;

            /*
            if (!Directory.Exists(this.Path))
            {
                Directory.CreateDirectory(this.Path);
            }
            */

            this.Watcher = new FileSystemWatcher(this.Path)
            {
                NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                Filter = "*.*",
                IncludeSubdirectories = true,
                EnableRaisingEvents = true,
            };

            
            this.Watcher.Created += new FileSystemEventHandler(CreateElement);
            this.Watcher.Deleted += new FileSystemEventHandler(DeleteElement);
            this.Watcher.Renamed += new RenamedEventHandler(RenamedElement);
            this.Watcher.Changed += new FileSystemEventHandler(ChangedElement);

        }

        /// <summary>
        /// Changeds the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        protected abstract void ChangedElement(object sender, FileSystemEventArgs e);

        /// <summary>
        /// Creates the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        protected abstract void CreateElement(object sender, FileSystemEventArgs e);
        /// <summary>
        /// Deletes the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        protected abstract void DeleteElement(object sender, FileSystemEventArgs e);
        /// <summary>
        /// Renameds the element.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        protected abstract void RenamedElement(object sender, FileSystemEventArgs e);

        /// <summary>
        /// Clears the elements.
        /// </summary>
        public abstract void ClearElements();

        /// <summary>
        /// TabCtrl에 포함되야 하는 요소들을 검색하여 초기화
        /// </summary>
        /// <param name="sub">The sub.</param>
        public abstract void InitializeElements(string sub = "");

        /// <summary>
        /// Initializes the component.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TabCtrl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Name = "TabCtrl";
            this.ResumeLayout(false);

        }
    }
}
