//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GadgetsOnlineShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CartDetail
    {
        public int CartId { get; set; }
        public string CartProductName { get; set; }
        public string CartImage { get; set; }
        public Nullable<int> CartPrice { get; set; }
        public string UserName { get; set; }
        public int CartNo { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
    }
}
