using Com.Ambassador.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.BalanceStockDateMasterViewModel
{
    public class GarmentBalanceStockDateMasterViewModel : BasicViewModel, IValidatableObject
    {
        public DateTimeOffset? BalanceStockDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BalanceStockDate == null || BalanceStockDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal Balance Stok tidak boleh kosong", new List<string> { "BalanceStockDate" });
            }
        }
    }
}
