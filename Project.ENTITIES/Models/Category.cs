using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Kategori İsmi")]
        public string CategoryName { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        //Relational Properties
        public virtual List<Product> Products { get; set; }
    }
}
