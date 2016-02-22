using DromitorySystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormitorySystem.Repositories.EntityFramework
{
    public class DormitoryInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DromityDbContext>
    {
        protected override void Seed(DromityDbContext context)
        {
            var levels = new List<Level>
            {
                new Level { lev_Text = "超级用户"},
                new Level { lev_Text = "操作用户" },
                new Level { lev_Text = "查询用户" }
            };
            levels.ForEach(s => context.Levels.AddOrUpdate(p => p.lev_Text, s));
            context.SaveChanges();

            var users = new List<User>
            {
                new User {
                    usr_Code="001",
                    usr_Name="管理员",
                    usr_Password="admin",
                    usr_lev_Id = levels.Single(l=>l.lev_Text=="超级用户").Id
                }
            };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.usr_Code, s));
            context.SaveChanges();

            var dormOptionTypes = new List<DormOptionType>
            {
                new DormOptionType{option_Name="性别"},
                new DormOptionType{option_Name="级别"},
                new DormOptionType{option_Name="部门"},
            };
            dormOptionTypes.ForEach(d => context.DormOptionTypes.AddOrUpdate(o => o.option_Name, d));
            context.SaveChanges();

            var dormSetting = new List<DormSetting>
            {
                new DormSetting{ set_Content="C",set_TypeId=dormOptionTypes.Single(o=>o.option_Name=="级别").Id}
            };
            dormSetting.ForEach(d => context.DormSettings.AddOrUpdate(s=>s.set_Content, d));
            context.SaveChanges();
        }
    }
}
