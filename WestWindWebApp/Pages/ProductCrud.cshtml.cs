using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

#region Developer add namespaces
using Microsoft.AspNetCore.Mvc.Rendering; // for SelectList
using WestWindSystem.BLL;
using WestWindSystem.Entities;
#endregion

// TODO: Complete the TODO in ProductCrud.cshtml

namespace WestWindWebApp.Pages
{
    public class ProductCrudModel : PageModel
    {
        private readonly CategoryServices _categoryServices;
        private readonly ProductServices _productServices;
        private readonly SupplierServices _supplierServices;

        public ProductCrudModel(CategoryServices categoryServices,
            ProductServices productServices,
            SupplierServices supplierServices)
        {
            _categoryServices = categoryServices;
            _productServices = productServices;
            _supplierServices = supplierServices;
        }

        #region Properties for info and error feedback messages
        [TempData]
        public string FeedbackMessage { get; set; }

        public bool HasFeedback => !string.IsNullOrWhiteSpace(FeedbackMessage) ? true : false;

        [TempData]
        public string ErrorMessage { get; set; }

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage) ? true : false;
        #endregion

        #region The current Product to create, edit/udpate, or delete
        [BindProperty]
        public Product CurrentProduct { get; set; }
        #endregion

        #region Properties to bind to select control using SelectList
        public SelectList CategorySelectList { get; set; }

        public SelectList SupplierSelectList { get; set; }
        #endregion

        #region Properties to bind to select control using list of entity objects
        public List<Category> CategoryList { get; set; }    
        public List<Supplier> SupplierList { get; set; }
        #endregion

        public void PopulateDropDownList()
        {
            // Using a SelectList to bind to a select control with a dictionary
            CategorySelectList = new SelectList(_categoryServices.Category_Dictionary(), "Key", "Value", CurrentProduct.CategoryId);
            SupplierSelectList = new SelectList(_supplierServices.Supplier_Dictionary(), "Key", "Value", CurrentProduct.SupplierId);
            // Alternate method of creating a SelectList using a list of objects
            //CategorySelectList = new SelectList(_categoryServices.Category_List(), "CategoryId", "CategoryName", CurrentProduct.CategoryId);
            //SupplierSelectList = new SelectList(_supplierServices.Supplier_List(), "SupplierId", "CompanyName", CurrentProduct.SupplierId);

            // Using a list of objects to bind to a select control
            CategoryList = _categoryServices.Category_List();
            SupplierList = _supplierServices.Supplier_List();
        }

        public void OnGet()
        {
            CurrentProduct = new Product();
            PopulateDropDownList();
        }

        public IActionResult OnPostAddProduct()
        {
            // Remove the Category and Supplier key from ModelState of the CurrentProduct 
            // so work around issue where the generated entities include navigation properties that are not set yet
            ModelState.Remove("CurrentProduct.Category");
            ModelState.Remove("CurrentProduct.Supplier");

            // TODO: Throw a new Exception if the CurrentProduct.CategoryId was not selected
            //       Throw a new Exception if the CurrentProduct.SupplierId was not selected
            //       If CurrentProduct data is valid then add CurrentProduct to the database
            //       Catch any exceptions thrown and display the error message

            return Page();
        }

        public IActionResult OnPostClear()
        {
            // TODO: Create a new Product and redirect to the same page
            return Page();
        }


        private Exception GetInnerException(Exception ex)
        {
            while (ex.InnerException != null)
                ex = ex.InnerException;
            return ex;
        }
    }
}
