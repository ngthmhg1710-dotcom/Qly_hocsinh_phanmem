using System;
using System.Collections.Generic;
using DAL;
using DTO;

namespace BUS
{
    // Đổi tên lớp từ EventBUS thành LichHenGioBUS
    public class LichHenGioBUS
    {
        // Đổi tên biến từ _eventDAL thành _lichHenGioDAL
        private readonly LichHenGioDAL _lichHenGioDAL;

        // Đổi tên constructor
        public LichHenGioBUS()
        {
            // Khởi tạo lớp LichHenGioDAL mới
            _lichHenGioDAL = new LichHenGioDAL();
        }

        public List<Event> GetEventsByTeacherId(int teacherId)
        {
            if (teacherId <= 0)
            {
                throw new ArgumentException("ID giáo viên không hợp lệ.");
            }
            try
            {
                // Gọi phương thức từ _lichHenGioDAL
                return _lichHenGioDAL.GetEventsByTeacherId(teacherId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi từ lớp DAL khi lấy danh sách sự kiện: " + ex.Message);
            }
        }

        public void AddNewEvent(Event newEvent, int teacherId)
        {
            if (newEvent.EndTime <= newEvent.StartTime)
            {
                throw new InvalidOperationException("Thời gian kết thúc phải sau thời gian bắt đầu.");
            }
            try
            {
                // Gọi phương thức từ _lichHenGioDAL
                _lichHenGioDAL.InsertEvent(newEvent, teacherId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi từ lớp DAL khi thêm sự kiện: " + ex.Message);
            }
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            if (eventToUpdate.EndTime <= eventToUpdate.StartTime)
            {
                throw new InvalidOperationException("Thời gian kết thúc phải sau thời gian bắt đầu.");
            }
            try
            {
                // Gọi phương thức từ _lichHenGioDAL
                _lichHenGioDAL.UpdateEvent(eventToUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi từ lớp DAL khi cập nhật sự kiện: " + ex.Message);
            }
        }

        public void DeleteEvent(int eventId)
        {
            if (eventId <= 0)
            {
                throw new ArgumentException("ID sự kiện không hợp lệ.");
            }
            try
            {
                // Gọi phương thức từ _lichHenGioDAL
                _lichHenGioDAL.DeleteEvent(eventId);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi từ lớp DAL khi xóa sự kiện: " + ex.Message);
            }
        }
    }
}