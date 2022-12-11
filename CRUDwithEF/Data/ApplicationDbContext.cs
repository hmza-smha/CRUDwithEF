using CRUDwithEF.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDwithEF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public ApplicationDbContext(DbContextOptions opt) : base(opt)
        {
        }
    }
}
