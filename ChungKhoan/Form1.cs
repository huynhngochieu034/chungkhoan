using ChungKhoan.model;
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
            
            addListView();
            listView1.Show();
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
            param.Value = 0;
            param = cmd.Parameters.Add("@isinc", SqlDbType.Int);
            param.Value = 0;


            int i = 0;
            SqlDataReader rdr = cmd.ExecuteReader();
            ArrayList myList = new ArrayList(); 
            while(rdr.Read()){

                listView1.Columns.Add(rdr.GetName(i));
                
                i++;
                
            }
            conn.Close();
        }

        public int getValueRadioButton()
        {
           
            return 0;
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
    }
}
