using ChungKhoan.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChungKhoan
{
     static class Program
    {
     
        public static String connnectionString = "Data Source=DESKTOP-7V9QME6\\SQLEXPRESS;Initial Catalog=CHUNGKHOANTEST;Integrated Security=True";
        public static List<MaHoa> listMahoa = new List<MaHoa>();
        public static List<TapF> listTapF = new List<TapF>();
        public static List<TapL> listTapL = new List<TapL>();
        public static int minSup = 0;
       
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
