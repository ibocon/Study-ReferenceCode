using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace nemonic
{
    public partial class TransparentImage : UserControl
    {
        protected Image _image;

        public class ImageLayer : Layers
        {
            public int width;
            public int height;
            public byte[] image;
        }

        public TransparentImage()
        {
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            InitializeComponent();
            // Enable double duffering to stop flickering.
            /*
            //this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.Opaque, false);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            */

            this.SetStyle(ControlStyles.Selectable, true);
            this.TabStop = true;
            ControlManager.ControlMoverOrResizer.Init(this);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Console.WriteLine("OnKeyDown " + e.KeyCode);

            if (e.KeyCode == Keys.Delete)
            {
                if(this.Parent != null && this.Parent is LayersCtrl)
                {
                    (this.Parent as LayersCtrl).DeleteCtrl(this);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();
            base.OnMouseDown(e);
        }
        
        protected override void OnPaintBackground(PaintEventArgs e)
        {

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            (this.Parent as LayersCtrl).ControlMouseEvent(this, e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_image != null)
            {
                e.Graphics.DrawImage(_image, 0, 0, this.Width, this.Height);
            }

            if (this.Focused)
            {
                var rc = this.ClientRectangle;
                //rc.Inflate(0, 0);
                ControlPaint.DrawFocusRectangle(e.Graphics, rc);
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            this.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            if(this.Parent != null && this.Parent is LayersCtrl)
            {
                (this.Parent as LayersCtrl).ReOrderCtrl();
            }
            //RecreateHandle();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x20; // WS_EX_TRANSPARENT
                //cp.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return cp;
            }
        }

        public Image Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                RecreateHandle();
            }
        }

        private void TransparentImage_DragDrop(object sender, DragEventArgs e)
        {
            (this.Parent as LayersCtrl).Image_DragDrop(e);
        }

        private void TransparentImage_DragEnter(object sender, DragEventArgs e)
        {
            (this.Parent as LayersCtrl).Image_DragEnter(e);
        }
    }
}
