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
    public class categoryMap : EntityTypeConfiguration<categoryDto>
    {
        public categoryMap()
        {
            this.HasKey(t => t.key);

            this.Property(t => t.key).HasColumnName("categoryid").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.name).HasMaxLength(50).IsUnicode().IsRequired();

            this.Property(t => t.shutout).HasColumnName("shutout");

            ToTable("category");

            HasMany(t => t.children).WithOptional().HasForeignKey(t => t.pid);

        }
    }
}
