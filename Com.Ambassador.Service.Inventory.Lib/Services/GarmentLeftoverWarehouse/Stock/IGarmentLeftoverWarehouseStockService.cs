using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Com.Ambassador.Service.Inventory.Lib.Interfaces;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.Stock;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.Stock;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.Stock
{
    public interface IGarmentLeftoverWarehouseStockService : IMap<GarmentLeftoverWarehouseStock, GarmentLeftoverWarehouseStockViewModel>
    {
        ReadResponse<GarmentLeftoverWarehouseStock> Read(int page, int size, string order, List<string> select, string keyword, string filter);
        ReadResponse<dynamic> ReadDistinct(int page, int size, string order, List<string> select, string keyword, string filter);
        GarmentLeftoverWarehouseStock ReadById(int Id);
        Task<int> StockIn(GarmentLeftoverWarehouseStock stock, string StockReferenceNo, int StockReferenceId, int StockReferenceItemId);
        Task<int> StockOut(GarmentLeftoverWarehouseStock stock, string StockReferenceNo, int StockReferenceId, int StockReferenceItemId);

    }
}
