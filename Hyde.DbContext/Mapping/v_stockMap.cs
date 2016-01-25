using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using Hyde.Domain.Model;
namespace Hyde.Context.Mapping
{
    public class v_stockMap : EntityTypeConfiguration<v_stockDto>
    {
        public v_stockMap()
        {
            HasKey(t => new { t.warehouseid, t.productid, t.sizeid });

            this.Property(t => t.warehouseid).HasColumnName("warehouseid");

            ToTable("v_stock");

            HasRequired(t => t.product).WithMany(t=>t.v_stocks).HasForeignKey(t => t.productid);
            HasRequired(t => t.size).WithMany().HasForeignKey(t => t.sizeid);
            HasRequired(t => t.warehouse).WithMany(t => t.v_stocks).HasForeignKey(t => t.warehouseid);
            HasRequired(t => t.supply).WithMany().HasForeignKey(t => t.supplyid);
        }
    }
}
