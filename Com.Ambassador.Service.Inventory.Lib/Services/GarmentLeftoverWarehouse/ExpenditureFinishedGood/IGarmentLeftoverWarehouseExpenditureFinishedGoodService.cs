using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.ExpenditureFinishedGood;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.ExpenditureFinishedGood;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.ExpenditureFinishedGood
{
    public interface IGarmentLeftoverWarehouseExpenditureFinishedGoodService : IBaseService<GarmentLeftoverWarehouseExpenditureFinishedGood, GarmentLeftoverWarehouseExpenditureFinishedGoodViewModel>
    {
        MemoryStream GenerateExcel(DateTime? dateFrom, DateTime? dateTo, int offset);
        Tuple<List<ExpenditureFInishedGoodReportViewModel>, int> GetReport(DateTime? dateFrom, DateTime? dateTo, int page, int size, string order, int offset);
    }
}
