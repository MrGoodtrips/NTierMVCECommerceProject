using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Order : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Gönderim Adresi")]
        public string ShippedAddress { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Email Adresi")]
        [EmailAddress(ErrorMessage ="{0} alanı Email formatında olmalıdır")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Display(Name = "Toplam Fiyat")]
        public decimal TotalPrice { get; set; }//Sipariş işlemlerinin icerisindeki bilgileri daha rahat yakalamak adına actıgımız property'lerden bir tanesi TotalPrice'dir...
        public int? AppUserID { get; set; }

        //Relational Properties

        public virtual AppUser AppUser { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
