using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vera.Models;
using Vera.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Vera.Infrastructure {
    public class VeraContext : IdentityDbContext<ApplicationUser> {

        public VeraContext() { }
        public VeraContext(DbContextOptions<VeraContext> options) : base(options) { }
        public DbSet<User> UserProfil { get; set; } = null!;
        public DbSet<BloodPressure> BloodPressures { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            // установка пути к текущему каталогу
            builder.SetBasePath(Directory.GetCurrentDirectory());
            // получаем конфигурацию из файла appsettings.json
            builder.AddJsonFile("appsettings.json");
            // создаем конфигурацию
            var config = builder.Build();
            // получаем строку подключения
            var connectionString = config.GetConnectionString("DefaultConnection");
                        
            optionsBuilder.UseMySql(connectionString,
                new MySqlServerVersion(new Version(8, 0, 33)));
        }

        /* protected override void  OnModelCreating(ModelBuilder modelBuilder){                            

             base.OnModelCreating(modelBuilder);

             //Исключение с помощью Fluent API:
             //modelBuilder.Entity<ApplicationUser>().Ignore(u => u.ConcurrencyStamp);
             //modelBuilder.Entity<ApplicationUser>().Ignore(u => u.LockoutEnd);            
             //modelBuilder.Entity<ApplicationUser>().Ignore(u => u.NormalizedEmail);
             //modelBuilder.Entity<ApplicationUser>().Ignore(u => u.NormalizedUserName);    
             //"Unknown column 'u.LockoutEnabled' in 'field list'"                                
         }*/

    }
}
