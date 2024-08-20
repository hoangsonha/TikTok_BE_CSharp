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
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using X.PagedList.Extensions;

namespace TikTokAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("account")]
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


        [HttpGet("search")]
        public ObjectResponse GetAccountByNickNameAndFullName(string fullName, string nickName)
        {
            List<Account> lists = _accountService.GetAllAccountsByNickNameAndFullName(fullName, nickName);
            if (lists.Count > 0) return new ObjectResponse() { Code = "Success", Message = "Get accounts successfully", data = lists };
            return new ObjectResponse() { Code = "Failed", Message = "Get accounts failed", data = null };
        }


        [HttpGet("getAll")]
        public ObjectResponse GetAccounts()
        {
            List<Account> lists = _accountService.GetAllAccounts().ToList();
            if(lists.Count > 0) return new ObjectResponse() { Code = "Success", Message = "Get accounts successfully", data = lists };
            return new ObjectResponse() { Code = "Failed", Message = "Get accounts failed", data = null };
        }

        [HttpGet("getAllByPage")]
        public ObjectPageList GetPage(int page = 1, int pageSize = 5)
        {
            int totalItem = _accountService.GetAllAccounts().Count();
            List<Account> lists = _accountService.GetAllAccounts();
            var result = lists.ToPagedList(page, pageSize);
            if (result.Count > 0)
            {
                return new ObjectPageList() { Code = "Success", Message = "Get accounts successfully", data = result, CurrentPage= result.PageNumber, PageSize = result.PageSize, TotalItem= result.TotalItemCount, TotalPage = result.PageCount };
            }
            return new ObjectPageList() { Code = "Failed", Message = "Get accounts failed", data = null, CurrentPage = 0, PageSize = 0, TotalItem = 0, TotalPage = 0 };
        }

        [HttpGet("get/{id}")]
        public ObjectResponse GetAccount(int id)
        {
            var account = _accountService.GetAccountByID(id);

            if (account != null) return new ObjectResponse() { Code = "Success", Message = "Get account successfully", data = account };

            return new ObjectResponse() { Code = "Failed", Message = "Get account failed", data = null };
        }

        [HttpGet("get/nickName")]
        public ObjectResponse GetAccount(string nickName)
        {
            var account = _accountService.GetAccountByNickName(nickName);

            if (account != null) return new ObjectResponse() { Code = "Success", Message = "Get account successfully", data = account };

            return new ObjectResponse() { Code = "Failed", Message = "Get account failed", data = null };
        }

        [HttpPut("update")]
        public ObjectResponse PutAccount([FromForm] UpdateRequest request)
        {
            Console.WriteLine($"ID {request.Id}| contact {request.Contact}| fullName {request.FullName}| nickName {request.NickName}");
            Account account = _accountService.GetAccountByID(request.Id);
            if(account != null)
            {
                if(!request.Contact.IsNullOrEmpty())
                    account.Contact = request.Contact;
                
                if(!request.Password.IsNullOrEmpty() && !request.NewPassword.IsNullOrEmpty() && request.Password == account.Password)
                    account.Password = request.NewPassword;
                
                if(!request.FullName.IsNullOrEmpty())
                    account.FullName = request.FullName;

                if (!request.NickName.IsNullOrEmpty())
                    account.NickName = request.NickName;

                if(request.Avatar != null)
                {
                    Task<String> url = _uploadImageSerive.Upload(request.Avatar);

                    if (!url.Result.IsNullOrEmpty())
                        account.Avatar = url.Result;
                }

                return new ObjectResponse() { Code = "Success", Message = "Update account successfully", data = _accountService.UpdateAccount(account, request.Id) };
            }
            return new ObjectResponse() { Code = "Failed", Message = "Update account failed", data = null };
        }

        [HttpPost("create")]
        public ObjectResponse CreateAccount(RegisterRequest request)
        {
            String base64 = _uploadImageSerive.GenerateImageWithInitial(request.Email);
            Task<String> avatarStorage = _uploadImageSerive.UploadFileBase64Async(base64);
            String avatar = avatarStorage.Result;
            Account account = new Account()
            {
                Email = request.Email,
                Password = request.Password,
                NickName = generateRandom(),
                Avatar = avatar,
                Contact = null,
                FullName = generateRandom(),
                Followed = 0,
                Liked = 0,
                Status = 1
            };
            return new ObjectResponse() { Code = "Success", Message = "Add account successfully", data = _accountService.AddAccount(account) };
        }

        [HttpDelete("delete/{id}")]
        public Account DeleteAccount(int id)
        {
            return _accountService.DeleteAccount(id);
        }

        private String generateRandom()
        {
            string prefix = "user";
            int length = 20;

            int lengthRandom = length - prefix.Length;

            String random = Guid.NewGuid().ToString().Replace("-", "").Substring(0, lengthRandom);

            return prefix + random;
        }

    }
}
