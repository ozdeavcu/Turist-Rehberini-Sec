using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rehbersorgulama.Models
{
    public class cls_anasayfa
    {
        rehber_sorgulamaEntities db=new rehber_sorgulamaEntities();
        List<tbl_otel> otel;
        List<vw_rehber_bilgi_getir> rehber;
        List<tbl_ilce> ilce; 

        int sayfa = 0;
        public List<tbl_otel> otel_getir(string ajaxmi, string sayfano, int sayfadakiürünsayısı)
        {
                
                if (ajaxmi == "")
                {
                    //home index
                    otel = db.tbl_otel.OrderByDescending(p => p.onecikanlar).Take(8).ToList();
                }
                else if (ajaxmi == "hayir")
                {
                    otel = db.tbl_otel.OrderByDescending(p => p.onecikanlar).Take(sayfadakiürünsayısı).ToList();
                }
                else
                {
                    sayfa = Convert.ToInt32(sayfano);
                    otel = db.tbl_otel.OrderByDescending(p => p.onecikanlar).Skip(sayfa * sayfadakiürünsayısı).Take(sayfadakiürünsayısı).ToList();
                }
          
            return otel;
        }
        public List<vw_rehber_bilgi_getir> rehber_getir( string ajaxmi, string sayfano, int sayfadakiürünsayısı)
        {  
                if (ajaxmi == "")
                {
                    //home index
                    rehber = db.vw_rehber_bilgi_getir.OrderByDescending(p => p.onecikanlar).Take(8).ToList();
                }
                else if (ajaxmi == "hayir")
                {
                    rehber = db.vw_rehber_bilgi_getir.OrderByDescending(p => p.onecikanlar).Take(sayfadakiürünsayısı).ToList();
                }
                else
                {
                    sayfa = Convert.ToInt32(sayfano);
                    rehber = db.vw_rehber_bilgi_getir.OrderByDescending(p => p.onecikanlar).Skip(sayfa * sayfadakiürünsayısı).Take(sayfadakiürünsayısı).ToList();
                }
           
            return rehber;
        }
        public List<tbl_ilce> sehir_getir(string ajaxmi, string sayfano, int sayfadakiürünsayısı)
        {

            if (ajaxmi == "")
            {
                //home index
                ilce = db.tbl_ilce.OrderByDescending(p => p.onecikanlar).Take(8).ToList();
            }
            else if (ajaxmi == "hayir")
            {
                ilce = db.tbl_ilce.OrderByDescending(p => p.onecikanlar).Take(sayfadakiürünsayısı).ToList();
            }
            else
            {
                sayfa = Convert.ToInt32(sayfano);
                ilce = db.tbl_ilce.OrderByDescending(p => p.onecikanlar).Skip(sayfa * sayfadakiürünsayısı).Take(sayfadakiürünsayısı).ToList();
            }

            return ilce;
        }
        public void onecikanlararttir(int id,tbl_otel o)
        {
          
            tbl_otel otel = db.tbl_otel.FirstOrDefault(p => p.otel_id==id);
            otel.onecikanlar = otel.onecikanlar + 1;
            db.SaveChanges();
        }
        public void onecikanrehber(int id)
        {
            tbl_rehber rehber = db.tbl_rehber.FirstOrDefault(p => p.rehber_id == id);
            rehber.onecikanlar=rehber.onecikanlar + 1;
            db.SaveChanges();
        }
        public void onecikanilce(int id)
        {
            tbl_ilce ilce = db.tbl_ilce.FirstOrDefault(p => p.ilce_id == id);
            ilce.onecikanlar = ilce.onecikanlar + 1;
            db.SaveChanges();
        }

    }
}