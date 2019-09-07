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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");

        private void button1_Click(object sender, EventArgs e)                 // otomasyondaki kullanıcı girişi yapabilmeyi sağlayan form
        {
            con.Open();
            SqlCommand komut = new SqlCommand("Select *from kullanicigirisi where kullaniciadi='" + textBox1.Text + "'and sifre='" + textBox2.Text + "'", con);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form1 frm1 = new Form1();
                frm1.Show();
            }
            else
            {
                MessageBox.Show("KULLANICI ADI VEYA ŞİFRE HATALI");
            }
            con.Close();
        }
       
    }
}
