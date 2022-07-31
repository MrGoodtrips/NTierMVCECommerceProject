using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AppUserMap : BaseMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("Kullanıcılar");

            Property(x => x.UserName).HasColumnName("Kullanıcı Adı").IsRequired();
            Property(x => x.Password).HasColumnName("Şifre").IsRequired();
            Property(x => x.ActivationCode).HasColumnName("Aktivasyon Kodu");
            Property(x => x.Email).HasColumnName("E-mail Adresi").IsRequired();
            Property(x => x.Active).HasColumnName("Aktiflik Durumu");

            HasOptional(x => x.AppUserProfile).WithRequired(x => x.AppUser);
        }
    }
}
