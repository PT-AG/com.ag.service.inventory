﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Ambassador.Service.Inventory.Test.Helpers
{
    public abstract class BaseDataUtil<TModel, TFacade>
    {
        public TFacade Facade { get; set; }
        public abstract TModel GetNewData();
        public abstract Task<TModel> GetTestData();

        public BaseDataUtil(TFacade facade)
        {
            this.Facade = facade;
        }
    }
}
