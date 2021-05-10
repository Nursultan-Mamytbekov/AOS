using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;
using AOS.Data;

namespace AOS.Data
{
    public class ApplicationDbContext : IdentityDbContext<User> 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<Homework> Homeworks { get; set; }
        public DbSet<MaterialFile> MaterialFiles { get; set; }
        public DbSet<HomeworkFile> HomeworkFiles { get; set; }
        public DbSet<Result> Results { get; set; }
    }
}
