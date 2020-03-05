﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SW.Services.Account;
using Test_SW.Helpers;

namespace Test_SW.Services.Account_Test
{
    [TestClass]
    public class Account_Test
    {
        
        [TestMethod]
        public void ConsultaDeSaldoByUser()
        {
            var build = new BuildSettings();
            BalanceAccount account = new BalanceAccount(build.Url, build.User, build.Password);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
        [TestMethod]
        public void ConsultaDeSaldoByToken()
        {
            var build = new BuildSettings();
            BalanceAccount account = new BalanceAccount(build.Url, build.Token);
            var response = account.ConsultarSaldo();
            Assert.IsTrue(response.status == "success", response.messageDetail);
        }
    }
}
