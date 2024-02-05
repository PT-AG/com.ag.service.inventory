using Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.CustomOut;
using Com.Ambassador.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut;
using Com.Ambassador.Service.Inventory.Lib.Services.GarmentLeftoverWarehouse.CustomsOuts;
using Com.Ambassador.Service.Inventory.Lib.Services;

namespace Com.Ambassador.Service.Inventory.WebApi.Controllers.v1.GarmentLeftoverWarehouse.CustomOut
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/garment/leftover-customs-out")]
    [Authorize]
    public class CustomOutController : BaseController<CustomsOutModel, CustomOutVM, ICustomsOutService>
    {
        public CustomOutController(IIdentityService identityService, IValidateService validateService, ICustomsOutService service) : base(identityService, validateService, service, "1.0.0")
        {
        }
    }
}
