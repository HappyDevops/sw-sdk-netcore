﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SW.Services.Taxpayer;
using Test_SW.Helpers;

namespace Test_SW_sdk_45.Services.TaxpayersService
{
    class Taxplayers_test
    {

        [TestClass]
        public class Account_Test_45
        {


            [TestMethod]
            public void Taxpayer_Test_45_TaxpayerByUser()
            {
                var build = new BuildSettings();
                Taxpayer Taxpayer = new Taxpayer(build.Url, build.User, build.Password);
                var response = Taxpayer.GetTaxpayer("ZNS1101105T3");
                Assert.IsTrue(response.status == "success", response.message);
            }
            [TestMethod]
            public void Taxpayer_Test_45_TaxpayerByToken()
            {
                var build = new BuildSettings();
                Taxpayer Taxpayer = new Taxpayer(build.Url, build.Token);
                var response = Taxpayer.GetTaxpayer("ZNS1101105T3");
                Assert.IsTrue(response.status == "success", response.message);
            }

            [TestMethod]
            public void Taxpayer_Test_45_TaxpayerByUserError()
            {
                var build = new BuildSettings();
                Taxpayer Taxpayer = new Taxpayer(build.Url, build.User, build.Password);
                var response = Taxpayer.GetTaxpayer("ZNS1101105T4");
                Assert.IsTrue(response.status == "error", response.message);
            }
            [TestMethod]
            public void Taxpayer_Test_45_TaxpayerByTokenerror()
            {
                var build = new BuildSettings();
                Taxpayer Taxpayer = new Taxpayer(build.Url, build.Token);
                var response = Taxpayer.GetTaxpayer("ZNS1101105T4");
                Assert.IsTrue(response.status == "error", response.message);
            }
        }
    }
}
