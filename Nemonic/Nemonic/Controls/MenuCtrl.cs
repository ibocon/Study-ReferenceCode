using System;
using System.Drawing;
using System.Windows.Forms;

namespace nemonic
{
    //실제 데이터 연산과 UI의 반응방법을 구분하기 위해, 이벤트만을 전달한다. 
    //WPF의 방식을 닮아가기 위해 노력중...
    public partial class MenuCtrl : UserControl
    {
        //Form의 Active 상태에 따라, 사이즈가 달라져서 계산이 어긋나는 문제를 해결하기 위해,
        //NemonicApp에서 실제 사이즈 측정을 위한 값. Designer 값과 반드시 일치 시킬 것!
        //개선할 수 있으면 개선이 필요한 코드
        public static int MenuHeight = 60;

        private Point startPoint = new Point(0, 0);
        private bool drag = false;

        public MenuCtrl()
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
            MenuHeight = this.Height;

            this.MouseDown += new MouseEventHandler(Menu_MouseDown);
            this.MouseUp += new MouseEventHandler(Menu_MouseUp);
            this.MouseMove += new MouseEventHandler(Menu_MouseMove);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            MenuHeight = this.Height;
        }

        public void Menu_MouseUp(object sender, MouseEventArgs e)
        {
            this.drag = false;
        }

        public void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            this.startPoint = e.Location;
            this.drag = true;
        }

        public void Menu_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.drag)
            { // if we should be dragging it, we need to figure out some movement
                Point p1 = new Point(e.X, e.Y);
                Point p2 = this.PointToScreen(p1);
                Point p3 = new Point(p2.X - this.startPoint.X,
                                     p2.Y - this.startPoint.Y);
                this.Parent.Location = p3;
            }
        }

        public enum SubCtrl
        {
            Select, Color, Utility, ScreenShot
        }

        public void ChangeSubCtrl(SubCtrl ctrl)
        {
            bool utility = false;
            bool select = false;
            bool color = false;
            bool screenshot = false;

            switch (ctrl)
            {
                case SubCtrl.Select:
                    select = true;
                    break;
                case SubCtrl.Utility:
                    utility = true;
                    break;
                case SubCtrl.Color:
                    color = true;
                    break;
                case SubCtrl.ScreenShot:
                    screenshot = true;
                    break;
            }

            this.SelectCtrl.Visible = select;
            this.SelectCtrl.Enabled = select;

            this.UtilityCtrl.Visible = utility;
            this.UtilityCtrl.Enabled = utility;

            this.ColorCtrl.Visible = color;
            this.ColorCtrl.Enabled = color;

            this.ScreenShotMenuCtrl.Visible = screenshot;
            this.ScreenShotMenuCtrl.Enabled = screenshot;

            if (select)
            {
                this.SelectCtrl.Focus();
            }
            else if (utility)
            {
                this.UtilityCtrl.Focus();
            }
            else if (color)
            {
                this.ColorCtrl.Focus();
            }
            else if (select)
            {
                this.ScreenShotMenuCtrl.Focus();
            }

        }

        //이벤트만을 전달하기 위한 함수들
        public void Close()
        {
            (this.Parent as NemonicForm).ClosedByUser();
        }

        public void TakeScreenShot()
        {
            (this.Parent as NemonicForm).TakeScreenShot();
        }

        public void SaveJson()
        {
            (this.Parent as NemonicForm).SaveJson(false);
        }

        public void OpenJson()
        {
            (this.Parent as NemonicForm).OpenSettings(SettingsForm.Tab.Memo);
        }

        public void ControlKeyDown(object sender, KeyEventArgs e)
        {
            (this.Parent as NemonicForm).ControlKeyDown(sender, e);
        }

        public void ChangeColor(ColorType type)
        {
            (this.Parent as NemonicForm).ChangeColor(type);
        }

        public void SpawnImage()
        {
            (this.Parent as NemonicForm).SpawnImage();
        }

        public void ChangeTemplate()
        {
            (this.Parent as NemonicForm).OpenSettings(SettingsForm.Tab.Template);
        }

        public void ChangePaper()
        {
            (this.Parent as NemonicForm).ChangePaper();
        }

        public void ChangeSticky()
        {
            (this.Parent as NemonicForm).ChangeSticky();
        }

        public void ToggleText()
        {
            (this.Parent as NemonicForm).ToggleText();
        }

        private void MenuCtrl_KeyDown(object sender, KeyEventArgs e)
        {
            this.ControlKeyDown(sender, e);
        }

        public void Print()
        {
            (this.Parent as NemonicForm).Print();
        }

        public void OpenNew()
        {
            (this.Parent as NemonicForm).OpenNew();
        }

        public void OpenSettings()
        {
            (this.Parent as NemonicForm).OpenSettings(SettingsForm.Tab.Option);
        }

        public void CancelScreenShot()
        {
            (this.Parent as NemonicForm).CancelScreenShot();
        }

        public void ChangePaperIcon(Paper paper)
        {
            this.UtilityCtrl.ChangePaperIcon(paper);
        }

        public void ChangeStickyIcon(Sticky sticky)
        {
            this.UtilityCtrl.ChangeStickyIcon(sticky);
        }

        public void ChangeTextIcon(bool isText)
        {
            this.SelectCtrl.ChangeTextIcon(isText);
        }


    }
}
