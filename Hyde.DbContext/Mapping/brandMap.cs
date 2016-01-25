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
    public class brandMap : EntityTypeConfiguration<brandDto>
    {
        public brandMap()
        {
            this.HasKey(t => t.key);

            this.Property(t => t.key).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity).HasColumnName("brandid");

            this.Property(t => t.name).IsUnicode().HasMaxLength(20).IsRequired();

            this.Property(t => t.enname).HasMaxLength(20).IsUnicode();

            this.Property(t => t.imgpath).HasMaxLength(255).IsUnicode();

            this.Property(t => t.shutout).HasColumnName("shutout");

            ToTable("brand");
        }
    }
}
