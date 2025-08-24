using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Student.Shared.DomainModels.Authentication.SystemUsers;
using Student.Shared.DomainModels.CourseManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace Student.Shared.DataLayer
{
    public partial class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes()
                         .Where(t =>
                             t.ClrType.GetProperties()
                                 .Any(p => p.CustomAttributes.Any(
                                     a => a.AttributeType == typeof(DatabaseGeneratedAttribute)))))
            {
                foreach (var property in entity.ClrType.GetProperties()
                             .Where(p => p.PropertyType == typeof(Guid) &&
                                         p.CustomAttributes.Any(a =>
                                             a.AttributeType == typeof(DatabaseGeneratedAttribute))))
                {
                    modelBuilder
                        .Entity(entity.ClrType)
                        .Property(property.Name)
                        .ValueGeneratedOnAdd();
                }
            }

            CreateForeignKeys(modelBuilder);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            SeedCourses(modelBuilder);
        }


        private void SeedCourses(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = new Guid("7e94c5e4-1a5c-45f9-8c62-45e3b9b66b70"),
                    CourseCode = "CS102",
                    CourseName = "Data Structures",
                    Instructor = "Dr. Anderson",
                    Credits = 3,
                    Schedule = "MWF 10:00-11:00",
                    Department = "Computer Science",
                    AvailableSeats = 15,
                    MaxSeats = 30,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("c34f7a21-2b18-41b1-93e7-f2d2a02a6e5b"),
                    CourseCode = "MATH301",
                    CourseName = "Linear Algebra",
                    Instructor = "Prof. Davis",
                    Credits = 3,
                    Schedule = "TTH 9:00-10:30",
                    Department = "Mathematics",
                    AvailableSeats = 8,
                    MaxSeats = 25,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("2f5fcb2e-df29-445f-bb09-b1d83c8e70e2"),
                    CourseCode = "PHYS201",
                    CourseName = "Physics I",
                    Instructor = "Dr. Wilson",
                    Credits = 4,
                    Schedule = "MWF 1:00-2:00",
                    Department = "Physics",
                    AvailableSeats = 0,
                    MaxSeats = 20,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("9be8ac3f-6df6-4a92-8157-962f8fca54b7"),
                    CourseCode = "CHEM101",
                    CourseName = "General Chemistry",
                    Instructor = "Prof. Taylor",
                    Credits = 4,
                    Schedule = "TTH 2:00-3:30",
                    Department = "Chemistry",
                    AvailableSeats = 12,
                    MaxSeats = 28,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("58b9c3a4-4331-4de0-a6f0-24a7e2de5f48"),
                    CourseCode = "BIO150",
                    CourseName = "Introduction to Biology",
                    Instructor = "Dr. Martinez",
                    Credits = 3,
                    Schedule = "MWF 11:00-12:00",
                    Department = "Biology",
                    AvailableSeats = 20,
                    MaxSeats = 35,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("abf2c6d8-3a9c-4979-91cf-6c4492a6b73e"),
                    CourseCode = "CS101",
                    CourseName = "Introduction to Computer Science",
                    Instructor = "Dr. Smith",
                    Credits = 3,
                    Schedule = "MWF 9:00-10:00",
                    Department = "Computer Science",
                    AvailableSeats = 5,
                    MaxSeats = 30,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                },
                new Course
                {
                    Id = new Guid("f0a3b9d4-1e7a-4f64-9e32-8a5c71e2c4d9"),
                    CourseCode = "ART120",
                    CourseName = "Digital Art Fundamentals",
                    Instructor = "Prof. Chen",
                    Credits = 2,
                    Schedule = "TTH 3:00-4:30",
                    Department = "Fine Arts",
                    AvailableSeats = 18,
                    MaxSeats = 20,
                    CreatedBy = "system",
                    CreatedOn = new DateTime(2024, 7, 25)
                }
            );
        }
    }
}
