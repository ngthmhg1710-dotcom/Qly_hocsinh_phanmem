using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace GUI.CONGCU.BANGTRANG
{
    public partial class BANGTRANG : Form
    {
        private Bitmap canvas;
        private Pen currentPen;
        private bool isDrawing = false;
        private Point lastPoint;

        // Lưu ảnh (nhưng KHÔNG kéo)
        private class FixedImage
        {
            public Image Img;
            public Point Position;
        }

        private List<FixedImage> images = new List<FixedImage>();

        // Lưu nét vẽ
        private class LineSegment
        {
            public Point Start;
            public Point End;
            public Color Color;
            public float Width;
        }
        private List<LineSegment> lines = new List<LineSegment>();

        public BANGTRANG()
        {
            InitializeComponent();
        }

        private void BANGTRANG_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(picBoard.Width, picBoard.Height);
            picBoard.Image = canvas;

            currentPen = new Pen(Color.Black, 3)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
        }

        private void RedrawCanvas()
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.Clear(Color.White);

                // Vẽ ảnh
                foreach (var img in images)
                    g.DrawImage(img.Img, img.Position);

                // Vẽ nét
                foreach (var line in lines)
                {
                    using (Pen p = new Pen(line.Color, line.Width)
                    {
                        StartCap = LineCap.Round,
                        EndCap = LineCap.Round
                    })
                    {
                        g.DrawLine(p, line.Start, line.End);
                    }
                }
            }

            picBoard.Invalidate();
        }

        private void picBoard_MouseDown(object sender, MouseEventArgs e)
        {
            // KHÔNG kiểm tra ảnh – chỉ vẽ
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void picBoard_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                LineSegment segment = new LineSegment
                {
                    Start = lastPoint,
                    End = e.Location,
                    Color = currentPen.Color,
                    Width = currentPen.Width
                };

                lines.Add(segment);

                using (Graphics g = Graphics.FromImage(canvas))
                {
                    using (Pen p = new Pen(segment.Color, segment.Width)
                    {
                        StartCap = LineCap.Round,
                        EndCap = LineCap.Round
                    })
                    {
                        g.DrawLine(p, segment.Start, segment.End);
                    }
                }

                lastPoint = e.Location;
                picBoard.Invalidate();
            }
        }

        private void picBoard_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void btnMau_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                currentPen.Color = c.Color;
        }

        private void btnDoDay_Click_1(object sender, EventArgs e)
        {
            currentPen.Width = trkSize.Value;
        }

        private void btnBut_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Black;
        }

        private void btnGom_Click(object sender, EventArgs e)
        {
            lines.Clear();
            images.Clear();

            using (Graphics g = Graphics.FromImage(canvas))
                g.Clear(Color.White);

            picBoard.Invalidate();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "PNG|*.png";

            if (s.ShowDialog() == DialogResult.OK)
                canvas.Save(s.FileName, ImageFormat.Png);
        }

        private void btnGGDrive_Click(object sender, EventArgs e)
        {
            string tmp = "whiteboard.png";
            canvas.Save(tmp, ImageFormat.Png);

            string id = DichVuGoogleBUS.UploadFile(tmp);
            DichVuGoogleBUS.MakeFilePublic(id);
            string link = DichVuGoogleBUS.GetPublicViewLink(id);

            MessageBox.Show("Upload thành công!\n" + link);
        }

        private void btnTaiHinh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(ofd.FileName);
                images.Add(new FixedImage { Img = img, Position = new Point(0, 0) });
                RedrawCanvas();
            }
        }

        private void btnViet_Click(object sender, EventArgs e)
        {
            RedrawCanvas();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                currentPen.Color = c.Color;
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            RedrawCanvas();
        }
    }
}
