using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs;
using TikTokRepositories.Repositories;
using TikTokDAOs.Entities;

namespace TikTokRepositories.RepositoriesImp
{
    public class VideoRepositoryImp : VideoRepository
    {
        private readonly VideoDAO _videoDAO = null;

        public VideoRepositoryImp()
        {
            _videoDAO = new VideoDAO();
        }

        public List<Video> GetAllVideos()
        {
            return _videoDAO.GetAllVideos();
        }
        public Video GetVideoByID(int id)
        {
            return _videoDAO.GetVideoByID(id);
        }

        public List<Video> GetVideoByAccountID(int id)
        {
            List < Video > lists = GetAllVideos();
            return lists.Where((vid) => vid.IdAccount == id).ToList();
        }
        public Video AddVideo(Video video)
        {
            return _videoDAO.AddVideo(video);
        }
        public Video UpdateAccount(Video video, int id)
        {
            return _videoDAO.UpdateVideo(video, id);
        }
        public Video DeleteAccount(int id)
        {
            return _videoDAO.DeleteVideo(id);
        }

        public int GetTotalLikedVideoByAccount(int accountID)
        {
            return _videoDAO.GetTotalLikedVideo(accountID);
        }
    }
}
