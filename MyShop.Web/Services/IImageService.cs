namespace MyShop.Web.Services;

public interface IImageService
{
    Task<(bool isUpload, string? errorMessage)> UploadImageAsync(IFormFile image, string imageName, string folderPath);
    (bool isDelete, string? deleteMessage) DeleteImage(string imagePath);
}

