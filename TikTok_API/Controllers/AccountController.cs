using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TikTokService.Services;
using TikTokService.ServicesImp;
using TikTokDAOs.Entities;

namespace TikTokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService = null;

        public AccountController()
        {
            if (_accountService == null) _accountService = new AccountServiceImp();
        }

        // GET: api/Account
        [HttpGet]
        public List<Account> GetAccounts()
        {
            return _accountService.GetAllAccounts().ToList();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public Account GetAccount(int id)
        {
            var account = _accountService.GetAccountByID(id);

            if (account == null) return null;

            return account;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Account PutAccount(int id, Account account)
        {
            return _accountService.UpdateAccount(account, id);
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Account PostAccount(Account account)
        {
            return _accountService.AddAccount(account);
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public Account DeleteAccount(int id)
        {
            return _accountService.DeleteAccount(id);
        }

    }
}
