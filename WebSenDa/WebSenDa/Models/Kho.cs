//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebSenDa.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kho
    {
        public int IDSanPham { get; set; }
        public decimal GiaBan { get; set; }
        public int SoLuongTon { get; set; }
    
        public virtual SanPham SanPham { get; set; }
    }
}
