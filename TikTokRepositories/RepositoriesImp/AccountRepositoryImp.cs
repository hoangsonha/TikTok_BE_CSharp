using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokRepositories.Repositories;
using TikTokDAOs.Entities;
using TikTokDAOs;

namespace TikTokRepositories.RepositoriesImp
{
    public class AccountRepositoryImp : AccountRepository
    {
        private readonly AccountDAO _accountDAO = null;

        public AccountRepositoryImp()
        {
            if (_accountDAO == null) _accountDAO = new();
        }
        public Account AddAccount(Account account)
        {
            return _accountDAO.AddAccount(account);
        }

        public Account DeleteAccount(int id)
        {
            return _accountDAO.DeleteAccount(id);
        }

        public Account GetAccountByID(int id)
        {
            return _accountDAO.GetAccountByID(id);
        }

        public List<Account> GetAllAccounts()
        {
            return _accountDAO.GetAllAccounts();
        }

        public Account UpdateAccount(Account account, int id)
        {
            return _accountDAO.UpdateAccount(account, id);
        }
    }
}
