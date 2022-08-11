using System;
using System.Collections.Generic;
using System.Text;
using Com.Ambassador.Service.Inventory.Lib.Models.InventoryWeavingModel;

namespace Com.Ambassador.Service.Inventory.Lib.Helpers
{
    public class ListResult<T>
    {
        public ListResult(List<T> data, int page, int size, int totalRow)
        {
            Data = data;
            Page = page;
            Size = size;
            Total = totalRow;
        }

        public List<T> Data { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public int Total { get; set; }
    }
}
