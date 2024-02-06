using Com.Ambassador.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut
{
    public class CustomOutItemVM : BasicViewModel
    {
        public int CustomsOutId { get; set; }
        public int AvalExpenditureId { get; set; }
        public string AvalExpenditureNo { get; set; }
        public string ProductName { get; set; }
        public UomViewModel Uom { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
