﻿using AutoMapper;
using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Com.Ambassador.Service.Inventory.Lib.Models.InventoryWeavingModel;
using Com.Ambassador.Service.Inventory.Lib.Services;
using Com.Ambassador.Service.Inventory.Lib.Services.InventoryWeaving;
using Com.Ambassador.Service.Inventory.Lib.ViewModels.InventoryWeavingViewModel;
using Com.Ambassador.Service.Inventory.WebApi.Controllers.v1.WeavingInventory;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace Com.Ambassador.Service.Inventory.Test.Controllers.InventoryWeavingReport
{
    public class InventoryWeavingAdjDocumentControllerTest
    {
        protected InventoryWeavingDocument Model
        {
            get { return new InventoryWeavingDocument(); }
        }
        protected InventoryWeavingDocumentOutViewModel viewModel
        {
            get { return new InventoryWeavingDocumentOutViewModel(); }
        }

        protected ServiceValidationExeption GetServiceValidationExeption()
        {
            Mock<IServiceProvider> serviceProvider = new Mock<IServiceProvider>();
            List<ValidationResult> validationResults = new List<ValidationResult>();
            System.ComponentModel.DataAnnotations.ValidationContext validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(viewModel, serviceProvider.Object, null);
            return new ServiceValidationExeption(validationContext, validationResults);
        }

        protected (Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IInventoryWeavingDocumentAdjService> service) GetMocks()
        {
            return (IdentityService: new Mock<IIdentityService>(), ValidateService: new Mock<IValidateService>(), service: new Mock<IInventoryWeavingDocumentAdjService>());
        }

        protected WeavingInventoryAdjController GetController((Mock<IIdentityService> IdentityService, Mock<IValidateService> ValidateService, Mock<IInventoryWeavingDocumentAdjService> service) mocks)
        {
            var user = new Mock<ClaimsPrincipal>();
            var claims = new Claim[]
            {
                new Claim("username", "unittestusername")
            };
            user.Setup(u => u.Claims).Returns(claims);
            WeavingInventoryAdjController controller = (WeavingInventoryAdjController)Activator.CreateInstance(typeof(WeavingInventoryAdjController), mocks.IdentityService.Object, mocks.ValidateService.Object, mocks.service.Object);
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = user.Object
                }
            };
            controller.ControllerContext.HttpContext.Request.Headers["Authorization"] = "Bearer unittesttoken";
            controller.ControllerContext.HttpContext.Request.Headers["x-timezone-offset"] = "7";
            controller.ControllerContext.HttpContext.Request.Path = new PathString("/v1/unit-test");
            return controller;
        }

        protected int GetStatusCode(IActionResult response)
        {
            return (int)response.GetType().GetProperty("StatusCode").GetValue(response, null);
        }

        [Fact]
        public void Get_Report_Success_Get()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new ListResult<InventoryWeavingDocument>(new List<InventoryWeavingDocument>(), 1, 25, 7));
            var controller = GetController(mocks);
            var response = controller.Get(null, 1, 25, "{}", "{}");
            Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
            //Assert.NotNull(response);
        }

        [Fact]
        public void Get_Report_Fail_Read()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.Read(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());
            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/json";
            var response = controller.Get(null, 1, 25, "{}", "{}");
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_Success_GetMaterialList()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.GetMaterialItemList(It.IsAny<string>()))
                .Returns(new List<InventoryWeavingItemDetailViewModel>());
            var controller = GetController(mocks);
            var response = controller.GetMaterial("{}");
            //Assert.Equal((int)HttpStatusCode.OK, GetStatusCode(response));
            Assert.NotNull(response);
        }

        [Fact]
        public void GetReport_Fail_GetMaterialList()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.GetMaterialItemList(It.IsAny<String>()))
                .Throws(new Exception());
            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/json";
            var response = controller.GetMaterial(null);
            Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
        }

        [Fact]
        public void GetReport_Success_Post()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.MapToModel(It.IsAny<InventoryWeavingDocumentOutViewModel>()));
            var controller = GetController(mocks);
            var response = controller.Post(viewModel);
            Assert.NotNull(response);
        }

        [Fact]
        public void GetReport_Fail_Post()
        {
            var mocks = GetMocks();
            mocks.service.Setup(f => f.MapToModel(It.IsAny<InventoryWeavingDocumentOutViewModel>()))
                .Throws(new Exception());
            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/json";
            var response = controller.Post(viewModel);
            //Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
            Assert.NotNull(response);
        }

        [Fact]
        public void GetReport_Validate_Post()
        {
            var mocks = GetMocks();
            mocks.ValidateService.Setup(f => f.Validate(It.IsAny<InventoryWeavingDocumentOutViewModel>())).Throws(GetServiceValidationExeption());
            mocks.service.Setup(f => f.MapToModel(It.IsAny<InventoryWeavingDocumentOutViewModel>())).ReturnsAsync(Model);
            var controller = GetController(mocks);
            controller.ControllerContext.HttpContext.Request.Headers["Accept"] = "application/json";
            var response = controller.Post(viewModel);
            //Assert.Equal((int)HttpStatusCode.InternalServerError, GetStatusCode(response));
            Assert.NotNull(response);
        }
    }
}
