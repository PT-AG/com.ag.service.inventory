using Com.Ambassador.Service.Inventory.Lib.Services;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.StockTransferNoteViewModel;
using Com.Moonlay.NetCore.Lib.Service;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Service.Inventory.Test.Services
{
  public  class ValidateServiceTest
    {
        [Fact]
        public void Validate_Throws_ServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            StockTransferNoteViewModel viewModel = new StockTransferNoteViewModel();

            ValidateService service = new ValidateService(serviceProvider.Object);
            Assert.Throws<ServiceValidationExeption>(() => service.Validate(viewModel));

        }
    }
}
