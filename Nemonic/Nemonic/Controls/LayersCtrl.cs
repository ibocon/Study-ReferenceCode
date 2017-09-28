using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.IO;

namespace nemonic
{
    public enum Sticky
    {
        Top = 0, Left = 1, Bottom = 2, Right = 3
    }

    public enum Layer
    {
        Text = 0, Drawing = 1, Image = 2, Template = 3
    }

    /// <summary>
    /// 프로그램의 핵심이 담겨 있는 컨트롤.
    /// 모든 레이어를 통제하는 역할을 한다.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class LayersCtrl : UserControl
    {
        //public Sticky CurrentSticky;
        public Color PaddingColor;

        public bool isTransparent;

        /// <summary>
        /// 모든 레이어의 정보를 가지고 있다.
        /// </summary>
        public LinkedList<Control> CtrlList;

        /// <summary>
        /// 스티커의 위치를 표시하는 레이어
        /// </summary>
        public StickyCtrl StickyCtrl;

        public LayersCtrl()
        {
            InitializeComponent();

            // Enable double duffering to stop flickering.
            /*
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Opaque, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            */
            this.isTransparent = false;
            this.CtrlList = new LinkedList<Control>();

            this.Controls.Add(this.Layer_Template);
            this.Controls.SetChildIndex(this.Layer_Template, 3);
            CtrlList.AddLast(this.Layer_Template);
            this.Layer_Template.SizeMode = PictureBoxSizeMode.StretchImage;

            this.Controls.Add(this.Layer_Sticky);
            this.Controls.SetChildIndex(this.Layer_Sticky, 2);
            CtrlList.AddLast(this.Layer_Sticky);

            this.Controls.Add(this.Layer_TextField);
            this.Controls.SetChildIndex(this.Layer_TextField, 1);
            CtrlList.AddLast(this.Layer_TextField);

            this.Controls.Add(this.Layer_Screenshot);
            this.Controls.SetChildIndex(this.Layer_Screenshot, 0);
            CtrlList.AddLast(this.Layer_Screenshot);

            ControlManager.ControlMoverOrResizer.WorkType = ControlManager.ControlMoverOrResizer.MoveOrResize.MoveAndResize;

            this.Layer_Sticky.ChangeSticky(Paper.p80x80, Sticky.Top);

            this.ActiveControl = this.Layer_TextField;
            this.Layer_TextField.SelectionStart = this.Layer_TextField.Text.Length;
            this.Layer_TextField.Focus();

            this.StickyCtrl = this.Layer_Sticky;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            e.Graphics.DrawRectangle(new Pen(PaddingColor, this.Padding.All * 2), this.ClientRectangle);
        }

        /// <summary>
        /// Deletes the control.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        public void DeleteCtrl(Control ctrl)
        {
            this.Controls.Remove(ctrl);
            CtrlList.Remove(ctrl);
            (this.Parent as NemonicForm).IsSaved = false;
        }

        /// <summary>
        /// Adds the control.
        /// </summary>
        /// <param name="ctrl">The control.</param>
        protected void AddCtrl(Control ctrl)
        {
            this.Controls.Add(ctrl);
            if (ctrl is TransparentImage)
            {
                for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
                {
                    if (node.Value == this.Layer_Sticky)
                    {
                        continue;
                    }
                    else if (node.Value == this.Layer_Template)
                    {
                        continue;
                    }
                    else if (node.Value is TransparentImage)
                    {
                        continue;
                    }
                    else if (node.Value is TransparentRichText)
                    {
                        CtrlList.AddBefore(node, ctrl);
                        (this.Parent as NemonicForm).IsSaved = false;
                        break;
                    }
                }
            }

            this.ReOrderCtrl();
        }

        /// <summary>
        /// Saves the json.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <param name="thumbnail">The thumbnail.</param>
        /// <returns></returns>
        public IList<JsonObject> SaveJson(Config config, Image thumbnail)
        {
            LinkedList<Control> src = this.CtrlList;
            IList<JsonObject> objects = new List<JsonObject>();

            config.Sticky = Convert.ToString(this.Layer_Sticky.CurrentSticky);
            objects.Add(config);

            int index = src.Count;
            for (LinkedListNode<Control> node = src.First; node != src.Last.Next; node = node.Next)
            {
                int priority = --index;
                //Layer Template만이 PictureBox일 경우를 가정하여 작성

                if (node.Value is StickyCtrl)
                {
                    continue;
                }

                if (node.Value is PictureBox)
                {
                    TemplateLayer layer = new TemplateLayer()
                    {
                        priority = priority,
                        x = node.Value.Location.X,
                        y = node.Value.Location.Y,
                        image = NemonicApp.ImageToByte((node.Value as PictureBox).Image)
                    };

                    objects.Add(layer);
                }
                else if (node.Value is TransparentImage)
                {
                    TransparentImage.ImageLayer layer = new TransparentImage.ImageLayer()
                    {
                        priority = priority,
                        x = node.Value.Location.X,
                        y = node.Value.Location.Y,
                        width = node.Value.Size.Width,
                        height = node.Value.Size.Height,
                        image = NemonicApp.ImageToByte((node.Value as TransparentImage).Image)
                    };

                    objects.Add(layer);
                }
                else if (node.Value is TransparentRichText)
                {
                    TransparentRichText.RichTextLayer layer = new TransparentRichText.RichTextLayer()
                    {
                        priority = priority,
                        x = node.Value.Location.X,
                        y = node.Value.Location.Y,
                        text = (node.Value as TransparentRichText).Text,
                        font = (node.Value as TransparentRichText).Font
                    };
                    (node.Value as TransparentRichText).SelectAll();
                    layer.align = (node.Value as TransparentRichText).SelectionAlignment;
                    (node.Value as TransparentRichText).DeselectAll();

                    objects.Add(layer);
                }
            }

            Thumbnail thumbNailData = new Thumbnail()
            {
                image = NemonicApp.ImageToByte(thumbnail)
            };
            objects.Add(thumbNailData);

            return objects;
        }

        /// <summary>
        /// Opens the json.
        /// </summary>
        /// <param name="objects">The objects.</param>
        public void OpenJson(List<JsonObject> objects)
        {
            List<KeyValuePair<int, Control>> ctrls = new List<KeyValuePair<int, Control>>();

            foreach (JsonObject obj in objects)
            {
                if (obj is Config)
                {
                    Config config = obj as Config;
                    Enum.TryParse<Paper>(config.Paper, out Paper paper);
                    Enum.TryParse<ColorType>(config.Color, out ColorType color);
                    Enum.TryParse<Sticky>(config.Sticky, out Sticky sticky);
                    
                    (this.Parent as NemonicForm).ChangePaper(paper);
                    (this.Parent as NemonicForm).ChangeColor(color);
                    this.ChangeSticky(paper, sticky);
                }
                else if (obj is TemplateLayer)
                {
                    TemplateLayer layer = obj as TemplateLayer;
                    this.ChangeTemplate(NemonicApp.ByteToImage(layer.image));
                }
                else if (obj is TransparentImage.ImageLayer)
                {
                    TransparentImage.ImageLayer layer = obj as TransparentImage.ImageLayer;

                    TransparentImage imageBox = new TransparentImage()
                    {
                        Location = new Point(layer.x, layer.y),
                        Image = NemonicApp.ByteToImage(layer.image),
                        Size = new Size(layer.width, layer.height)
                    };

                    ctrls.Add(new KeyValuePair<int, Control>(layer.priority, imageBox));
                }
                else if (obj is TransparentRichText.RichTextLayer)
                {
                    TransparentRichText.RichTextLayer layer = obj as TransparentRichText.RichTextLayer;
                    this.Layer_TextField.Font = layer.font;
                    this.Layer_TextField.SelectAll();
                    this.Layer_TextField.SelectionAlignment = layer.align;
                    this.Layer_TextField.DeselectAll();
                    this.Layer_TextField.Text = layer.text;
                }
            }

            ctrls.Sort(delegate (KeyValuePair<int, Control> A, KeyValuePair<int, Control> B)
            {
                return B.Key.CompareTo(A.Key);
            });

            foreach (KeyValuePair<int, Control> element in ctrls)
            {
                this.AddCtrl(element.Value);
            }
        }

        /// <summary>
        /// 스크린샷을 통해, 템플릿을 바꾼다.
        /// </summary>
        public void ChangeTemplateFromScreenShot(bool silent = false)
        {
            if (this.isTransparent)
            {
                this.Visible = false;
                this.ChangeTemplate(this.TakeScreenShot(), silent);

                /*
                if (this.IsEmptyMemo())
                {
                    this.ChangeTemplate(this.TakeScreenShot(), silent);
                }
                else
                {
                    using (Bitmap screenshot = this.TakeScreenShot())
                    {
                        string path = NemonicApp.TemporaryPath + @"\" + DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss-tt") + @".png";
                        screenshot.Save(path, System.Drawing.Imaging.ImageFormat.Png);
                        (this.Parent as NemonicForm).OpenNew(path);
                    }
                }
                */

                this.Visible = true;
                this.DoTransparent(false);

                (this.Parent as NemonicForm).AllowResize = false;
                (this.Parent as NemonicForm).ChangePaper((this.Parent as NemonicForm).CurrentPaper);
            }
            else
            {
                this.DoTransparent(true);
                (this.Parent as NemonicForm).AllowResize = true;
            }
            
        }

        /// <summary>
        /// 스크린샷을 찍어, 정보를 전달한다.
        /// </summary>
        /// <returns>스크린샷 이미지</returns>
        public Bitmap TakeScreenShot()
        {
            this.Layer_Sticky.Hide();

            //이유는 모르겠지만, Form 사이즈가 변경될 때 LayerCtrl 크기만 변경되고 내부 Control의 크기는 업데이트가 안됨.
            //Rectangle layerRect = this.Layer_Template.DisplayRectangle;
            Rectangle layerRect = new Rectangle(0, 0, this.Width - this.Padding.All * 2, this.Height - this.Padding.All * 2);
            Rectangle rect = this.Layer_Template.RectangleToScreen(layerRect);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            this.Layer_Sticky.Show();
            return bmp;
        }

        public void Print()
        {
            (this.Parent as NemonicForm).Print();
        }

        /// <summary>
        /// 레이어를 재정렬한다.
        /// </summary>
        public void ReOrderCtrl()
        {
            int index = CtrlList.Count;
            for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
            {
                this.Controls.SetChildIndex(node.Value, --index);
                node.Value.BringToFront();
            }
            this.Validate();
        }

        /// <summary>
        /// Changes the sticky.
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="defualtSticky">if set to <c>true</c> [defualt sticky].</param>
        public void ChangeSticky(Paper paper, bool defualtSticky = false)
        {
            this.StickyCtrl.ChangeSticky(paper, defualtSticky);
            (this.Parent as NemonicForm).MenuCtrl.ChangeStickyIcon(this.StickyCtrl.CurrentSticky);
        }

        /// <summary>
        /// Changes the sticky.
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="sticky">The sticky.</param>
        public void ChangeSticky(Paper paper, Sticky sticky)
        {
            this.StickyCtrl.ChangeSticky(paper, sticky);
            (this.Parent as NemonicForm).MenuCtrl.ChangeStickyIcon(this.StickyCtrl.CurrentSticky);
        }

        private Color Color;
        /// <summary>
        /// 메모에 포함된 모든 레이어를 투명화하여, 스크린샷을 찍을 수 있게 한다.
        /// </summary>
        /// <param name="isTrans">if set to <c>true</c> [is trans].</param>
        public void DoTransparent(bool isTrans)
        {
            this.isTransparent = isTrans;

            if (isTransparent)
            {
                this.Color = this.BackColor;
                this.BackColor = Color.Transparent;
                (this.Parent as NemonicForm).TransparencyKey = (this.Parent as NemonicForm).BackColor;
                for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
                {
                    if (node.Value == this.Layer_Screenshot)
                    {
                        this.Layer_Screenshot.Visible = true;
                    }
                    else
                    {
                        node.Value.Visible = false;
                    }
                }
            }
            else
            {
                this.BackColor = this.Color;
                (this.Parent as NemonicForm).TransparencyKey = Color.Empty;
                for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
                {
                    if(node.Value == this.Layer_Screenshot)
                    {
                        this.Layer_Screenshot.Visible = false;
                    }
                    else
                    {
                        node.Value.Visible = true;
                    }
                }
            }

            this.Validate();
        }

        //public bool SpawnImage = false;
        //public string SpawnPath = null;
        //public Bitmap SpawnTarget = null;
        //public bool SpawnText = false;
        public void ControlMouseEvent(object sender, MouseEventArgs e)
        {
            //디버깅용 코드
            //Console.WriteLine("ControlMouseEvent");

            this.Focus();

            /*
            if (SpawnText)
            {
                this.AddText();
                this.SpawnText = false;
            }
            */
            /*
            if (SpawnImage)
            {
                if(SpawnTarget != null)
                {
                    this.AddImage(SpawnTarget, SpawnPath);
                    this.SpawnTarget = null;
                }
                this.SpawnImage = false;
            }
            */
        }

        /// <summary>
        /// 템플릿 레이어에 새로운 이미지를 적용한다.
        /// </summary>
        /// <param name="image">The image.</param>
        public void ChangeTemplate(Bitmap image, bool silent = false)
        {

            if (image == null)
                return;

            Paper paper = (this.Parent as NemonicForm).CurrentPaper;
            if (!this.IsPoperPaperSize(paper, image.Size))
            {
                if (!this.IsEmptyMemo() && !silent)
                {
                    DialogResult ans = MessageBox.Show(nemonic.Properties.Messages.Msg_ChangeTemplate, "Warning", MessageBoxButtons.YesNo);
                    if (ans == DialogResult.No)
                    {
                        return;
                    }
                }

                foreach (Paper p in Enum.GetValues(typeof(Paper)))
                {
                    if (this.IsPoperPaperSize(p, image.Size))
                    {
                        paper = p;
                        break;
                    }
                }
            }
            this.Layer_Template.Image = image;

            (this.Parent as NemonicForm).ChangePaper(paper);

            (this.Parent as NemonicForm).IsSaved = false;
        }

        /// <summary>
        /// Determines whether [is poper paper size] [the specified paper].
        /// </summary>
        /// <param name="paper">The paper.</param>
        /// <param name="size">The size.</param>
        /// <returns>
        ///   <c>true</c> if [is poper paper size] [the specified paper]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsPoperPaperSize(Paper paper, Size size)
        {
            float sizeRatio = 0;
            float paperRatio = 0;

            //사이즈비율을 계산
            if (size.Height > size.Width)
            {
                sizeRatio = (float)size.Height / size.Width;
                paperRatio = (float)NemonicApp.MemoPixel[paper].Height / NemonicApp.MemoPixel[paper].Width;
            }
            else
            {
                sizeRatio = size.Width / (float)size.Height;
                paperRatio = NemonicApp.MemoPixel[paper].Width / (float)NemonicApp.MemoPixel[paper].Height;
            }

            /*
            if (NemonicApp.MemoPixel[paper].Height > NemonicApp.MemoPixel[paper].Width)
            {
                
            }
            else
            {
                
            }
            */

            if (Math.Abs(sizeRatio - paperRatio) < 0.1F)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Checks the image file extension match or not.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="e">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        /// <returns></returns>
        static public bool CheckImageFileExt(out string filename, DragEventArgs e)
        {
            bool rt = false;
            filename = String.Empty;
            if ((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
            {
                Array data = ((IDataObject)e.Data).GetData("FileDrop") as Array;
                if (data != null)
                {
                    if ((data.Length == 1) && (data.GetValue(0) is String))
                    {
                        filename = ((string[])data)[0];
                        string ext = Path.GetExtension(filename).ToLower();
                        if ((ext == ".jpg") || (ext == ".png") || (ext == ".bmp"))
                        {
                            rt = true;
                        }
                    }
                }
            }
            return rt;
        }

        /// <summary>
        /// Adds the image.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="path">The path.</param>
        /// <param name="location">The location.</param>
        public void AddImage(Bitmap image, string path = null, Point location = new Point())
        {
            TransparentImage imageBox = new TransparentImage();

            int maxWidth = this.Width - (this.Padding.All * 2);
            int maxHeight = this.Height - (this.Padding.All * 2);

            int imageWidth = image.Width / 2;
            int imageHeight = image.Height / 2;

            //사이즈 결정
            if (imageWidth > maxWidth && imageHeight > maxHeight)
            {
                if (imageWidth > imageHeight)
                {
                    int calcHeight = (int)Math.Round(((double)imageHeight / imageWidth) * maxWidth);
                    imageBox.Size = new Size(maxWidth, calcHeight);
                }
                else
                {
                    int calcWidth = (int)Math.Round(((double)imageWidth / imageHeight) * maxHeight);
                    imageBox.Size = new Size(calcWidth, maxHeight);
                }
            }
            else if (imageWidth > maxWidth && imageHeight < maxHeight)
            {
                int calcHeight = (int)Math.Round(((double)imageHeight / maxHeight) * maxHeight);
                imageBox.Size = new Size(maxWidth, calcHeight);
            }
            else if (imageWidth < maxWidth && imageHeight > maxHeight)
            {
                int calcWidth = (int)Math.Round(((double)imageWidth / maxWidth) * maxWidth);
                imageBox.Size = new Size(calcWidth, maxHeight);
            }
            else
            {
                imageBox.Size = new Size(imageWidth, imageHeight);
            }

            //위치를 고정된 곳으로 잡아 달라는 요청에 따른 코드
            location = new Point(this.Padding.All, this.Padding.All);
            
            /*
            //위치결정
            if (location.X + imageWidth > maxWidth && location.Y + imageHeight > maxHeight)
            {
                location.X -= (location.X + imageWidth - maxWidth);
                location.Y -= (location.Y + imageHeight - maxHeight);
            }
            else if (location.X + imageWidth > maxWidth)
            {
                location.X -= (location.X + imageWidth - maxWidth);
            }
            else if (location.Y + imageHeight > maxHeight)
            {
                location.Y -= (location.Y + imageHeight - maxHeight);
            }

            //최솟값 
            if (location.X < this.Padding.All)
            {
                location.X = this.Padding.All;
            }

            if(location.Y < this.Padding.All)
            {
                location.Y = this.Padding.All;
            }
            */

            //결정사항 반영
            if (path != null)
            {
                imageBox.Name = Path.GetFileName(path);
            }
            imageBox.Image = image;
            imageBox.Location = location;
            this.AddCtrl(imageBox);

            this.Layer_TextField.Enabled = false;

        }

        public bool ToggleRichText()
        {
            this.Layer_TextField.Enabled = !this.Layer_TextField.Enabled;
            if (this.Layer_TextField.Enabled)
            {
                this.ActiveControl = this.Layer_TextField;
                this.Layer_TextField.SelectionStart = this.Layer_TextField.Text.Length;
                this.Layer_TextField.Focus();
            }
            return this.Layer_TextField.Enabled;

        }

        /// <summary>
        /// 이미지가 레이어에 드래그되어 왔을 때, 효과
        /// </summary>
        /// <param name="drgevent">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        public void Image_DragEnter(DragEventArgs drgevent)
        {
            bool imageFileValid = LayersCtrl.CheckImageFileExt(out string path, drgevent);
            if (imageFileValid)
            {
                drgevent.Effect = DragDropEffects.Copy;
            }
            else
            {
                drgevent.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 이미지가 레이어에 드랍되었을 때, 이미지가 레이어에 추가되는 이벤트를 발생시킨다.
        /// </summary>
        /// <param name="drgevent">The <see cref="DragEventArgs"/> instance containing the event data.</param>
        public void Image_DragDrop(DragEventArgs drgevent)
        {
            bool imageFileValid = LayersCtrl.CheckImageFileExt(out string path, drgevent);
            if (imageFileValid)
            {
                this.AddImage(NemonicApp.GetImage(path), path);
            }
        }

        /// <summary>
        /// Determines whether memo is printable.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is printable; otherwise, <c>false</c>.
        /// </returns>
        public bool IsPrintable()
        {
            //템플릿이 있는지 체크
            if(this.Layer_Template.Image != null)
            {
                return true;
            }

            //텍스트가 있는지 체크
            if (!String.IsNullOrEmpty(this.Layer_TextField.Text))
            {
                return true;
            }

            //이미지가 있는지 체크
            for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
            {
                if (node.Value is TransparentImage && node.Value != this.Layer_Sticky)
                {
                    return true;
                }
            }

            return false;
        }

        /*
        private void AddText(string text = null)
        {
            TransparentText textBox = new TransparentText();
            textBox.Location = this.Layer_Template.PointToClient(Cursor.Position);
            if(text == null)
            {
                textBox.Text = "Hello, World!";
            }
            else
            {
                textBox.Text = text;
            }
            this.AddCtrl(textBox);
        }
        */

        /// <summary>
        /// Changes the color.
        /// </summary>
        /// <param name="type">컬러 종류</param>
        public void ChangeColor(ColorType type)
        {
            Colors colors = NemonicApp.MemoColors[type];
            this.BackColor = colors.Paper;
            this.PaddingColor = colors.Paper;
        }

        /// <summary>
        /// Determines whether [is empty memo].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is empty memo]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmptyMemo()
        {
            if (this.Layer_Template.Image != null)
            {
                return false;
            }

            for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
            {
                if (node.Value is TransparentImage && !(node.Value is StickyCtrl))
                {
                    return false;
                }
            }
            
            if (this.Layer_TextField.Text != string.Empty)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clears the memo.
        /// </summary>
        /// <param name="template">if set to <c>true</c> [template].</param>
        /// <param name="image">if set to <c>true</c> [image].</param>
        /// <param name="text">if set to <c>true</c> [text].</param>
        public void ClearMemo(bool template = true, bool image = true, bool text = true)
        {
            if (template)
            {
                this.Layer_Template.Image = null;
            }

            if (image)
            {
                for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
                {
                    if (node.Value is TransparentImage && !(node.Value is StickyCtrl))
                    {
                        this.DeleteCtrl(node.Value);
                    }
                }
            }

            if (text)
            {
                this.Layer_TextField.Text = string.Empty;
            }
        }

        /// <summary>
        /// 이미지 순서를 재조정한다.
        /// </summary>
        public void ReOrderImages()
        {
            for (LinkedListNode<Control> node = CtrlList.First; node != CtrlList.Last.Next; node = node.Next)
            {
                if (node.Value is TransparentImage && !(node.Value is StickyCtrl))
                {
                    node.Value.Location = new Point(this.Padding.All, this.Padding.All);
                }
            }
        }

        private void Layer_TextField_TextChanged(object sender, EventArgs e)
        {
            (this.Parent as NemonicForm).IsSaved = false;
        }

        private void LayersCtrl_DragDrop(object sender, DragEventArgs e)
        {
            this.Image_DragDrop(e);
        }

        private void LayersCtrl_DragEnter(object sender, DragEventArgs e)
        {
            this.Image_DragEnter(e);
        }
    }
}
