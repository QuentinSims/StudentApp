using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student.Shared.DomainModels.Authentication.SystemUsers;
using Student.Shared.DomainModels.CourseManagement;

namespace Student.Shared.DataLayer
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private static void CreateForeignKeys(ModelBuilder modelBuilder)
        {
            ConfigureApplicationUserEntity(modelBuilder);
            CreateEnrolledForeignKeys(modelBuilder);
        }

        private static void ConfigureApplicationUserEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasQueryFilter(e => !e.IsDeleted);
                entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.FirstName).HasMaxLength(250);
                entity.Property(e => e.LastName).HasMaxLength(250);
                entity.Property(e => e.PhoneNumber).HasMaxLength(500);
            });
        }
        private static void CreateEnrolledForeignKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EnrolledCourse>()
                .HasOne(x => x.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId)
                 .IsRequired();

            modelBuilder.Entity<EnrolledCourse>()
                           .HasOne(x => x.Course)
                           .WithMany()
                           .HasForeignKey(e => e.CourseId)
                            .IsRequired();
        }

    }
}
