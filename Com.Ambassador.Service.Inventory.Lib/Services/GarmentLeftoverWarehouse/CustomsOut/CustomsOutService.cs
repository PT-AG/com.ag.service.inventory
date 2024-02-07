using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.CustomOut;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.ExpenditureAval;
using Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.CustomsOuts;
using Com.Ambassador.Service.Inventory.Lib.ViewModels;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.CustomsOut
{
    public class CustomsOutService : ICustomsOutService
    {
        private const string UserAgent = "CustomsOutService";
        private InventoryDbContext DbContext;
        private DbSet<CustomsOutModel> DbSet;
        private DbSet<CustomsOutItemModel> DbSetItem;
        private DbSet<GarmentLeftoverWarehouseExpenditureAval> garmentLeftoverWarehouseExpenditureAvals;

        private readonly IServiceProvider ServiceProvider;
        private readonly IIdentityService IdentityService;
        public CustomsOutService(InventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<CustomsOutModel>();
            DbSetItem = DbContext.Set<CustomsOutItemModel>();

            garmentLeftoverWarehouseExpenditureAvals = DbContext.Set<GarmentLeftoverWarehouseExpenditureAval>();

            ServiceProvider = serviceProvider;
            IdentityService = (IIdentityService)serviceProvider.GetService(typeof(IIdentityService));
        }

        public CustomsOutModel MapToModel(CustomOutVM viewModel)
        {
            CustomsOutModel model = new CustomsOutModel();
            PropertyCopier<CustomOutVM, CustomsOutModel>.Copy(viewModel, model);

            model.Items = new List<CustomsOutItemModel>();
            foreach (var viewModelItem in viewModel.Items)
            {
                CustomsOutItemModel modelItem = new CustomsOutItemModel();
                PropertyCopier<CustomOutItemVM, CustomsOutItemModel>.Copy(viewModelItem, modelItem);

                if (viewModelItem.Uom != null)
                {
                    modelItem.UomId = long.Parse(viewModelItem.Uom.Id);
                    modelItem.UomUnit = viewModelItem.Uom.Unit;
                }

                model.Items.Add(modelItem);
            }
            return model;
        }

        public CustomOutVM MapToViewModel(CustomsOutModel model)
        {
            CustomOutVM viewModel = new CustomOutVM();
            PropertyCopier<CustomsOutModel, CustomOutVM>.Copy(model, viewModel);

            viewModel.Items = new List<CustomOutItemVM>();
            foreach (var modelItem in model.Items)
            {
                CustomOutItemVM viewModelItem = new CustomOutItemVM();
                PropertyCopier<CustomsOutItemModel, CustomOutItemVM>.Copy(modelItem, viewModelItem);

                viewModelItem.Uom = new UomViewModel
                {
                    Id = modelItem.UomId.ToString(),
                    Unit = modelItem.UomUnit
                };

                viewModel.Items.Add(viewModelItem);
            }
            return viewModel;
        }

        public async Task<int> CreateAsync(CustomsOutModel model)
        {
            using (var transaction = DbContext.Database.CurrentTransaction ?? DbContext.Database.BeginTransaction())
            {
                try
                {
                    int Created = 0;

                    model.FlagForCreate(IdentityService.Username, UserAgent);
                    model.FlagForUpdate(IdentityService.Username, UserAgent);
                    foreach (var item in model.Items)
                    {
                        var expenditureAval = garmentLeftoverWarehouseExpenditureAvals.FirstOrDefault(x => x.Id == item.AvalExpenditureId);
                        expenditureAval.SetIsBC(true, IdentityService.Username, UserAgent);

                        item.FlagForCreate(IdentityService.Username, UserAgent);
                        item.FlagForUpdate(IdentityService.Username, UserAgent);
                    }
                    DbSet.Add(model);
                    Created = await DbContext.SaveChangesAsync();
                    transaction.Commit();

                    return Created;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public ReadResponse<CustomsOutModel> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<CustomsOutModel> Query = DbSet.Include(x => x.Items);

            List<string> SearchAttributes = new List<string>()
            {
                 "BCNo"
            };
            Query = QueryHelper<CustomsOutModel>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<CustomsOutModel>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<CustomsOutModel>.Order(Query, OrderDictionary);

            List<string> SelectedFields = (select != null && select.Count > 0) ? select : new List<string>()
            {
                "Id", "BCNo", "BCDate", "BCType", "Items"
            };

            Query = Query.Select(s => new CustomsOutModel
            {
                Id = s.Id,
                BCNo = s.BCNo,
                BCDate = s.BCDate,
                BCType = s.BCType,
                Items = s.Items
            });

            Pageable<CustomsOutModel> pageable = new Pageable<CustomsOutModel>(Query, page - 1, size);
            List<CustomsOutModel> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return new ReadResponse<CustomsOutModel>(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public async Task<CustomsOutModel> ReadByIdAsync(int id)
        {
            return await DbSet
                 .Where(w => w.Id == id)
                 .Include(i => i.Items)
                 .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(int id, CustomsOutModel model)
        {
            using (var transaction = DbContext.Database.CurrentTransaction ?? DbContext.Database.BeginTransaction())
            {
                try
                {
                    int Updated = 0;

                    CustomsOutModel existingModel = await ReadByIdAsync(id);
                    if (existingModel.BCNo != model.BCNo)
                    {
                        existingModel.BCNo = model.BCNo;
                    }
                    if (existingModel.BCDate != model.BCDate)
                    {
                        existingModel.BCDate = model.BCDate;
                    }
                    if (existingModel.Remark != model.Remark)
                    {
                        existingModel.Remark = model.Remark;
                    }
                    existingModel.FlagForUpdate(IdentityService.Username, UserAgent);

                    foreach (var existingItem in existingModel.Items)
                    {
                        var item = model.Items.FirstOrDefault(i => i.Id == existingItem.Id);
                        if (item == null)
                        {
                            var expenditureAval = garmentLeftoverWarehouseExpenditureAvals.FirstOrDefault(x => x.Id == existingItem.AvalExpenditureId);
                            expenditureAval.SetIsBC(false, IdentityService.Username, UserAgent);

                            existingItem.FlagForDelete(IdentityService.Username, UserAgent);
                        }
                        else
                        {
                            if (existingItem.Price != item.Price)
                            {
                                existingItem.Price = item.Price;
                            }
                            existingItem.FlagForUpdate(IdentityService.Username, UserAgent);
                        }
                    }

                    foreach (var item in model.Items.Where(i => i.Id == 0))
                    {
                        var expenditureAval = garmentLeftoverWarehouseExpenditureAvals.FirstOrDefault(x => x.Id == item.AvalExpenditureId);
                        expenditureAval.SetIsBC(true, IdentityService.Username, UserAgent);


                        item.FlagForCreate(IdentityService.Username, UserAgent);
                        item.FlagForUpdate(IdentityService.Username, UserAgent);
                        existingModel.Items.Add(item);
                    }

                    Updated = await DbContext.SaveChangesAsync();
                    transaction.Commit();

                    return Updated;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            using (var transaction = DbContext.Database.CurrentTransaction ?? DbContext.Database.BeginTransaction())
            {
                try
                {
                    int Deleted = 0;

                    CustomsOutModel model = await ReadByIdAsync(id);
                    model.FlagForDelete(IdentityService.Username, UserAgent);
                    foreach (var item in model.Items)
                    {
                        var expenditureAval = garmentLeftoverWarehouseExpenditureAvals.FirstOrDefault(x => x.Id == item.AvalExpenditureId);
                        expenditureAval.SetIsBC(false, IdentityService.Username, UserAgent);

                        item.FlagForDelete(IdentityService.Username, UserAgent);
                    }

                    Deleted = await DbContext.SaveChangesAsync();

                    transaction.Commit();

                    return Deleted;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
            }
        }

        public async Task<List<ReportCustomsOutVM>>GetQuery(DateTime dateFrom,DateTime dateTo)
        {
            var Query = (from a in DbSet
                         join b in DbSetItem on a.Id equals b.CustomsOutId
                         where a._IsDeleted == false
                         && a.BCDate.AddHours(7).Date >= dateFrom.Date
                         && a.BCDate.AddHours(7).Date <= dateTo.Date
                         select new ReportCustomsOutVM
                         {
                             BCNo = a.BCNo,
                             BCDate = a.BCDate,
                             ProductCode = b.ProductName,
                             ProductName = b.ProductName == "AV001" ? "AVAL FABRIC" : b.ProductName == "AV002" ? "AVAL KOMPONEN" : "AVAL BAHAN PENOLONG",
                             UomUnit = b.UomUnit,
                             Quantity = b.Quantity,
                             Price = b.Price
                         }).Distinct().OrderBy(x => x.BCDate).ToList();


            return Query;
        }

        public async Task<MemoryStream> GenerateExcel(DateTime dateFrom, DateTime dateTo)
        {
            var Query = await GetQuery(dateFrom, dateTo);

            DataTable result = new DataTable();
            result.Columns.Add(new DataColumn() { ColumnName = "No", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nomor", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Tanggal", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Kode Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nama Barang", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Satuan", DataType = typeof(String) });
            result.Columns.Add(new DataColumn() { ColumnName = "Jumlah", DataType = typeof(double) });
            result.Columns.Add(new DataColumn() { ColumnName = "Nilai", DataType = typeof(double) });

            if (Query.ToArray().Count() == 0)
            {
                result.Rows.Add("", "", "", "", "", "", 0, 0); // to allow column name to be generated properly for empty data as template
            }
            else
            {
                int i = 0;
                foreach (var item in Query)
                {
                    i++;
                    var bcDate = item.BCDate.ToString("dd MMM yyyy");
                    result.Rows.Add(i.ToString(), item.BCNo, bcDate, item.ProductCode, item.ProductName, item.UomUnit, item.Quantity, item.Price);
                }
            }
            return Excel.CreateExcel(new List<KeyValuePair<DataTable, string>>() { new KeyValuePair<DataTable, string>(result, "Territory") }, true);

        }
    }
}
