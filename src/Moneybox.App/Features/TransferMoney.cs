using Moneybox.App.Domain.Helpers;
using Moneybox.App.Domain.Services;
using Moneybox.App.Utilities;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        //prevent fields being reassigned during runtime
        private readonly INotificationService notificationService;
        private readonly RepositoryHelper repositoryHelper;

        public TransferMoney(RepositoryHelper repositoryHelper, INotificationService notificationService)
        {
            this.repositoryHelper = repositoryHelper;
            this.notificationService = notificationService;
        }

        //I would create a base class, (perhaps a struct) called 'Money' which acts as a wrapper for 'decimal amount' with properties currency , conversion rate etc and carry out validate within the 'Money' class. 
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            //Ensure Amount is positive before we do anything!
            Validator.CheckAmountPositive(amount);

            //Assuming account repository will implement logic and throw an exception if no account is found.
            var from = repositoryHelper.GetAccountById(fromAccountId);
            var to = repositoryHelper.GetAccountById(toAccountId);

            bool lowFundsFlag = false, highPaidInLimitFlag = false;

            from.CheckFunds(amount);

            to.CheckPayInLimit(amount);

            if (from.LowFundsAfterTransaction(amount))
            {
                lowFundsFlag = true;
            }

            if (to.ApproachingPayInLimit(amount))
            {
                highPaidInLimitFlag = true;
            }

            //Here I have reordered the logic to ensure the account repository is update successfully before notifying the user of low funds.
            if(from.Transfer(to, amount))
            {
                repositoryHelper.Update(from, to);

                if (lowFundsFlag)
                {
                    this.notificationService.NotifyFundsLow(from.User.Email);
                }

                if (highPaidInLimitFlag)
                {
                    this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
                }
            }

        }
    }
}
