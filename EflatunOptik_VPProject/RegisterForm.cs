using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace EflatunOptik_VPProject
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            VeriTabaniIslemleri.VeritabaniOlustur();

            using (var baglanti = new SQLiteConnection(VeriTabaniIslemleri.BaglantiCumlesi))
            {
                try
                {
                    baglanti.Open();
                    string sql = "INSERT INTO Kullanicilar (Ad, Soyad, KullaniciAdi, Sifre, Telefon, Cinsiyet) VALUES (@ad, @soyad, @kadi, @sifre, @tel, @cinsiyet)";
                    using (var komut = new SQLiteCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@ad", textBox1.Text);
                        komut.Parameters.AddWithValue("@soyad", textBox2.Text);
                        komut.Parameters.AddWithValue("@kadi", textBox3.Text);
                        komut.Parameters.AddWithValue("@sifre", textBox4.Text);
                        komut.Parameters.AddWithValue("@tel", maskedTextBox1.Text);
                        string cinsiyet = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : "Belirtilmedi";
                        komut.Parameters.AddWithValue("@cinsiyet", cinsiyet);

                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kaydınız başarıyla oluşturuldu! Şimdi giriş yapabilirsiniz.");
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
            }
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
