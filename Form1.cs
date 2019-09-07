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
    public partial class Form1 : Form
    {
        public Form9 frm9;
        public Form4 frm4;   
        public Form2 frm2;
        public Form3 frm3;         // formlar tanıtıldı
        public Form6 frm6;
       
        public Form1()
        {
            frm9 = new Form9();
            frm6 = new Form6();
            frm2 = new Form2();     // formlar oluşturuldu
            frm3 = new Form3();
            frm4 = new Form4();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frm2.ShowDialog();                                 // butonlara tıklatıldıgında yeni formlar açılacak şekilde
                                                               // tasarlandı
        } 
        private void button2_Click(object sender, EventArgs e)
        {
            frm3.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frm4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frm6.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            frm9.ShowDialog();
        }
    }
}
