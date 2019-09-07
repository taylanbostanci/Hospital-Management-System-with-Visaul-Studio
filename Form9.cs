using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace hastaneotomasyonu
{
    public partial class Form9 : Form
    {
SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");


        public Form9()

        {
            InitializeComponent();
            Fillcombo();
            Fillcombo1();
        }
        public void Fillcombo()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select taniadi from kodlistesi ";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string isim = reader["taniadi"].ToString();
                    comboBox1.Items.Add(isim);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        public void Fillcombo1()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select ilacadi from ilackod ";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string isim = reader["ilacadi"].ToString();
                    comboBox2.Items.Add(isim);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();


            string sorgu = "Select protokolid,taniadi,tanikodu from Tani where tanikodu Like '" + textBox1.Text + "%'";

            SqlDataAdapter adap = new SqlDataAdapter(sorgu, con);

            DataSet ds = new DataSet();

            adap.Fill(ds, "tanikodu");
            try
            {
                this.dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {
                MessageBox.Show("Geçerli isime ait tanı bulunamadı");
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();


            string sorgu = "Select protokolid,ilacinadi from Recete where ilacinadi Like '%" + textBox3.Text + "%'";

            SqlDataAdapter adap = new SqlDataAdapter(sorgu, con);

            DataSet ds = new DataSet();

            adap.Fill(ds, "ilacinadi");
            try
            {
                this.dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {
                MessageBox.Show("Geçerli isime ait ilaç bulunamadı");
            }
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            con.Open();


            string sorgu = "Select protokolid,taniadi,tanikodu from Tani where taniadi Like '%" + textBox2.Text + "%'";

            SqlDataAdapter adap = new SqlDataAdapter(sorgu, con);

            DataSet ds = new DataSet();

            adap.Fill(ds, "taniadi");
            try
            {
                this.dataGridView1.DataSource = ds.Tables[0];

            }
            catch (Exception)
            {
                MessageBox.Show("Geçerli isime ait tanı bulunamadı");
            }
            con.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select taniadi from kodlistesi where taniadi='%" + comboBox1.Text + "%'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string isim = reader["taniadi"].ToString();
                    textBox2.Text = isim;
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            {
                string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string Query = "Select ilacadi from ilackod where ilacadi='%" + comboBox1.Text + "%'";
                SqlConnection cdn = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand(Query, cdn);

                SqlDataReader reader;
                try
                {
                    cdn.Open();
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string isim = reader["ilacadi"].ToString();
                        textBox3.Text = isim;
                    }

                }
                catch (Exception e1)
                {
                    MessageBox.Show(e1.Message);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
