using Com.Ambassador.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels
{
    public class ProductionOrderViewModel
    {
        public string Id { get; set; }
        public string OrderNo { get; set; }
        public double? OrderQuantity { get; set; }
        public bool IsCompleted { get; set; }
        public double? DistributedQuantity { get; set; }
        public OrderTypeViewModel OrderType { get; set; }
    }
}
