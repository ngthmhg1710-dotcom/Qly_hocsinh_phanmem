using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanCongLopDTO
    {
        public int ID_GV { get; set; }
        public int MaLop { get; set; }
        public string VaiTro { get; set; }

        public PhanCongLopDTO() { }

        public PhanCongLopDTO(int id_gv, int maLop, string vaiTro)
        {
            ID_GV = id_gv;
            MaLop = maLop;
            VaiTro = vaiTro;
        }
    }
}

