using Microsoft.EntityFrameworkCore;
using System.Linq;
using PizzaPlaceAPI.Controllers.Model;
using PizzaPlaceAPI.DB.Models;
using System.Collections.Generic;

namespace PizzaPlaceAPI.DB
{
    public class PizzaPlaceRepository : IPizzaPlaceRepository
    {
        private PizzaPlaceContext db;
        public PizzaPlaceRepository(PizzaPlaceContext db)
        {
            this.db = db;
        }

        public void BulkInsert(string datasetType, string file)
        {
            // Convert text to list
            // Remove Header row
            List<string> rows = file.Split('\n').ToList();
            rows.RemoveAt(0);
            switch (datasetType)
            {
                case "orders":
                    // TRUNCATE to avoid ID conflicts
                    this.db.OrderStamp.RemoveRange(this.db.OrderStamp);

                    foreach (var item in rows)
                    {
                        // Skip Empty lines
                        if (item == "") continue;
                        var insert = new OrderStamp();
                        var values = item.Split(",");
                        insert.order_id = Int32.Parse(values[0]);
                        insert.date = DateOnly.Parse(values[1]);
                        insert.time = TimeOnly.Parse(values[2]);
                        this.db.OrderStamp.Add(insert);
                    }
                    break;
                case "order_details":
                    // TRUNCATE to avoid ID conflicts
                    this.db.OrderDetail.RemoveRange(this.db.OrderDetail);

                    foreach (var item in rows)
                    {
                        // Skip Empty lines
                        if (item == "") continue;
                        var insert = new OrderDetail();
                        var values = item.Split(",");
                        insert.order_detail_id = Int32.Parse(values[0]);
                        insert.order_id = Int32.Parse(values[1]);
                        insert.pizza_id = values[2];
                        insert.quantity = Int32.Parse(values[3]);
                        this.db.OrderDetail.Add(insert);
                    }
                    break;
                case "pizzas":
                    // TRUNCATE to avoid ID conflicts
                    this.db.Pizza.RemoveRange(this.db.Pizza);

                    foreach (var item in rows)
                    {
                        // Skip Empty lines
                        if (item == "") continue;
                        var insert = new Pizza();
                        var values = item.Split(",");
                        insert.pizza_id = values[0];
                        insert.recipe_id = values[1];
                        insert.size = values[2];
                        insert.price = Double.Parse(values[3]);
                        this.db.Pizza.Add(insert);
                    }
                    break;
                case "pizza_types":
                    // TRUNCATE to avoid ID conflicts
                    this.db.Recipe.RemoveRange(this.db.Recipe);
                    this.db.Category.RemoveRange(this.db.Category);
                    this.db.RecipeIngredient.RemoveRange(this.db.RecipeIngredient);
                    this.db.Ingredient.RemoveRange(this.db.Ingredient);

                    // Key-Value pair for category/ingredient name-id
                    var catDict = new Dictionary<string, int>();
                    var ingDict = new Dictionary<string, int>();
                    foreach (var item in rows)
                    {
                        // Skip Empty lines
                        if (item == "") continue;
                        var values = item.Split(",");

                        // Check if name is already in Dict
                        // Insert category name if not
                        if (!catDict.ContainsKey(values[2]))
                        {
                            var newCategory = new Category();
                            newCategory.name = values[2];
                            this.db.Category.Add(newCategory);
                            this.db.SaveChanges();
                            catDict[newCategory.name] = newCategory.category_id;
                        }

                        // Insert Recipe
                        var insert = new Recipe();
                        insert.recipe_id = values[0];
                        insert.name = values[1];
                        insert.category_id = catDict[values[2]];
                        this.db.Recipe.Add(insert);

                        // Loop for Ingredient list
                        var ingredientList = item.Split("\"")[1].Split(",");
                        foreach (var ing in ingredientList)
                        {
                            var rcp_ing = new RecipeIngredient();
                            var cln_ing = ing.Trim();
                            // Check if name is already in Dict
                            // Insert ingredient name if not
                            if (!ingDict.ContainsKey(cln_ing))
                            {
                                var newIngredient = new Ingredient();
                                newIngredient.name = cln_ing;
                                this.db.Ingredient.Add(newIngredient);
                                this.db.SaveChanges();
                                ingDict[newIngredient.name] = newIngredient.ingredient_id;
                            }
                            // Insert Recipe Ingredients
                            rcp_ing.recipe_id = insert.recipe_id;
                            rcp_ing.ingredient_id = ingDict[cln_ing];
                            this.db.RecipeIngredient.Add(rcp_ing);
                        }
                    }
                    break;
            }
            this.db.SaveChanges();
        }
        public OrderStamp InsertOrder()
        {
            var order = new OrderStamp();
            var timestamp = DateTime.UtcNow;
            order.date = DateOnly.FromDateTime(timestamp);
            order.time = TimeOnly.FromDateTime(timestamp);
            this.db.OrderStamp.Add(order);
            this.db.SaveChanges();
            return order;
        }
        public List<OrderItem> InsertOrderDetails(int orderId, List<OrderItem> orderItem)
        {
            var orderDetails = new List<OrderDetail>();
            foreach ( var item in orderItem)
            {
                var order = new OrderDetail();
                order.order_id = orderId;
                order.pizza_id = item.pizza_id;
                order.quantity = item.quantity;
                orderDetails.Add(order);
            }
            this.db.OrderDetail.AddRange(orderDetails);
            this.db.SaveChanges(true);
            return orderItem;
        }

        public IQueryable<MenuItem> GetPizzaList()
        {
            return this.QueryMenuItem();
        }
        public IQueryable<MenuItem> GetPizzaListByCategory(int categId)
        {
            return this.QueryCategoryItem(categId);
        }
        public IQueryable<MenuItem> SearchPizza(SearchQuery query)
        {
            return this.QuerySearchItem(query);
        }
        public int GetPizzaCount()
        {
            return this.QueryMenuItem().Count();
        }
        public int GetPizzaCountByCategory(int categId)
        {
            return this.QueryCategoryItem(categId).Count();
        }
        public string? GetCategoryName(int categId)
        {
            var qCategory = this.db.Category.AsQueryable();
            return (
                from categ in qCategory
                where categ.category_id == categId
                select categ.name
            ).FirstOrDefault();
        }

        // Reusable Query for getting Menu Items
        private IQueryable<MenuItem> QueryMenuItem()
        {
            var qPizza = this.db.Pizza.AsQueryable();
            var qRecipe = this.db.Recipe.AsQueryable();
            var qCategory = this.db.Category.AsQueryable();

            return from pizza in qPizza
                join recipe in qRecipe on pizza.recipe_id equals recipe.recipe_id
                join category in qCategory on recipe.category_id equals category.category_id
                select new MenuItem
                {
                    name = recipe.name,
                    category = category.name,
                    size = pizza.size,
                    price = pizza.price
                };
        }
        // Reusable Query for getting Menu Items by Category
        private IQueryable<MenuItem> QueryCategoryItem(int categId)
        {
            var qPizza = this.db.Pizza.AsQueryable();
            var qRecipe = this.db.Recipe.AsQueryable();
            var qCategory = this.db.Category.AsQueryable();

            return from pizza in qPizza
                join recipe in qRecipe on pizza.recipe_id equals recipe.recipe_id
                join category in qCategory on recipe.category_id equals category.category_id
                where category.category_id == categId
                select new MenuItem
                {
                    name = recipe.name,
                    category = category.name,
                    size = pizza.size,
                    price = pizza.price
                };
        }
        private IQueryable<MenuItem> QuerySearchItem(SearchQuery query)
        {
            // Pre-Filter tables
            var qPizza = this.db.Pizza.AsQueryable()
                .Where(pizza => query.size == null || pizza.size == query.size);
            var qRecipe = this.db.Recipe.AsQueryable()
                .Where(recipe => query.name == null || recipe.name.Contains(query.name));
            var qCategory = this.db.Category.AsQueryable()
                .Where(category => query.categId == null || category.category_id == query.categId);
            // Ingredients Filtering
            var qIngredient = this.db.Ingredient.AsQueryable();
            var qRecipeIngredient = this.db.RecipeIngredient.AsQueryable();
            IQueryable<RecipeIngredient>? included = null;
            IQueryable<RecipeIngredient>? excluded = null;
            if (query.contains != null && query.contains.Count > 0)
            {
                var contains = query.contains.AsQueryable();
                included = from ingredient in qIngredient
                    join recIng in qRecipeIngredient on ingredient.ingredient_id equals recIng.ingredient_id
                    where contains.Any(ing => ing == ingredient.name)
                    select recIng;
            }
            if (query.exclude != null && query.exclude.Count > 0)
            {
                var exclude = query.exclude.AsQueryable();
                excluded = from ingredient in qIngredient
                    join recIng in qRecipeIngredient on ingredient.ingredient_id equals recIng.ingredient_id
                    where exclude.Any(ing => ing == ingredient.name)
                    select recIng;
            }

            // Join all Filtered Tables
            var result = from pizza in qPizza
                join recipe in qRecipe on pizza.recipe_id equals recipe.recipe_id
                join category in qCategory on recipe.category_id equals category.category_id
                where (included == null || included.Any(recipe_id => recipe_id.recipe_id == recipe.recipe_id))
                    && (excluded == null || !excluded.Any(recipe_id => recipe_id.recipe_id == recipe.recipe_id))
                select new MenuItem
                {
                    name = recipe.name,
                    category = category.name,
                    size = pizza.size,
                    price = pizza.price
                };

            return result;
        }
    }
}
