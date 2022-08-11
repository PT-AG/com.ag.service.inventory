using Com.Ambassador.Service.Inventory.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Ambassador.Service.Inventory.Test.Helpers
{
    public  class EmailValidationTest
    {
        [Fact]
       public void IsValidEmail_Return_True()
        {
            Assert.True(EmailValidation.IsValidEmail("adepamungkas@gmail.com"));
        }

        [Fact]
        public void IsValidEmail_Return_False()
        {
            Assert.False(EmailValidation.IsValidEmail(null));
        }

    }
}
