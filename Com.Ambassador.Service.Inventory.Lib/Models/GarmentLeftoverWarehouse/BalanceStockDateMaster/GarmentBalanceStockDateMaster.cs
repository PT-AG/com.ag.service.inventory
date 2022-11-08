using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.BalanceStockDateMaster
{
    public class GarmentBalanceStockDateMaster : StandardEntity, IValidatableObject
    {
        public DateTimeOffset BalanceStockDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
