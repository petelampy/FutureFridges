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
            __StockItemController.CreateStockItem(StockItem);

            string _ProductName = __ProductController.GetProduct(StockItem.Product_UID).Name;

            __AuditLogController.Create(User.Identity.Name, string.Format(LOG_INSERT_ITEM, _ProductName), LogType.ItemAdd);

            return RedirectToPage("StockManagement");
        }

        private void CreateProductSelector ()
        {
            List<Product> _Products = __ProductController.GetAll();

            ProductSelection = _Products.Select(product =>
                new SelectListItem
                {
                    Text = product.Name,
                    Value = product.UID.ToString()
                }).ToList();
        }

        [BindProperty(SupportsGet = true)]
        public StockItem StockItem { get; set; }
        public List<SelectListItem> ProductSelection { get; set; }
    }
}
