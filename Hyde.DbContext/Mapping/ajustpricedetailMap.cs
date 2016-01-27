using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Hyde.Domain.Model;
using System.ComponentModel.DataAnnotations.Schema;
namespace Hyde.Context.Mapping
{
   public class ajustpricedetailMap:EntityTypeConfiguration<ajustpricedetailDto>
    {
        public ajustpricedetailMap()
        {
            this.HasKey(t => t.key);

            this.Property(t => t.key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("ajustpricedetailid");

            ToTable("ajustpricedetail");

            this.HasRequired(t => t.product).WithMany().HasForeignKey(t => t.productid);

            this.HasRequired(t => t.ajustpriceheade).WithMany(t => t.details).HasForeignKey(t => t.ajustpriceheadeid);
        }
    }
}
