using FutureFridges.Business.AuditLog;
using FutureFridges.Business.Enums;
using FutureFridges.Business.OrderManagement;
using FutureFridges.Business.StockManagement;
using FutureFridges.Business.UserManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace FutureFridges.Pages.ProductManagement
{
    [Authorize]
    public class CreateEditProductModel : PageModel
    {
        private const string ACCESS_ERROR_PAGE_PATH = "../Account/AccessError"; //MOVE PAGE PATHS INTO A GLOBAL RESX FILE??
        private const string LOG_ENTRY_FORMAT_CATEGORY_UPDATE = "{0}'s category was changed from {1} to {2}";
        private const string LOG_ENTRY_FORMAT_STOCK_LEVEL_UPDATE = "{0}'s minimum stock level was changed from {1} to {2}";
        private const string LOG_ENTRY_FORMAT_CREATE = "{0} supplied by {1} was created";
        private const string LOG_ENTRY_FORMAT_DAYS_UPDATE = "{0}'s shelf life was changed from {1} to {2}";
        private const string LOG_ENTRY_FORMAT_RENAME = "{0} was renamed to {1}";
        private const string PRODUCT_IMAGES_PATH = "wwwroot/Images/Products";
        private const string IMAGES_PATH = "wwwroot/Images";

        private readonly IAuditLogController __AuditLogController;
        private readonly IWebHostEnvironment __Environment;
        private readonly IProductController __ProductController;
        private readonly ISupplierController __SupplierController;
        private readonly IUserPermissionController __UserPermissionController;

        public CreateEditProductModel (IWebHostEnvironment environment)
        {
            __ProductController = new ProductController();
            __UserPermissionController = new UserPermissionController();
            __SupplierController = new SupplierController();
            __AuditLogController = new AuditLogController();
            __Environment = environment;
        }

        private void CreateAuditLogs ()
        {
            if (UID == Guid.Empty)
            {
                string? _SupplierName = __SupplierController.Get(Product.Supplier_UID).Name;
                __AuditLogController.Create(User.Identity.Name, string.Format(LOG_ENTRY_FORMAT_CREATE, Product.Name, _SupplierName), LogType.ProductCreate);
            }
            else
            {
                Product _CurrentProduct = __ProductController.GetProduct(Product.UID);

                if (_CurrentProduct.DaysShelfLife != Product.DaysShelfLife)
                {
                    __AuditLogController.Create(
                        User.Identity.Name,
                        string.Format(LOG_ENTRY_FORMAT_DAYS_UPDATE, _CurrentProduct.Name, _CurrentProduct.DaysShelfLife, Product.DaysShelfLife),
                        LogType.ProductUpdate
                        );
                }

                if (_CurrentProduct.Category != Product.Category)
                {
                    __AuditLogController.Create(
                        User.Identity.Name,
                        string.Format(LOG_ENTRY_FORMAT_CATEGORY_UPDATE, _CurrentProduct.Name, _CurrentProduct.Category, Product.Category),
                        LogType.ProductUpdate
                        );
                }

                if (_CurrentProduct.Name != Product.Name)
                {
                    __AuditLogController.Create(
                        User.Identity.Name,
                        string.Format(LOG_ENTRY_FORMAT_RENAME, _CurrentProduct.Name, Product.Name),
                        LogType.ProductUpdate
                        );
                }
                if (_CurrentProduct.MinimumStockLevel != Product.MinimumStockLevel)
                {
                    __AuditLogController.Create(
                        User.Identity.Name,
                        string.Format(LOG_ENTRY_FORMAT_STOCK_LEVEL_UPDATE, _CurrentProduct.Name, _CurrentProduct.MinimumStockLevel, Product.MinimumStockLevel),
                        LogType.ProductUpdate
                        );
                }
            }
        }

        private void CreateProductImageFile (IFormFile file)
        {
            string _FileType = file.FileName.Split(".")[1];
            string _ProductFileName = Product.Name + "." + _FileType;

            string _FilePath = Path.Combine(__Environment.ContentRootPath, PRODUCT_IMAGES_PATH, _ProductFileName);
            using (FileStream _FileStream = new FileStream(_FilePath, FileMode.Create))
            {
                file.CopyTo(_FileStream);
            }

            Product.ImageName = _ProductFileName;
        }

        private void CreateSupplierSelector ()
        {
            List<Supplier> _Suppliers = __SupplierController.GetAll();

            SupplierSelection = _Suppliers.Select(supplier =>
                new SelectListItem
                {
                    Text = supplier.Name,
                    Value = supplier.UID.ToString(),
                    Selected = UID != Guid.Empty && Product.Supplier_UID.Equals(UID)
                }).ToList();
        }

        public IActionResult OnGet ()
        {
            string _CurrentUserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserPermissions _CurrentUserPermissions = __UserPermissionController.GetPermissions(new Guid(_CurrentUserID));

            if (_CurrentUserPermissions.ManageProduct)
            {
                if (UID != Guid.Empty)
                {
                    Product = __ProductController.GetProduct(UID);

                }
                else
                {
                    Product = new Product();
                    Product.DaysShelfLife = 1;
                    Product.MinimumStockLevel = 1;
                }

                CreateSupplierSelector();

                return Page();
            }
            else
            {
                return RedirectToPage(ACCESS_ERROR_PAGE_PATH);
            }


        }

        public IActionResult OnPost ()
        {
            ValidateModel();
            if (ModelState.ErrorCount > 0)
            {
                CreateSupplierSelector();
                return Page();
            }

            CreateAuditLogs();

            if (UID != Guid.Empty)
            {
                if (Product.ImageName != null)
                {
                    RenameImageFile(Product.ImageName, Product.Name);
                }
                if (FileUpload != null)
                {
                    CreateProductImageFile(FileUpload);
                }

                __ProductController.UpdateProduct(Product);
            }
            else
            {
                if (FileUpload != null)
                {
                    CreateProductImageFile(FileUpload);
                }
                else
                {
                    LoadSampleImage(Product.Name);
                }

                __ProductController.CreateProduct(Product);
            }

            return RedirectToPage("ProductManagement");
        }

        private void RenameImageFile (string oldFilePath, string newProductName)
        {
            string _FilePath = Path.Combine(__Environment.ContentRootPath, PRODUCT_IMAGES_PATH, oldFilePath);
            string _FileType = oldFilePath.Split(".")[1];
            string _NewProductFileName = newProductName + "." + _FileType;
            string _NewFilePath = Path.Combine(__Environment.ContentRootPath, PRODUCT_IMAGES_PATH, _NewProductFileName);

            System.IO.File.Move(_FilePath, _NewFilePath);
            Product.ImageName = _NewProductFileName;
        }

        private void LoadSampleImage (string productName)
        {
            string _FilePath = Path.Combine(__Environment.ContentRootPath, IMAGES_PATH, "SampleProduct.png");
            string _NewProductFileName = productName + ".png";
            string _NewFilePath = Path.Combine(__Environment.ContentRootPath, PRODUCT_IMAGES_PATH, _NewProductFileName);

            System.IO.File.Copy(_FilePath, _NewFilePath, true);
            Product.ImageName = _NewProductFileName;
        }

        private void ValidateModel ()
        {
            List<string?> _NamesInUse = __ProductController.GetAll().Select(product => product.Name).ToList();
            Product _CurrentProduct = __ProductController.GetProduct(Product.UID);
            _CurrentProduct.Name = _CurrentProduct.Name == null ? string.Empty : _CurrentProduct.Name;

            if (_NamesInUse.Contains(Product.Name) && _CurrentProduct.Name != Product.Name)
            {
                ModelState.AddModelError("Product.Name", "Product Name in use!");
            }

            if (Product.Name.IsNullOrEmpty())
            {
                ModelState.AddModelError("Product.Name", "Product name is required!");
            }

            if (Product.MinimumStockLevel < 1)
            {
                ModelState.AddModelError("Product.MinimumStockLevel", "Minimum Stock Level cannot be less than 1!");
            }
            
            if (Product.DaysShelfLife < 1)
            {
                ModelState.AddModelError("Product.DaysShelfLife", "Shelf Life cannot be less than 1 day!");
            }
        }

        [BindProperty]
        public IFormFile? FileUpload { get; set; }

        [BindProperty]
        public Product Product { get; set; }

        public List<SelectListItem> SupplierSelection { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid UID { get; set; }
    }
}
