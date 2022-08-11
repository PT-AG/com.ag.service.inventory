using Com.Ambassador.Service.Inventory.Lib;
using Com.Ambassador.Service.Inventory.Lib.Models;
using Com.Ambassador.Service.Inventory.Lib.Services;
using Com.Ambassador.Service.Inventory.Lib.ViewModels;
using Com.Ambassador.Service.Inventory.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Com.Ambassador.Service.Inventory.Lib.PDFTemplates;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Com.Ambassador.Service.Inventory.Lib.Services.FpRegradingResultDocs;

namespace Com.Ambassador.Service.Inventory.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/FpRegradingResultDocs")]
    [Authorize]
    public class FpRegradingResultDocsController : BaseController<FpRegradingResultDocs, FpRegradingResultDocsViewModel, IFpRegradingResultDocsService>
    {
        public FpRegradingResultDocsController(IIdentityService identityService, IValidateService validateService, IFpRegradingResultDocsService service) : base(identityService, validateService, service, "1.0.0")
        {
        }

        [HttpGet("pdf/{Id}")]
        public async Task<IActionResult> GetPdfById([FromRoute] int Id)
        {
            try
            {
                var model = await Service.ReadByIdAsync(Id);
                var viewModel = Service.MapToViewModel(model);

                FpRegradingResultDocsPdfTemplate PdfTemplate = new FpRegradingResultDocsPdfTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"Bon Retur Barang {viewModel.Code}.pdf"
                };
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
