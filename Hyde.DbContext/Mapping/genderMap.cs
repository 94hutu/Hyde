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
    public class genderMap : EntityTypeConfiguration<genderDto>
    {
        public genderMap()
        {
            HasKey(t => t.key);

            this.Property(t => t.key).HasColumnName("genderid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.name).IsUnicode().IsRequired().HasMaxLength(50);

            this.Property(t => t.imgpath).IsUnicode().HasMaxLength(255);

            this.Property(t => t.shutout).HasColumnName("shutout");

            ToTable("gender");
        }
    }
}
