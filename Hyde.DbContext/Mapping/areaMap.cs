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
    public class areaMap : EntityTypeConfiguration<areaDto>
    {
        public areaMap()
        {
            this.HasKey(t => t.key);

            this.Property(t => t.key).HasColumnName("areaid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.name).HasMaxLength(baseMap.namelength).IsUnicode().IsRequired();

            this.Property(t => t.code).HasMaxLength(baseMap.codelength).IsUnicode().IsRequired();

            this.Property(t => t.zip).HasMaxLength(baseMap.codelength).IsUnicode();

            ToTable("area");

            this.HasMany(t => t.children).WithOptional().HasForeignKey(t => t.pid);
        }
    }
}
