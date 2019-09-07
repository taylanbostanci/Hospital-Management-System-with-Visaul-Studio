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
    public partial class Form10 : Form
    {
        SqlConnection con = new SqlConnection("Data Source = 192.168.0.15; Initial Catalog =hastaneotomasyon; User Id=admin; password=admin;");
        SqlCommand komut7;
        string imgLoc = "";
        public Form10()                          // form2 deki gibi hastanın fotoğraflarını veritabanına aktarıldığı form
        {                                        // Bu form, hastanın muayene fotoğraflarının kaydedildiği bölüm
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files (*.png)|*.png|All Files (*.*)|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgLoc = dlg.FileName.ToString();
                pictureBox1.ImageLocation = imgLoc;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            byte[] img = null;
            FileStream fs = new FileStream(imgLoc, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            img = br.ReadBytes((int)fs.Length);

            con.Open();
            string sql = "insert into resim(muayeneno,resim)values('" + textBox1.Text + "',@img)";
            komut7 = new SqlCommand(sql, con);
            komut7.Parameters.Add(new SqlParameter("@img", img));
            int x = komut7.ExecuteNonQuery();
            con.Close();
            MessageBox.Show(x.ToString() + "resim kaydedildi");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            string sql = "Select muayeneno,resim from resim where muayeneno='" + textBox1.Text + "'";
            komut7 = new SqlCommand(sql, con);
            SqlDataReader dr = komut7.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                textBox1.Text = dr[0].ToString();
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
            else
            {
                MessageBox.Show("Fotoğraf bulunamadı.");
            }
            con.Close();
        }
    }
}
