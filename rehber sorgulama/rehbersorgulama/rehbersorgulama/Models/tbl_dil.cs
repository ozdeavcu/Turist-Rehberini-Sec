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
    
    public partial class tbl_dil
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tbl_dil()
        {
            this.kullanici_dil_detay = new HashSet<kullanici_dil_detay>();
            this.rehber_dil_detay = new HashSet<rehber_dil_detay>();
            this.tbl_kullanici = new HashSet<tbl_kullanici>();
        }
    
        public int dil_id { get; set; }
        public string dil_ad { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<kullanici_dil_detay> kullanici_dil_detay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rehber_dil_detay> rehber_dil_detay { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tbl_kullanici> tbl_kullanici { get; set; }
    }
}
