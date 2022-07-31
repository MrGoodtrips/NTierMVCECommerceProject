using Project.ENTITIES.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.ENTITIES.Models
{
    public abstract class BaseEntity
    {
        public int ID { get; set; }

        [Display(Name = "Veri Oluşturulma Tarihi")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Veri Güncellenme Tarihi")]
        public DateTime ModifiedDate { get; set; }

        [Display(Name = "Veri Silinme Tarihi")]
        public DateTime DeletedDate { get; set; }

        [Display(Name = "Veri Durumu")]
        public DataStatus Status { get; set; }

        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
            Status = DataStatus.Inserted;
        }

    }
}
