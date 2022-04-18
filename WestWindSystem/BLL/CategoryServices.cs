using WestWindSystem.DAL; // for WestWindContext
using WestWindSystem.Entities; // for Category

using Microsoft.EntityFrameworkCore;    // for extensions methods 

namespace WestWindSystem.BLL
{
    public class CategoryServices
    {
        // Step 1: Define a readonly data field for the custom DbContext class
        // and use constructor injection to set the value of the data field
        private readonly WestWindContext _dbContext;
        internal CategoryServices(WestWindContext context)
        {
            _dbContext = context;
        }

        // Step 2: Define methods for entity
        public List<Category> Category_List()
        {
            var query = _dbContext
                .Categories
                .OrderBy(item => item.CategoryName);
            return query.ToList();
        }

        public Dictionary<int, string> Category_Dictionary()
        {
            var query = _dbContext
                .Categories
                .OrderBy(item => item.CategoryName)
                .Select(item => new { item.CategoryId, item.CategoryName });
            return query.ToDictionary(item => item.CategoryId, item => item.CategoryName);
        }

        public Category? Category_GetById(int categoryId)
        {
            var query = _dbContext
                .Categories
                .Where(item => item.CategoryId == categoryId);
            return query.FirstOrDefault();
        }

 
    }
}
