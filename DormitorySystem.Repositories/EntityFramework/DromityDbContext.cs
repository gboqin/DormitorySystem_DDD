using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories.EntityFramework
{
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DromityDbContext:DbContext
    {
        #region 构造函数
        public DromityDbContext():base("default") { }
        public DromityDbContext(string NameOrConnectionString) : base(NameOrConnectionString) { }       
        #endregion

        public DbSet<User> Users { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Goods> Goodses { get; set; }
        public DbSet<DormOptionType> DormOptionTypes { get; set; }
        public DbSet<DormSetting> DormSettings { get; set; }
        public DbSet<Dormitory> Dormitories { get; set; }
        public DbSet<DormSetRelation> DormSetRelations { get; set; }
        public DbSet<DormDeductmoney> DormDeductmonies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
