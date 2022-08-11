using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Com.Ambassador.Service.Inventory.Lib.Interfaces;
using Com.Ambassador.Service.Inventory.Lib.Models.InventoryModel;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.InventoryViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.Inventory
{
    public interface IInventoryMovementService : IMap<InventoryMovement, InventoryMovementViewModel>
    {
        Task<int> Create(InventoryMovement model);
        ReadResponse<InventoryMovement> Read(int page, int size, string order, string keyword, string filter);
        InventoryMovement ReadModelById(int id);
        Tuple<List<InventoryMovementViewModel>, int> GetReport(string storageCode, string productCode, string type, DateTime? dateFrom, DateTime? dateTo, int page, int size, string Order, int offset);
        MemoryStream GenerateExcel(string storageCode, string productCode, string type, DateTime? dateFrom, DateTime? dateTo, int offset);
        Task<int> RefreshInventoryMovement();
    }
}
