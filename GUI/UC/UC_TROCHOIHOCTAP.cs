using FlashcardFlipGame;
using GAME1_;
using RANDOMSO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class UC_TROCHOIHOCTAP : UserControl
    {
        public UC_TROCHOIHOCTAP()
        {
            InitializeComponent();
        }

        private void btnNgheChonHinh_Click(object sender, EventArgs e)
        {
            TAOCAUHOI_NGHECHONHINH createGameForm = new TAOCAUHOI_NGHECHONHINH();
            createGameForm.Show();
        }

        private void btnSapXepCau_Click(object sender, EventArgs e)
        {
            // Lấy ID trực tiếp từ Session
            int currentId = DTO.UserSession.TeacherId;

            // Mở form game
            SapXepCau.frmTaoGame createDeckForm = new SapXepCau.frmTaoGame(currentId);
            createDeckForm.Show();
        }

        private void btnFlashcard_Click(object sender, EventArgs e)
        {
            // 1. Lấy ID từ UserSession (Phiên đăng nhập hiện tại)
            int currentId = DTO.UserSession.TeacherId;

            // 2. Khởi tạo form FlashCard và truyền ID vào
            // SỬA LỖI: Thay 'giaoVienID' thành 'currentId'
            CreateDeckForm createDeckForm = new CreateDeckForm(currentId);

            // 3. Hiển thị
            createDeckForm.Show();
        }

        private void btnVongQuay_Click(object sender, EventArgs e)
        {
            LSVONGQUAY createGameForm = new LSVONGQUAY();

            createGameForm.Show();
        }

    }
}
