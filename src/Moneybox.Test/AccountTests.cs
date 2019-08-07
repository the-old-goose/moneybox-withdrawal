using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moneybox.App;
using System;

namespace Moneybox.Test
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void CheckFunds_Insufficient_ThrowsInvalidException()
        {
            //Arrange
            Account account = new Account(999m);
            decimal overlimit = account.Balance + 1m; ;


            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => account.CheckFunds(overlimit));
        }

        [TestMethod]
        public void CheckPayInLimit_ReachedLimit_ThrowsInvalidException()
        {
            //TODO
        }
    }
}
