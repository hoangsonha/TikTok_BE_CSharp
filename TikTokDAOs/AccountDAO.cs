using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;

namespace TikTokDAOs
{
    public class AccountDAO
    {
        private readonly TikTokDbContext _context = null;

        public AccountDAO()
        {
            if (_context == null) _context = new();
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }

        public Account GetAccountByID(int id)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.Id == id);
        }

        public Account GetAccountByNickName(String nickName)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.NickName == nickName);
        }

        public Account GetAccountByEmail(String email)
        {
            return _context.Accounts.FirstOrDefault(acc => acc.Email == email);
        }

        public Account AddAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
            return account;
        }

        public Account UpdateAccount(Account account, int id)
        {
            Account acc = GetAccountByID(id);
            if (acc != null)
            {
                acc.Avatar = account.Avatar;
                acc.FullName = account.FullName;
                acc.Password = account.Password;
                acc.Email = account.Email;
                acc.Contact = account.Contact;

                _context.Accounts.Update(acc);
                _context.SaveChanges();
            }
            return acc;
        }

        public Account DeleteAccount(int id)
        {
            Account account = GetAccountByID(id);
            _context.Accounts.Remove(account);
            _context.SaveChanges();
            return account;
        }
    }
}
