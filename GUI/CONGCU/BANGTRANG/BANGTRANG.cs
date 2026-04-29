using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
// using System.Windows.Media;  <-- THIS LINE MUST BE REMOVED/COMMENTED OUT

namespace GUI.CONGCU.BANGTRANG
{
    public partial class BANGTRANG : Form
    {
        private Bitmap canvas;
        private Pen currentPen;
        private bool isDrawing = false;
        private Point lastPoint;
        public BANGTRANG()
        {
            InitializeComponent();
        }

        private void btnMau_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                currentPen.Color = c.Color;
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
            // FIX: Specify ImageFormat.Png to ensure correct save format
            canvas.Save(tmp, ImageFormat.Png);

            // Assuming DichVuGoogleBUS.cs is corrected with the two missing methods
            string id = DichVuGoogleBUS.UploadFile(tmp);
            DichVuGoogleBUS.MakeFilePublic(id);

            string link = DichVuGoogleBUS.GetPublicViewLink(id);

            MessageBox.Show("Upload thành công!\n" + link);
        }

        private void BANGTRANG_Load(object sender, EventArgs e)
        {
            // Note: Assuming 'picBoard' is the correct name for pictureBox1 now (must be fixed in Designer.cs)
            canvas = new Bitmap(picBoard.Width, picBoard.Height);
            picBoard.Image = canvas;

            currentPen = new Pen(Color.Black, 3)
            {
                // LineCap is now unambiguous after removing System.Windows.Media
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e) // Assuming fix for naming consistency
        {
            isDrawing = true;
            lastPoint = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e) // Assuming fix for naming consistency
        {
            if (!isDrawing) return;

            using (Graphics g = Graphics.FromImage(canvas))
                g.DrawLine(currentPen, lastPoint, e.Location);

            lastPoint = e.Location;
            picBoard.Invalidate();
        }


        private void btnGom_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(canvas))
                g.Clear(Color.White);

            picBoard.Invalidate();
        }


        private void btnBut_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Black;
        }

        private void btnDoDay_Click_1(object sender, EventArgs e)
        {
            currentPen.Width = trkSize.Value;

        }

        private void picBoard_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;

        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            currentPen.Color = Color.Black;
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            if (c.ShowDialog() == DialogResult.OK)
                currentPen.Color = c.Color;
        }
    }
}