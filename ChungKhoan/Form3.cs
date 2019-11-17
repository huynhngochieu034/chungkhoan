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
        private List<string> list = new List<string>();
        private int k = 0;
        private string tempLastChar = "";
        private string tempNearLastChar = "";
        
        public Form3(int k)
        {
            InitializeComponent();
            this.k = k;

            if (Form1.checkTangGiam == 1)
            {
                label6.Text = "CÁC CỔ PHIẾU CÙNG TĂNG";
                 
            }
            else label6.Text = "CÁC CỔ PHIẾU CÙNG GIẢM";

            label3.Text = "0";
            listView1.Columns.Add("TEN CTY");
            getRules();
        }

 


        private string xoaPhanTuCuoi(string str)
        {
            string a = "";
            for (int i = str.Length - 1; i >= 0; i--)
            {
                tempLastChar += str[i];
                if (str[i] == ' ')
                {
                    a = str.Remove(i);
                    break;
                }
            }
            return a.Trim();
        }

 

        private string xoaPhanTuKeCuoi(string str)
        {
            string a = "";
            int dem = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                tempNearLastChar += str[i];
                if (str[i] == ' ')
                {
                    
                    dem++;                   
                    if (dem == 2)
                    {
                        
                        a = str.Remove(i + 1);
                        break;
                    }
                }
            }
            return a + tempLastChar.Trim();
        }


        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            getRules();
        }

        private void getRules()
        {
            string strRemoveNearLast;
            string strRemoveLast;
            string[] tempString;
            string[] check;
            string cacKiTuCuoi;
            string kiTuDau;

            string[] checktoListView;
            StringBuilder strBuilder = new StringBuilder();

            listView1.Items.Clear();
            foreach (var t in Program.listTapL[k])
            {
                foreach (string str in t.Key)
                {

                    check = str.Split(' ');
                    strRemoveLast = xoaPhanTuCuoi(str);
                    strRemoveNearLast = xoaPhanTuKeCuoi(str);

                    tempString = tempNearLastChar.Trim().Split(' ');
                    cacKiTuCuoi = layCacKiTuCuoi(str);
                    kiTuDau = layKiTuDau(str).Trim();

                    foreach (var t2 in Program.listTapL[k - 1])
                    {
                        foreach (string strr in t2.Key)
                        {
                            if (strr.Equals(strRemoveLast))
                            {

                                //Console.WriteLine("str: " + strRemoveLast + "=>" + tempLastChar.Trim());
                                //Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                {

                                    checktoListView = strRemoveLast.Split(' ');
                                    foreach (string s in checktoListView)
                                    {
                                        
                                        strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                        strBuilder.Append(", ");
                                    }

                                    if (strBuilder.Length == 0)
                                    {
                                        strBuilder.Append(", ");
                                    }
                                    strBuilder.Length--;
                                    strBuilder.Length--;

                                    strBuilder.Append(" => ");
                                   
                                    strBuilder.Append(getNameCP(getNameMaHoa(tempLastChar)));
                                    listView1.Items.Add(strBuilder.ToString());

                                    strBuilder.Clear();
                                }
                            }


                            if (strr.Equals(strRemoveNearLast))
                            {
                                //Console.WriteLine("str: " + strRemoveNearLast + "=>" + tempString[1]);
                               // Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                {

                                    checktoListView = strRemoveNearLast.Split(' ');
                                    foreach (string s in checktoListView)
                                    {
                                        //strBuilder.Append(getNameMaHoa(s));
                                        strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                        strBuilder.Append(", ");
                                    }
                                    if (strBuilder.Length == 0)
                                    {
                                        strBuilder.Append(", ");
                                    }
                                    strBuilder.Length--;
                                    strBuilder.Length--;
                                    strBuilder.Append(" => ");
                                    //strBuilder.Append(getNameMaHoa(tempString[1]));
                                    strBuilder.Append(getNameCP(getNameMaHoa(tempString[1])));
                                    listView1.Items.Add(strBuilder.ToString());
                                    strBuilder.Clear();
                                }
                            }
                            if (check.Length >= 3 && strr.Equals(cacKiTuCuoi))
                            {
                                //Console.WriteLine("str: " + cacKiTuCuoi + "=>" + kiTuDau);
                                //Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                {

                                    checktoListView = cacKiTuCuoi.Split(' ');
                                    foreach (string s in checktoListView)
                                    {

                                        //strBuilder.Append(getNameMaHoa(s));
                                        strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                        strBuilder.Append(", ");
                                    }
                                    if (strBuilder.Length == 0)
                                    {
                                        strBuilder.Append(", ");
                                    }
                                    strBuilder.Length--;
                                    strBuilder.Length--;
                                    strBuilder.Append(" => ");
                                    //strBuilder.Append(getNameMaHoa(kiTuDau));
                                    strBuilder.Append(getNameCP(getNameMaHoa(kiTuDau)));
                                    listView1.Items.Add(strBuilder.ToString());
                                    strBuilder.Clear();
                                }
                            }

                        }
                    }
                    if (check.Length >= 3)
                    {
                        foreach (var t2 in Program.listTapL[0])
                        {
                            foreach (string strr in t2.Key)
                            {
                                if (strr.Equals(tempLastChar.Trim()))
                                {
                                    //Console.WriteLine("str: " + tempLastChar.Trim() + "=>" + strRemoveLast);
                                    //Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                    if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                    {

                                        //strBuilder.Append(getNameMaHoa(tempLastChar.Trim()));
                                        strBuilder.Append(getNameCP(getNameMaHoa(tempLastChar.Trim())));

                                        strBuilder.Append(" => ");
                                        checktoListView = strRemoveLast.Split(' ');
                                        foreach (string s in checktoListView)
                                        {
                                            strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                            //strBuilder.Append(getNameMaHoa(s));
                                            strBuilder.Append(", ");
                                        }
                                        if (strBuilder.Length == 0)
                                        {
                                            strBuilder.Append(", ");
                                        }
                                        strBuilder.Length--;
                                        strBuilder.Length--;

                                        listView1.Items.Add(strBuilder.ToString());
                                        strBuilder.Clear();
                                    }
                                }
                                if (strr.Equals(tempString[1]))
                                {
                                   // Console.WriteLine("str: " + tempString[1] + "=>" + strRemoveNearLast);
                                   // Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                    if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                    {

                                        //strBuilder.Append(getNameMaHoa(tempString[1]));
                                        strBuilder.Append(getNameCP(getNameMaHoa(tempString[1])));
                                        strBuilder.Append(" => ");
                                        checktoListView = strRemoveNearLast.Split(' ');
                                        foreach (string s in checktoListView)
                                        {
                                            //strBuilder.Append(getNameMaHoa(s));
                                            strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                            strBuilder.Append(", ");
                                        }
                                        if (strBuilder.Length == 0)
                                        {
                                            strBuilder.Append(", ");
                                        }
                                        strBuilder.Length--;
                                        strBuilder.Length--;
                                        listView1.Items.Add(strBuilder.ToString());
                                        strBuilder.Clear();
                                    }
                                }
                                if (strr.Equals(kiTuDau))
                                {
                                    //Console.WriteLine("str: " + kiTuDau + "=>" + cacKiTuCuoi);
                                    //Console.WriteLine("Sup: " + t.Value * 100 / t2.Value);
                                    if (t.Value * 100 / t2.Value >= trackBar1.Value)
                                    {

                                        //strBuilder.Append(getNameMaHoa(kiTuDau));
                                        strBuilder.Append(getNameCP(getNameMaHoa(kiTuDau)));
                                        strBuilder.Append(" => ");
                                        checktoListView = cacKiTuCuoi.Split(' ');
                                        foreach (string s in checktoListView)
                                        {
                                            //strBuilder.Append(getNameMaHoa(s));
                                            strBuilder.Append(getNameCP(getNameMaHoa(s)));
                                            strBuilder.Append(", ");
                                        }
                                        if (strBuilder.Length == 0)
                                        {
                                            strBuilder.Append(", ");
                                        }
                                        strBuilder.Length--;
                                        strBuilder.Length--;
                                        listView1.Items.Add(strBuilder.ToString());
                                        strBuilder.Clear();
                                    }
                                }
                            }
                        }

                    }

                    tempLastChar = "";
                    tempNearLastChar = "";
                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
            return null;
        }

        private string layCacKiTuCuoi(string str)
        {
            string a = "";
            int dem = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ' ')
                {
                    dem = 1;
                }
                if (dem == 1)
                {
                    a += str[i];
                }


            }
            return a.Trim();
        }



        private string layKiTuDau(string str)
        {
            string a = "";
            for (int i = 0; i < str.Length; i++)
            {
                a += str[i];
                if (str[i] == ' ')
                {
                    break;
                }
            }
            return a;
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

   



    }
}
