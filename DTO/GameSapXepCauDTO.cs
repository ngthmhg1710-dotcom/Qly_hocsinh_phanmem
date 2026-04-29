using System;

namespace DTO // Namespace ngắn gọn theo tên Project của bạn
{
    public class GameSapXepCauDTO
    {
        public int ID_GameInstance { get; set; }
        public string TenGame { get; set; }
        public DateTime NgayTao { get; set; }
        public int ID_GV { get; set; }
    }
}