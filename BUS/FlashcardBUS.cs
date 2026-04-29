using System;
using System.Collections.Generic;
using System.Data;
using DAL;
using DTO; // Hoặc namespace chứa class Flashcard của bạn

namespace BUS
{
    public class FlashcardBUS
    {
        private FlashcardDAL dal = new FlashcardDAL();

        // Nghiệp vụ: Lấy lịch sử
        public DataTable GetHistory(int giaoVienID)
        {
            return dal.GetLichSuBoThe(giaoVienID);
        }

        // Nghiệp vụ: Xóa bộ thẻ
        public bool DeleteDeck(int gameId)
        {
            return dal.DeleteBoThe(gameId);
        }

        // Nghiệp vụ: Lấy chi tiết bộ thẻ và chuyển đổi từ DataTable sang List<Flashcard> cho GUI dùng
        public List<Flashcard> GetDeckDetails(int gameId)
        {
            DataTable dt = dal.GetChiTietBoThe(gameId);
            List<Flashcard> list = new List<Flashcard>();

            foreach (DataRow row in dt.Rows)
            {
                list.Add(new Flashcard(
                    row["FrontText"].ToString(),
                    row["BackText"].ToString(),
                    row["FrontImagePath"].ToString(),
                    row["BackImagePath"].ToString()
                ));
            }
            return list;
        }

        // Nghiệp vụ: Lưu bộ thẻ (Kiểm tra hợp lệ tại đây)
        public bool SaveDeck(string tenBoThe, int giaoVienID, List<Flashcard> cards, out string message)
        {
            // 1. Kiểm tra logic
            if (string.IsNullOrWhiteSpace(tenBoThe))
            {
                message = "Vui lòng nhập tên cho bộ thẻ.";
                return false;
            }
            if (cards == null || cards.Count == 0)
            {
                message = "Danh sách thẻ trống. Vui lòng tạo ít nhất một thẻ.";
                return false;
            }

            try
            {
                // 2. Chuyển đổi List<Flashcard> sang DataTable để gửi xuống DAL
                DataTable dtCards = new DataTable();
                dtCards.Columns.Add("FrontText", typeof(string));
                dtCards.Columns.Add("BackText", typeof(string));
                dtCards.Columns.Add("FrontImagePath", typeof(string));
                dtCards.Columns.Add("BackImagePath", typeof(string));

                foreach (var card in cards)
                {
                    dtCards.Rows.Add(card.FrontText, card.BackText, card.FrontImagePath, card.BackImagePath);
                }

                // 3. Gọi DAL
                bool result = dal.SaveBoThe(tenBoThe, giaoVienID, dtCards);
                message = result ? "Lưu thành công!" : "Lưu thất bại.";
                return result;
            }
            catch (Exception ex)
            {
                message = "Lỗi hệ thống: " + ex.Message;
                return false;
            }
        }
    }
}