using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MSSQLConnectionWithAdo.NET
{
    class Program
    {
        static SqlConnection con = new SqlConnection(@"Data Source = DESKTOP-*******\SQLEXPRESS; Initial Catalog = adonet; User ID = sa; Password = 1");
        static void Main(string[] args)
        {
            kayitlariGetir();
            kayitEkle("süleyman", "4", "üye");
            kayitGuncelle(3, "ali");
            kayitSil(4);
        }

        public static void kayitlariGetir()
        {
            List<Musteri> musteriList = new List<Musteri>();
            
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from loginTable", con);
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                Musteri musteri = new Musteri();
                musteri.id = int.Parse(dr["id"].ToString());
                musteri.kullaniciAdi = dr["kullaniciAdi"].ToString();
                musteri.sifre = dr["sifre"].ToString();
                musteri.yetki = dr["yetki"].ToString();
                musteriList.Add(musteri);
            }

            con.Close();

            foreach (Musteri musteri in musteriList)
            {
                Console.WriteLine("Id: " + musteri.id + " Kullanıcı Adı: " + musteri.kullaniciAdi + " Şifre: " + musteri.sifre + " Yetki: " + musteri.yetki);
            }

            Console.ReadLine();
        }

        public static void kayitEkle(string kullaniciAdi, string sifre, string yetki)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("insert into loginTable(kullaniciAdi, sifre, yetki) values(@kullaniciAdi, @sifre, @yetki)", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@sifre", sifre);
            cmd.Parameters.AddWithValue("@yetki", yetki);
            int donenDeger = cmd.ExecuteNonQuery();

            if(donenDeger == 1)
            {
                Console.WriteLine("Kayıt eklenmiştir.");
            }
            else
            {
                Console.WriteLine("Kayıt eklenirken bir sorun oluştu.");
            }
            Console.ReadLine();
            con.Close();
        }

        public static void kayitGuncelle(int id, string kullaniciAdi)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update loginTable set kullaniciAdi = @kullaniciAdi where id = @id", con);
            cmd.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
            cmd.Parameters.AddWithValue("@id", id);
            int donenDeger = cmd.ExecuteNonQuery();

            if (donenDeger == 1)
            {
                Console.WriteLine("Kayıt güncellenmiştir.");
            }
            else
            {
                Console.WriteLine("Kayıt güncellenirken bir sorun oluştu.");
            }
            Console.ReadLine();
            con.Close();
        }
    
        public static void kayitSil(int id)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from loginTable where id = @id", con);
            cmd.Parameters.AddWithValue("@id", id);
            int donenDeger = cmd.ExecuteNonQuery();

            if (donenDeger == 1)
            {
                Console.WriteLine("Kayıt silinmiştir.");
            }
            else
            {
                Console.WriteLine("Kayıt silinirken bir sorun oluştu.");
            }
            Console.ReadLine();
            con.Close();
        }
    }
}
