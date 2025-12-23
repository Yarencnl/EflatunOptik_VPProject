using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace EflatunOptik_VPProject
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reload();
            if (Properties.Settings.Default.BeniHatirla == "true")
            {
                textBox1.Text = Properties.Settings.Default.KullaniciAdi;
                textBox2.Text = Properties.Settings.Default.Sifre;
                checkBeniHatirla.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //string kullaniciAdi = textBox1.Text.Trim(); 
            //string sifre = textBox2.Text.Trim(); 
            //if (kullaniciAdi == "Yaren" && sifre == "123") 
            //{ //MainPageForm mainPageForm = new MainPageForm(); 
            //mainPageForm.Show(); 
            //this.Hide(); 
            //} 
            //else 
            //{ 
            //MessageBox.Show("Kullanıcı bulunamadı. Bilgilerinizi kontrol edin ya da kayıt oluşturun!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            //}
            using (var baglanti = new SQLiteConnection(VeriTabaniIslemleri.BaglantiCumlesi))
            {
                try
                {
                    baglanti.Open();
                    string sql = "SELECT Rol FROM Kullanicilar WHERE KullaniciAdi=@user AND Sifre=@pass";
                    using (var komut = new SQLiteCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@user", textBox1.Text);
                        komut.Parameters.AddWithValue("@pass", textBox2.Text);

                        object sonuc = komut.ExecuteScalar();

                        if (sonuc != null)
                        {
                            if (checkBeniHatirla.Checked)
                            {
                                Properties.Settings.Default.KullaniciAdi = textBox1.Text;
                                Properties.Settings.Default.Sifre = textBox2.Text;
                                Properties.Settings.Default.BeniHatirla = "true";
                                Properties.Settings.Default.Save();
                            }
                            else
                            {
                                Properties.Settings.Default.KullaniciAdi = "";
                                Properties.Settings.Default.Sifre = "";
                                Properties.Settings.Default.BeniHatirla = "false";
                            }
                            Properties.Settings.Default.Save(); 

                            MainPageForm mainPageForm = new MainPageForm(); 
                            mainPageForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bağlantı Hatası: " + ex.Message);
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            if (textBox3.Text.Trim() == "Yaren" && textBox4.Text.Trim() == "1234")
            {
                AdminForm adminForm = new AdminForm();
                adminForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı ya da şifreniz yanlıştır! Admin değilseniz zorlamayın", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void textBox1_TextChanged(object sender, EventArgs e) 
        {

        }
        private void textBox2_TextChanged(object sender, EventArgs e) 
        {

        }
        private void label5_Click(object sender, EventArgs e) 
        {

        }
        private void tabPage1_Click(object sender, EventArgs e) 
        {

        }
    }
}