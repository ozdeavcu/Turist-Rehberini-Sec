using rehbersorgulama.Models;
using rehbersorgulama.MVVM;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Collections;

namespace rehbersorgulama.Controllers
{
    public class HomeController : BaseController
    {
        rehber_sorgulamaEntities db = new rehber_sorgulamaEntities();
        cls_anasayfa anasayfa = new cls_anasayfa();
        AnasayfaModel asm = new AnasayfaModel();
        cls_kullanici users = new cls_kullanici();

        int sayfadakilistesayisi = 1;
        public HomeController()
        {
            sayfadakilistesayisi = ViewBag.sayfadakilistesayisi;

        }
        public ActionResult Index()
        {

            asm.onecikan_otel = anasayfa.otel_getir("", "", sayfadakilistesayisi);
            asm.onecikan_rehber = anasayfa.rehber_getir("", "", sayfadakilistesayisi);
            asm.onecikan_ilce = anasayfa.sehir_getir("", "", sayfadakilistesayisi);
            return View(asm);

        }
        public ActionResult girisyap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult girisyap(tbl_kullanici login)
        {
            tbl_kullanici log = users.uye_kontrol(login);

            if (log == null)
            {
                TempData["FeedBack"] = "Giriş bilgileriniz hatalı.Tekrar deneyiniz";
                TempData["FeedBackIcon"] = "error";
            }
            else
            {
              
                Session["ID"] = log.kullanici_id;
                Session["USERNAME"] = log.adsoyad;
                Session["ISADMIN"] = log.ISADMIN;
                if (log.ISADMIN == true)//Eğer ISADMIN kolonu True ise Admin Sayfasına yönlendirilecek.
                {
                    Session["rehber_id"] = log.rehber_id;
                    Session["fotograf"] = log.tbl_rehber.fotograf;
                    Session["adsoyad"] = log.tbl_rehber.adsoyad;
                    TempData["FeedBack"] = "Giriş Başarılı.Hoş Geldiniz.";
                    TempData["FeedBackIcon"] = "success";
                    return RedirectToAction("anasayfa", "Admin");
                }
                else
                {
                    Session["USERNAME"] = log.adsoyad;
                    TempData["FeedBack"] = "Giriş Başarılı";
                    TempData["FeedBackIcon"] = "success";
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
        public ActionResult kaydol()
        {

            return View();
        }
        public ActionResult captcha()
        {
            Bitmap bmp = new Bitmap(60, 20);
            Graphics graphics = Graphics.FromImage(bmp);
            graphics.Clear(Color.Blue);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font font = new Font("Ariel", 8, FontStyle.Bold);

            string ranstr = "";

            int[] myIntArray = new int[5];
            Random rnd = new Random();
            for (int i = 0; i < myIntArray.Length; i++)
            {
                myIntArray[i] = rnd.Next(0, 10);
                ranstr += myIntArray[i].ToString();
            }

            Session["ranstr"] = ranstr;
            users.captcha = ranstr;

            graphics.DrawString(ranstr, font, Brushes.White, 3, 3);
            Response.ContentType = "image/GIF";
            bmp.Save(Response.OutputStream, ImageFormat.Gif);
            font.Dispose();
            graphics.Dispose();
            bmp.Dispose();
            return View();
        }
        [HttpPost]
        public ActionResult kaydol(tbl_kullanici kullanici, string sifretekrar)
        {
            string captcha = Request.Form["txt_captcha"];
            if (Session["ranstr"].ToString() == captcha)
            {
                bool sonuc = users.uye_ekle(kullanici, sifretekrar);
                if (sonuc == true)
                {
                    TempData["FeedBack"] = "Üyelik kaydınız başarılı";
                    TempData["FeedBackIcon"] = "success";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["FeedBack"] = "Üyelik kaydı yapılamadı.Şifreler uyuşmuyor.";
                    TempData["FeedBackIcon"] = "error";
                    return View();
                }

            }
            else if (Session["ranstr"].ToString() != captcha)
            {
                TempData["FeedBack"] = "Güvenlik Rakamını Yanlış Girdiniz";
                TempData["FeedBackIcon"] = "error";
                return View();
            }
         
            else
            {
                TempData["FeedBack"] = "Üyelik Kaydı Yapılamadı.";
                TempData["FeedBackIcon"] = "error";
                return View();
            }


        }
        public ActionResult konaklama_detay(int id,tbl_otel o)
        {
            var deger = db.tbl_otel.Where(p => p.ilce_id == id).ToList();//ilçe id sine göre otelleri listeleyip döndürüyorum
            ViewBag.otelsayi = db.tbl_otel.Where(p => p.ilce_id == id).Count();
            ViewBag.sehirad = db.tbl_otel.Where(p => p.ilce_id == id).Select(p => p.tbl_ilce.ilce_ad).FirstOrDefault();
            anasayfa.onecikanlararttir(id,o);
            return View(deger);
        }
        public ActionResult sehiricerik_detay(int id)
        {
            var deger = db.tbl_tarihiyerler.Where(p => p.ilce_no == id).ToList();
            ViewBag.tarihiyerler = db.tbl_tarihiyerler.Where(p => p.ilce_no == id).ToList();
            ViewBag.sehirad = db.tbl_ilce.Where(p => p.ilce_id == id).Select(p => p.ilce_ad).FirstOrDefault();
            ViewBag.rehberler = db.vw_rehber_bilgi_getir.Where(p => p.ilce_id == id).ToList();
            ViewBag.aktivite = db.tbl_aktivite.Where(p => p.ilce_id == id).ToList();
            anasayfa.onecikanilce(id);
            return View(deger);
        }
        public ActionResult rehberlerimiz(int id)
        {
            var deger = db.vw_rehber_sehir_getir.Where(p => p.ilce_id == id).ToList();
            ViewBag.ilceadgetir = db.tbl_ilce.Where(p => p.ilce_id == id).Select(p => p.ilce_ad).FirstOrDefault();

            return View(deger);
        }

        public ActionResult rehber_detay(int id)
        {
            vw_rehber_bilgi_getir rehbergetir = db.vw_rehber_bilgi_getir.FirstOrDefault(p => p.rehber_id == id);

            anasayfa.onecikanrehber(id);
            ViewBag.diller = db.vw_rehber_dil.Where(p => p.rehber_id == id).ToList();
            return View(rehbergetir);
        }
        public ActionResult rehber_formu()
        {
            ViewBag.ilce = db.tbl_ilce.ToList();
            ViewBag.diller = db.tbl_dil.ToList();
            ViewBag.ulkeler = db.tbl_ulke.ToList();

            return View();
        }
        [HttpPost]
        public ActionResult rehber_formu(tbl_kullanici k, string submitTime)
        {
            
           

            if (Session["ID"] == null)
            {
                TempData["FeedBack"] = "Mesajınız gönderilemedi.Önce giriş yapmalısınız.";
                TempData["FeedBackIcon"] = "error";

            }
            else
            {
                int id = (int)Session["ID"];
                bool formsonuc = users.form_gonder(k, id, submitTime);               
                if (formsonuc == true && k.ISADMIN==false)
                {
                 
                    TempData["FeedBack"] = "Form Başarıyla Gönderildi.";
                    TempData["FeedBackIcon"] = "success";
                }
                
                else
                {
                
                    TempData["FeedBack"] = "Form Gönderilemedi.";
                    TempData["FeedBackIcon"] = "error";
                }
            }
            return View();
        }
        [HttpPost]
        public JsonResult GetRehberler(int? ilce_id, string tip)
        {
            List<SelectListItem> sonuc = new List<SelectListItem>();
            //bu işlem başarılı bir şekilde gerçekleşti mi onun kontrolunnü yapıyorum
            bool basariliMi = true;
            try
            {
                switch (tip)
                {
                    case "ilceGetir":
                        //veritabanımızdaki ilçeler tablomuzdan ilçelerimizi sonuc değişkenimze atıyoruz
                        foreach (var ilce in db.tbl_ilce.ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = ilce.ilce_ad,
                                Value = ilce.ilce_id.ToString()
                            });
                        }
                        break;
                    case "rehberGetir":
                        //ilcelere göre getireceğimiz rehberleri selecten seçilen rehber_id sine göre 
                        foreach (var rehber in db.rehber_ilce_detay.Where(ilce => ilce.ilce_id == ilce_id).ToList())
                        {
                            sonuc.Add(new SelectListItem
                            {
                                Text = rehber.tbl_rehber.adsoyad,
                                Value = rehber.rehber_id.ToString()
                            });
                        }
                        break;

                    default:
                        break;

                }
            }
            catch (Exception)
            {
                //hata ile karşılaşırsak buraya düşüyor
                basariliMi = false;
                sonuc = new List<SelectListItem>();
                sonuc.Add(new SelectListItem
                {
                    Text = "Bir hata oluştu :(",
                    Value = "Default"
                });

            }



            //Oluşturduğum sonucları json olarak geriye gönderiyorum
            return Json(new { ok = basariliMi, text = sonuc });
        }
        public ActionResult gonderilen_formlar(int id)
        {
            var deger = db.tbl_kullanici.Find(id);
            ViewBag.kullanici = db.tbl_kullanici.Where(p => p.kullanici_id == id).ToList();
            return View(deger);
        }
    }
}