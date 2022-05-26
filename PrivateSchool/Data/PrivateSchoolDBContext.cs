using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PrivateSchool.Entities;

namespace PrivateSchool.Data
{
    public class PrivateSchoolDBContext : IdentityDbContext<User>
    {

        public PrivateSchoolDBContext(DbContextOptions<PrivateSchoolDBContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        //public DbSet<StudentClasses> StudentClasses { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<StudentClasses>(e =>
            //{
            //    e.HasKey(k => new { k.StudentId, k.ClassId });
            //});


            //modelBuilder.Entity<Class>()
            //    .HasOne(c => c.Teacher)
            //    .WithMany(t => t.Classes)
            //    .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<User>()
                .Ignore(e => e.LockoutEnabled)
                .Ignore(e => e.LockoutEnd)
                .Ignore(e => e.NormalizedEmail)
                .Ignore(e => e.NormalizedUserName)
                .Ignore(e => e.Email)
                .Ignore(e => e.EmailConfirmed)
                .Ignore(e => e.TwoFactorEnabled)
                .Ignore(e => e.PhoneNumber)
                .Ignore(e => e.PhoneNumberConfirmed)
                .Ignore(e => e.SecurityStamp)
                .Ignore(e => e.AccessFailedCount)
                .Ignore(e => e.ConcurrencyStamp);

            base.OnModelCreating(modelBuilder);
        }
        

    }
}
