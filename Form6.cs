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
    public partial class Form6 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
        string komut = "Select muayeneno,hastaAdi,hastaSoyadi from Muayene";
        string komut1 = "Select * from Recete";
        
      
        public Form6()
        {
            InitializeComponent();
            Fillcombo();
            Fillcombo1();
        }



        void sqlveriyaz()
        {
            try
            {

                SqlConnection coni = new SqlConnection();

                coni.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut11 = "insert into Recete(protokolid,dozaj,ilacinadi)values('" + textBox1.Text + "','" + textBox4.Text + "','" + textBox3.Text + "')";


                coni.Open();
                SqlCommand komutsatirim = new SqlCommand(komut11, coni);
                komutsatirim.ExecuteNonQuery();
                MessageBox.Show("VERİ GİRİŞİ BAŞARILI");

            }
            catch (Exception hata1)
            {
                MessageBox.Show("Bir hata meydana geldi !!!" + hata1.ToString());
            }
        }

        void listele1()
        {

            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView2.DataSource = tablo;

        }

        void listele2()
        {

            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
           

        }

        public void Fillcombo()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select dozaj from dozaj ";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string isim = reader["dozaj"].ToString();
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
            listele2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listele1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek istediginizden emin misiniz", "Dikkat", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from Recete where receteid=@numara";
                    cmd.Parameters.AddWithValue("@numara", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    listele1();
                }
            }
        }
        void temizle()
        {
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();

        }
        private void button6_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update Recete set protokolid='" + textBox1.Text + "',dozaj='" + textBox4.Text + "',ilacinadi='" + textBox3.Text + "' where  receteid=@numara";
                cmd.Parameters.AddWithValue("@numara", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                listele1();
                temizle();
                MessageBox.Show("Güncelleme başarılı şekilde tamamlandı.");
                


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlveriyaz();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select dozaj from dozaj where dozaj='" + comboBox1.Text + "'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string isim = reader["dozaj"].ToString();
                    textBox4.Text = isim;
                }

            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select ilacadi from ilackod where ilacadi='" + comboBox2.Text + "'";
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
}
