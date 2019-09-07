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
    public partial class Form3 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
      
        string komut = "Select * from Randevu";
        public Form3()
        {
            InitializeComponent();
            Fillcombo();
        }
        public void Fillcombo()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select cinsiyet from cinsiyet ";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["cinsiyet"].ToString();
                    comboBox1.Items.Add(name);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select cinsiyet from cinsiyet where cinsiyet='" + comboBox1.Text + "'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["cinsiyet"].ToString();
                    textBox4.Text = name;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }


        }
        void verilerial()
        {



            string hastaadi = textBox1.Text;
            string hastasoyadi = textBox2.Text;
            string telefon = textBox3.Text;
            string cinsiyet = textBox4.Text;
            
        }
        void sqlveriyaz()
        {
            try
            {

                SqlConnection coni = new SqlConnection();

                coni.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut11 = "insert into Randevu(hastaAdi,hastaSoyadi,tcno,telefon,cinsiyet,tarih)values('" + textBox1.Text + "','" + textBox3.Text + "','"+textBox5.Text+"','" + textBox2.Text + "','" + textBox4.Text + "','" + this.dateTimePicker1.Text + "')";


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
        void listele()
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
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            comboBox1.Text= dataGridView1.CurrentRow.Cells[5].Value.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlveriyaz();
            verilerial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek istediginizden emin misiniz", "Dikkat", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from Randevu where randevuno=@numara";
                    cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    listele();
                }
            }
        }
     void   temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox1.Focus();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update Randevu set hastaAdi='" + textBox1.Text + "',hastaSoyadi='" + textBox3.Text + "',tcno='" + textBox5.Text + "',telefon='" + textBox2.Text + "',cinsiyet='" + textBox4.Text + "',tarih='" + this.dateTimePicker1.Text + "'where randevuno=@numara";
                cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                listele();
                temizle();
                MessageBox.Show("Güncelleme başarılı şekilde tamamlandı.");



            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
