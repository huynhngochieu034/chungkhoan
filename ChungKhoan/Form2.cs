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

        public Form2(ListView listViewD, ListView listViewDetail)
        {
            InitializeComponent();
            this.listViewD = listViewD;
            this.listViewDetail = listViewDetail;
            tapF1();
            ungVien();
            button1.Enabled = false;
        }

        public void tapF1()
        {
            listView1.Columns.Add("SO CP");
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
                        strBuild.Append(j.ToString());
                        strBuild.Append(",");
                    }
                }
                strBuild.Length--;
                listView1.Items[i].SubItems.Add(strBuild.ToString());
                strBuild.Clear();
            }
        }



        public void ungVien()
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
            PrioriGen PrioriGen = new PrioriGen(2, distinct.Count);
            PrioriGen.generate();
            // in kết quả
            foreach (string result in PrioriGen.ResultList) 
            {
                listGen.Add(result);
            }

            listGen.RemoveAt(0);
            listGen.RemoveAt(listGen.Count - 1);

            StringBuilder sp = new StringBuilder();
            foreach (string result in listGen)
            {
                sp.Append(result);
                
            }
            listView1.Items.Add(sp.ToString());
            //List<string> listtemp = new List<string>();
            //foreach (string result in listGen)
            //{
            //    listtemp.AddRange(result.Select(c => c.ToString()));
                
                
            //}
            //foreach (string result in listtemp)
            //{
            //    listView1.Items.Add(result);

            //}
            



        }
    }
}
