using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using Hyde.Domain;

namespace Hyde.Context.Mapping
{
    public class addressMap : ComplexTypeConfiguration<address>
    {
        public addressMap()
        {
            this.Property(t => t.name).HasMaxLength(20).IsUnicode().HasColumnName("name").IsRequired();

            this.Property(t => t.receptionaddress).HasMaxLength(baseMap.remarklength).IsRequired().IsUnicode().HasColumnName("receptionaddress");

            this.Property(t => t.telephone).HasMaxLength(baseMap.namelength).IsRequired().IsUnicode().HasColumnName("telephone");
        }
    }
}
