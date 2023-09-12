using Microsoft.EntityFrameworkCore;
using UserForm.API.Models;


namespace UserForm.API.Data
{
    public class UserFormDbContext : DbContext
    {
        public UserFormDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
