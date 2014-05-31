using QuantumSchool.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QuantumSchool.DAL {
    public class SchoolContext : DbContext {
        public SchoolContext()
            : base("SchoolContext") {
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}