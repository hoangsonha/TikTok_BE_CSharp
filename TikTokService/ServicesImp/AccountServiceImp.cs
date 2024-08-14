using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokRepositories.Repositories;
using TikTokRepositories.RepositoriesImp;
using TikTokService.Services;
using TikTokDAOs.Entities;

namespace TikTokService.ServicesImp
{
    public class AccountServiceImp : AccountService
    {
        private readonly AccountRepository _accountRepository = null;

        public AccountServiceImp()
        {
            if (_accountRepository == null) _accountRepository = new AccountRepositoryImp();
        }

        public Account CheckLogin(string email, string password)
        {
            Account account = _accountRepository.GetAccountByEmail(email);
            if (account != null) 
                if (account.Password == password)    
                    return account;
            
            return null;
        }

        public List<Account> GetAllAccountsByNickNameAndFullName(string nickName, string fullName)
        {
            List<Account> accounts = _accountRepository.GetAllAccounts();
            return accounts.Where(acc => acc.FullName.ToLower().Contains(fullName.ToLower()) || acc.NickName.ToLower().Contains(nickName.ToLower())).ToList();
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
