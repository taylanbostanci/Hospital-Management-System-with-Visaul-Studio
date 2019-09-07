using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using System.Windows.Forms.ComponentModel;
namespace hastaneotomasyonu
{
    public partial class Form4 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
        string komut = "Select * from Muayene";
        SqlCommand komut7;
        string imgLoc = "";
        string komut1 = "Select randevuno,hastaAdi,hastaSoyadi from Randevu";
        string komut2 = "Select protokolno,hastaAdi,hastaSoyadi from HastaKayit";
        string komut3 = "Select muayenetipi from muayenetipi";
        public Form5 frm5;
        public Form8 frm8;
        public Form10 frm10;
        public Form4()
        {
            frm10 = new Form10();
            frm8 = new Form8();
            frm5 = new Form5();
            InitializeComponent();
            Fillcombo();



        }
        public void Fillcombo()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select muayenetipi from muayenetipi ";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string isim = reader["muayenetipi"].ToString();
                    comboBox1.Items.Add(isim);
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

            string Query = "Select muayenetipi from muayenetipi where muayenetipi='" + comboBox1.Text + "'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["muayenetipi"].ToString();
                    textBox3.Text = name;
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }


        }

        void sqlveriyaz()
        {
            try
            {

                SqlConnection coni = new SqlConnection();

                coni.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut11 = "insert into Muayene(hastaAdi,hastaSoyadi,hikayesi,sikayeti,ozgecmisi,soygecmisi,kullanilanilaclar,muayenetipi)values('" + textBox1.Text + "','" + textBox2.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + richTextBox7.Text + "','" + textBox3.Text + "')";


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

        private void button8_Click(object sender, EventArgs e)
        {
            frm5.ShowDialog();

        }
        void listele2()
        {

            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut2, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }
        void listele1()
        {

            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }
        void listele()
        {

            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, con);
            adp.Fill(tablo);
            dataGridView2.DataSource = tablo;

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
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
           
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            richTextBox1.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            richTextBox2.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            richTextBox3.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            richTextBox4.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            richTextBox7.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();

           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listele1();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listele2();
        }

        private void button7_Click(object sender, EventArgs e)
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
                    cmd.CommandText = "delete from Muayene where muayeneno=@numara";
                    cmd.Parameters.AddWithValue("@numara", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    listele();
                }
            }
        }
        void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox7.Clear();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update Muayene set hastaAdi='" + textBox1.Text + "',hastaSoyadi='" + textBox2.Text + "',hikayesi='" + richTextBox1.Text + "',sikayeti='" + richTextBox2.Text + "',ozgecmisi='" + richTextBox3.Text + "',soygecmisi='" + richTextBox4.Text + "',kullanilanilaclar='" + richTextBox7.Text + "',muayenetipi='" + textBox3.Text + "' where  muayeneno=@numara";
                cmd.Parameters.AddWithValue("@numara", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                listele();
                temizle();
                MessageBox.Show("Güncelleme başarılı şekilde tamamlandı.");



            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlveriyaz();
        }


      

        

        
        

        private void button12_Click(object sender, EventArgs e)
        {
            frm8.ShowDialog();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            frm10.ShowDialog();
        }
    }
}
