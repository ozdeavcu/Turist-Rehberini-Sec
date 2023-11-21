using rehbersorgulama.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace rehbersorgulama.Controllers
{
    public class AdminController : Controller
    {
        rehber_sorgulamaEntities db = new rehber_sorgulamaEntities();
        cls_admin admin = new cls_admin();
        // GET: Admin
        public ActionResult anasayfa()
        {
            return View();
        }
        public ActionResult form_listesi(int id)
        {

            Session["rehber_id"] = id;
            List<tbl_kullanici> kullanici = db.tbl_kullanici.Where(p => p.rehber_id == id).ToList();
          
            return View(kullanici);
        }
        public ActionResult profil(int id)
        {
            Session["rehber_id"] = id;
            var deger = db.tbl_rehber.Find(id);
            return View(deger);
        }
        [HttpPost]
        public ActionResult profil(tbl_rehber bilgigetir, HttpPostedFileBase fileuploader, string sifretekrar, tbl_kullanici kullanici)
        {
            Session["rehber_id"] = bilgigetir.rehber_id;
            var rehberbilgi = db.tbl_rehber.Where(p => p.rehber_id == bilgigetir.rehber_id).FirstOrDefault();
            if (fileuploader != null && fileuploader.ContentLength > 0)
            {
                string dosyaadi = Path.GetFileNameWithoutExtension(fileuploader.FileName);//fotoğraf yolunu uzantı olmadan alır.
                string uzanti = Path.GetExtension(fileuploader.FileName);
                string yol = "~/Content/img/Rehber/" + dosyaadi + uzanti;
                fileuploader.SaveAs(Server.MapPath(yol));
                rehberbilgi.fotograf = "/Content/img/Rehber/" + dosyaadi + uzanti;

            }
            else
            {
                bilgigetir.fotograf = rehberbilgi.fotograf;
            }
           
            bool sonuc = admin.admin_guncelle(bilgigetir);
            
            if (sonuc == true)
            {
                db.SaveChanges();
               
                TempData["FeedBack"] = "Bilgiler Başarıyla Güncellendi.";
                TempData["FeedBackIcon"] = "success";

            }
            else
            {
             
                TempData["FeedBack"] = "Bilgiler Güncellenemedi.";
                TempData["FeedBackIcon"] = "error";

            }
            return RedirectToAction("profil", new { id = rehberbilgi.rehber_id });

        }
        public ActionResult dil_listele(int id)
        {
            List<rehber_dil_detay> detay = db.rehber_dil_detay.Where(p => p.rehber_id == id).ToList();
            return View(detay);
        }
        public ActionResult musteri_detay(int id)
        {
            Session["ID"] = id;
            var deger = db.tbl_kullanici.Find(id);
            //Session nesnesinde "ID" adında bir değişken oluşturur ve değerini id değişkenine eşitler. Bu, kullanıcının kimliğini tutmak için bir oturum değişkeni kullanır.
            return View(deger);
        }
        public ActionResult dilEkle(int id)
        {
            Session["rehber_id"] = id;
            List<tbl_dil> dil = db.tbl_dil.ToList();
            ViewBag.dilseviye = db.tbl_dilseviye.ToList();
            return View(dil);
        }
        [HttpPost]
        public ActionResult dilEkle(rehber_dil_detay r,int? rID)
        {
            rID = (int)Session["rehber_id"];
            r.rehber_id = rID;
            bool sonuc = admin.dil_ekle(r);
            if (sonuc == true)
            {
                
                    TempData["FeedBack"] = "Dil Eklendi.";
                    TempData["FeedBackIcon"] = "success";

            }
            else
            {
                TempData["FeedBack"] = "Dil Eklenemedi.";
                TempData["FeedBackIcon"] = "error";
            }

            return RedirectToAction("dil_listele", new { id= rID });

        }
        public ActionResult dilGuncelle(int id)
        {
            var deger = db.rehber_dil_detay.Find(id);

            ViewBag.rehberid = deger.rehber_id;
            ViewBag.dil = db.tbl_dil.ToList();
            ViewBag.dilseviye = db.tbl_dilseviye.ToList();
            return View(deger);
        }
        [HttpPost]
        public ActionResult dilGuncelle(rehber_dil_detay r, int? rID)
        {
            rID = (int)Session["rehber_id"];
            r.rehber_id = rID;
            bool sonuc = admin.dil_guncelle(r);
            if (sonuc == true)
            {

                TempData["FeedBack"] = "Dil Güncellendi.";
                TempData["FeedBackIcon"] = "success";

            }
            else
            {
                TempData["FeedBack"] = "Dil Güncellenemedi.";
                TempData["FeedBackIcon"] = "error";
            }

            return RedirectToAction("dil_listele", new { id = rID });

        }
        public ActionResult dilSil(int id)
        {

            var deger = db.rehber_dil_detay.Where(p => p.id == id).FirstOrDefault();
            bool sonuc = admin.dilSil(id);
            if (sonuc == true)
            {
                TempData["FeedBack"] = "Dil Başarıyla Silindi.";
                TempData["FeedBackIcon"] = "success";
            }
            else
            {
                TempData["FeedBack"] = "Dil Başarıyla Silinemedi.";
                TempData["FeedBackIcon"] = "error";
            }
            return RedirectToAction("dil_listele", new { id = deger.rehber_id });
        }
        
        public ActionResult parola_degistir(int id)
        {
            var deger = db.tbl_kullanici.FirstOrDefault(p=>p.rehber_id==id);

            return View(deger);
        }
        [HttpPost]
        public ActionResult parola_degistir(tbl_kullanici k,int? rID,string sifretekrar)
        {
            rID = (int)Session["rehber_id"];
            k.rehber_id = rID;
            bool sonuc = admin.paroladegistir(k,sifretekrar);
           

                if (sonuc == true)
                {
                    TempData["FeedBack"] = "Kullanıcı Bilgileri Başarıyla Güncellendi";
                    TempData["FeedBackIcon"] = "success";
                   
                }
                else
                {
                    TempData["FeedBack"] = "Kullanıcı Bilgileri Güncellenemedi.Şifreler uyuşmuyor.";
                    TempData["FeedBackIcon"] = "error";
                 
                }

    

            return RedirectToAction("parola_degistir", new { id = rID });
        }
    }
}