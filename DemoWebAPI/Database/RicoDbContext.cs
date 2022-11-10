using DemoWebAPI.Controllers;
using DemoWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoWebAPI.Database
{
    public class RicoDbContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }


        public RicoDbContext(DbContextOptions options):base(options)
        {

        }

     
    }
}
