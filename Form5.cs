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
    public partial class Form5 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
        string komut = "Select muayeneno,hastaAdi,hastaSoyadi from Muayene ";
        string komut1 = "Select *from Tani";
        string komut2 = "Select *from kodlistesi";
        public Form5()
        {
            InitializeComponent();
            Fillcombo();
            
            
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
                    string name = reader["taniadi"].ToString();
                    comboBox1.Items.Add(name);
                    comboBox2.Items.Add(name);
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

            string Query = "Select taniadi,tanikodu from kodlistesi where taniadi='" + comboBox1.Text + "'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);

            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["taniadi"].ToString();
                    string name1 = reader["tanikodu"].ToString();
                    textBox1.Text = name;
                    textBox7.Text = name1;
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

            string Query = "Select taniadi,tanikodu from kodlistesi where taniadi='" + comboBox2.Text + "'";
            SqlConnection cdn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand(Query, cdn);
 
            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                    string name = reader["taniadi"].ToString();
                    string name1 = reader["tanikodu"].ToString();
                    textBox2.Text = name;
                    textBox6.Text = name1;
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

                SqlConnection con = new SqlConnection();

                con.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut11 = "insert into Tani(protokolid,taniadi,ontani,tanikodu,ontanikodu)values('" + textBox5.Text + "','" + textBox1.Text + "','" +textBox2.Text + "','"+textBox6.Text+"','"+textBox7.Text+"')";


                con.Open();
                SqlCommand komutsatirim = new SqlCommand(komut11, con);
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
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }
        void listele1()
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

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut1, con);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sqlveriyaz();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            listele1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "update Tani set protokolid='" + textBox5.Text + "',taniadi='" + textBox1.Text + "',ontani='" + textBox2.Text + "' ,tanikodu='"+textBox7.Text+"',ontanikodu='"+textBox6.Text+"' where  taniid=@numara";
                cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                listele();
                
                MessageBox.Show("Güncelleme başarılı şekilde tamamlandı.");



            }
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
                    cmd.CommandText = "delete from Tani where taniid=@numara";
                    cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    con.Close();
                    listele();
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

            con.Open();


            string sorgu = "Select * from kodlistesi where tanikodu Like '" + textBox8.Text + "%'";

            SqlDataAdapter adap = new SqlDataAdapter(sorgu, con);

            DataSet ds = new DataSet();

            adap.Fill(ds, "kodlistesi");
            try
            {
                this.dataGridView1.DataSource = ds.Tables[0];
                textBox7.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            }
            catch(Exception)
            {
                MessageBox.Show("GEÇERLİ GİRDİYLE ALAKALI TANI BULUNAMADI");
            }


            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {

            con.Open();


            string sorgu = "Select * from kodlistesi where tanikodu Like '" + textBox9.Text + "%'";

            SqlDataAdapter adap = new SqlDataAdapter(sorgu, con);

            DataSet ds = new DataSet();

            adap.Fill(ds, "kodlistesi");

            this.dataGridView1.DataSource = ds.Tables[0];
            textBox6.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            con.Close();

        }

        
    }


}
