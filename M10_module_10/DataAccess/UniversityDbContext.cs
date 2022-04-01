using DataAccess.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess
{
    public class UniversityDbContext : DbContext
    {
        public DbSet<HomeworkDb> Homeworks { get; set; }
        public DbSet<LectorDb> Lectors { get; set; }
        public DbSet<LectureDb> Lectures { get; set; }
        public DbSet<StudentDb> Students { get; set; }
        public DbSet<AttendanceDb> Attendances { get; set; }

        public UniversityDbContext(DbContextOptions<UniversityDbContext> options) : base(options)
        {
           // Database.EnsureDeleted();
            Database.EnsureCreated();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfiguration());


            modelBuilder.Entity<LectureDb>().HasData(
                new LectureDb[]
                {
                    new LectureDb {Id=1 , Name = "Math"},
                    new LectureDb {Id=2 , Name = "English"},
                    new LectureDb {Id=3 , Name = "PE"},
                });

            modelBuilder.Entity<LectorDb>().HasData(
                new LectorDb[]
                {
                    new LectorDb {Id=1 , FirstName = "Vladimir", LastName="Lusev", Email="lysev.v.i@yandex.ru"},
                    new LectorDb {Id=2 , FirstName = "Alexandr", LastName="Starkov",  Email="starkov.a.s@yandex.ru"},
                    new LectorDb {Id=3 , FirstName = "Alexey", LastName="Timofeevskiy", Email="timAlex@yandex.ru"},
                });

            modelBuilder.Entity<HomeworkDb>().HasData(
                new HomeworkDb[]
                {
                    new HomeworkDb {Id=1 , Name = "Math_Homework", CourseMark = 2},
                    new HomeworkDb {Id=2 , Name = "English_Homework", CourseMark =5},
                    new HomeworkDb {Id=3 , Name = "PE_Homework", CourseMark =4},
                });

            modelBuilder.Entity<StudentDb>().HasData(
                new StudentDb[]
                {
                    new StudentDb {Id=1 , FirstName = "Mikhail", LastName= "Savelev", Email = "Savelev@yandex.ru", Phone ="+79800553535"},
                    new StudentDb {Id=2 , FirstName = "Geka", LastName= "Plechev", Email = "PlecevGekaChill@yandex.ru", Phone ="+79800553535"},
                    new StudentDb {Id=3 , FirstName = "Ilya", LastName= "Kugov", Email = "VerniSotku@yandex.ru",Phone ="+79800553535"},
                });         

        }
    }
    internal class ProductConfiguration : IEntityTypeConfiguration<LectorDb>
    {
        public void Configure(EntityTypeBuilder<LectorDb> builder)
        {
            builder.Property(p => p.Email).IsRequired().HasMaxLength(50); 
        }
    }
}
