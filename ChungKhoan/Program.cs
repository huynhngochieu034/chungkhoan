using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChungKhoan
{
    public static class Program
    {
     
        public static String connnectionString = "Data Source=DESKTOP-7V9QME6\\SQLEXPRESS;Initial Catalog=CHUNGKHOAN;Integrated Security=True";

       
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
