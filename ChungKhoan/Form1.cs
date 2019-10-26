
using System;
using System.Collections;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            labelMinSub.Text = "0";

            radioButtonTang.Checked = true;
            addListView();
            maHoaItems();
        }
        public void addListView()
        {
            SqlConnection conn = new SqlConnection(Program.connnectionString);
            conn.Open();

            String spName = "SP_GIAOTAC";
            SqlCommand cmd = new SqlCommand(spName, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter param;
            param = cmd.Parameters.Add("@minsup", SqlDbType.Float);
            param.Value = Int32.Parse(labelMinSub.Text);
            param = cmd.Parameters.Add("@isinc", SqlDbType.Int);
            param.Value = getValueRadioButton();


            int i = 0;
            SqlDataReader rdr = cmd.ExecuteReader();
            string[] date;
            ArrayList mylist = new ArrayList();
            string temp;

            for (int j = 0; j < rdr.FieldCount; j++)
            {
                temp = rdr.GetName(j).ToString();
                listView1.Columns.Add(temp);
                mylist.Add(temp);
            }

            while (rdr.Read())
            {
                date = rdr["NGAY"].ToString().Split(' ');
                listView1.Items.Add(date[0]);
                foreach (string str in mylist)
                {
                    if (!str.Equals("NGAY"))
                        listView1.Items[i].SubItems.Add(rdr[str].ToString());
                }
                i++;
            }
            conn.Close();
        }

        public int getValueRadioButton()
        {
            if (radioButtonTang.Checked == true)
            {
                return 1;
            }
            else return 0;

        }
        public void maHoaItems()
        {
            listView2.Columns.Add("Mã Item");
            listView2.Columns.Add("Mã Cổ Phiếu");

            for (int i = 0; i < listView1.Columns.Count; i++)
            {
                listView2.Items.Add((i).ToString());
                listView2.Items[i].SubItems.Add(listView1.Columns[i].Text);
            }
            if (listView2.Items.Count != 0)
            listView2.Items.Remove(listView2.Items[0]);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            labelMinSub.Text = trackBar1.Value.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Clear();
            listView2.Clear();
            addListView();
            maHoaItems();



            if (listView1.Items.Count == 0)
                MessageBox.Show("Không có tập D nào thỏa minSub!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2(listView1, listView2);
            frm2.ShowDialog();
        }
    }
}
