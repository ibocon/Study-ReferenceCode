using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace nemonic
{
    public enum ColorType
    {
        White = 0, Yellow = 1, Pink = 2, Blue = 3, Green = 4
    }

    public class Colors
    {
        public Color Menu, Hover, Paper;

        public Colors()
        {
            Menu = Color.FromArgb(138, 140, 142);
            Hover = Color.FromArgb(120, 122, 123);
            Paper = Color.FromArgb(248, 248, 248);
        }

        public Colors(Color menu, Color hover, Color paper)
        {
            Menu = menu;
            Hover = hover;
            Paper = paper;
        }
    }

    static class NemonicApp
    {
        //확장자
        public static string Extension = @".nemo";
        //기본 폴더 위치를 지정한다.
        public static string MemoPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\nemonic" + @"\Memo";
        public static string TemporaryPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\nemonic" + @"\Temporary";
        public static string TemplatePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Template";

        //순수 메모가 차지하는 픽셀 크기를 저장
        public static Dictionary<Paper, Size> MemoPixel = new Dictionary<Paper, Size>()
        {
            {Paper.p80x80, new Size(288, 288) },
            {Paper.p80x56, new Size(288, 192) },
            {Paper.p56x80, new Size(192, 288) },
            {Paper.p104x80, new Size(384, 288) },
            {Paper.p80x104, new Size(288, 384) }
        };

        //메모 폼의 크기 측정을 위한 수
        public static int FormMargin = 26;

        //메모 프로그램의 크기를 저장
        public static Dictionary<Paper, Size> FormSize = new Dictionary<Paper, Size>()
        {
            //Designer.cs를 위해 필요.
            { Paper.p80x80, new Size(MemoPixel[Paper.p80x80].Width + FormMargin, MemoPixel[Paper.p80x80].Height + FormMargin + MenuCtrl.MenuHeight) },
            { Paper.p80x56, new Size(MemoPixel[Paper.p80x56].Width + FormMargin, MemoPixel[Paper.p80x56].Height + FormMargin + MenuCtrl.MenuHeight) },
            { Paper.p56x80, new Size(MemoPixel[Paper.p56x80].Width + FormMargin, MemoPixel[Paper.p56x80].Height + FormMargin + MenuCtrl.MenuHeight) },
            { Paper.p104x80, new Size(MemoPixel[Paper.p104x80].Width + FormMargin, MemoPixel[Paper.p104x80].Height + FormMargin + MenuCtrl.MenuHeight) },
            { Paper.p80x104, new Size(MemoPixel[Paper.p80x104].Width + FormMargin, MemoPixel[Paper.p80x104].Height + FormMargin + MenuCtrl.MenuHeight) },
        };

        //메모 컬러 정보를 저장
        public static Dictionary<ColorType, Colors> MemoColors = new Dictionary<ColorType, Colors>()
        {
            {ColorType.White, new Colors(Color.FromArgb(138, 140, 142), Color.FromArgb(120, 122, 123), Color.FromArgb(248, 248, 248)) },
            {ColorType.Yellow, new Colors(Color.FromArgb(250, 149, 40), Color.FromArgb(219, 122, 41), Color.FromArgb(255, 242, 181)) },
            {ColorType.Pink, new Colors(Color.FromArgb(242, 117, 174), Color.FromArgb(223, 95, 147), Color.FromArgb(255, 202, 210)) },
            {ColorType.Blue, new Colors(Color.FromArgb(93, 151, 207), Color.FromArgb(87, 126, 182), Color.FromArgb(182, 222, 234)) },
            {ColorType.Green, new Colors(Color.FromArgb(115, 172, 80), Color.FromArgb(99, 148, 61), Color.FromArgb(219, 243, 174)) }
        };

        public static NemonicContext AppContext;

        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Process[] nemonic = Process.GetProcessesByName("nemonic");
            if (nemonic.Length > 1)
            {
                //TODO: 새 메모장을 열 수 있도록, 기존의 프로세스로 메세지를 보내는 방법이 필요하다.
                MessageBox.Show("nemonic App already running.", "Notification", MessageBoxButtons.OK);
                return;
            }
#if (!DEBUG)
            try
#endif
            {
                //Arguments 해석하여, 메모에 반영한다.
                Paper paper = Paper.p80x80;
                Sticky sticky = Sticky.Top;
                Image template = null;
                string path = null;
                bool startup = false;
#if DEBUG
                //path = @"C:\Users\wlfka\Documents\nemonic\Memo\2017-06-25_10-09-13-오전.nemo";
#endif

                //TODO: 파라미터를 전달하는 알고리즘을 순서가 상관없도록 하자.
                ColorType color = ColorType.White;
                try
                {
                    if (args.Length >= 1 && args[0] != string.Empty)
                    {
                        Enum.TryParse<Paper>(args[0], out paper);
                    }
                    if (args.Length >= 2 && args[1] != string.Empty)
                    {
                        Enum.TryParse<Sticky>(args[1], out sticky);
                    }
                    if (args.Length >= 3 && args[2] != string.Empty)
                    {
                        Enum.TryParse<ColorType>(args[2], out color);
                    }
                    if (args.Length >= 4 && args[3] != string.Empty)
                    {
                        if (!args[3].Equals("null"))
                        {
                            template = Image.FromFile(args[3]);
                        }
                    }
                    if (args.Length >= 5 && args[4] != string.Empty)
                    {
                        if (!args[4].Equals("null"))
                        {
                            path = args[4];
                        }
                    }
                    if (args.Length >= 6 && args[5] != string.Empty)
                    {
                        if (args[5].Equals("true"))
                        {
                            startup = true;
#if DEBUG
                            //MessageBox.Show("startup = " + startup);
#endif
                        }
                    }

                }
                catch (Exception e)
                {
#if DEBUG
                    MessageBox.Show("Error! Invalid argument used. \n\n" + e.StackTrace);
#endif
                }

                //기본적으로 필요한 폴더가 존재하는지 체크하고 생성한다.
                if (!Directory.Exists(MemoPath))
                {
                    Directory.CreateDirectory(MemoPath);
                }
                if (!Directory.Exists(TemporaryPath))
                {
                    Directory.CreateDirectory(TemporaryPath);
                }

                NemonicForm form = new NemonicForm(paper, sticky, color, template, path);

                Application.EnableVisualStyles();
                try
                {
                    Application.Run(AppContext = new NemonicContext(form, startup));
                }
                catch (ApplicationException e)
                {
#if DEBUG
                    //MessageBox.Show(e.Message);
#endif
                    Application.Exit();
                }
                
            }
#if (!DEBUG)
            catch (Exception e)
            {
                //MessageBox.Show("FATAL ERROR!\n" + e.Message +"\n\n" + e.StackTrace);
            }
#endif
        }

        /// <summary>
        /// 이미지를 파일 위치에서 가져와, 메모리에 올린다.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static Bitmap GetImage(string path)
        {
            Bitmap tempBitmap = null;
            using (Bitmap originalBmp = new Bitmap(path))
            {
                tempBitmap = new Bitmap(originalBmp.Width, originalBmp.Height);
                using (Graphics g = Graphics.FromImage(tempBitmap))
                {
                    g.DrawImage(originalBmp, 0, 0, originalBmp.Width, originalBmp.Height);
                }
            }
            return tempBitmap;
        }

        /// <summary>
        /// 이미지 정보를 파일에 저장할 수 있도록 바이트 정보로 전환한다.
        /// </summary>
        /// <param name="img">The img.</param>
        /// <returns>바이트</returns>
        public static byte[] ImageToByte(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }

        /// <summary>
        /// 파일에서 읽은 바이트 정보를 이미지 정보로 전환한다.
        /// </summary>.
        /// <param name="img">The img.</param>
        /// <returns>이미지</returns>
        public static Bitmap ByteToImage(byte[] img)
        {
            if (img != null && img.Length > 0)
            {
                ImageConverter converter = new ImageConverter();
                return (Bitmap)converter.ConvertFrom(img);
            }
            else
            {
                return null;
            }
            
        }
    }

    //메모의 정보를 Serialize 하여 JSON 파일로 저장하기 위해 공통적인 상속 클래스가 필요해 만든 클래스
    public abstract class JsonObject { }

    public class Config : JsonObject
    {
        public string Paper;
        public string Sticky;
        public string Color;
    }

    public class Layers : JsonObject
    {
        public int priority;
        public int x;
        public int y;
    }

    public class Thumbnail : JsonObject
    {
        public byte[] image;
    }

    public class TemplateLayer : Layers
    {
        public byte[] image;
    }
}
