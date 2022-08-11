using Com.Ambassador.Service.Inventory.Lib.Models.FpReturnFromBuyers;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.FpReturnFromBuyers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Lib.Services.FpReturnFromBuyers
{
    public interface IFpReturnFromBuyerService : IBaseService<FpReturnFromBuyerModel, FpReturnFromBuyerViewModel>
    {
        Task<int> VoidDocumentById(int id);
    }
}
