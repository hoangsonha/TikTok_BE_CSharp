namespace TikTokAPI.Request
{
    public class VideoRequest
    {
        public String Title { get; set; }
        public IFormFile SrcVideo { get; set; }
        public int AccountID { get; set; }
    }
}
