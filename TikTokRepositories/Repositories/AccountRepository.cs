
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;


namespace TikTokRepositories.Repositories
{
    public interface AccountRepository
    {
        public List<Account> GetAllAccounts();
        public Account GetAccountByEmail(String email);
        public Account GetAccountByID(int id);
        public Account AddAccount(Account account);
        public Account UpdateAccount(Account account, int id);
        public Account DeleteAccount(int id);

    }
}
