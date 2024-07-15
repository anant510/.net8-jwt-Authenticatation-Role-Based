using Microsoft.EntityFrameworkCore;
using thirdProject.Models;

namespace thirdProject.context
{
    public class FirstContext : DbContext
    {
        public FirstContext(DbContextOptions<FirstContext> options) : base(options)
        {
            
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
