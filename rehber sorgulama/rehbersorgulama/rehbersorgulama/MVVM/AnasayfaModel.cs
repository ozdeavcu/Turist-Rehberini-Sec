using rehbersorgulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rehbersorgulama.MVVM
{
    public class AnasayfaModel
    {
        public List<tbl_otel> onecikan_otel { get; set; }
        public List<vw_rehber_bilgi_getir> onecikan_rehber{ get; set; }
        public List<tbl_ilce> onecikan_ilce { get; set; }
    }
}