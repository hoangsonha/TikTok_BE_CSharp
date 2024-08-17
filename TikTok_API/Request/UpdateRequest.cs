using System.ComponentModel;

namespace TikTokAPI.Request
{

    public class UpdateRequest
    {

        public int Id { get; set; }
        public String? Password { get; set; }
        public String? NewPassword { get; set; }
        public IFormFile? Avatar { get; set; }
        public String? FullName { get; set; }
        public String? Contact { get; set; }
        public String? NickName { get; set; }
    }
}
