using System;

namespace Moneybox.App.DataAccess
{
    public interface IAccountRepository
    {
        Account GetAccountById(Guid accountId);

        void Update(Account account);// I would remove

        // void Update(params Account[] account); I would add
    }
}
