using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class OrderDetailMap : BaseMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            ToTable("Sipariş Detayları");

            Property(x => x.SubTotal).HasColumnName("Ara Toplam").HasColumnType("money").IsRequired();
            Property(x => x.Quantity).HasColumnName("Adet");

            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.OrderID,
                x.ProductID
            });
        }
    }
}
