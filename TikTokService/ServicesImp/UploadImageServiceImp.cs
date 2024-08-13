using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TikTokService.Services;
using System.Diagnostics.Metrics;
using System.Drawing.Imaging;
using Google.Apis.Auth.OAuth2;
using FirebaseAdmin;
using Google.Cloud.Storage.V1;



namespace TikTokService.ServicesImp
{
    public class UploadImageServiceImp : UploadImageSerive
    {

        private readonly String bucketName = "swp391-f046d.appspot.com";
        private readonly String contentType = "image/png";
        private readonly String getStream = "Configs/swp391-f046d-firebase-adminsdk-drdyq-bc797fce80.json";
        private readonly String getURL = @"https://firebasestorage.googleapis.com/v0/b/swp391-f046d.appspot.com/o/{0}?alt=media";
        private readonly String folderStorage = "Tiktok_BE";


        public UploadImageServiceImp()
        {
            FirebaseApp.Create(new AppOptions(){ Credential = GoogleCredential.FromFile(getStream) });
        }

        public async Task<string> UploadFileBase64Async(string base64Image)
        {
            string fileName = Guid.NewGuid().ToString() + ".png";  // Generate a random file name
            string folder = $"{folderStorage}/{fileName}";
            string objectName = folder;

            // Create a StorageClient using the service account credentials
            GoogleCredential credential;
            using (var fileStream = new FileStream(getStream, FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(fileStream);
            }

            var storageClient = StorageClient.Create(credential);

            // Decode the base64 string to bytes
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            using (var memoryStream = new MemoryStream(imageBytes))
            {
                // Upload the file to Google Cloud Storage
                await storageClient.UploadObjectAsync(bucketName, objectName, contentType, memoryStream);
            }

            string downloadUrl = string.Format(getURL, Uri.EscapeDataString(folder));
            return downloadUrl;
        }

        public String Upload(IFormFile file)
        {
            return null;
        }

        public String Upload(List<IFormFile> files)
        {
            return null;
        }

        public String GenerateImageWithInitial(String userName)
        {
            int width = 100;
            int height = 100;
            string text = null;
            if (userName != null)
            {
                text = userName[0].ToString().ToUpper();
            } 

            using (var bitmap = new Bitmap(width, height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Orange);

                    // Tạo Font và Brush để vẽ chữ
                    using (var font = new Font("Arial", width / 2, FontStyle.Bold, GraphicsUnit.Pixel))
                    using (var brush = new SolidBrush(Color.Green))
                    {
                        // Tính toán vị trí để vẽ chữ ở giữa
                        var textSize = graphics.MeasureString(text, font);
                        var position = new PointF((width - textSize.Width) / 2, (height - textSize.Height) / 2);

                        // Vẽ chữ lên hình ảnh
                        graphics.DrawString(text, font, brush, position);
                    }

                    // Lưu hình ảnh vào MemoryStream
                    using (var memoryStream = new MemoryStream())
                    {
                        bitmap.Save(memoryStream, ImageFormat.Png);

                        // Chuyển đổi sang Base64
                        var imageBytes = memoryStream.ToArray();
                        return Convert.ToBase64String(imageBytes);
                    }
                }
            }
        }

    }
}

// using: để quản lí các tìa nguyên, khi trong khối using kết thúc thì sẽ tự động gọi hàm thu hồi để giải phóng bộ nhớ

// các file IDisposable sẽ k nằm trong trình dọn rác của .net như các đối tượng đồ hoạ, kết nối hệ thống, File nên phải dùng using để giải phóng bộ nhớ

//  using (var resource = new MyResource())
//  {
// Sử dụng resource ở đây
//  } // resource.Dispose() sẽ được gọi tự động ở đây


// IDisposable: Là một giao diện được sử dụng để cung cấp phương thức Dispose() nhằm giải phóng các tài nguyên không được quản      lý.
// Dispose(): Được gọi khi bạn muốn giải phóng tài nguyên của đối tượng một cách rõ ràng.
// using: Một cách tiện lợi để đảm bảo rằng Dispose() được gọi tự động, giúp quản lý tài nguyên một cách hiệu quả.

// nếu muốn tự tạo hàm  Dispose() phải overide lại IDisposable