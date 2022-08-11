using Com.Ambassador.Service.Inventory.Lib;
using Com.Ambassador.Service.Inventory.Lib.Services.StockTransferNoteService;
using Com.Ambassador.Service.Inventory.Test.Helpers;
using Model = Com.Ambassador.Service.Inventory.Lib.Models.StockTransferNoteModel;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.StockTransferNoteViewModel;
using Com.Ambassador.Service.Inventory.Test.DataUtils.StockTransferNoteDataUtil;
using Com.Ambassador.Service.Inventory.WebApi.Controllers.v1;
using Moq;
using System.Net;

namespace Com.Ambassador.Service.Inventory.Test.Controllers.StockTransferNote
{
    public class StockTransferNoteBasicTest : BaseControllerTest<StockTransferNoteController, Model.StockTransferNote, StockTransferNoteViewModel, IStockTransferNoteService>
    {
        [Fact]
        public async void Approve_WithoutException_ReturnNoContent()
        {
            var mocks = GetMocks();
            mocks.Service.Setup(f => f.UpdateIsApprove(It.IsAny<int>())).ReturnsAsync(true);
            
            var response = await GetController(mocks).Approve(1);
            Assert.Equal((int)HttpStatusCode.NoContent, GetStatusCode(response));
        }

        [Fact]
        public async void Approve_WithoutException_InternalServer()
        {
            var mocks = GetMocks();
            mocks.Service.Setup(f => f.UpdateIsApprove(It.IsAny<int>())).ReturnsAsync(false);

            var response = await GetController(mocks).Approve(1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public async void Approve_WithException_InternalServer()
        {
            var mocks = GetMocks();
            mocks.Service.Setup(f => f.UpdateIsApprove(It.IsAny<int>())).ThrowsAsync(new Exception());

            var response = await GetController(mocks).Approve(1);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetByUser_WithoutException_ReturnOk()
        {
            var mocks = GetMocks();
            mocks.Service.Setup(f => f.ReadModelByNotUser(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Tuple<List<Model.StockTransferNote>, int, Dictionary<string, string>, List<string>>(new List<Model.StockTransferNote>(), 1, new Dictionary<string, string>(), new List<string>()));
            mocks.Service.Setup(f => f.MapToViewModel(It.IsAny<Model.StockTransferNote>())).Returns(ViewModel);
            var response = GetController(mocks).GetByUser();
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
        }

        [Fact]
        public void GetByUser_WithException_InternalServer()
        {
            var mocks = GetMocks();
            mocks.Service.Setup(f => f.ReadModelByNotUser(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            mocks.Service.Setup(f => f.MapToViewModel(It.IsAny<Model.StockTransferNote>())).Returns(ViewModel);
            var response = GetController(mocks).GetByUser();
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }
    }
}
