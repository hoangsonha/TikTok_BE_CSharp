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
using TikTokAPI.Request;
using TikTokAPI.Response;
using System.Collections;

namespace TikTokAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService = null;
        private readonly UploadImageSerive _uploadImageSerive = null;

        public AccountController()
        {
            if (_accountService == null) _accountService = new AccountServiceImp();
            if (_uploadImageSerive == null) _uploadImageSerive = new UploadImageServiceImp();
        }


        [HttpPost("login")]
        public ObjectResponse Login(LoginRequest reuqest)
        {
            Account acc = _accountService.CheckLogin(reuqest.Email, reuqest.Password);
            if(acc!= null)
                return new ObjectResponse() { Code = "Success", Message = "Login successfully", data = acc };
            return new ObjectResponse() { Code = "Failed", Message = "Login failed", data = null };
        }


        [HttpGet("getAll")]
        public ObjectResponse GetAccounts()
        {
            List<Account> lists = _accountService.GetAllAccounts().ToList();
            if(lists.Count > 0) return new ObjectResponse() { Code = "Success", Message = "Get accounts successfully", data = lists };
            return new ObjectResponse() { Code = "Failed", Message = "Get accounts failed", data = null };
        }

        [HttpGet("account/{id}")]
        public ObjectResponse GetAccount(int id)
        {
            var account = _accountService.GetAccountByID(id);

            if (account != null) return new ObjectResponse() { Code = "Success", Message = "Get account successfully", data = account };

            return new ObjectResponse() { Code = "Failed", Message = "Get account failed", data = null };
        }

        [HttpPut("account/update/{id}")]
        public ObjectResponse PutAccount(int id, UpdateRequest request)
        {
            Account account = _accountService.GetAccountByID(id);
            if(account != null)
            {
                if(request.Contact != null)
                    account.Contact = request.Contact;
                
                if(request.Password != null && request.NewPassword != null && request.Password == account.Password)
                    account.Password = request.NewPassword;
                
                if(request.FullName != null)
                    account.FullName = request.FullName;

                Task<String> url = _uploadImageSerive.Upload(request.Avatar);

                if(url.Result != null)
                    account.Avatar = url.Result;

                return new ObjectResponse() { Code = "Success", Message = "Update account successfully", data = _accountService.UpdateAccount(account, id) };
            }
            return new ObjectResponse() { Code = "Success", Message = "Update account successfully", data = account };
        }

        [HttpPost("account/create")]
        public ObjectResponse CreateAccount(RegisterRequest request)
        {
            String base64 = _uploadImageSerive.GenerateImageWithInitial(request.Email);
            Task<String> avatarStorage = _uploadImageSerive.UploadFileBase64Async(base64);
            String avatar = avatarStorage.Result;
            Account account = new Account()
            {
                Email = request.Email,
                Password = request.Password,
                Avatar = avatar,
                Contact = null,
                FullName = null,
                Followed = 0,
                Liked = 0
            };
            return new ObjectResponse() { Code = "Success", Message = "Add account successfully", data = _accountService.AddAccount(account) };
        }

        [HttpDelete("account/delete/{id}")]
        public Account DeleteAccount(int id)
        {
            return _accountService.DeleteAccount(id);
        }

    }
}
