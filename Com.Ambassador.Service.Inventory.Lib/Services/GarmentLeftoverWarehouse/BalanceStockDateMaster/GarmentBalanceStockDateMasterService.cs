using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.BalanceStockDateMasterViewModel;
using Com.Moonlay.Models;
using Com.Moonlay.NetCore.Lib;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.BalanceStockDateMaster
{
    public class GarmentBalanceStockDateMasterService : IGarmentBalanceStockDateMasterService
    {
        private const string UserAgent = "GarmentBalanceStockDateMasterService";

        private InventoryDbContext DbContext;
        private DbSet<GarmentBalanceStockDateMaster> DbSet;
        private readonly IServiceProvider ServiceProvider;
        private readonly IIdentityService IdentityService;

       
        public GarmentBalanceStockDateMasterService(InventoryDbContext dbContext, IServiceProvider serviceProvider)
        {
            DbContext = dbContext;
            DbSet = DbContext.Set<GarmentBalanceStockDateMaster>();

            ServiceProvider = serviceProvider;
            IdentityService = (IIdentityService)serviceProvider.GetService(typeof(IIdentityService));

     }

        public async Task<int> CreateAsync(GarmentBalanceStockDateMaster model)
        {
            using (var transaction = DbContext.Database.CurrentTransaction ?? DbContext.Database.BeginTransaction())
            {
                try
                {
                    int Created = 0;

                    model.FlagForCreate(IdentityService.Username, UserAgent);
                    model.FlagForUpdate(IdentityService.Username, UserAgent);

                   
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

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public GarmentBalanceStockDateMaster MapToModel(GarmentBalanceStockDateMasterViewModel viewModel)
        {
            GarmentBalanceStockDateMaster model = new GarmentBalanceStockDateMaster();
            PropertyCopier<GarmentBalanceStockDateMasterViewModel, GarmentBalanceStockDateMaster>.Copy(viewModel, model);

        
            return model;
        }

        public GarmentBalanceStockDateMasterViewModel MapToViewModel(GarmentBalanceStockDateMaster model)
        {
            GarmentBalanceStockDateMasterViewModel viewModel = new GarmentBalanceStockDateMasterViewModel();
            PropertyCopier<GarmentBalanceStockDateMaster, GarmentBalanceStockDateMasterViewModel>.Copy(model, viewModel);

          
            return viewModel;
        }

        public ReadResponse<GarmentBalanceStockDateMaster> Read(int page, int size, string order, List<string> select, string keyword, string filter)
        {
            IQueryable<GarmentBalanceStockDateMaster> Query = DbSet;

            List<string> SearchAttributes = new List<string>()
            {
                 "_CreatedBy"
            };
            Query = QueryHelper<GarmentBalanceStockDateMaster>.Search(Query, SearchAttributes, keyword);

            Dictionary<string, object> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(filter);
            Query = QueryHelper<GarmentBalanceStockDateMaster>.Filter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(order);
            Query = QueryHelper<GarmentBalanceStockDateMaster>.Order(Query, OrderDictionary);

            List<string> SelectedFields = (select != null && select.Count > 0) ? select : new List<string>()
            {
                "Id",  "BalanceStockDate", "_CreatedBy"
            };

            Query = Query.Select(s => new GarmentBalanceStockDateMaster
            {
                Id = s.Id,
                BalanceStockDate = s.BalanceStockDate,
                _CreatedBy = s._CreatedBy
            }).OrderByDescending(s => s.BalanceStockDate);

            Pageable<GarmentBalanceStockDateMaster> pageable = new Pageable<GarmentBalanceStockDateMaster>(Query, page - 1, size);
            List<GarmentBalanceStockDateMaster> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return new ReadResponse<GarmentBalanceStockDateMaster>(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public async Task<GarmentBalanceStockDateMaster> ReadByIdAsync(int id)
        {
            return await DbSet
                .Where(w => w.Id == id)

                .FirstOrDefaultAsync();
        }

        public Task<int> UpdateAsync(int id, GarmentBalanceStockDateMaster model)
        {
            throw new NotImplementedException();
        }
    }
}