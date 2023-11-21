using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rehbersorgulama.Models
{
    public class cls_admin
    {
        rehber_sorgulamaEntities db = new rehber_sorgulamaEntities();
        public bool admin_guncelle(tbl_rehber bilgigetir)
        {
            try
            {
                var rehberbilgi = db.tbl_rehber.Where(p => p.rehber_id == bilgigetir.rehber_id).FirstOrDefault();
                rehberbilgi.cv_aciklama = bilgigetir.cv_aciklama;
                rehberbilgi.yas = bilgigetir.yas;
                rehberbilgi.baslama_saati = bilgigetir.baslama_saati;
                rehberbilgi.bitis_saati = bilgigetir.bitis_saati;
                rehberbilgi.fiyat = bilgigetir.fiyat;
                rehberbilgi.parabirimi = bilgigetir.parabirimi;
                rehberbilgi.email = bilgigetir.email;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool sifre_degistir(tbl_kullanici k, string sifretekrar)
        {

            try
            {

                tbl_kullanici kullanici = db.tbl_kullanici.Where(p => p.kullanici_id == k.kullanici_id).FirstOrDefault(p => p.sifre == k.sifre && p.kullaniciad == k.kullaniciad);
                if (k.ISADMIN == true && k.sifre == sifretekrar)
                {

                    kullanici.sifre = k.sifre;
                }
                return true;


            }
            catch (Exception)
            {

                return false;
            }


        }
        public bool dil_ekle(rehber_dil_detay dildetay)
        {
            try
            {
                dildetay.Aktif = true;
                db.rehber_dil_detay.Add(dildetay);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool dil_guncelle(rehber_dil_detay dildetay)
        {
            try
            {
               
                var deger = db.rehber_dil_detay.Where(p => p.rehber_id == dildetay.rehber_id).FirstOrDefault();
                deger.dil_id = dildetay.dil_id;
                deger.dil_seviyeid = dildetay.dil_seviyeid;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool dilSil(int id)
        {
            try
            {

                rehber_dil_detay dildetay = db.rehber_dil_detay.Where(p => p.id == id).FirstOrDefault();
                dildetay.Aktif = false;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }
       
        public bool paroladegistir(tbl_kullanici k,string sifretekrar)
        {
            var deger = db.tbl_kullanici.FirstOrDefault(p=>p.rehber_id==k.rehber_id);
            try
            {
                if (deger.ISADMIN == true && k.sifre==sifretekrar)
                {
                    deger.kullaniciad = k.kullaniciad;
                    deger.sifre = k.sifre;
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