using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Hyde.Context.Mapping;
namespace Hyde.Context
{
    public class HydeContext : DbContext
    {

        static HydeContext()
        {
            Database.SetInitializer<HydeContext>(null);//如果没有数据库则注释这行代码，运行自动生成
        }

        public HydeContext()
            : base("name=HydeContext")
        {
            //this.Configuration.AutoDetectChangesEnabled = false;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ValidateOnSaveEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.AddFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
