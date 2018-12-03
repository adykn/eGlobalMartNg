using eGlobalMartNg.api.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace eGlobalMartNg.api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
       
        public DbSet<Value> Values { get; set; }
        public DbSet<User> User { get; set; }
    }
}