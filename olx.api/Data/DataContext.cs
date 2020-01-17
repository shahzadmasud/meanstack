using System;
using Microsoft.EntityFrameworkCore;
using olx.api.Models;

namespace olx.api.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base (options) { }
        public DbSet<Value> Values {get; set; }
        public DbSet<User> Users { get; set; }
    }
}