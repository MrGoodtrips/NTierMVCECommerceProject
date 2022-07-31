using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUserProfile : BaseEntity
    {
        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "İsim")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Soy İsim")]
        public string LastName { get; set; }

        //Relational Properties
        public virtual AppUser AppUser { get; set; }
    }
}
