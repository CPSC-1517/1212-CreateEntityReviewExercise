using WestWindSystem.DAL; // for WestWindContext
using WestWindSystem.Entities; // for Category

using Microsoft.EntityFrameworkCore;    // for extensions methods 

namespace WestWindSystem.BLL
{
    public class SupplierServices
    {
        // Step 1: Define a readonly data field for the custom DbContext class
        // and use constructor injection to set the value of the data field
        private readonly WestWindContext _dbContext;
        internal SupplierServices(WestWindContext context)
        {
            _dbContext = context;
        }

        // Step 2: Define methods for entity
        public List<Supplier> Supplier_List()
        {
            var query = _dbContext
                .Suppliers
                .OrderBy(item => item.CompanyName);
            return query.ToList();
        }

        public Dictionary<int, string> Supplier_Dictionary()
        {
            var query = _dbContext
                .Suppliers
                .OrderBy(item => item.CompanyName)
                .Select(item => new { item.SupplierId, item.CompanyName, item.ContactName, item.ContactTitle});
            return query.ToDictionary(item => item.SupplierId, 
                item => item.CompanyName + " (" + item.ContactName + "," + item.ContactTitle + ")");
        }

        public Supplier? Supplier_GetById(int supplierId)
        {
            var query = _dbContext
                .Suppliers
                .Where(item => item.SupplierId == supplierId);
            return query.FirstOrDefault();
        }

 
    }
}
