namespace PizzaPlaceAPI.DB
{
    public interface IPizzaPlaceRepository
    {
        public void BulkInsert(string table, string file);
    }
}
