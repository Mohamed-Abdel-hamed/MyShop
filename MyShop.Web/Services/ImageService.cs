namespace MyShop.Web.Services;

public class ImageService(IWebHostEnvironment webHostEnvironment) : IImageService
{
    private readonly IWebHostEnvironment _webHostEnvironment=webHostEnvironment;

    private readonly List<string> _allowedExtensions = new() { ".jpg", ".jpeg", ".png" };

    private readonly int _maxAllowedSize = 2097152;

    public async Task<(bool isUpload, string? errorMessage)> UploadImageAsync(IFormFile image, string imageName, string folderPath)
    {
        var imageExtension = Path.GetExtension(image.FileName);

        if (!_allowedExtensions.Contains(imageExtension))

            return (false, "Extension not valid ");

        if (_maxAllowedSize < image.Length)

            return (false, "Size not valid ");

        var path = Path.Combine($"{_webHostEnvironment.WebRootPath}{folderPath}", imageName);

        using var stream = File.Create(path);

        await image.CopyToAsync(stream);

        stream.Dispose();

        return (true,null);
    }

    public (bool isDelete, string? deleteMessage) DeleteImage(string imagePath)
    {
        var oldImagePath = $"{_webHostEnvironment.WebRootPath}{imagePath}";

        if (File.Exists(oldImagePath))
        {
            File.Delete(oldImagePath);
            return (true, null);
        }

        return (false,"not found image");
    }
}