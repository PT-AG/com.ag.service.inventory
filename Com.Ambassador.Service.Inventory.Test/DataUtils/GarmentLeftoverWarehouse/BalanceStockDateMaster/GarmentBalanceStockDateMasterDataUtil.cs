using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Test.DataUtils.GarmentLeftoverWarehouse.BalanceStockDateMaster
{
    public class GarmentBalanceStockDateMasterDataUtil
    {
        private readonly GarmentBalanceStockDateMasterService Service;

        public GarmentBalanceStockDateMasterDataUtil(GarmentBalanceStockDateMasterService service)
        {
            Service = service;
        }

     

        public GarmentBalanceStockDateMaster GetNewData()
        {
            return new GarmentBalanceStockDateMaster
            {
                BalanceStockDate = DateTimeOffset.Now
                
            };
        }
      
    }
}
