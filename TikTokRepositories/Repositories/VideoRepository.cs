using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs;
using TikTokDAOs.Entities;

namespace TikTokRepositories.Repositories
{
    public interface VideoRepository
    {
        public List<Video> GetAllVideos();
        public Video GetVideoByID(int id);
        public Video AddVideo(Video video);
        public Video UpdateAccount(Video video, int id);
        public Video DeleteAccount(int id);

        public int GetTotalLikedVideoByAccount(int accountID);
    }
}
