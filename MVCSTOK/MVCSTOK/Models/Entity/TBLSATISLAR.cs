//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVCSTOK.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLSATISLAR
    {
        public int SatısId { get; set; }
        public Nullable<int> Urun { get; set; }
        public Nullable<int> Musteri { get; set; }
        public Nullable<byte> Adet { get; set; }
        public Nullable<decimal> Fiyat { get; set; }
    
        public virtual TBLMUSTERİLER TBLMUSTERİLER { get; set; }
        public virtual TBLURUNLER TBLURUNLER { get; set; }
    }
}
