using System;
using Com.Ambassador.Service.Inventory.Lib.Helpers;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentReceiptSubconWasteProductionViewModel.ExpenditureWaste
{
    public class GarmentSubconExpenditureWasteProductionItemViewModel : BasicViewModel
    {
        public string GarmentReceiptWasteNo { get; set; }
        public int GarmentReceiptWasteId { get; set; }
        public double Quantity { get; set; }
        
    }
}
