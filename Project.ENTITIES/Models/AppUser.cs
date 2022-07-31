using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public class AppUser : BaseEntity
    {
        public AppUser()
        {
            Role = UserRole.Member;
        }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} alanı boş bırakılamaz")]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [Display(Name = "Aktivasyon Kodu")]
        public Guid ActivationCode { get; set; }

        [Display(Name = "Aktiflik Durumu")]
        public bool Active { get; set; }

        [EmailAddress(ErrorMessage = "{0} alanı Email formatında olmalı")]
        [Display(Name = "{0} alanı boş bırakılamaz")]
        public string Email { get; set; }

        [Display(Name = "Kullanıcı Rolü")]
        public UserRole Role { get; set; }

        //Relational Properties
        public virtual AppUserProfile AppUserProfile { get; set; }
        public virtual List<Order> Orders { get; set; }
    }
}
