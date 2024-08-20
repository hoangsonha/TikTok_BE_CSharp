using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TikTokAPI.Request;
using TikTokDAOs;
using TikTokService.Services;
using TikTokService.ServicesImp;
using TikTokDAOs.Entities;
using TikTokAPI.Response;
using X.PagedList.Extensions;


namespace TikTokAPI.Controllers
{
    [Route("video")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoService _videoService = null;
        private readonly AccountService _accountService = null;
        private readonly UploadImageSerive _uploadImageSerive = null;
        

        public VideoController()
        {
            if(_videoService == null) _videoService = new VideoServiceImp();
            if (_accountService == null) _accountService = new AccountServiceImp();
            if (_uploadImageSerive == null) _uploadImageSerive = new UploadImageServiceImp();
        }

        [HttpGet("getAll")]
        public ObjectResponse GetVideos()
        {
            List<Video> lists = _videoService.GetAllVideos().ToList();
            if (lists.Count > 0) return new ObjectResponse() { Code = "Success", Message = "Get videos successfully", data = lists };
            return new ObjectResponse() { Code = "Failed", Message = "Get videos failed", data = null };
        }

        [HttpGet("getAll/account")]
        public ObjectResponse GetVideosByAccountID(int accountID)
        {
            List<Video> lists = _videoService.GetVideoByAccountID(accountID);
            if (lists.Count > 0) return new ObjectResponse() { Code = "Success", Message = "Get videos by accountID successfully", data = lists };
            return new ObjectResponse() { Code = "Failed", Message = "Get videos by accountID failed", data = null };
        }

        [HttpGet("liked/account")]
        public int GetTotalLikedByAccountID(int accountID)
        {
            return _videoService.GetTotalLikedVideoByAccount(accountID);
        }


        [HttpGet("get/{id}")]
        public Video GetVideo(int id)
        {
            var video = _videoService.GetVideoByID(id);

            if (video == null) return null;

            return video;
        }

        [HttpPut("update/{id}")]
        public Video PutAccount(int id, Video video)
        {
            return _videoService.UpdateVideo(video, id);
        }


        [HttpPost("create")]
        public Video PostAccount([FromForm] VideoRequest requestVideo)
        {

            Task<string> srcVideo = _uploadImageSerive.UploadVideo(requestVideo.SrcVideo);
     
            Video postVideo = new();
            postVideo.Title = requestVideo.Title;
            postVideo.SrcVideo = srcVideo.Result;
            postVideo.Commented = 0;
            postVideo.Liked = 0;
            postVideo.Shared = 0;
            postVideo.IdAccount = requestVideo.AccountID;
            return _videoService.AddVideo(postVideo);
        }


        [HttpDelete("delete/{id}")]
        public Video DeleteAccount(int id)
        {
            return _videoService.DeleteVideo(id);
        }




        [HttpGet("page")]
        public List<Video> GetPage(int page = 1, int pageSize = 5)
        {
            List<Video> lists = _videoService.GetAllVideos().ToPagedList(page, pageSize).ToList();
            if(lists.Count > 0)
            {
                return lists;
            }
            return null;
        }





    }
}
