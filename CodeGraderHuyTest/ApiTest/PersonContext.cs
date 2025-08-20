using Microsoft.EntityFrameworkCore;

namespace ApiTest
{
    public class PersonContext : DbContext
    {
        public PersonContext() { }

        public PersonContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new PersonConfig());
        }
        public DbSet<Person> People { get; set; }
    }
}
