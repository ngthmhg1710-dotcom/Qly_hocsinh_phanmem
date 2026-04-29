using FlashcardFlipGame;
using GAME1_;
using GUI.TROCHOIHOCTAP.GIAODIEN;
using RANDOMSO;
using SapXepCau;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.TROCHOIHOCTAP
{
    public partial class MENUTROCHOI : Form
    {
        public MENUTROCHOI()
        {
            InitializeComponent();
        }

        private void btnVongQuayMayMan_Click(object sender, EventArgs e)
        {
            LSVONGQUAY createGameForm = new LSVONGQUAY();

            // 1. Ẩn menu hiện tại
            this.Hide();

            // 2. Khi form con đóng -> Hiện lại menu
            createGameForm.FormClosed += (s, args) => this.Show();

            // 3. Hiện form con
            createGameForm.Show();
        }

        private void btnNgheChonHinh_Click(object sender, EventArgs e)
        {
            // Lấy ID giáo viên
            int currentId = DTO.UserSession.TeacherId;

            TAOCAUHOI_NGHECHONHINH createGameForm = new TAOCAUHOI_NGHECHONHINH(currentId);

            // 1. Ẩn menu hiện tại
            this.Hide();

            // 2. Khi form con đóng -> Hiện lại menu
            createGameForm.FormClosed += (s, args) => this.Show();

            // 3. Hiện form con
            createGameForm.Show();
        }

        private void btnSapXepCau_Click(object sender, EventArgs e)
        {
            // Lấy ID trực tiếp từ Session
            int currentId = DTO.UserSession.TeacherId;

            SapXepCau.frmTaoGame createDeckForm = new SapXepCau.frmTaoGame(currentId);

            // 1. Ẩn menu hiện tại
            this.Hide();

            // 2. Khi form con đóng -> Hiện lại menu
            createDeckForm.FormClosed += (s, args) => this.Show();

            // 3. Hiện form con
            createDeckForm.Show();
        }

        private void btnFlashCard_Click(object sender, EventArgs e)
        {
            // Lấy ID từ UserSession
            int currentId = DTO.UserSession.TeacherId;

            CreateDeckForm createDeckForm = new CreateDeckForm(currentId);

            // 1. Ẩn menu hiện tại
            this.Hide();

            // 2. Khi form con đóng -> Hiện lại menu
            createDeckForm.FormClosed += (s, args) => this.Show();

            // 3. Hiện form con
            createDeckForm.Show();
        }
    }
}