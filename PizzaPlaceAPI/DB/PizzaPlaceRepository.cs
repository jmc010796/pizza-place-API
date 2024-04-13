using Microsoft.EntityFrameworkCore;
using PizzaPlaceAPI.DB.Models;

namespace PizzaPlaceAPI.DB
{
    public class PizzaPlaceRepository : IPizzaPlaceRepository
    {
        private PizzaPlaceContext db;
        public PizzaPlaceRepository(PizzaPlaceContext db)
        {
            this.db = db;
        }

        public void BulkInsert(string dataset_type, string file)
        {
            // Convert text to list
            // Remove Header row
            List<string> rows = file.Split('\n').ToList();
            rows.RemoveAt(0);
            switch (dataset_type)
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
                        var ingredientList = values[3].Split(',');
                        foreach (var ing in ingredientList)
                        {
                            var rcp_ing = new RecipeIngredient();
                            var cln_ing = ing.Replace("\"", "");
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
    }
}
