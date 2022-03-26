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
    using System.ComponentModel.DataAnnotations;
    
    public partial class DeliveryDetail
    {
        public int DeliveryId { get; set; }
        [Required(ErrorMessage ="UserName Required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "District Required")]

        public string DeliveryDistrict { get; set; }
        [Required(ErrorMessage = "State Required")]

        public string DeliveryState { get; set; }
        [Required(ErrorMessage = "ContactNo Required")]
        [RegularExpression(@"[6,7,8,9]{1}[0-9]{9}", ErrorMessage ="Invalid ContactNo")]

        public string UserContactNo { get; set; }
        [Required(ErrorMessage = "Address Required")]

        public string DeliveryAddress { get; set; }
    
        public virtual UserDetail UserDetail { get; set; }
    }
}