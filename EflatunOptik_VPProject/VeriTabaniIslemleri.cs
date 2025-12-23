using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EflatunOptik_VPProject
{
    public static class VeriTabaniIslemleri
    {
        private static string dbYolu = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EflatunOptik.db");
        public static string BaglantiCumlesi = $"Data Source={dbYolu};Version=3;";

        public static void VeritabaniOlustur()
        {
            if (!File.Exists(dbYolu))
            {
                SQLiteConnection.CreateFile(dbYolu);
            }

            using (var baglanti = new SQLiteConnection(BaglantiCumlesi))
            {
                baglanti.Open();

                string sqlKullanicilar = @"CREATE TABLE IF NOT EXISTS Kullanicilar (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Ad TEXT, Soyad TEXT, KullaniciAdi TEXT UNIQUE, 
                                    Sifre TEXT, Telefon TEXT, Cinsiyet TEXT, 
                                    Rol TEXT DEFAULT 'User')";

                string sqlGozlukler = @"CREATE TABLE IF NOT EXISTS Gozlukler (
                                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    Kategori TEXT, 
                                    ModelAdi TEXT, 
                                    Beden TEXT,
                                    StokMiktari INTEGER)";

                using (var cmd1 = new SQLiteCommand(sqlKullanicilar, baglanti)) { cmd1.ExecuteNonQuery(); }
                using (var cmd2 = new SQLiteCommand(sqlGozlukler, baglanti)) { cmd2.ExecuteNonQuery(); }

                string kontrol = "SELECT COUNT(*) FROM Gozlukler";
                using (var check = new SQLiteCommand(kontrol, baglanti))
                {
                    if (Convert.ToInt32(check.ExecuteScalar()) == 0)
                    {
                        string seed = @"
                    INSERT INTO Gozlukler (Kategori, ModelAdi, Beden, StokMiktari) VALUES ('Erkek', 'Standart', '40', 10);
                    INSERT INTO Gozlukler (Kategori, ModelAdi, Beden, StokMiktari) VALUES ('Kadin', 'Standart', '40', 10);
                    INSERT INTO Gozlukler (Kategori, ModelAdi, Beden, StokMiktari) VALUES ('Cocuk', 'Standart', '40', 10);";
                        using (var cmdSeed = new SQLiteCommand(seed, baglanti)) { cmdSeed.ExecuteNonQuery(); }
                    }
                }
            }
        }
    }
}
