using Microsoft.EntityFrameworkCore;
using PizzaPlaceAPI.DB.Models;

namespace PizzaPlaceAPI.DB
{
    public class PizzaPlaceContext : DbContext
    {
        public IConfiguration config { get; set; }
        public DbSet<OrderStamp> OrderStamp { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public DbSet<Ingredient> Ingredient { get; set; }

        public PizzaPlaceContext(IConfiguration config)
        {
            this.config = config;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            string? conn_string = config.GetConnectionString("New");
            builder.UseSqlServer(conn_string);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderStamp>();
            builder.Entity<OrderDetail>();
            builder.Entity<Pizza>();
            builder.Entity<Recipe>();
            builder.Entity<Category>();
            builder.Entity<RecipeIngredient>();
            builder.Entity<Ingredient>();
        }
    }
}
