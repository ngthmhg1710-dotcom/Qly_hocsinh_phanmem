using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using DTO;

namespace DAL
{
    // Đổi tên lớp từ EventDAL thành LichHenGioDAL
    public class LichHenGioDAL
    {
        private readonly string connectionString = "Data Source=DESKTOP-C7KPB1H;Initial Catalog=QuanLyHocSinh;Integrated Security=True;";

        public List<Event> GetEventsByTeacherId(int teacherId)
        {
            var events = new List<Event>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetLichHenByGiaoVien", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", teacherId);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var evt = new Event
                        {
                            Id = Convert.ToInt32(reader["ID_LichHen"]),
                            Title = reader["TieuDe"].ToString(),
                            Description = reader["MoTa"].ToString(),
                            StartTime = Convert.ToDateTime(reader["ThoiGianBatDau"]),
                            EndTime = Convert.ToDateTime(reader["ThoiGianKetThuc"]),
                            EventColor = Color.FromArgb(int.Parse(reader["MauSuKien"].ToString())),
                            TextColor = Color.White
                        };
                        if (reader["ThoiGianNhacNho"] != DBNull.Value)
                        {
                            evt.ReminderTime = Convert.ToDateTime(reader["ThoiGianNhacNho"]);
                        }
                        else
                        {
                            evt.ReminderTime = null; // Gán là null nếu trong CSDL là NULL
                        }
                        events.Add(evt);
                    }
                }
            }
            return events;
        }

        public void InsertEvent(Event newEvent, int teacherId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InsertLichHen", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_GV", teacherId);
                    cmd.Parameters.AddWithValue("@TieuDe", newEvent.Title);
                    cmd.Parameters.AddWithValue("@MoTa", newEvent.Description);
                    cmd.Parameters.AddWithValue("@ThoiGianBatDau", newEvent.StartTime);
                    cmd.Parameters.AddWithValue("@ThoiGianKetThuc", newEvent.EndTime);
                    cmd.Parameters.AddWithValue("@MauSuKien", newEvent.EventColor.ToArgb().ToString());
                    cmd.Parameters.AddWithValue("@ThoiGianNhacNho", (object)newEvent.ReminderTime ?? DBNull.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEvent(Event eventToUpdate)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_UpdateLichHen", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_LichHen", eventToUpdate.Id);
                    cmd.Parameters.AddWithValue("@TieuDe", eventToUpdate.Title);
                    cmd.Parameters.AddWithValue("@MoTa", eventToUpdate.Description);
                    cmd.Parameters.AddWithValue("@ThoiGianBatDau", eventToUpdate.StartTime);
                    cmd.Parameters.AddWithValue("@ThoiGianKetThuc", eventToUpdate.EndTime);
                    cmd.Parameters.AddWithValue("@MauSuKien", eventToUpdate.EventColor.ToArgb().ToString());
                    cmd.Parameters.AddWithValue("@ThoiGianNhacNho", (object)eventToUpdate.ReminderTime ?? DBNull.Value);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEvent(int eventId)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_DeleteLichHen", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID_LichHen", eventId);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}