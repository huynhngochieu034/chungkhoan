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
            
            List<int> distinct = list.Distinct().ToList();
            distinct.Sort();

            model.TapL tapl = new model.TapL();
            tapl.Lable = "Tập L1";

            List<string> tempList = new List<string>();
            int i = 0;
            int dem = 0;
            foreach (int number in distinct)
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

        private void TapF_To_TapL(model.TapF tapf, model.TapL tapl)
        {

        }

        private List<string> TimTapC(List<string> list, int k)
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
            List<int> distinct = list.Distinct().ToList();
            List<string> listGen = new List<string>();
            distinct.Sort();

            string clean;
            List<string> listClean = new List<string>();
            List<string> result = new List<string>();
            List<string> resultLast = new List<string>();
            StringBuilder strBuild = new StringBuilder();
            
          
            foreach (ListViewItem item in listView1.Items)
            {
               
                clean = item.SubItems[1].Text.Replace("{","").Replace("}","").Replace(",","");
                Console.WriteLine(clean);
                foreach (char ch in clean)
                {
                    listClean.Add(ch.ToString());
                }
               
                result = TimTapC(listClean, k);
                listClean.Clear();
                for (int i = 0; i < result.Count; i++)
                {
                    strBuild.Append("{");
                    strBuild.Append(result[i].ToString());
                    strBuild.Append("}");
                    strBuild.Append(",");
                }
               
                strBuild.Length--;
                Console.WriteLine("Chuoi item: " + strBuild.ToString());
                resultLast.Add(strBuild.ToString());
                //listView1.Items[index].SubItems.Add(strBuild.ToString());
                strBuild.Clear();
                
            }
            showListView1(resultLast);
            List<string> abc = resultLast.Distinct().ToList();
           
            foreach(string aa in abc){
                Console.WriteLine(aa);
            }
        }

        private void showListView1(List<string> listShow)
        {
            listView1.Items.Clear();
            for (int i = 0; i < listShow.Count; i++)
            {
                listView1.Items.Add((i + 1).ToString());
                listView1.Items[i].SubItems.Add(listShow[i]);
            }
        }
    }
}
