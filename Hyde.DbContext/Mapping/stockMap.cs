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
    public class stockMap : EntityTypeConfiguration<stockDto>
    {
        public stockMap()
        {
            HasKey(t => t.key);

            this.Property(t => t.key).HasColumnName("stockid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.createdatetime).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            ToTable("stock");

            HasRequired(t => t.product).WithMany().HasForeignKey(t => t.productid);
            HasRequired(t => t.size).WithMany().HasForeignKey(t => t.sizeid);
            HasRequired(t => t.warehouse).WithMany().HasForeignKey(t => t.warehouseid);
            HasRequired(t => t.supply).WithMany().HasForeignKey(t => t.supplyid);
        }
    }
}
