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
using System.Net;
using System.IO;
using Google.Apis.Storage.v1.Data;


namespace TikTokService.ServicesImp
{
    public class UploadImageServiceImp : UploadImageSerive
    {

        private readonly String bucketName = "swp391-f046d.appspot.com";
        private readonly String contentType = "image/png";
        private readonly String contentTypeVideo = "video/mp4";
        private readonly String getStream = "Configs/swp391-f046d-firebase-adminsdk-drdyq-6dc6c24b5a.json";
        private readonly String getURL = @"https://firebasestorage.googleapis.com/v0/b/swp391-f046d.appspot.com/o/{0}?alt=media";
        private readonly String folderStorage = "Tiktok_BE";
        private readonly String folderStorageVideo = "Tiktok_Video";

        private readonly FirebaseApp app = null;

        public UploadImageServiceImp()
        {
            if (FirebaseApp.DefaultInstance == null)      
                app = FirebaseApp.Create(new AppOptions(){ Credential = GoogleCredential.FromFile(getStream) });
            else
                app = FirebaseApp.DefaultInstance;
        }

        public async Task<string> UploadFileAsync(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string objectName = $"{folderStorage}/{fileName}";

                var storageClient = StorageClient.Create(app.Options.Credential);

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await storageClient.UploadObjectAsync(bucketName, objectName, contentType, fileStream);
                }

                string downloadUrl = string.Format(getURL, Uri.EscapeDataString(objectName));

                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file upload: {ex.Message}");

                return null;
            }
        }

        public async Task<string> UploadFileVideoAsync(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                string objectName = $"{folderStorageVideo}/{fileName}";

                var storageClient = StorageClient.Create(app.Options.Credential);

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    await storageClient.UploadObjectAsync(bucketName, objectName, contentTypeVideo, fileStream);
                }

                string downloadUrl = string.Format(getURL, Uri.EscapeDataString(objectName));

                return downloadUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during file upload: {ex.Message}");

                return null;
            }
        }


        public async Task<string> UploadFileBase64Async(string base64Image)
        {
            try
            {
                string fileName = Guid.NewGuid().ToString() + ".png";
                string folder = $"{folderStorage}/{fileName}";          // create path contains folder and fileName (fileName is genareted random + .png)
                string objectName = folder;

                var storageClient = StorageClient.Create(app.Options.Credential);   // create firebaseStorage

                byte[] imageBytes = Convert.FromBase64String(base64Image);      // convert base64 to byte[]

                using (var memoryStream = new MemoryStream(imageBytes))
                {
                    await storageClient.UploadObjectAsync(bucketName, objectName, contentType, memoryStream);  // upload to firebase
                }
                string downloadUrl = string.Format(getURL, Uri.EscapeDataString(folder));       // format url which is located in firebase

                return downloadUrl;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error at UploadFileBase64Async : {ex}");
                return null;
            }
        }


        private async Task<string> ConvertToFileAsync(IFormFile file, string fileName)
        {
            try
            {
                var filePath = Path.Combine(Path.GetTempPath(), fileName);

                await using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);        // Copy the file data to a new file
                }

                return filePath;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error at ConvertToFileAsync : {ex}");
                return null;
            }
        }

        public async Task<string> UploadVideo(IFormFile file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);                       // Get the file name
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";          // Generate a unique file name with extension
                var filePath = await ConvertToFileAsync(file, fileName);              // Convert IFormFile to File path
                var url = await UploadFileVideoAsync(filePath);                            // Upload the file and get the URL
                File.Delete(filePath);                                                // Delete the file after upload
                return url;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<string> Upload(IFormFile file)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName);                       // Get the file name
                fileName = $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";          // Generate a unique file name with extension
                var filePath = await ConvertToFileAsync(file, fileName);              // Convert IFormFile to File path
                var url = await UploadFileAsync(filePath);                            // Upload the file and get the URL
                File.Delete(filePath);                                                // Delete the file after upload
                return url;
            }
            catch (Exception e)
            {
                return null;
            }
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