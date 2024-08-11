using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokRepositories.Repositories;
using TikTokRepositories.RepositoriesImp;
using TikTokService.Services;
using TikTokDAOs.Entities;

namespace TikTokService.ServicesImp
{
    public class VideoServiceImp : VideoService
    {
        private readonly VideoRepository _videoRepository = null;

        public VideoServiceImp()
        {
            if (_videoRepository == null) _videoRepository = new VideoRepositoryImp();
        }

        public Video AddVideo(Video video)
        {
            return _videoRepository.AddVideo(video);
        }

        public Video DeleteVideo(int id)
        {
            return (_videoRepository.DeleteAccount(id));
        }

        public List<Video> GetAllVideos()
        {
            return _videoRepository.GetAllVideos();
        }

        public Video GetVideoByID(int id)
        {
            return _videoRepository.GetVideoByID(id);
        }

        public Video UpdateVideo(Video video, int id)
        {
            return _videoRepository.UpdateAccount(video, id);
        }

        public int GetTotalLikedVideoByAccount(int accountID)
        {
            return _videoRepository.GetTotalLikedVideoByAccount(accountID);
        }
    }
}
