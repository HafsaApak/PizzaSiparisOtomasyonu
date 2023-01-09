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

namespace PizzaSiparisOtomasyonu
{
    public partial class FrmSiparis : Form
    {
        public FrmSiparis()
        {
            InitializeComponent();
        }
        decimal ucret = 0;
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter dr;

        public string constring = "Data Source=.;Initial Catalog=pizzasiparis;Integrated Security=True";
        void verilerigetir()
        {
            baglanti = new SqlConnection("Data Source=LAPTOP-7TMBT5D0\\SQLEKSPRESS;Initial Catalog=pizzasiparis;Integrated Security=True");
            baglanti.Open();
            dr = new SqlDataAdapter("SELECT *FROM tSiparis", baglanti); // sqldataadapter, verileri almak ve kaydetmek için ve SQL arasında köprü görevi görür
            DataTable tablo = new DataTable();
            dr.Fill(tablo);
            dataGridView1.DataSource = tablo; //DataGridView e tabloyu çağırdık.
            baglanti.Close();
        }

        private void btnSiparisAl_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO tSiparis(AdSoyad,Telefon,Adres,PizzaBoyVeAdet,İçecekVeAdet,Ücret) VALUES (@AdSoyad,@Telefon,@Adres,@PizzaBoyVeAdet,@İçecekVeAdet,@Ücret)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
            komut.Parameters.AddWithValue("@Telefon", txtTelefon.Text);
            komut.Parameters.AddWithValue("@Adres", txtAdres.Text);
            komut.Parameters.AddWithValue("@PizzaBoyVeAdet", cmbPizzaBoy.Text);
            komut.Parameters.AddWithValue("@İçecekVeAdet", cmbİcecek.Text);
            komut.Parameters.AddWithValue("@Ücret",textBox1.Text);
            baglanti.Open();
            komut.ExecuteNonQuery(); //verileri kullanmadan(Insert,update) DataSet değiştirmek için kullanabilirsiniz
            baglanti.Close();
            verilerigetir();

            

             //Pizza Boyut
            if (cmbPizzaBoy.Text=="küçük")
            {
                ucret += nmrPizzaAdet.Value * 45;
            }
            else if (cmbPizzaBoy.Text == "orta")
            {
                ucret += nmrPizzaAdet.Value * 60;
            }
            else if (cmbPizzaBoy.Text == "büyük")
            {
                ucret += nmrPizzaAdet.Value * 100;
            }

            //İçecek
            if (cmbİcecek.Text == "2,5 lt Coco Cola")
            {
                ucret += nmrİcecekAdet.Value * 28;
            }
            else if (cmbİcecek.Text == "2,5 lt Fanta")
            {
                ucret += nmrİcecekAdet.Value * 26;
            }
            else if (cmbİcecek.Text == "2,5 lt Sprite")
            {
                ucret += nmrİcecekAdet.Value * 26;
            }
            textBox1.Text = Convert.ToString(ucret);
            komut.Parameters.AddWithValue("@Ücret", textBox1.Text);

            //Textboxlardakini listboxlara ekleme
            lstAdSoyad.Items.Add(txtAdSoyad.Text);
            lstTelefon.Items.Add(txtTelefon.Text);
            lstAdres.Items.Add(txtAdres.Text);
            lstPizzaBoyVeAdet.Items.Add("Adet"+ nmrPizzaAdet.Value + "Boyut" + cmbPizzaBoy.Text);
            lstİcecekVeAdet.Items.Add("Adet" + nmrİcecekAdet.Value + "İcecek" + cmbİcecek.Text);
            lstÜcret.Items.Add(ucret +"TL");

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox seçenek ekledik
            verilerigetir();
            cmbPizzaBoy.Items.Add("küçük");
            cmbPizzaBoy.Items.Add("orta");
            cmbPizzaBoy.Items.Add("büyük");

            cmbİcecek.Items.Add("2,5 lt Coco Cola");
            cmbİcecek.Items.Add("2,5 lt Fanta");
            cmbİcecek.Items.Add("2,5 lt Sprite");

           
        }
        private void btnSiparisleriTemizle_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM tSiparis Where id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@id",Convert.ToInt32(txtno.Text));
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigetir();

            //textboxlarımızı temizledik
            txtAdSoyad.Text = "";
            txtAdres.Clear();
            txtTelefon.Text = null;

            //comboboxlarımı temizledik.
            cmbİcecek.Text = "";
            cmbPizzaBoy.Text = "";

            nmrİcecekAdet.Value = 0;
            nmrPizzaAdet.Value = 0;

           
        }
        
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();      //tablo satırlarına tıkladığında sutünlardaki bilgiler
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString(); // textboxlara yerleşiyor.
            txtTelefon.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtAdres.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            cmbPizzaBoy.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            cmbİcecek.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();

        }
        private void btncıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit(); //çıkış yap
        }
    }
}
