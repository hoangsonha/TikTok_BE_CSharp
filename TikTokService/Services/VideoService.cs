using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;

namespace TikTokService.Services
{
    public interface VideoService
    {
        public List<Video> GetAllVideos();
        public Video GetVideoByID(int id);
        public Video AddVideo(Video video);
        public Video UpdateVideo(Video video, int id);
        public Video DeleteVideo(int id);
    }
}
