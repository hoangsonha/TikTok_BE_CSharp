using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;
using TikTokRepositories.Repositories;
using TikTokRepositories.RepositoriesImp;
using TikTokService.Services;

namespace TikTokService.ServicesImp
{
    public class AccountServiceImp : AccountService
    {
        private readonly AccountRepository _accountRepository = null;

        public AccountServiceImp()
        {
            if (_accountRepository == null) _accountRepository = new AccountRepositoryImp();
        }


        public Account AddAccount(Account account)
        {
            return _accountRepository.AddAccount(account);
        }

        public Account DeleteAccount(int id)
        {
            return _accountRepository.DeleteAccount(id);
        }

        public Account GetAccountByID(int id)
        {
            return _accountRepository.GetAccountByID(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }

        public Account UpdateAccount(Account account, int id)
        {
            return _accountRepository.UpdateAccount(account, id);
        }
    }
}
