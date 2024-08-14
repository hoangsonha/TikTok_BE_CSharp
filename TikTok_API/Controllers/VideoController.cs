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

        [HttpGet("liked")]
        public int GetTotalLikedByAccountID(int accountID)
        {
            return _videoService.GetTotalLikedVideoByAccount(accountID);
        }


        [HttpGet("{id}")]
        public Video GetVideo(int id)
        {
            var video = _videoService.GetVideoByID(id);

            if (video == null) return null;

            return video;
        }

        [HttpPut("{id}")]
        public Video PutAccount(int id, Video video)
        {
            return _videoService.UpdateVideo(video, id);
        }


        [HttpPost("video/create")]
        public Video PostAccount(VideoRequest requestVideo)
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


        [HttpDelete("{id}")]
        public Video DeleteAccount(int id)
        {
            return _videoService.DeleteVideo(id);
        }

        

    }
}
