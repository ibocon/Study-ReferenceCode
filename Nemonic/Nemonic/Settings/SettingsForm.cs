using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace nemonic
{

    public partial class SettingsForm : Form
    {
        public delegate void HideForm();

        public enum Tab
        {
            Memo, Template, Option
        }

        public static NemonicForm App;

        private TabPage MemoTab;
        private MemoCtrl MemoCtrl;
        private TabPage TemplateTab;
        private TemplateCtrl TemplateCtrl;

        public SettingsForm(NemonicForm app)
        {
            InitializeComponent();

            SettingsForm.App = app;

            //TabPage를 미리 구상해서 저장해두고, 로딩하는 형식

            //MemoTab 구성
            this.MemoTab = new TabPage("Memo");
            this.MemoCtrl = new MemoCtrl(NemonicApp.MemoPath, this.Hide)
            {
                Dock = DockStyle.Fill
            };
            this.MemoTab.Controls.Add(MemoCtrl);
            this.MemoCtrl.InitializeElements();

            //TemplateTab 구성
            this.TemplateTab = new TabPage("Template");
            this.TemplateCtrl = new TemplateCtrl(NemonicApp.TemplatePath, this.Hide)
            {
                Dock = DockStyle.Fill
            };
            this.TemplateTab.Controls.Add(this.TemplateCtrl);
            this.TemplateCtrl.InitializeElements();

            this.SelectTab(Tab.Memo);
        }

        /// <summary>
        /// Selects the tab.
        /// </summary>
        /// <param name="tab">The tab.</param>
        public void SelectTab(Tab tab)
        {
            switch (tab)
            {
                case Tab.Template:
                    this.ImportTemplate.Checked = true;
                    break;
                case Tab.Option:
                    this.Settings.Checked = true;
                    break;
                case Tab.Memo:
                default:
                    this.OpenMemo.Checked = true;
                    break;
            }
        }

        /// <summary>
        /// 세팅 폼을 삭제하는 게 아니라, 숨김으로 세팅 폼을 열 때마다 초기화하지 않아도 된다.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Button_Close_Click(object sender, EventArgs e)
        {
            this.Hide();
            //this.Close();
        }

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

        /// <summary>
        /// Form의 BorderStyle이 None이라, 폼 위에서 마우스 클릭으로 움직일 수 있도록 세팅
        /// </summary>
        /// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1) //HTCLIENT
                        m.Result = (IntPtr)0x2; //HTCAPTION 
                    return;
            }

            base.WndProc(ref m);
        }

        //다른 메뉴 탭이 선택될 때마다 탭을 전환한다.
        private void OpenMemo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.OpenMemo.Checked)
            {
                if (!this.TabControl.Contains(MemoTab))
                {
                    this.TabControl.TabPages.Add(MemoTab);
                }
                this.OpenMemo.Image = global::nemonic.Properties.Resources.op_menu_meno_3;
                
            }
            else
            {
                this.TabControl.TabPages.Remove(MemoTab);
                this.OpenMemo.Image = global::nemonic.Properties.Resources.op_menu_meno_1;
            }
        }

        private void ImportTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ImportTemplate.Checked)
            {
                if (!this.TabControl.Contains(TemplateTab))
                {
                    this.TabControl.TabPages.Add(TemplateTab);
                }
                this.ImportTemplate.Image = global::nemonic.Properties.Resources.op_menu_temp_3;
            }
            else
            {
                this.TabControl.TabPages.Remove(TemplateTab);
                this.ImportTemplate.Image = global::nemonic.Properties.Resources.op_menu_temp_1;
            }
        }

        private void Settings_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Settings.Checked)
            {
                this.Settings.Image = global::nemonic.Properties.Resources.op_menu_set_3;
            }
            else
            {
                this.Settings.Image = global::nemonic.Properties.Resources.op_menu_set_1;
            }
        }

        private void Help_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Help.Checked)
            {
                this.Help.Image = global::nemonic.Properties.Resources.op_menu_help_3;
            }
            else
            {
                this.Help.Image = global::nemonic.Properties.Resources.op_menu_help_1;
            }
        }

        public void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Win32.ReleaseCapture();
                Win32.SendMessage(Handle, Win32.WM_NCLBUTTONDOWN, Win32.HT_CAPTION, 0);
            }
        }
        
        private void Panel_All_MouseMove(object sender, MouseEventArgs e)
        {
            this.MoveForm(sender, e);
        }

        public void ControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Hide();
            }
        }

        private void TabControl_KeyDown(object sender, KeyEventArgs e)
        {
            this.ControlKeyDown(sender, e);
        }

        private void Button_Close_KeyDown(object sender, KeyEventArgs e)
        {
            this.ControlKeyDown(sender, e);
        }

        private void Panel_Dock_MouseMove(object sender, MouseEventArgs e)
        {
            this.MoveForm(sender, e);
        }

        private void PictureBox_Logo_MouseMove(object sender, MouseEventArgs e)
        {
            this.MoveForm(sender, e);
        }
    }

    
}
