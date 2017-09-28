using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Management;
using System.Diagnostics;
using System.Threading;

namespace nemonic
{
    /// <summary>
    /// 페이퍼 타입을 표시하는 Enum
    /// </summary>
    public enum Paper
    {
        p80x80 = 0, p104x80 = 1, p56x80 = 2, p80x104 = 3, p80x56 = 4
    }

    //실제적인 연산이 이루어지는 ViewModel에 해당한다고 할 수 있다.    
    /// <summary>
    /// 메인 폼
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class NemonicForm : Form
    {
        /// <summary>
        /// The settings form
        /// </summary>
        public SettingsForm SettingsForm;

        /// <summary>
        /// The current paper
        /// </summary>
        public Paper CurrentPaper;
        /// <summary>
        /// The current color
        /// </summary>
        public ColorType CurrentColor;

        /// <summary>
        /// 저장되었는지를 체크하고 표시
        /// </summary>
        public bool IsSaved;
        /// <summary>
        /// Nemonic Form의 크기를 수정할 수 있는지 표시
        /// </summary>
        public bool AllowResize;

        /// <summary>
        /// Initializes a new instance of the <see cref="NemonicForm"/> class.
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="sticky">The sticky.</param>
        /// <param name="color">The color.</param>
        /// <param name="template">The template.</param>
        /// <param name="path">The path.</param>
        public NemonicForm(Paper paper = Paper.p80x80, Sticky sticky = Sticky.Top, ColorType color = ColorType.White, Image template = null, string path = null)
        {
            InitializeComponent();

            // Enable double duffering to stop flickering.
            /*
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            */
            
            this.CurrentPaper = paper;
            this.CurrentColor = color;

            this.SettingsForm = new SettingsForm(this);
            this.ActiveControl = this.LayersCtrl;

            //this.ChangePaper(this.CurrentPaper);

            this.IsSaved = false;
            this.AllowResize = false;

            this.saveJsonDialog.InitialDirectory = NemonicApp.MemoPath;
            this.openJsonDialog.InitialDirectory = NemonicApp.MemoPath;

            if (template != null)
            {
                this.LayersCtrl.ChangeTemplate(new Bitmap(template));
            }

            if (!string.IsNullOrEmpty(path))
            {
                string ext = Path.GetExtension(path);
                if (ext.Equals(NemonicApp.Extension))
                {
                    this.OpenJson(path);
                }
            }

            this.Size = NemonicApp.FormSize[this.CurrentPaper];
            this.Height -= nemonic.MenuCtrl.MenuHeight;
            this.LayersCtrl.ChangeSticky(this.CurrentPaper, sticky);
            this.MenuCtrl.ChangeColor(this.CurrentColor);
        }

        private bool isClosedByUser = false;
        public void ClosedByUser()
        {
            this.isClosedByUser = true;
            this.Close();
        }

        /// <summary>
        /// 컨트롤의 속성을 변경
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   //WS_EX_COMPOSITED
                cp.ExStyle |= 0x00080000;   //WS_EX_LAYERED
                cp.ClassStyle |= 0x00020000;    //CS_DROPSHADOW
                return cp;
            }
        }

        /*
        protected override void WndProc(ref Message m)
        {
            //Windows 메시지 코드
            const UInt32 WM_NCHITTEST = 0x0084;
            const UInt32 WM_MOUSEMOVE = 0x0200;

            const UInt32 HTBOTTOMRIGHT = 17;

            bool handled = false;

            if (this.AllowResize && (m.Msg == WM_NCHITTEST || m.Msg == WM_MOUSEMOVE))
            {
                Point screenPoint = new Point(m.LParam.ToInt32());
                Point clientPoint = this.PointToClient(screenPoint);

                Rectangle hitTestArea = this.Panel_Resize.RectangleToClient(this.Panel_Resize.ClientRectangle);
                if (hitTestArea.Contains(clientPoint))
                {
                    m.Result = (IntPtr)HTBOTTOMRIGHT;
                    handled = true;
                }
            }

            if (!handled)
                base.WndProc(ref m);
        }
        */

        /// <summary>
        /// 폼에 GDI를 활용해 추가적으로 그려야 할 외곽선을 그리도록 함.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Color.Black, ButtonBorderStyle.Solid);
        }

        /// <summary>
        /// 폼의 색을 지정된 컬러로 바꾼다.
        /// </summary>
        /// <param name="type">적용할 색의 정보가 담긴 구조체</param>
        public void ChangeColor(ColorType type)
        {
            this.CurrentColor = type;
            this.LayersCtrl.ChangeColor(type);
            foreach (Control ctrl in this.MenuCtrl.Controls)
            {
                if (ctrl is CommonCtrl)
                {
                    (ctrl as CommonCtrl).ChangeColor(type);
                }
            }

            this.Panel_Resize.BackColor = NemonicApp.MemoColors[type].Menu;
        }
        /// <summary>
        /// 용지 사이즈 변경
        /// </summary>
        /// <param name="paper">변경하는 용지 사이즈</param>
        //const int FormMargin = 26;
        public void ChangePaper(Paper paper)
        {
            this.Activate();
            this.Size = NemonicApp.FormSize[paper];
            this.CurrentPaper = paper;
            this.LayersCtrl.ChangeSticky(CurrentPaper, true);
            this.MenuCtrl.ChangePaperIcon(this.CurrentPaper);
        }

        /// <summary>
        /// 용지 사이즈 변경. 현재 용지의 다음 용지로 자동변경
        /// </summary>
        public void ChangePaper()
        {
            if (this.LayersCtrl.Layer_Template.Image != null)
            {
                DialogResult dr = MessageBox.Show(nemonic.Properties.Messages.Msg_ChangePaperWarning, "", MessageBoxButtons.OKCancel);
                if (dr == DialogResult.Cancel)
                {
                    return;
                }

                this.LayersCtrl.Layer_Template.Image = null;
            }

            Paper nextPaper = (Paper)(((int)this.CurrentPaper + 1) % Enum.GetNames(typeof(Paper)).Length);
            this.ChangePaper(nextPaper);
            this.LayersCtrl.ChangeSticky(this.CurrentPaper, true);
            this.LayersCtrl.ReOrderImages();

            this.Validate();
        }

        /// <summary>
        /// Resize 하는 버튼의 활성화를 토글
        /// </summary>
        public void ToggleResizeBtn()
        {
            /*
            if (this.LayersCtrl.isTransparent)
            {
                this.Panel_Resize.Enabled = false;
                this.Panel_Resize.Visible = false;
            }
            else
            {
                this.Panel_Resize.Enabled = true;
                this.Panel_Resize.Visible = true;
            }
            */

            this.Panel_Resize.Enabled = !this.Panel_Resize.Enabled;
            this.Panel_Resize.Visible = !this.Panel_Resize.Visible;
        }

        /// <summary>
        /// Changes the template from screen shot.
        /// </summary>
        public void TakeScreenShot()
        {
            ToggleResizeBtn();
            this.LayersCtrl.ChangeTemplateFromScreenShot(true);
        }

        /// <summary>
        /// Cancels the screen shot.
        /// </summary>
        public void CancelScreenShot()
        {
            ToggleResizeBtn();
            this.LayersCtrl.DoTransparent(false);
            this.ChangePaper(this.CurrentPaper);
        }

        private string GetFileSaveLocation(bool isAutoSave)
        {
            string path = NemonicApp.MemoPath;
            string filename = this.Text;

            if (isAutoSave && (filename.Equals("Untitled") || !File.Exists(path + filename)))
            {
                path = NemonicApp.TemporaryPath;
            }

            if (filename.Equals("Untitled"))
            {
                filename = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt");
            }

            return path + @"\" + filename + NemonicApp.Extension;
        }

        /// <summary>
        /// 현재 메모를 Json 형태로 저장
        /// </summary>
        /// <param name="thumbnail">프리뷰에 표시될 썸네일 이미지</param>
        /// <returns>메모가 저장된 경로</returns>
        public string SaveJson(bool isAutoSave, Image thumbnail = null)
        {
            if (this.LayersCtrl.IsEmptyMemo())
            {
                return null;
            }

            if (thumbnail == null)
            {
                thumbnail = this.LayersCtrl.TakeScreenShot();
            }

            //수동으로 저장할 때, Temporary 메모를 Memo 메모로 이동
            if (!isAutoSave)
            {
                string name = this.Text;
                string src = NemonicApp.TemporaryPath + @"\" + name + NemonicApp.Extension;
                string dest = NemonicApp.MemoPath + @"\" + name + NemonicApp.Extension;

                if (File.Exists(src))
                {
                    NemonicApp.AppContext.RemoveFormCondition(src);
                    File.Move(src, dest);
                }
            }

            //메모를 어디에 저장할 것인지 분류
            string saveLocation = this.GetFileSaveLocation(isAutoSave);
            //저장될 메모의 정보를 입력
            NemonicApp.AppContext.AddFormCondition(saveLocation, this.Location);

            Config config = new Config()
            {
                Paper = Convert.ToString(this.CurrentPaper),
                Color = Convert.ToString(this.CurrentColor)
            };
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            File.WriteAllText(saveLocation, JsonConvert.SerializeObject(this.LayersCtrl.SaveJson(config, thumbnail), Formatting.Indented, settings));
            
            this.Text = Path.GetFileNameWithoutExtension(saveLocation);
            this.IsSaved = true;

            thumbnail.Dispose();

            if (!isAutoSave)
            {
                this.ShowToastMessage(nemonic.Properties.Messages.Msg_SavedLog);
            }
            
            return saveLocation;
        }

        /// <summary>
        /// 상위 폴더와 하위 폴더의 관계가 맞는지 체크
        /// </summary>
        /// <param name="parentDir">상위 폴더 위치</param>
        /// <param name="childDir">하위 폴더 위치</param>
        /// <returns></returns>
        private bool CheckValidDirectory(string parentDir, string childDir)
        {
            DirectoryInfo parentDirInfo = new DirectoryInfo(parentDir);
            DirectoryInfo childDirInfo = new DirectoryInfo(childDir);
            bool isParent = false;
            while (childDirInfo != null)
            {
                if (childDirInfo.FullName == parentDirInfo.FullName)
                {
                    isParent = true;
                    break;
                }
                else
                {
                    childDirInfo = childDirInfo.Parent;
                } 
            }
            return isParent;
        }
        /// <summary>
        /// JSON 형태로 표현된 메모정보를 읽어 현재 메모에 로드
        /// </summary>
        /// <param name="path">XML 파일 위치</param>
        /// <param name="clearMemo">값이 <c>true</c>로 설정되면 [메모 초기화].</param>
        public void OpenJson(string path, bool clearMemo = true)
        {

            if (clearMemo)
            {
                this.LayersCtrl.ClearMemo(true, true, true);
            }

            string json = "";
            if (string.IsNullOrEmpty(path))
            {
                DialogResult result = this.openJsonDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    path = this.openJsonDialog.FileName;
                }
                else
                {
                    return;
                }
            }

            json = File.ReadAllText(path);
            this.Text = Path.GetFileNameWithoutExtension(path);

            if (!string.IsNullOrEmpty(json))
            {
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
                this.LayersCtrl.OpenJson(JsonConvert.DeserializeObject<List<JsonObject>>(json, settings));
            }

            this.IsSaved = true;
        }

        /// <summary>
        /// 메모에 이미지를 추가
        /// </summary>
        public void SpawnImage()
        {
            DialogResult result = openImageDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = openImageDialog.FileName;

                Bitmap target = NemonicApp.GetImage(path);
                if (target != null)
                {
                    this.LayersCtrl.AddImage(target, path);
                }
            }
        }
        /// <summary>
        /// Changes the sticky.
        /// </summary>
        public void ChangeSticky()
        {
            this.LayersCtrl.ChangeSticky(this.CurrentPaper);
            //this.MenuCtrl.ChangeStickyIcon(this.LayersCtrl.StickyCtrl.CurrentSticky);
        }
        /// <summary>
        /// Toggles the rich text.
        /// </summary>
        public void ToggleText()
        {
            this.MenuCtrl.ChangeTextIcon(this.LayersCtrl.ToggleRichText());
        }

        /// <summary>
        /// 메인 폼이 아니라, 컨트롤에 포커스가 맞춰져 있을 때 키 입력을 처리.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        public void ControlKeyDown(object sender, KeyEventArgs e)
        {
            //디버깅 코드
            //Console.WriteLine("KeyDowned");

            if (this.LayersCtrl.isTransparent && e.KeyCode == Keys.Escape)
            {
                this.LayersCtrl.DoTransparent(false);
            }
            else if (e.Control && e.KeyCode == Keys.P)
            {
                this.Print();
            }
            else if (e.Control && e.KeyCode == Keys.S)
            {
                this.SaveJson(false);
            }
            else if (e.Control && e.KeyCode == Keys.O)
            {
                this.OpenSettings(SettingsForm.Tab.Memo);
            }
        }

        /// <summary>
        /// Determines whether this instance is printable.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is printable; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPrintable()
        {
            return this.LayersCtrl.IsPrintable();
        }

        /// <summary>
        /// 메모를 출력한다.
        /// </summary>
        public void Print()
        {
            if (!IsPrintable())
            {
                MessageBox.Show(nemonic.Properties.Messages.Msg_NoContentError, "Warning", MessageBoxButtons.OK);
                return;
            }

            //Nemonic 프린터를 찾는 과정
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            foreach (ManagementObject printer in searcher.Get())
            {
                string printerName = printer["Name"].ToString().ToLower();

                if (printerName.Equals("nemonic mip-001"))
                {
                    if ((bool)printer["WorkOffline"])
                    {
                        this.ShowPrintDialog();
                    }
                    else
                    {
#if DEBUG
                        //스크린샷 프린트 방식
                        using (NemonicDocument document = new NemonicDocument(printer["Name"].ToString(), this.CreateGraphics(), this.CurrentPaper, this.LayersCtrl, true))
                        {
                            document.Print();
                        }
#endif
                        //레이어를 그리는 방식
                        using (NemonicDocument document = new NemonicDocument(printer["Name"].ToString(), this.CreateGraphics(), this.CurrentPaper, this.LayersCtrl, false))
                        {
                            document.Print();
                        }
                    }
                    return;
                }
            }

            this.ShowPrintDialog();
        }

        private void ShowPrintDialog()
        {
            DialogResult rs = MessageBox.Show(nemonic.Properties.Messages.Msg_NoNemonicPrinterError, "", MessageBoxButtons.YesNo);
            if (rs == DialogResult.Yes)
            {
                DialogResult ans = this.printDialog.ShowDialog();
                if (ans == DialogResult.OK)
                {
                    using (NemonicDocument print = new NemonicDocument(this.printDialog.PrinterSettings.PrinterName, this.CreateGraphics(), this.CurrentPaper, this.LayersCtrl, false))
                    {
                        print.Print();
                    }
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// 새로운 메모장을 연다.
        /// </summary>
        /// <param name="templatepath">적용할 템플릿 이미지</param>
        /// <param name="path">새 메모장에 로드할 JSON 파일 위치</param>
        public void OpenNew(string templatepath = "null", string path = "null")
        {
            Paper paper = this.CurrentPaper;
            Sticky sticky = this.LayersCtrl.StickyCtrl.CurrentSticky;
            ColorType color = this.CurrentColor;
            Image template = null;
            if (!templatepath.Equals("null"))
            {
                template = Image.FromFile(templatepath);
            }
            NemonicForm form = new NemonicForm(paper, sticky, color, template, path);
            NemonicApp.AppContext.OpenNew(form);
            
        }

        /// <summary>
        /// 세팅 폼을 연다.
        /// </summary>
        /// <param name="tab">The tab.</param>
        public void OpenSettings(SettingsForm.Tab tab = SettingsForm.Tab.Memo)
        {
            //this.SettingsForm = new SettingsForm(this);
            this.SettingsForm.SelectTab(tab);
            this.SettingsForm.Show();
        }

        /// <summary>
        /// Handles the Activated event of the NemonicForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void NemonicForm_Activated(object sender, EventArgs e)
        {
            this.MenuCtrl.Visible = true;
            this.MenuCtrl.Enabled = true;

            //this.Size = NemonicApp.FormSize[this.CurrentPaper];
            this.Height += nemonic.MenuCtrl.MenuHeight;
            this.Location = new Point(this.Location.X, this.Location.Y - nemonic.MenuCtrl.MenuHeight);
        }

        /// <summary>
        /// Handles the Deactivate event of the NemonicForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        private void NemonicForm_Deactivate(object sender, EventArgs e)
        {
            this.MenuCtrl.Visible = false;
            this.MenuCtrl.Enabled = false;

            //this.Size = NemonicApp.FormSize[this.CurrentPaper];
            //메뉴가 사라지면, 높이가 줄어야 한다.
            this.Height -= nemonic.MenuCtrl.MenuHeight;
            this.Location = new Point(this.Location.X, this.Location.Y + nemonic.MenuCtrl.MenuHeight);
            if (!IsSaved && !this.LayersCtrl.IsEmptyMemo() && !this.isClosedByUser)
            {
                string path = this.SaveJson(true);
            }
        }

        /// <summary>
        /// 메모가 닫히기 전에, 메모가 저장이 되었는지 확인한다.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (!IsSaved && !this.LayersCtrl.IsEmptyMemo())
            {
                
                using (Image thumbnail = this.LayersCtrl.TakeScreenShot())
                {
                    if (this.isClosedByUser)
                    {
                        DialogResult result = MessageBox.Show(nemonic.Properties.Messages.Msg_AskSavingOrNot, "", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            this.SaveJson(false, thumbnail);
                        }
                    }
                }
            }

            if (this.isClosedByUser)
            {
                string loc = NemonicApp.MemoPath + @"\" + this.Text + NemonicApp.Extension;
                if (!File.Exists(loc))
                {
                    loc = loc.Replace(NemonicApp.MemoPath, NemonicApp.TemporaryPath);
                }
                NemonicApp.AppContext.RemoveFormCondition(loc);
            }

            this.SettingsForm.Close();
        }

        /// <summary>
        /// Handles the MouseDown event of the Panel_Resize button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        private void Panel_Resize_MouseDown(object sender, MouseEventArgs e)
        {
            this.Panel_Resize.Cursor = Cursors.SizeNWSE;
            this.PreMouseLocation = e.Location;
            this.PreFormSize = this.Size;
        }

        private Point PreMouseLocation;
        private Size PreFormSize;
        private void Panel_Resize_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.Panel_Resize.Cursor == Cursors.SizeNWSE)
            {
                int changeWidth = (e.Location.X - this.PreMouseLocation.X) + this.PreFormSize.Width;
                int changeHeight = (int)((changeWidth) * ((float)(NemonicApp.FormSize[this.CurrentPaper].Height) / NemonicApp.FormSize[this.CurrentPaper].Width));
                Size PostFormSize = new Size(changeWidth, changeHeight);
                
                if (PostFormSize.Width >= NemonicApp.FormSize[this.CurrentPaper].Width && PostFormSize.Height >= NemonicApp.FormSize[this.CurrentPaper].Height)
                {
                    this.Size = PostFormSize;
                    this.PreFormSize = this.Size;
                }

                
                this.Validate();
            }
        }

        /// <summary>
        /// Handles the MouseUp event of the Panel_Resize control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.MouseEventArgs" /> instance containing the event data.</param>
        private void Panel_Resize_MouseUp(object sender, MouseEventArgs e)
        {
            this.Panel_Resize.Cursor = Cursors.Default;
        }

        public void ShowToastMessage(string message)
        {
            this.Label_ToastMessage.Text = message;

            this.Label_ToastMessage.Visible = true;
            this.Label_ToastMessage.Enabled = true;

            
            //TODO: TimerCallBack으로 해결해야 하는데, Thread 문제가 일어난다.
            System.Threading.Thread.Sleep(500);

            this.Label_ToastMessage.Visible = false;
            this.Label_ToastMessage.Enabled = false;
            
            
            //TimerCallback TimerDelegate = new TimerCallback(DisableToastMessage);
            //System.Threading.Timer TimerItem = new System.Threading.Timer(TimerDelegate, Label_ToastMessage, 1000, Timeout.Infinite);
            
        }

        private static void DisableToastMessage(object label)
        {
            if (label is Label)
            {
                Label lb = label as Label;
                if (lb.InvokeRequired)
                {
                    lb.Invoke((MethodInvoker)delegate
                    {
                        DisableToastMessage(lb);
                    });
                }
                else
                {
                    lb.Visible = false;
                    lb.Enabled = false;
                }
            }
            
        }

        private void MenuCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            this.ControlKeyDown(sender, e);
        }

        private void NemonicForm_LocationChanged(object sender, EventArgs e)
        {
            
            if (NemonicApp.AppContext != null && !this.LayersCtrl.IsEmptyMemo())
            {
                //메모 위치가 변경된 정보를 입력
                NemonicApp.AppContext.ChangeFormCondition(this.GetFileSaveLocation(true), this.Location);
            }
            
        }
    }
}
