using Com.Ambassador.Service.Inventory.Lib;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using Com.Ambassador.Service.Inventory.Lib.Services;
using Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.BalanceStockDateMasterViewModel;
using Com.Ambassador.Service.Inventory.Test.DataUtils.GarmentLeftoverWarehouse.BalanceStockDateMaster;
using Com.Ambassador.Service.Inventory.Test.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Com.Ambassador.Service.Inventory.Test.Services.GarmentLeftoverWarehouse.BalanceStockDateMaster
{
    public class BasicTest
    {
        private const string ENTITY = "GarmentBalanceStockDate";

        [MethodImpl(MethodImplOptions.NoInlining)]
        public string GetCurrentMethod()
        {
            StackTrace st = new StackTrace();
            StackFrame sf = st.GetFrame(1);

            return string.Concat(sf.GetMethod().Name, "_", ENTITY);
        }

        private InventoryDbContext _dbContext(string testName)
        {
            DbContextOptionsBuilder<InventoryDbContext> optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            optionsBuilder
                .UseInMemoryDatabase(testName)
                .ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));

            InventoryDbContext dbContext = new InventoryDbContext(optionsBuilder.Options);

            return dbContext;
        }

        private Mock<IServiceProvider> GetServiceProvider()
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IIdentityService)))
                .Returns(new IdentityService() { Token = "Token", Username = "Test" });

            return serviceProvider;
        }

        [Fact]
        public async Task Read_Success()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();

            

            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

         

            var result = service.Read(1, 25, "{}", null, null, "{}");

            Assert.Empty(result.Data);

        }
        private GarmentBalanceStockDateMasterDataUtil _dataUtil(GarmentBalanceStockDateMasterService service)
        {
            return new GarmentBalanceStockDateMasterDataUtil(service);
        }


        [Fact]
        public async Task Create_Success()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();
            
            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

            var data = _dataUtil(service).GetNewData();

            var result = await service.CreateAsync(data);

            Assert.NotEqual(0, result);
        }
        [Fact]
        public async Task ReadByIdAsync()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

            var data = _dataUtil(service).GetNewData();
             
            await Assert.ThrowsAnyAsync<NotImplementedException>(() => service.ReadByIdAsync(data.Id));
        }

        [Fact]
        public async Task UpdateAsync()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

            var data = _dataUtil(service).GetNewData();

            await Assert.ThrowsAnyAsync<NotImplementedException>(() => service.UpdateAsync(data.Id,data));
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();

            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

            var data = _dataUtil(service).GetNewData();

            await Assert.ThrowsAnyAsync<NotImplementedException>(() => service.DeleteAsync(data.Id));
        }

        [Fact]
        public void MapToModel()
        {
            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), GetServiceProvider().Object);

            var data = new GarmentBalanceStockDateMasterViewModel
            {
                BalanceStockDate = DateTimeOffset.Now,
                 
            };

            var result = service.MapToModel(data);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task MapToViewModel()
        {
            var serviceProvider = GetServiceProvider();

            var stockServiceMock = new Mock<IGarmentBalanceStockDateMasterService>();
           
            serviceProvider
                .Setup(x => x.GetService(typeof(IGarmentBalanceStockDateMasterService)))
                .Returns(stockServiceMock.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IHttpService)))
                .Returns(new HttpTestService());

            GarmentBalanceStockDateMasterService service = new GarmentBalanceStockDateMasterService(_dbContext(GetCurrentMethod()), serviceProvider.Object);

            var data =   _dataUtil(service).GetNewData();

            var result = service.MapToViewModel(data);

            Assert.NotNull(result);
        }

        [Fact]
        public void ValidateViewModel()
        {
            GarmentBalanceStockDateMasterViewModel viewModel = new GarmentBalanceStockDateMasterViewModel()
            {
                BalanceStockDate = null 
            };
            var result = viewModel.Validate(null);
            Assert.True(result.Count() >0);

           
        }
    }
}
