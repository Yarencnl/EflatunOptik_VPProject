using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EflatunOptik_VPProject
{
    public partial class MainPageFormE : Form
    {
        public MainPageFormE()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainPageForm mainPageForm = new MainPageForm();
            mainPageForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string secilenBeden = comboBox1.SelectedItem?.ToString(); 
            string kategori = "Erkek"; 

            if (string.IsNullOrEmpty(secilenBeden))
            {
                MessageBox.Show("Lütfen bir beden seçiniz!");
                return;
            }

            using (var baglanti = new SQLiteConnection(VeriTabaniIslemleri.BaglantiCumlesi))
            {
                baglanti.Open();
                string sql = "UPDATE Gozlukler SET StokMiktari = StokMiktari - 1 WHERE Kategori=@kat AND Beden=@beden AND StokMiktari > 0";

                using (var komut = new SQLiteCommand(sql, baglanti))
                {
                    komut.Parameters.AddWithValue("@kat", kategori);
                    komut.Parameters.AddWithValue("@beden", secilenBeden);

                    int etkilenenSatir = komut.ExecuteNonQuery();

                    if (etkilenenSatir > 0)
                        MessageBox.Show($"{kategori} Gözlük ({secilenBeden} Beden) satın alındı.");
                    else
                        MessageBox.Show("Seçilen bedende stok tükenmiş!");
                }
            }
        }

    }
}
