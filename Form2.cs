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
using System.IO;
using System.Drawing.Imaging;

namespace hastaneotomasyonu
{
    public partial class Form2 : Form
    {
        SqlConnection conn = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
        string komut = "Select * from HastaKayit";       // sql baglantısı saglandı
        SqlCommand sqlcmmd;                             // sql sorgusu olusturuldu
        string imgLoc = "";
        public Form2()
        {
            InitializeComponent();
            Fillcombo();
        }
        public void Fillcombo()
        {
            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

            string Query = "Select cinsiyet from cinsiyet ";
            SqlConnection cdn = new SqlConnection(conn);            //sql baglantısı saglandı
            SqlCommand cmd = new SqlCommand(Query, cdn);       // sql sorgusu olusturuldu

            SqlDataReader reader;
            try
            {                                                 //  veri okuyucu olusturuldu
                cdn.Open();
                reader = cmd.ExecuteReader();                   // veri okuyucuyu calıstıracak fonksiyonun kodu
                while (reader.Read())
                {
                    string name = reader["cinsiyet"].ToString();
                    comboBox1.Items.Add(name);                               // combobox a veriler eklendi
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }
        void verilerial()
        {



            string hastaadi = textBox1.Text;
            string hastasoyadi = textBox2.Text;
            string tcno = textBox3.Text;

            string adres = richTextBox1.Text;
            string telefon = textBox5.Text;
            string cinsiyet = textBox6.Text;
        }
        void sqlveriyaz()
        {
            try                                         // try-catch blogu olusturuldu
            {

                SqlConnection coni = new SqlConnection();     // yeni baglantı olusturuldu

                coni.ConnectionString = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";

                string komut1 = "insert into HastaKayit(hastaAdi,hastaSoyadi,tcno,muayenetarihi,adres,telefon,cinsiyet,dogumtarihi)values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + this.dateTimePicker1.Text + "','" + richTextBox1.Text + "','" + textBox5.Text + "','" + textBox6.Text + "','" + this.dateTimePicker2.Text + "')";
                                                                    // sql komutu girildi

                coni.Open();    // baglantı acıldı
                SqlCommand komutsatirim = new SqlCommand(komut1, coni);     // yeni nesne olusturuldu
                komutsatirim.ExecuteNonQuery();                            // girilen komutu calistiracak fonksiyon tanımlandı
                MessageBox.Show("VERİ GİRİŞİ BAŞARILI");

            }
            catch (Exception hata1)        // herhangi bir hata olustugunda ekrana cıkacak hata mesajı girildi
            {
                MessageBox.Show("Bir hata meydana geldi !!!" + hata1.ToString());
            }
        }
        void listele()
        {

            DataTable tablo = new DataTable();                             // database deki verileri listeleyen komut
            SqlDataAdapter adp = new SqlDataAdapter(komut, conn);
            adp.Fill(tablo);                                            // veriler datagridView e aktarılıyor
            dataGridView1.DataSource = tablo;

        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();                  // veriler datagridview den textbox lara çekildi
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();     
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable tablo = new DataTable();  // tablo adında nesne olusturuldu
            SqlDataAdapter adp = new SqlDataAdapter(komut, conn);     //adapter nesnesi olusturuldu
            adp.Fill(tablo);                                          // sql deki tablo verileri datagrid lere çekildi
            dataGridView1.DataSource = tablo;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable tablo = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(komut, conn);
            adp.Fill(tablo);
            dataGridView1.DataSource = tablo;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sqlveriyaz();
            verilerial();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listele();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)    
        {
            // comboboxta mouse ile tıklandıgında veriyi textbox a ceken metod

            string conn = "Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;";  // sql baglantisi

            string Query = "Select cinsiyet from cinsiyet where cinsiyet='" + comboBox1.Text + "'";   //sql sorgusu
            SqlConnection cdn = new SqlConnection(conn);       // nesne olusturuldu
            SqlCommand cmd = new SqlCommand(Query, cdn);       
            SqlDataReader reader;
            try
            {
                cdn.Open();
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["cinsiyet"].ToString();
                    textBox6.Text = name;     // secilen deger textbox a aktariliyor
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Silmek istediginizden emin misiniz", "Dikkat", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (conn.State == ConnectionState.Closed)
                {                                                    // database deki verileri silen metod
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();           // baglantı acıldı
                                                                 // command nesnesi olusturuldu
                    cmd.Connection = conn;
                    cmd.CommandText = "delete from HastaKayit where protokolno=@numara";     //sql sorgusu tanımlandı
                    cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());  // datagridviewdeki secili olan veriye göre silme islemi yapılıyor
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    conn.Close();
                    listele();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Close(); // formu kapatan fonksiyon
        }
        void temizle()
        {

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            richTextBox1.Clear();  // textboxları temizleyen fonksiyon
            textBox5.Clear();
            textBox6.Clear();
            textBox1.Focus();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();             //baglantı acıldı                        // database deki verileri guncellemeyi saglayan metod
                SqlCommand cmd = new SqlCommand();                   // yeni nesne olusturuldu
                cmd.Connection = conn;                       //yeni command nesne ile baglantı saglandı
                cmd.CommandText = "update HastaKayit set hastaAdi='" + textBox1.Text + "',hastaSoyadi='" + textBox2.Text + "',tcno='" + textBox3.Text + "',muayenetarihi='" + this.dateTimePicker1.Text + "',adres='" + richTextBox1.Text + "',telefon='" + textBox5.Text + "',cinsiyet='" + textBox6.Text + "',dogumtarihi='" + this.dateTimePicker2.Text + "'where protokolno=@numara";
                cmd.Parameters.AddWithValue("@numara", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                cmd.ExecuteNonQuery();                         // datagridviewdeki secili olan veriye göre guncelleme islemi yapılıyor
                cmd.Dispose();
                conn.Close();
                listele();
                temizle();
                MessageBox.Show("Güncelleme başarılı şekilde tamamlandı.");



            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
                                                                              // yüklenecek fotografların uzantıları belirlendi
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;                            // picturebox a aktarıldı
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] img = null;
            FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);                  // yüklenen fotografların  database e kayıt aşaması oluşturuldu        
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);             // gorsel byte olarak aktariliyor                            
            conn.Open(); //baglanti acildi
            string sql = "insert into hastafoto(protokolno,hastaresim)values('" + textBox4.Text + "',@img)";             //sql sorgusuyla girilen veri  database e aktarılıyor

            sqlcmmd = new SqlCommand(sql, conn);     // komut ile sql baglantisi saglandı
            sqlcmmd.Parameters.Add(new SqlParameter("@img", img));  
            int x = sqlcmmd.ExecuteNonQuery();      
            conn.Close(); //baglanti kapatildi
            MessageBox.Show(x.ToString() + "resim kaydedildi"); // ekrana resim kaydedildi olarak bilgi veriliyor

        }

        private void button8_Click(object sender, EventArgs e)
        {                                                               // database de daha onceden yuklenen bir fotografı aramamizi saglayan metod
            conn.Open(); // baglantı acıldı
            string sql = "Select protokolno,hastaresim from hastafoto where protokolno='" + textBox4.Text + "'";
            sqlcmmd = new SqlCommand(sql, conn);                                                // sql sorgusu girildi
            SqlDataReader dr = sqlcmmd.ExecuteReader();
            dr.Read();                                     
            if (dr.HasRows)                                    // textbox a girilen degere göre database deki verileri picturebox a aktarmayı sağlayan kod parçası
            {
                textBox4.Text = dr[0].ToString();
                byte[] img = ((byte[])dr[1]);

                if (img == null)
                {
                    pictureBox1.Image = null;
                }
                else
                {
                    MemoryStream mstreem = new MemoryStream(img);
                    pictureBox1.Image = Image.FromStream(mstreem);
                }

            }
            conn.Close();
        }

    }
}