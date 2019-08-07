using Moneybox.App.Domain.Helpers;
using Moneybox.App.Domain.Services;
using Moneybox.App.Utilities;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        //private readonly IAccountRepository accountRepository;
        private readonly RepositoryHelper repositoryHelper;
        private readonly INotificationService notificationService;

        public WithdrawMoney(RepositoryHelper accountRepository, INotificationService notificationService)
        {
            this.repositoryHelper = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            Validator.CheckAmountPositive(amount);

            //As the type is obvious and assuming the correct checks are implemented in 'accountRepository.GetAccountById', declaring an explicit is not required. 
            var from = this.repositoryHelper.GetAccountById(fromAccountId);
        
            bool lowFundsFlag=false;
            
            from.CheckFunds(amount);

            from.CheckWithdrawalLimit(amount);

            if (from.LowFundsAfterTransaction(amount))
            {
                lowFundsFlag = true;
            }

            //The same logic is followed with TransferMoney
            if(from.Withdraw(amount))
            {
                this.repositoryHelper.Update(from);

                if (lowFundsFlag)
                {
                    this.notificationService.NotifyFundsLow(from.User.Email);
                }
            }
        }
    }
}
