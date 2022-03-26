using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace GadgetsOnlineShop.Models
{
    public class CreditCard
    {
        [Required(ErrorMessage ="Card Holder Name Required")]
        public String CreditCardHolderName { get; set;}
        [Required(ErrorMessage ="Card Number Required")]
        public int CreditCardNo { get; set; }
        [Required(ErrorMessage = "ExpDate Required")]

        public String ExpNo { get; set; }
        [Required(ErrorMessage = "CVV Required")]

        public int CVV { get; set; }
        [Required(ErrorMessage = "Amount Required")]

        public int Amount { get; set; }
    }
}