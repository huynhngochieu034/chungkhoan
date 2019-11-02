using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChungKhoan
{
    public partial class Form2 : Form
    {

        private ListView listViewD;
        private ListView listViewDetail;
        private List<int> list = new List<int>();
        private int k = 1;
        private string tempChar = "";

        public Form2(ListView listViewD, ListView listViewDetail)
        {
            InitializeComponent();
            this.listViewD = listViewD;
            this.listViewDetail = listViewDetail;

            listView1.Columns.Add("NGAY");
            listView1.Columns.Add("UNG VIEN");

            listView2.Columns.Add("UNG VIEN");
            listView2.Columns.Add("SUPPORT");

            KhoiTaoTapF1();
            KhoiTaoTapL1();

            button1.Enabled = false;

        }

        private void TapFToListView(model.TapF tapf)
        {
            
            int i = 0;
            StringBuilder strBuild = new StringBuilder();
            label2.Text = tapf.Lable;
            foreach (var t in tapf)
            {
                listView1.Items.Add(t.Key);
               
                foreach(string str in t.Value){
                    strBuild.Append(str);
                    strBuild.Append(", ");
                }
                if(strBuild.Length > 0)
                strBuild.Length--;
                strBuild.Length--;
                listView1.Items[i++].SubItems.Add(strBuild.ToString());
                strBuild.Clear();
            }

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

        }

        private void TapLToListView(model.TapL tapl)
        {
            label3.Text = tapl.Lable;
            int i = 0;
            foreach (var t in tapl)
            {
                foreach(string k in t.Key){
                    listView2.Items.Add(k);
                }
                listView2.Items[i++].SubItems.Add(t.Value.ToString());

            }
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void KhoiTaoTapF1()
        {
            model.TapF tapf = new model.TapF();
            tapf.Lable = "Tập F1";

            List<string> listTemp = new List<string>();
            string[] temp;
            int n = listViewD.Items.Count;
            for (int i = 0; i < n; i++)
            {
                temp = listViewD.Items[i].SubItems[0].Text.ToString().Split('/');
                
                for (int j = 1; j <= listViewDetail.Items.Count; j++)
                {
                    if (listViewD.Items[i].SubItems[j].Text == "1")
                    {
                        list.Add(j);
                        listTemp.Add(j.ToString());
                    }
                }
                tapf.Add(temp[0], new List<string>(listTemp));
                listTemp.Clear();
            }
            Program.listTapF.Add(tapf);
            TapFToListView(tapf);
        }

        private void KhoiTaoTapL1()
        {
            List<int> listMaHoa = new List<int>();
            foreach (var mh in Program.listMahoa)
            {
                listMaHoa.Add(mh.maHoa);
                Console.WriteLine("So ma hoa: "+mh.maHoa);
            }

            model.TapL tapl = new model.TapL();
            tapl.Lable = "Tập L1";

            List<string> tempList = new List<string>();
            int dem = 0;
            foreach (int number in listMaHoa)
            {
                tempList.Add(number.ToString());
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] == number)
                    {
                        dem++;
                    }
                }
                tapl.Add(new List<string>(tempList), dem);
                tempList.Clear();
                dem = 0;
            }
            Program.listTapL.Add(tapl);
            TapLToListView(tapl);
        }

        //private void TapF_To_TapL(model.TapF tapf, model.TapL tapl)
        //{
        //    List<string> listStr = new List<string>();
        //    foreach(var t in tapf){
        //        listStr = TimTapC(t.Value, 1);
        //        foreach (string str in listStr)
        //        {
        //            Console.WriteLine("Gia tri: " + str);
        //        }
        //    }
            

        //}

        private List<string> Tim_TapC_Tu_TapL(List<string> list, int k)
        {

            List<string> listTemp = new List<string>();
            if (k == 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        listTemp.Add(list[i] + " " + list[j]);
                    }
                }
            }
            else
            {

                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {   

                        if (list[i].Remove(list[i].Length - 1).Trim().CompareTo(list[j].Remove(list[i].Length - 1).Trim()) == 0)
                        {
                            string kq = list[i].Remove(list[i].Length - 1).Trim()
                                + " " + list[i].Remove(0, list[i].Length - 1).Trim()
                                + " " + list[j].Remove(0, list[j].Length - 1).Trim();
                            listTemp.Add(kq);
                        }
                    }
                }

            }

            return listTemp;
        }

        private void kiem_Tra_TapC_Voi_TapF(List<string> tapC, model.TapF tapf)
        {

        }

        private string xoaPhanTuCuoi(string str)
        {
            string a="";
            
            for (int i = str.Length-1; i >= 0; i--)
            {
                 tempChar += str[i];
                if(str[i]==' '){
                    a = str.Remove(i);
                    break;
                }
            }
            return a;
        }

        private string xoaPhanTuKeCuoi(string str)
        {
            string a = "";
            int dem = 0;
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == ' ')
                {
                    dem++;
                    if (dem == 2)
                    {
                        a = str.Remove(i+1);
                        break;
                    }
                    
                }
            }
            return a + tempChar.Trim();
        }

    
        private void Form2_Load(object sender, EventArgs e)
        {
           

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

     

        private void button2_Click(object sender, EventArgs e)
        {
            
            List<string> listStr = new List<string>();
            List<string> listResult = new List<string>();
            //model.TapF tapf = Program.listTapF[0];
            foreach (var t in Program.listTapL[0])
            {
                foreach (string str in t.Key)
                {
                    listStr.Add(str);
                }
                
            }

            listResult = Tim_TapC_Tu_TapL(listStr, 1);
            foreach (string str in listResult)
            {
                Console.WriteLine("Gia tri list string: " + str);
            }

            //string s = "11 22";
            //string[] check = s.Split(' ');

            //if (check.Length >= 2)
            //    Console.WriteLine(xoaPhanTuCuoi(s));
            //if (check.Length >= 2)
            //    Console.WriteLine(xoaPhanTuKeCuoi(s));
            //tempChar = "";


            string[] check;
            string resultLast;
            string resultNearLast;
            List<string> listKeyL = new List<string>();
            string temp="";
            foreach (var t in Program.listTapF[0])
            {
                foreach (string str in listResult)
                {
                    check = str.Split(' ');
                    if (check.Length >= 2)
                    {
                        resultLast = xoaPhanTuCuoi(str);
                        resultNearLast = xoaPhanTuKeCuoi(str);
                        tempChar = "";
                        if(t.Value.Contains(resultLast)){

                            if (t.Value.Contains(resultNearLast))
                            {
                                temp += resultLast;
                                temp += " ";
                                temp += resultNearLast;
                                temp += ", ";
                               
                                
                            }
                        }
                    }

                }
                listKeyL.Add(temp.Trim());
                temp = "";
            }
            foreach(string str in listKeyL){
                Console.WriteLine("L.KEY: "+str);
            }


            
           
        }
    }
}
