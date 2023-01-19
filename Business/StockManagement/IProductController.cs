﻿namespace FutureFridges.Business.StockManagement
{
    public interface IProductController
    {
        void CreateProduct (Product newProduct);
        void DeleteProduct (Guid uid);
        List<Product> GetAll ();
        Product GetProduct (Guid product_UID);
        List<Product> GetProducts (List<Guid> uids);
        bool IsProductInUse (Guid uid);
        void UpdateProduct (Product updatedProduct);
    }
}