using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rehbersorgulama.Models
{
    public class cls_kullanici
    {
        rehber_sorgulamaEntities db = new rehber_sorgulamaEntities();
        public string captcha { get; set; }
        public tbl_kullanici uye_kontrol(tbl_kullanici kullanici)
        {
            tbl_kullanici login = db.tbl_kullanici.FirstOrDefault(p => p.kullaniciad == kullanici.kullaniciad && p.sifre == kullanici.sifre);
            return login;
        }
        public bool uye_ekle(tbl_kullanici kullanici, string sifretekrar)
        {
            try
            {
               
                if (kullanici.sifre == sifretekrar)
                {
                    db.tbl_kullanici.Add(kullanici);
                    kullanici.ISADMIN = false;  
                    db.SaveChanges();
                    return true;

                }
                else
                {
                    return false;
                }


            }
            catch (Exception)
            {

                return false;
            }
        }
        public List<vw_rehber_bilgi_getir> rehberbilgi(int rehber_id)
        {
            List<vw_rehber_bilgi_getir> rehberlist = db.vw_rehber_bilgi_getir.Where(p=>p.rehber_id==rehber_id).ToList();
            return rehberlist;
        }
        public bool form_gonder(tbl_kullanici k,int id, string submitTime)
        {
            try
            {
                var kullanicibilgi = db.tbl_kullanici.FirstOrDefault(p => p.kullanici_id == id);

                if (kullanicibilgi != null)
                {
                    DateTime formSubmitTime = DateTime.Parse(submitTime);
                    kullanicibilgi.adsoyad = k.adsoyad; 
                    kullanicibilgi.email = k.email;
                    kullanicibilgi.telefon = k.telefon;
                    kullanicibilgi.cinsiyet = k.cinsiyet;
                    kullanicibilgi.yas = k.yas;
                    kullanicibilgi.dil_no = k.dil_no;
                    kullanicibilgi.ilce_no = k.ilce_no;
                    kullanicibilgi.ulke_no = k.ulke_no;
                    kullanicibilgi.tarih = k.tarih;
                    kullanicibilgi.kisi_sayisi = k.kisi_sayisi;
                    kullanicibilgi.rehber_id = k.rehber_id;
                    k.ISADMIN=kullanicibilgi.ISADMIN;
                    kullanicibilgi.durum = true;
                    kullanicibilgi.form_tarih = formSubmitTime;
                    db.SaveChanges();
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}