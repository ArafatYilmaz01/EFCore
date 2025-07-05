using EFCoreTutorial.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCoreTutorial.Data.Context
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
      

        // DbSet'ler
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<StudentAddress> StudentAddresses => Set<StudentAddress>();
        public DbSet<Book> Books => Set<Book>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Fluent API ile ilişki yapılandırmaları
            // Sadece gerekli ilişkiyi tanımlıyoruz:
            modelBuilder.Entity<StudentAddress>()
                .HasOne(address => address.Student)
                .WithOne(student => student.Address)
                .HasForeignKey<Student>(student => student.AddressId)
                .HasConstraintName("student_address_student_id_fk");
        }
    }
}
