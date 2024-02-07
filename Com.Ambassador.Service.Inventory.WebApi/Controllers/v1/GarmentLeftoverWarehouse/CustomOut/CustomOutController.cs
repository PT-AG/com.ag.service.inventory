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

        [HttpGet("report")]
        public async Task< IActionResult> GetReportAll(DateTime dateFrom, DateTime dateTo)
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];

            try
            {

                var data = await Service.GetQuery(dateFrom,dateTo);

                return Ok(new
                {
                    apiVersion = ApiVersion,
                    data = data,
                    info = new { total = data.Count() },
                    message = General.OK_MESSAGE,
                    statusCode = General.OK_STATUS_CODE
                });
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("report/download")]
        public async Task <IActionResult> GetXlsAll(DateTime dateFrom, DateTime dateTo)
        {

            try
            {
                byte[] xlsInBytes;
                int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);

                var xls = await Service.GenerateExcel(dateFrom, dateTo);

                string filename = String.Format("LAPORAN PENYELESAIAN WASTE / SCRAP - {0}  - {1}.xlsx", dateFrom.ToString("ddMMyyyy"), dateTo.ToString("ddMMyyyy"));

                xlsInBytes = xls.ToArray();
                var file = File(xlsInBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", filename);
                return file;

            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
