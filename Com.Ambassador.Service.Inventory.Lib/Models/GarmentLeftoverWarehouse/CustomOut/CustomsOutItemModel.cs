using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.CustomOut
{
    public class CustomsOutItemModel : StandardEntity
    {
        public int CustomsOutId { get; set; }
        public int AvalExpenditureId { get; set; }
        [MaxLength(20)]
        public string AvalExpenditureNo { get; set; }
        [MaxLength(20)]
        public string ProductName { get; set; }
        public long UomId { get; set; }
        [MaxLength(10)]
        public string UomUnit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public virtual CustomsOutModel CustomsOut { get; set; }
    }
}
