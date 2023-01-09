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
    public partial class FrmKullaniciGiris : Form
    {
        public FrmKullaniciGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataReader dr; //verilere erişim için

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM tKullanici where k_ad=@k_ad AND k_sifre=@k_sifre"; // Sorgu oluşturduk.
            baglanti = new SqlConnection("Data Source=LAPTOP-7TMBT5D0\\SQLEKSPRESS;Initial Catalog=pizzasiparis;Integrated Security=True");
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@k_ad", txtKullaniciAdi.Text); //Parametreyle veritabanından k_ad çağırdık .
            komut.Parameters.AddWithValue("@k_sifre", txtSifre.Text);
            baglanti.Open(); //baglantı yı açtık
            dr = komut.ExecuteReader(); 
            if (dr.Read()) 
            {
                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız. ");
                this.Hide();
                FrmSiparis Siparis= new FrmSiparis();
                Siparis.Show();
            }
            else 
            {
                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.");
            }
            
            baglanti.Close();
        }

        private void FrmKullaniciGiris_Load(object sender, EventArgs e)
        {
            
        }
    }
    
}
