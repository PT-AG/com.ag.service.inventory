using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Ambassador.Service.Inventory.Lib.Models.GarmentLeftoverWarehouse.CustomOut
{
    public class CustomsOutModel : StandardEntity,IValidatableObject
    {
        [MaxLength(255)]
        public string BCNo { get; set; }
        public DateTimeOffset BCDate { get; set; }
        [MaxLength(20)]
        public string BCType { get; set; }
        public bool IsSubcon { get; set; }
        [MaxLength(3000)]
        public string Remark { get; set; }
        public virtual ICollection<CustomsOutItemModel> Items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
