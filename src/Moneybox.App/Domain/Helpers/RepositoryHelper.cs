using Moneybox.App.DataAccess;
using System;

namespace Moneybox.App.Domain.Helpers
{
    public class RepositoryHelper : IAccountRepository
    {
        private readonly IAccountRepository _accountRepository;

        public RepositoryHelper(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Account GetAccountById(Guid accountId)
        {
            return this._accountRepository.GetAccountById(accountId);
        }

        // Assuming that accountRepository service will implement safety checks for updating
        public void Update(params Account[] accounts)
        {
            foreach (Account account in accounts)
            {
                _accountRepository.Update(account);
            }
        }

        public void Update(Account account)
        {
            //I would obviously edit the interface using the above signature and remove this from this class but instructions mention they do not want IAccountRepository modified.
            //As to me it seems redundant having to call update on every account.
            throw new NotImplementedException();
        }
    }
}
