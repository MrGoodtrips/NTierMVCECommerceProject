using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.MVCUI.ConsumeDTO
{
    public class PaymentDTO
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="{0} alanı boş bırakılamaz")]
        public string CardUserName { get; set; }

        [Required(ErrorMessage ="{0} alanı boş bırakılamaz")]
        public string SecurityNumber { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public int CardExpiryMonth { get; set; }
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public int CardExpiryYear { get; set; }
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        public decimal ShoppingPrice { get; set; }
    }
}