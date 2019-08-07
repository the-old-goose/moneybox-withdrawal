using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private readonly IAccountRepository accountRepository;
        private readonly INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            //As the type is obvious and assuming the correct checks are implemented in 'accountRepository', declaring 'from' as a var is fine. 
            var from = this.accountRepository.GetAccountById(fromAccountId);
        
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
                Update();

                if(lowFundsFlag)
                {
                    this.notificationService.NotifyFundsLow(from.User.Email);
                }
            }
        }

        private void Update()
        {
            this.accountRepository.Update(from);
        }
    }
}
