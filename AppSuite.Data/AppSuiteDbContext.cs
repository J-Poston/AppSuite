using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace AppSuite.Data
{
    public class AppSuiteDbContext : IdentityDbContext<IdentityUser>
    {
        public AppSuiteDbContext(DbContextOptions<AppSuiteDbContext> options) :base(options)
        {
            
        }

        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<EquipManuf> EquipManufs { get; set; }
        public DbSet<EquipModel> EquipModels { get; set; }
        public DbSet<EquipCategory> EquipCategories { get; set; }
        public DbSet<EquipSubcategory> EquipSubcategories { get; set;  }
    }
}
