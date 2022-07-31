using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class OrderMap : BaseMap<Order>
    {
        public OrderMap()
        {
            ToTable("Siparişler");

            Property(x => x.ShippedAddress).HasColumnName("Gönderim Adresi").IsRequired();
            Property(x => x.TotalPrice).HasColumnName("Toplam Fiyat").IsRequired();
            Property(x => x.UserName).HasColumnName("Kullanıcı Adı");
            Property(x => x.Email).HasColumnName("Kullanıcı E-mail Adresi");
        }
    }
}
