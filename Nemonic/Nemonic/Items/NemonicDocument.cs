using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace nemonic
{

    public partial class NemonicDocument : PrintDocument
    {
        private static Graphics Graphics;

        static float PixelToUnit = 0.49132F;
        static float ScreenToPaper = 2.0F;

        LayersCtrl LayerCtrl;
        bool IsScreenShot;

        float Angle;
        //private LinkedList<Control> Ctrls;

        //TODO: PaperSize에 따라, PrintableArea가 변경되지 않고 있음
        public NemonicDocument(string printerName, Graphics graphics, Paper paper, LayersCtrl layerCtrl, bool isScreenshot = true) : base()
        {
            this.PrinterSettings.PrinterName = printerName;
            this.IsScreenShot = isScreenshot;

            //Paper 종류에 맞게, 용지크기변경
            this.SetPaperSize(paper);

            //마진값설정
            this.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
            this.LayerCtrl = layerCtrl;
            this.Angle = ChangeAngle(paper, layerCtrl.StickyCtrl.CurrentSticky);
            Graphics = graphics;

            this.DefaultPageSettings.Landscape = false;
            this.DefaultPageSettings.PrinterSettings.Copies = 1;
        }

        private void SetPaperSize(Paper paper)
        {
            bool isFind = false;
            foreach (PaperSize ps in this.PrinterSettings.PaperSizes)
            {
                if (paper == Paper.p80x80 && ps.Height == 283)
                {
                    this.DefaultPageSettings.PaperSize = ps;
                    isFind = true;
                }
                else if ((paper == Paper.p80x104 || paper == Paper.p104x80) && ps.Height == 378)
                {
                    this.DefaultPageSettings.PaperSize = ps;
                    isFind = true;
                }
                else if ((paper == Paper.p56x80 || paper == Paper.p80x56) && ps.Height == 189)
                {
                    this.DefaultPageSettings.PaperSize = ps;
                    isFind = true;
                }
            }

            if (!isFind)
            {
                MessageBox.Show(nemonic.Properties.Messages.Msg_NotFoundFitPaperWarning,"Warning", MessageBoxButtons.OK);
            }
        }

        static float ChangePixelToUnit(float value = 0)
        {
            return value * ScreenToPaper * PixelToUnit;
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
        }

        //실제로 페이지에 정보를 입력하는 과정
        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            e.Graphics.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            RectangleF printArea = this.DecidePrintableArea();
            this.DecideGraphicsAngle(e.Graphics, printArea);

            //그리기 시작
            if (IsScreenShot)
            {
                e.Graphics.DrawImage(this.LayerCtrl.TakeScreenShot(), printArea);
            }
            else
            {
                TransparentRichText textLayer = null;

                for (LinkedListNode<Control> node = this.LayerCtrl.CtrlList.First; node != this.LayerCtrl.CtrlList.Last.Next; node = node.Next)
                {
                    if (node.Value is TransparentRichText)
                    {
                        textLayer = node.Value as TransparentRichText;
                    }
                    else if (node.Value is PictureBox)
                    {
                        PictureBox layer = node.Value as PictureBox;
                        if (layer.Image != null)
                        {
                            e.Graphics.DrawImage(layer.Image, printArea);
                        }
                    }
                    else if (node.Value is TransparentImage && !(node.Value is StickyCtrl))
                    {
                        TransparentImage layer = node.Value as TransparentImage;
                        RectangleF area = new RectangleF()
                        {
                            Location = new PointF(ChangePixelToUnit(layer.Location.X - LayerCtrl.Padding.All), ChangePixelToUnit(layer.Location.Y - LayerCtrl.Padding.All)),
                            Size = new SizeF(ChangePixelToUnit(layer.Size.Width), ChangePixelToUnit(layer.Height))
                        };
                        e.Graphics.DrawImage(layer.Image, area);
                    }
                }

                if (textLayer != null && !String.IsNullOrEmpty(textLayer.Text))
                {
                    string text = textLayer.Text;
                    text = text.Replace("\r", Environment.NewLine);
                    //TODO: 폰트크기에 따라, string을 그리는 방식이 달라질 수 있다. 스크린 샷을 찍는 방식은 글이 깨져서 안된다.
                    /*
                    RectangleF displayRectangle =
                        new RectangleF(
                            new PointF(ChangePixelToUnit(0), ChangePixelToUnit(0)),
                            new SizeF(printArea.Width + ChangePixelToUnit(0), printArea.Height + ChangePixelToUnit(0))
                        );
                    */
                    //WordWrap이 반영되도록 설정하는 부분!
                    StringFormat format = new StringFormat(StringFormatFlags.DisplayFormatControl)
                    {
                        //Horizontal Align
                        Alignment = StringAlignment.Near,
                        //Vertical Align
                        LineAlignment = StringAlignment.Near,
                        
                    };

                    //e.Graphics.DrawString(textLayer.Text, font, Brushes.Black, printArea, format);
                    e.Graphics.DrawString(text, textLayer.Font/*this.CalculateFont(288, textLayer.Font)*/, Brushes.Black, printArea);
                    //Debug 코드
                    /*
                    SizeF textSize = e.Graphics.MeasureString(textLayer.Text, textLayer.Font, displayRectangle.Size, format);
                    
                    //SizeF textSize = this.MeasureString(textLayer.Text, textLayer.Font);

                    textSize.Width = ChangePixelToUnit(textSize.Width);
                    textSize.Height = ChangePixelToUnit(textSize.Height);

                    e.Graphics.DrawRectangle(Pens.Black, displayRectangle.Location.X, displayRectangle.Location.Y, textSize.Width, textSize.Height);
                    */

                }
            }

            //Debug 코드
            #if DEBUG
                e.Graphics.DrawRectangle(Pens.Black, printArea.X, printArea.Y, printArea.Width, printArea.Height);
            #endif
        }

        private Font CalculateFont(float pixel, Font font)
        {
            float dpiX = Graphics.DpiX;
            float dpiY = Graphics.DpiY;

            float fontSize = (72.0F * font.Size * dpiX)/(25.4F * pixel);

            return new Font(font.FontFamily, fontSize, font.Style);
        }

        //Debug 함수
        private SizeF MeasureString(string text, Font font)
        {
            int margin = this.LayerCtrl.Padding.All * 2;
            Bitmap b = new Bitmap(this.LayerCtrl.Width - margin, this.LayerCtrl.Size.Height - margin);
            Graphics g = Graphics.FromImage(b);

            return g.MeasureString(text, font, b.Size);
            //return g.MeasureString(text, font);
        }

        private RectangleF DecidePrintableArea()
        {
            RectangleF printArea = this.DefaultPageSettings.PrintableArea;

            switch (this.Angle)
            {
                case 0.0F:
                case 180.0F:

                    break;
                case 90.0F:
                case 270.0F:
                    float temp = printArea.Width;
                    printArea.Width = printArea.Height;
                    printArea.Height = temp;
                    break;
            }

            return printArea;
        }

        private void DecideGraphicsAngle(Graphics g, RectangleF area)
        {
            switch (this.Angle)
            {
                case 0.0F:
                    break;
                case 90.0F:
                    g.TranslateTransform(area.Height, 0);
                    break;
                case 180.0F:
                    g.TranslateTransform(area.Width, area.Height);
                    break;
                case 270.0F:
                    g.TranslateTransform(0, area.Width);
                    break;
            }

            g.RotateTransform(this.Angle);
        }

        public float ChangeAngle(Paper paper, Sticky sticker)
        {
            float angle = 90.0F; //기본 각도

            switch (paper)
            {
                case Paper.p80x80:
                    {
                        if (sticker == Sticky.Right)
                        {
                            angle = 0;
                        }
                        else if (sticker == Sticky.Top)
                        {
                            angle = 90;
                        }
                        else if(sticker == Sticky.Left)
                        {
                            angle = 180;
                        }
                        else if (sticker == Sticky.Bottom)
                        {
                            angle = 270;
                        }
                    }
                    break;
                case Paper.p80x104:
                case Paper.p80x56:
                
                    {
                        if (sticker == Sticky.Right || sticker == Sticky.Left)
                        {
                            if (sticker == Sticky.Right)
                            {
                                angle = 0;
                            }
                            else if (sticker == Sticky.Left)
                            {
                                angle = 180;
                            }
                        }
                        else
                        {
                            throw new Exception("Code is broken!");
                        }
                    }
                    break;
                case Paper.p104x80:
                case Paper.p56x80:
                    {
                        if (sticker == Sticky.Top || sticker == Sticky.Bottom)
                        {
                            if (sticker == Sticky.Top)
                            {
                                angle = 90;
                            }
                            else if (sticker == Sticky.Bottom)
                            {
                                angle = 270;
                            }
                        }
                        else
                        {
                            throw new Exception("Code is broken!");
                        }
                    }
                    break;
            }

            return angle;
        }

        public static Bitmap RotateImage(Bitmap b, float angle)
        {
            //create a new empty bitmap to hold rotated image
            Bitmap returnBitmap = new Bitmap(b.Width, b.Height);
            //make a graphics object from the empty bitmap
            using (Graphics g = Graphics.FromImage(returnBitmap))
            {
                //move rotation point to center of image
                g.TranslateTransform((float)b.Width / 2, (float)b.Height / 2);
                //rotate
                g.RotateTransform(angle);
                //move image back
                g.TranslateTransform(-(float)b.Width / 2, -(float)b.Height / 2);
                //draw passed in image onto graphics object
                g.DrawImage(b, new Point(0, 0));
            }
            return returnBitmap;
        }
    }
}
