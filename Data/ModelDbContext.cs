using CarManufacturers.Models;
using Microsoft.EntityFrameworkCore;

namespace CarManufacturers.Data
{
    public class ModelDbContext : DbContext
    {
        public ModelDbContext(DbContextOptions<ModelDbContext> options )
            :base( options ) 
        {
            
        }

        public DbSet<Model> Models { get; set; }
    }
}
