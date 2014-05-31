using QuantumSchool.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace QuantumSchool.DAL {
    public class SchoolContext : DbContext {
        public SchoolContext()
            : base("SchoolContext") {
                //Run Web Application once to ensure database is created if it does not exist.
                Database.SetInitializer<SchoolContext>(new CreateDatabaseIfNotExists<SchoolContext>());
        }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}