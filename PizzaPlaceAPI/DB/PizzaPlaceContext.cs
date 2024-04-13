using Microsoft.EntityFrameworkCore;
using PizzaPlaceAPI.DB.Models;

namespace PizzaPlaceAPI.DB
{
    public class PizzaPlaceContext : DbContext
    {
        public IConfiguration config { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<PizzaType> PizzaType { get; set; }

        public PizzaPlaceContext(IConfiguration config)
        {
            this.config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string? conn_string = config.GetConnectionString("Default");
            builder.UseSqlServer(conn_string);
        }
    }
}
