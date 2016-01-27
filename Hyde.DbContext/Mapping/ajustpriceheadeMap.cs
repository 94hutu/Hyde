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
    public class ajustpriceheadeMap : EntityTypeConfiguration<ajustpriceheadeDto>
    {
        public ajustpriceheadeMap()
        {
            this.HasKey(t => t.key);

            this.Property(t => t.key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("ajustpriceheadeid");

            this.Property(t => t.remark).IsUnicode().HasMaxLength(baseMap.remarklength);

            this.Property(t => t.code).IsRequired().HasMaxLength(baseMap.codelength);

            ToTable("ajustpriceheade");
        }
    }
}
