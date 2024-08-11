using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokDAOs.Entities;

namespace TikTokDAOs
{
    public class VideoDAO
    {
        private readonly TikTokDbContext _context = null;
        private readonly AccountDAO _accountDAO = null;

        public VideoDAO()
        {
            if (_context == null) _context = new();
            if (_accountDAO == null) _accountDAO = new AccountDAO();
        }

        public List<Video> GetAllVideos()
        {
            return _context.Videos.ToList();
        }

        public Video GetVideoByID(int id)
        {
            return _context.Videos.FirstOrDefault(vid => vid.Id == id);
        }

        public Video AddVideo(Video video)
        {
            _context.Videos.Add(video);
            _context.SaveChanges();

            return video;
        }

        public Video UpdateVideo(Video video, int id)
        {
            Video vid = GetVideoByID(id);
            if (vid != null)
            {
                vid.Title = video.Title;
                vid.SrcVideo = video.SrcVideo;
                _context.Videos.Update(vid);
                _context.SaveChanges();
            }
            return vid;
        }

        public Video DeleteVideo(int id)
        {
            Video video = GetVideoByID(id);
            _context.Videos.Remove(video);
            _context.SaveChanges();
            return video;
        }
    }
}
