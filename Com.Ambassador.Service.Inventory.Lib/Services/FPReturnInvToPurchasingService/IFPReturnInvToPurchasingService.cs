using Com.Ambassador.Service.Inventory.Lib.Models.FPReturnInvToPurchasingModel;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.FPReturnInvToPurchasingViewModel;
using System;
using System.Collections.Generic;

namespace Com.Ambassador.Service.Inventory.Lib.Services.FPReturnInvToPurchasingService
{
    public interface IFPReturnInvToPurchasingService : IBaseService<FPReturnInvToPurchasing, FPReturnInvToPurchasingViewModel>
    {
        Tuple<List<object>, int, Dictionary<string, string>> Read(int page, int size, string order, string keyword, string filter);
    }
}
