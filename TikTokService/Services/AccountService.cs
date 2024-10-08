﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;

namespace TikTokService.Services
{
    public interface AccountService
    {
        public Account CheckLogin(string email, string password);
        public List<Account> GetAllAccountsByNickNameAndFullName(string nickName, string fullName);
        public List<Account> GetAllAccounts();
        public Account GetAccountByID(int id);
        public Account GetAccountByNickName(String nickName);
        public Account AddAccount(Account account);
        public Account UpdateAccount(Account account, int id);
        public Account DeleteAccount(int id);
    }
}
