using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokRepositories.Repositories;
using TikTokDAOs;
using TikTokDAOs.Entities;

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

        public Account GetAccountByEmail(String email)
        {
            return _accountDAO.GetAccountByEmail(email);
        }

        public Account GetAccountByNickName(String nickName)
        {
            return _accountDAO.GetAccountByNickName(nickName);
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
