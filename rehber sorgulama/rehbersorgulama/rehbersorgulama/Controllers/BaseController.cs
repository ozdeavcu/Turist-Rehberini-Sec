using rehbersorgulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rehbersorgulama.Controllers
{
    public class BaseController : Controller
    {
        rehber_sorgulamaEntities db=new rehber_sorgulamaEntities();
        // GET: Base
        public BaseController()
        {
           
            
            ViewBag.ilcead=db.tbl_ilce.ToList();
            ViewBag.sayfadakilistesayisi = db.tbl_ayarlar.FirstOrDefault(p => p.ID == 1).sayfadakilistesayisi;
           
        }
    }
}