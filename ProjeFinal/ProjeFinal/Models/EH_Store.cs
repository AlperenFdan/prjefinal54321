using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ProjeFinal.Models
{
    public partial class EH_Store : DbContext
    {
        public EH_Store()
            : base("name=EH_StoreDB")
        {
        }

   
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Emp> Emps { get; set; }
        public DbSet<GraphicsCard> GraphicsCards { get; set; }
        public DbSet<Memory> Memorys { get; set; }
        public DbSet<Processor> Processors { get; set; }
        public DbSet<Motherboard> Motherboards { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<SCart> SCarts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
