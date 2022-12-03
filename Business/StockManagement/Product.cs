﻿using FutureFridges.Business.Enums;

namespace FutureFridges.Business.StockManagement
{
    public class Product
    {
        public int Id { get; set; }
        public StockCategory Category { get; set; }
        public string? Name { get; set; }
        public Guid Product_UID { get; set; }
        //MAYBE RENAME THE UIDS TO JUST BE "UID"??
    }
}