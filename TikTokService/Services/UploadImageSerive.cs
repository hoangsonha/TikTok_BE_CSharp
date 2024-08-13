using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TikTokService.Services
{
    public interface UploadImageSerive
    {
        public Task<string> UploadFileBase64Async(string base64Image);

        public String Upload(IFormFile file);

        public String Upload(List<IFormFile> files);

        public String GenerateImageWithInitial(String userName);
    }
}
