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
            tapF1();
            ungVien1();
            button1.Enabled = false;
        }

        public void tapF1()
        {
            listView1.Columns.Add("NGAY");
            listView1.Columns.Add("UNG VIEN");

            StringBuilder strBuild = new StringBuilder();
            int n = listViewD.Items.Count;
            for (int i = 0; i < n; i++)
            {

                listView1.Items.Add((i + 1).ToString());
                for (int j = 1; j <= listViewDetail.Items.Count; j++)
                {
                    if (listViewD.Items[i].SubItems[j].Text == "1")
                    {
                        list.Add(j);
                        strBuild.Append("{");
                        strBuild.Append(j.ToString());
                        strBuild.Append("}");
                        strBuild.Append(",");
                    }
                }
                strBuild.Length--;
                listView1.Items[i].SubItems.Add(strBuild.ToString());
                strBuild.Clear();
            }
        }

        public void ungVien1()
        {
            listView2.Columns.Add("UNG VIEN");
            listView2.Columns.Add("SUPPORT");

            List<int> distinct = list.Distinct().ToList();
            distinct.Sort();

            int i = 0;
            int dem = 0;
            foreach (int number in distinct)
            {
                listView2.Items.Add(number.ToString());
                for (int j = 0; j < list.Count; j++)
                {
                    if (list[j] == number)
                    {
                        dem++;
                    }
                }
                listView2.Items[i++].SubItems.Add(dem.ToString());
                dem = 0;
            }
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
            
            //List<string> listTemp = TimTapC(listGen, 4);
            //foreach (string i in listTemp)
            //{
            //    Console.WriteLine(i);
            //}

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
