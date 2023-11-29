using DriverMonitorBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace DriverMonitorBackend
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }

        public DbSet<DriverActivityFile> DriverActivityFiles { get; set; }
    }
}
