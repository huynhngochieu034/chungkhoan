using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChungKhoan.model
{
    class MaHoa
    {
        public MaHoa(string macp, int mahoa)
        {
            maCp = macp;
            maHoa = mahoa;
        }
        public string maCp { get; set; }
        public int maHoa { get; set; }
    }
}
