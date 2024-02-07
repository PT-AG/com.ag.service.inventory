using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.CustomOut;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.CustomsOuts
{
    public interface ICustomsOutService : IBaseService<CustomsOutModel, CustomOutVM>
    {
        Task<MemoryStream> GenerateExcel(DateTime dateFrom, DateTime dateTo);
        Task<List<ReportCustomsOutVM>> GetQuery(DateTime dateFrom, DateTime dateTo);
    }
}
