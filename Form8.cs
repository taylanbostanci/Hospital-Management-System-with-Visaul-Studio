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
    public partial class Form8 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
       string  komut="Select * from fizikimuayene";
        string komut1 = "Select muayeneno,hastaAdi,hastaSoyadi from Muayene";
        public Form8()
        {
            InitializeComponent();
        }
        void sqlveriyaz()
        {
            try
            {

                SqlConnection coni = new SqlConnection();

                coni.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut11 = "insert into fizikimuayene(muayeneno,hastaAdi,hastaSoyadi,resim1,resim2,resim3,resim4,resim5,resim6,resim7,resim8,resim9,resim10)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + richTextBox1.Text + "','" + richTextBox2.Text + "','" + richTextBox3.Text + "','" + richTextBox4.Text + "','" + richTextBox5.Text + "','" + richTextBox6.Text + "','" + richTextBox7.Text + "','" + richTextBox8.Text + "','" + richTextBox9.Text + "','" + richTextBox10.Text + "')";


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
            dataGridView2.DataSource = tablo;
        }
        void listele1()
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, con);
            adp.Fill(tablo);
            dataGridView2.DataSource = tablo;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listele();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            richTextBox1.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
            richTextBox2.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
            richTextBox3.Text = dataGridView2.CurrentRow.Cells[6].Value.ToString();
            richTextBox4.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
            richTextBox5.Text = dataGridView2.CurrentRow.Cells[8].Value.ToString();
            richTextBox6.Text = dataGridView2.CurrentRow.Cells[9].Value.ToString();
            richTextBox7.Text = dataGridView2.CurrentRow.Cells[10].Value.ToString();
            richTextBox8.Text = dataGridView2.CurrentRow.Cells[11].Value.ToString();
            richTextBox9.Text = dataGridView2.CurrentRow.Cells[12].Value.ToString();
            richTextBox10.Text = dataGridView2.CurrentRow.Cells[13].Value.ToString();







        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Silmek istediginizden emin misiniz", "Dikkat", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "delete from fizikimuayene where fizikiid=@numara";
                    cmd.Parameters.AddWithValue("@numara", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    listele();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlveriyaz();
        }
        void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();
            richTextBox2.Clear();
            richTextBox3.Clear();
            richTextBox4.Clear();
            richTextBox5.Clear();
            richTextBox6.Clear();
            richTextBox7.Clear();
            richTextBox8.Clear();
            richTextBox9.Clear();
            richTextBox10.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update fizikimuayene set muayeneno='" + textBox1.Text + "',hastaAdi='" + textBox2.Text + "',hastaSoyadi='" + textBox3.Text + "',resim1='" + richTextBox1.Text + "',resim2='" + richTextBox2.Text + "',resim3='" + richTextBox3.Text + "',resim4='" + richTextBox4.Text + "',resim5='" + richTextBox5.Text + "',resim6='" + richTextBox6.Text + "',resim7='" + richTextBox7.Text + "',resim8='" + richTextBox8.Text + "',resim9='" + richTextBox9.Text + "',resim10='" + richTextBox10.Text + "' where  fizikiid=@numara";
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
            listele1();
        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }
    }
}
