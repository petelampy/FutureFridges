using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.StockManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutureFridges.Pages.StockManagement
{
    public class AddStockItemModel : PageModel
    {
        private const string LOG_INSERT_ITEM = "A {0} was inserted into the fridge";

        private readonly IProductController __ProductController;
        private readonly IStockItemController __StockItemController;
        private readonly IAuditLogController __AuditLogController;

        public AddStockItemModel()
        {
            __ProductController = new ProductController();
            __StockItemController = new StockItemController();
            __AuditLogController = new AuditLogController();
        }

        public void OnGet()
        {
            StockItem = new StockItem();

            CreateProductSelector();
        }

        public IActionResult OnPost()
        {
            ModelState.Clear();
            ValidateModel();
            if (!ModelState.IsValid)
            {
                CreateProductSelector();
                return Page();
            }

            __StockItemController.CreateStockItem(StockItem);

            string _ProductName = __ProductController.GetProduct(StockItem.Product_UID).Name;

            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_INSERT_ITEM, _ProductName), LogType.ItemAdd);

            return RedirectToPage("StockManagement");
        }

        private void CreateProductSelector ()
        {
            List<Product> _Products = __ProductController.GetAll();

            ProductSelection = new List<SelectListItem>();

            ProductSelection.Add(new SelectListItem
            {
                Text = "Please Select",
                Value = Guid.Empty.ToString()
            });
            
            ProductSelection.AddRange(_Products.Select(product =>
                new SelectListItem
                {
                    Text = product.Name,
                    Value = product.UID.ToString()
                }).ToList());
        }

        private void ValidateModel()
        {
            if (StockItem.Product_UID == Guid.Empty)
            {
                ModelState.AddModelError("StockItem.Product_UID", "Please select a product!");
            }

            if (StockItem.ExpiryDate == DateTime.MinValue)
            {
                ModelState.AddModelError("StockItem.ExpiryDate", "Please enter an expiry date!");
            }

            if (StockItem.ExpiryDate < DateTime.Now)
            {
                ModelState.AddModelError("StockItem.ExpiryDate", "Cannot add an expired item!");
            }
        }

        [BindProperty(SupportsGet = true)]
        public StockItem StockItem { get; set; }
        public List<SelectListItem> ProductSelection { get; set; }
    }
}
