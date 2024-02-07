using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut
{
    public class ReportCustomsOutVM
    {
        public string BCNo { get; set; }
        public DateTimeOffset BCDate { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string UomUnit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
    }
}
