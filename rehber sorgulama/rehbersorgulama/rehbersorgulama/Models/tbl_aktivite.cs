//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace rehbersorgulama.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_aktivite
    {
        public int activite_id { get; set; }
        public Nullable<int> ilce_id { get; set; }
        public string aktivite_adi { get; set; }
        public string aciklama { get; set; }
        public Nullable<int> onecikanlar { get; set; }
        public string fotograf { get; set; }
        public Nullable<bool> active { get; set; }
    
        public virtual tbl_ilce tbl_ilce { get; set; }
    }
}
