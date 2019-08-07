using System;

namespace Moneybox.App
{
    public class Account
    {
        private const decimal PayInLimit = 4000m;
        private const decimal NotificationThreshold = 500m;
        private const decimal WithdrawLimit = 12000m;

        public Guid Id { get; set; }

        public User User { get; set; }

        //These properties should never be set outside this class therefore I have added private accessibility to the properties setter.
        public decimal Balance { get; private set; }

        public decimal Withdrawn { get; private set; }

        public decimal PaidIn { get; private set; }

        //empty contructor set initial balance to 0
        public Account()
        {
            this.Balance = 0m;
        }

        //Set to predefined initial balance
        public Account(Decimal initialBalance)
        {
            this.Balance = initialBalance;
        }
        public void CheckFunds(decimal amount)
        {
            if ((this.Balance - amount) < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }
        }

        public void CheckPayInLimit(decimal amount)
        {
            decimal paidIn = this.PaidIn + amount;

            if (paidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }
        }

        public bool LowFundsAfterTransaction(decimal amount)
        {
            return ((this.Balance - amount) < 500m);   
        }

        public void CheckWithdrawalLimit(decimal amount)
        {
            decimal withdrawn = this.Withdrawn + amount;

            if (withdrawn > Account.WithdrawLimit)
            {
                throw new InvalidOperationException("Account withdrawal limit reached");
            }
        }

        public bool ApproachingPayInLimit(decimal amount)
        {
            return (Account.PayInLimit - (this.PaidIn + amount) < Account.NotificationThreshold);
            
        }

        public bool ApproachingWithdrawnLimit(decimal amount)
        {
            return (Account.WithdrawLimit - (this.PaidIn + amount) < Account.NotificationThreshold);
         
        }

        public bool Transfer(Account to,decimal amount)
        {
            //Its likely more logic will be added once the app expands so I have wrapped this in a try catch and will return a bool to the calling function.
            try
            {
                this.Balance -= amount;
                this.Withdrawn -= amount;

                to.Balance += amount;
                to.PaidIn += amount;

                return true;
            }
            catch (Exception failedTransfer)
            { 
                failedTransfer.ToString();
                return false;
            }
        }

        public bool Withdraw(decimal amount)
        {
            //Its likely more logic will be added once the app expands so I have wrapped this in a try catch and will return a bool to the calling function.
            try
            {
                this.Balance -= amount;
                this.Withdrawn += amount;
                return true;
            }
            catch (Exception failedWithdrawal)
            {
                failedWithdrawal.ToString();
                return false;
            }
        }

    }
}
