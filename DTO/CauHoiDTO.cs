using System;
using System.Collections.Generic;

namespace DTO
{
    public class CauHoiHinhAnhDTO
    {
        public List<string> Anh { get; set; }
        public string AmThanh { get; set; }
        public int DapAn { get; set; }

        public CauHoiHinhAnhDTO() { }

        public CauHoiHinhAnhDTO(List<string> anh, string amThanh, int dapAn)
        {
            if (anh == null || anh.Count != 3)
            {
                throw new ArgumentException("Danh sách ảnh phải có đúng 3 phần tử.");
            }
            Anh = anh;
            AmThanh = amThanh;
            DapAn = dapAn;
        }
    }
}