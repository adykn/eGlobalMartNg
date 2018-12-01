using eGlobalMartNg.api.Models;
using Microsoft.EntityFrameworkCore;

namespace eGlobalMartNg.api.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options){}
        public DbSet<Value> Values { get; set; }
    }
}