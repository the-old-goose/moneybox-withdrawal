using System;

namespace Moneybox.App
{
    public class Account
    {
        private const decimal PayInLimit = 4000m;
        private const decimal NotificationThreshold = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        //These properties should never be set outside this class
        public decimal Balance { get; private set; }

        public decimal Withdrawn { get; private set; }

        public decimal PaidIn { get; private set; }

        public void CheckFunds()
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

        public bool ApproachingPayInLimit(decimal amount)
        {
            return (Account.PayInLimit - (this.PaidIn + amount) < Account.NotificationThreshold);
            
        }

        public bool Transfer(Account to,decimal amount)
        {
            //Its likely more logic will be added once the app expands so I have wrapped in try catch and will return a bool to the calling function.
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

    }
}
