using Com.Ambassador.Service.Inventory.Lib.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.ViewModels.GarmentLeftoverWarehouse.CustomOut
{
    public class CustomOutVM : BasicViewModel, IValidatableObject
    {
        public string BCNo { get; set; }
        public DateTimeOffset BCDate { get; set; }
        public string BCType { get; set; }
        public bool IsSubcon { get; set; }
        public string Remark { get; set; }
        public List<CustomOutItemVM> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BCDate == null || BCDate == DateTimeOffset.MinValue)
            {
                yield return new ValidationResult("Tanggal BC tidak boleh kosong", new List<string> { "BCDate" });
            }

            if (string.IsNullOrWhiteSpace(BCNo))
            {
                yield return new ValidationResult("No BC tidak boleh kosong", new List<string> { "BCNo" });
            }

            if (Items == null || Items.Count < 1)
            {
                yield return new ValidationResult("Items tidak boleh kosong", new List<string> { "ItemsCount" });
            }
            else
            {
                int errorCount = 0;
                List<Dictionary<string, string>> errorItems = new List<Dictionary<string, string>>();

                foreach (var item in Items)
                {
                    Dictionary<string, string> errorItem = new Dictionary<string, string>();

                    if (item.AvalExpenditureId == null || item.AvalExpenditureId == 0)
                    {
                        errorItem["AvalExpenditureId"] = "No Bon tidak boleh kosong";
                        errorCount++;
                    }

                    if (item.Price == null  || item.Price == 0)
                    {
                        errorItem["Price"] = "Harga tidak boleh kosong";
                        errorCount++;
                    }

                    errorItems.Add(errorItem);
                }

                if (errorCount > 0)
                {
                    yield return new ValidationResult(JsonConvert.SerializeObject(errorItems), new List<string> { "Items" });
                }
            }
        }
    }
}
