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
using TikTokDAOs.Entities;
using TikTokService.Services;
using TikTokService.ServicesImp;

namespace TikTokAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoService _videoService = null;
        private readonly AccountService _accountService = null;

        public VideoController()
        {
           if(_videoService == null) _videoService = new VideoServiceImp();
            if (_accountService == null) _accountService = new AccountServiceImp();
        }

        // GET: api/Video
        [HttpGet]
        public List<Video> GetVideos()
        {
            return _videoService.GetAllVideos().ToList();
        }

        // GET: api/Account/5
        [HttpGet("{id}")]
        public Video GetVideo(int id)
        {
            var video = _videoService.GetVideoByID(id);

            if (video == null) return null;

            return video;
        }

        // PUT: api/Account/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public Video PutAccount(int id, Video video)
        {
            return _videoService.UpdateVideo(video, id);
        }

        // POST: api/Account
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public Video PostAccount(VideoRequest requestVideo)
        {
            Video postVideo = new();
            postVideo.Id = requestVideo.ID;
            postVideo.Title = requestVideo.Title;
            postVideo.Video1 = requestVideo.Video1;
            postVideo.Comment = requestVideo.Comment;
            postVideo.Liked = requestVideo.Liked;
            postVideo.IdAccount = requestVideo.AccountID;
            return _videoService.AddVideo(postVideo);
        }

        // DELETE: api/Account/5
        [HttpDelete("{id}")]
        public Video DeleteAccount(int id)
        {
            return _videoService.DeleteVideo(id);
        }

    }
}
