using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChungKhoan
{
    public partial class Form3 : Form
    {
        private int k = 0;
        List<string> List_Full = new List<string>();
        List<string> List_R = new List<string>();
        List<string> List_L = new List<string>();
        List<int> List_sup = new List<int>();
        List<int> List_supR = new List<int>();
        // List<string> list_ten = new List<string>();
        // List<KeyValuePair<string, string>> list_ten = new List<KeyValuePair<string, string>>();
        Dictionary<string, string> list_ten = new Dictionary<string, string>();

        public Form3(int k)
        {
            InitializeComponent();
            this.k = k;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //();
            // text1();
            Ten_CP();

            Sinh_Luat(k);
            Load_supR();
        }

        private List<string> Load_NhiPhan(int k)
        {
            List<string> listtemp = new List<string>();
            int n = k + 1;
            int[] mang = new int[n];
            int i;
            for (i = 0; i < n; i++)
            {
                mang[i] = 0;
            }
            // in ra cau hinh dau
            //for (i = 0; i < n; i++)
            //{
            //    Console.WriteLine(mang[i]);
            //}
            //xu ly
            for (i = n - 1; i >= 0; i--)
            {
                if (mang[i] == 0)  // neu gap phan tu 0 dau tien
                {
                    mang[i] = 1;  // gan no lai thanh 1
                    int j;
                    string ss = "";
                    for (j = i + 1; j < n; j++)  // gan toan bo phan tu phia sau no thanh 0
                    {
                        mang[j] = 0;
                    }
                    // in ra day so moi
                    for (j = 0; j < n; j++)
                    {
                        ss = ss + " " + mang[j];
                    }
                    listtemp.Add(ss.Trim());
                    //  Console.WriteLine(" ");

                    i = n;  // gan i = n de khi het vong lap i-- nen i se = n - 1, tu do chay lai tu vi tri cuoi.
                    // gan i = n - 1 la sai vi khi het vong lap hien tai i-- se thanh n - 2.
                }
            }
            return listtemp;
        }
        private void Sinh_chuoi(List<string> List_NP, string[] chuoiL, string ls, KeyValuePair<List<string>, int> l)
        {
            for (int j = 0; j < List_NP.Count - 1; j++)
            {
                string[] chuoi_np = List_NP[j].Split(' ');
                {
                    int g = 0;
                    string ss = "";
                    string sss = "";
                    for (g = 0; g < chuoi_np.Length; g++)
                    {
                        // int index = ls.IndexOf(chuoiL[g]);
                        // string s = ls.Remove(index); 
                        if (chuoi_np[g].Equals("0"))
                        {
                            ss = ss + " " + chuoiL[g];
                        }
                        if (chuoi_np[g].Equals("1"))
                        {
                            sss = sss + " " + chuoiL[g];
                        }
                    }
                    List_L.Add(sss.Trim());
                    List_R.Add(ss.Trim());
                    List_Full.Add(ls);
                    List_sup.Add(l.Value);
                }
            }
        }
        public void Sinh_Luat(int k)
        {
            for (int i = 0; i <= k; i++)
            {
                List<string> List_NP = new List<string>();
                List_NP = Load_NhiPhan(i);
                foreach (var l in Program.listTapL[i])
                {
                    foreach (string ls in l.Key)
                    {
                        if (i > 0)
                        {
                            string[] chuoiL = ls.Split(' ');
                            Sinh_chuoi(List_NP, chuoiL, ls, l);
                        }
                    }
                }
            }
            //foreach (string val in List_L)
            //{
            //    Console.WriteLine(val);
            //}
            //Console.WriteLine("---------------");
            //foreach (string val in List_R)
            //{
            //    Console.WriteLine(val);
            //}
            //Console.WriteLine("---------------");
            //foreach (string val in List_Full)
            //{
            //    Console.WriteLine(val);
            //}
            //Console.WriteLine("---------------");
            //foreach (int val in List_sup)
            //{
            //    Console.WriteLine(val);
            //}
        }
        public void Load_supR()
        {
            for (int i = 0; i < List_R.Count; i++)
            {
                Load_TapL(k, List_R[i]);
            }
        }
        public void Load_TapL(int k, string List_R)
        {
            for (int i = 0; i <= k; i++)
            {
                foreach (var l in Program.listTapL[i])
                {
                    for (int j = 0; j < l.Key.Count; j++)
                    {
                        if (List_R.Trim().Equals(l.Key[j].Trim()))
                        {
                            // Console.WriteLine("---"+List_R[i] + "------" + l.Value);
                            List_supR.Add(l.Value);
                        }
                    }
                }
            }
        }
        private string getName(string a)
        {
            foreach (var str in list_ten)
            {

                if (str.Key.ToString().Equals(a.Trim()))
                {

                    return str.Value.ToString();
                }
            }
            return null;
        }

        private string getNameMaHoa(string a)
        {
            foreach (var str in Program.listMahoa)
            {

                if (str.maHoa == Int32.Parse(a.Trim()))
                {

                    return str.maCp;
                }
            }
            return null;
        }
        public void Ten_CP()
        {

            for (int i = 0; i < Program.listMahoa.Count; i++)
            {
                list_ten.Add(Program.listMahoa[i].maHoa.ToString(), getNameCP(Program.listMahoa[i].maCp.ToString()));
            }

        }
        private string getNameCP(string maCP)
        {
            SqlConnection conn = new SqlConnection(Program.connnectionString);
            conn.Open();
            string sql = "select TENCTY from COPHIEU where MACP='" + maCP + "'";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = sql;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                return rdr.GetValue(0).ToString();
            }
            //conn.Close();
            return null;
        }
        public void Ket_Qua_1(int tb_conf)
        {
            lv_conf.Columns.Add("Luật sinh ra");
            lv_conf.Columns.Add(" %");

            for (int i = 0; i < List_sup.Count; i++)
            {
                double conf = Math.Round(((double)List_sup[i] / List_supR[i]) * 100);
                ListViewItem lv_Item = new ListViewItem();
                int dem = 0;
                string abc = "";
                string abcd = "";
                if (conf >= tb_conf)
                {
                    dem = i + 1;

                    string[] listtemp = List_R[i].Split(' ');
                    string[] listtemp1 = List_L[i].Split(' ');
                    int j = 0;
                    for (j = 0; j < listtemp.Length; j++)
                    {
                        // abc = abc +", "+ getNameMaHoa(listtemp[j]);
                        //  abcd = abcd + ", " + getNameMaHoa(listtemp1[j]);
                        abc = abc + ", " + getName(listtemp[j]);
                        // abcd = abcd + ", " + getNameCP(getNameMaHoa(listtemp1[j]));
                    }
                    for (j = 0; j < listtemp1.Length; j++)
                    {
                        // abc = abc + ", " + getNameMaHoa(listtemp[j]);
                        // abcd = abcd + ", " + getNameMaHoa(listtemp1[j]);
                        //abc = abc + ", " + getNameCP(getNameMaHoa(listtemp[j]));
                        abcd = abcd + ", " + getName(listtemp1[j]);
                    }
                    lv_Item.Text = abc.Remove(0, 1) + " ==> " + abcd.Remove(0, 1);
                    //lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = List_supR[i].ToString() });
                    // lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = List_sup[i].ToString() });
                    //lv_Item.Text = List_Full[i].Trim();
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = conf.ToString() + " %" });
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = dem.ToString() });
                    lv_conf.Items.Add(lv_Item);
                    // Console.WriteLine(List_R[i] + "==>" + List_L[i] + "  : " + conf);

                }


            }
            lv_conf.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            lv_conf.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        public void Ket_Qua(int tb_conf)
        {
            lv_conf.Columns.Add("Sinh luat");
            lv_conf.Columns.Add("Phan tram");
            lv_conf.Columns.Add("Phan tram");
            lv_conf.Columns.Add("Phan tram");
            lv_conf.Columns.Add("Phan tram");
            for (int i = 0; i < List_sup.Count; i++)
            {
                double conf = Math.Round(((double)List_sup[i] / List_supR[i]) * 100);
                ListViewItem lv_Item = new ListViewItem();
                if (conf > tb_conf)
                {
                    lv_Item.Text = List_R[i].Trim() + " ==> " + List_L[i].Trim();
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = List_Full[i].Trim() });
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = List_supR[i].ToString() });
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = List_sup[i].ToString() });
                    //lv_Item.Text = List_Full[i].Trim();
                    lv_Item.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = conf.ToString() + " %" });
                    lv_conf.Items.Add(lv_Item);
                    // Console.WriteLine(List_R[i] + "==>" + List_L[i] + "  : " + conf);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lv_conf.Clear();
            //  Ket_Qua(tb_trackbar.Value);
            Ket_Qua_1(tb_trackbar.Value);
        }

        private void tb_trackbar_Scroll(object sender, EventArgs e)
        {
            lb_conf.Text = tb_trackbar.Value + "";
        }
    }
}