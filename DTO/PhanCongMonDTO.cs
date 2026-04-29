using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PhanCongMonDTO
    {
        public int ID_GV { get; set; }
        public int MaMon { get; set; }

        public PhanCongMonDTO() { }

        public PhanCongMonDTO(int id_gv, int maMon)
        {
            ID_GV = id_gv;
            MaMon = maMon;
        }
    }
}

