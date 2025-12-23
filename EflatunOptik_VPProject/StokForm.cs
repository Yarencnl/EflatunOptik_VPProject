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
using System.Windows.Forms.DataVisualization.Charting;

namespace EflatunOptik_VPProject
{
    public partial class StokForm : Form
    {
        public StokForm()
        {
            InitializeComponent();
        }

        private void StokForm_Load(object sender, EventArgs e)
        {
            PastaVerileriniYukle(chartErkek, "Erkek");
            PastaVerileriniYukle(chartKadın, "Kadin");
            PastaVerileriniYukle(chartÇocuk, "Cocuk");
        }
        private void PastaVerileriniYukle(Chart grafik, string kategori)
        {
            grafik.Series.Clear();
            grafik.Titles.Clear();

            Title baslik = grafik.Titles.Add($"{kategori} Ürün Stok Durumu");
            baslik.Font = new System.Drawing.Font("Segoe Print", 10, System.Drawing.FontStyle.Bold);

            Series seri = grafik.Series.Add("StokDurumu");
            seri.ChartType = SeriesChartType.Pie;

            using (var baglanti = new SQLiteConnection(VeriTabaniIslemleri.BaglantiCumlesi))
            {
                try
                {
                    baglanti.Open();
                    string sql = "SELECT StokMiktari FROM Gozlukler WHERE Kategori = @kat";

                    using (var komut = new SQLiteCommand(sql, baglanti))
                    {
                        komut.Parameters.AddWithValue("@kat", kategori);
                        object sonuc = komut.ExecuteScalar();

                       
                        int mevcutStok = (sonuc != null && sonuc != DBNull.Value) ? Convert.ToInt32(sonuc) : 10;
                        int satinAlindi = 10 - mevcutStok;

                        
                        DataPoint p1 = seri.Points.Add(mevcutStok);
                        p1.Label = $"Stokta\n{mevcutStok} Adet";
                        p1.LegendText = "Stokta";
                        p1.Color = System.Drawing.Color.MediumPurple; 

                        DataPoint p2 = seri.Points.Add(satinAlindi);
                        p2.Label = $"Satıldı\n{satinAlindi} Adet";
                        p2.LegendText = "Satın Alındı";
                        p2.Color = System.Drawing.Color.LightGray;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Veri yükleme hatası: " + ex.Message);
                }
            }

            
            seri["PieLabelStyle"] = "Outside"; 
            grafik.ChartAreas[0].Area3DStyle.Enable3D = true; 
            grafik.Legends[0].Enabled = true; 
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AdminForm adminForm = new AdminForm();
            adminForm.Show();
            this.Hide();
        }

    }
}
