using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        //prevent fields being reassigned during runtime
        private readonly IAccountRepository accountRepository;
        private readonly INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

          
        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            //Assuming account repository will implement logic and throw an exception if no account is found.
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            bool lowFundsFlag = false;
            bool highPaidInLimitFlag = false;

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

            //Here I have reordered the logic to ensure the account repository is update successfully before notify the user of low funds.
            if(from.Transfer(to, amount))
            {
                Update(from, to);

                if (lowFundsFlag)
                {
                    this.notificationService.NotifyFundsLow(from.User.Email);
                }

                if (highPaidInLimitFlag)
                {
                    this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
                }
            }

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }

        // Assuming that accountRepository service will implement safety checks for updating
        private void Update(Account from,Account to)
        {
            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
