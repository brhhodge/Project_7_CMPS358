// Brian Hodge
// C00170400
// CMPS 358
// Project #7

using Microsoft.EntityFrameworkCore;

namespace ContactList
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }

    public class ContactDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=..\..\..\contacts.db");
        
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(new Contact[]
            {
                new Contact {Id = 1, Name = "Fred", Phone = "111"},
                new Contact {Id = 2, Name = "Barney", Phone = "222"}
            });
            
            base.OnModelCreating(modelBuilder);
        }
    }
}