using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Text;
using Com.Ambassador.Service.Inventory.Lib.Models.GarmentReceiptSubconWasteProduction.ExpenditureWaste;

namespace Com.Ambassador.Service.Inventory.Lib.Configs.GarmentWasteProductionConfig.GarmentReceiptSubconWasteProductionConfig.ExpenditureWasteConfig
{
    public class GarmentSubconExpenditureWasteProductionsItemConfig : IEntityTypeConfiguration<GarmentSubconExpenditureWasteProductionItems>
    {
        public void Configure(EntityTypeBuilder<GarmentSubconExpenditureWasteProductionItems> builder)
        {
            builder.Property(p => p.GarmentReceiptWasteNo).HasMaxLength(25);
        }


    }
}
