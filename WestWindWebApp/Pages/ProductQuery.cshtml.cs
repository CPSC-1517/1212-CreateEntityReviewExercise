using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Developer add namespaces
using Microsoft.AspNetCore.Mvc.Rendering; // for SelectList
using WestWindSystem.BLL;
using WestWindSystem.Entities;
#endregion

namespace WestWindWebApp.Pages
{
    public class ProductQueryModel : PageModel
    {
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;

        public ProductQueryModel(CategoryServices categoryServices,
            ProductServices productServices,
            SupplierServices supplierServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _supplierServices = supplierServices;
           
        }

        public List<Product> ProductList { get; set; } = new();
        public void OnGet()
        {
            ProductList = _productServices.Product_List();
        }
    }
}
