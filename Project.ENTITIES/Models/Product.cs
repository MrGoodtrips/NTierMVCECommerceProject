using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Ürün İsmi")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Birim Fiyatı")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Resim Yolu")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Stoktaki Ürün Sayısı")]
        public short UnitsInStock { get; set; }

        [Display(Name = "Kategori")]
        public int? CategoryID { get; set; }

        //Relational Properties
        public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
