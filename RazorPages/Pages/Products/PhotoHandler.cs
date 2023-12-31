using DataModel;

namespace RazorPages.Pages.Products;

public class PhotoHandler
{
    public static async Task<string?> GetPhoto(IWebHostEnvironment webHostEnvironment, IFormFileCollection formFileCollection)
    {
        foreach (var file in formFileCollection)
        {
            if (file is not { Length: > 0 }) continue;

            var uploads = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\images");
            var extension = Path.GetExtension(file.FileName);

            if (extension != ".jpg" && extension != ".png" && extension != ".pdf" && extension != ".jpeg") continue;

            var fileName = Guid.NewGuid().ToString().Replace("-", "") + extension;

            await using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fileName;
        }

        return null;
    }

    public static bool DeletePreviousPhoto(IWebHostEnvironment webHostEnvironment, string? path)
    {

        if (path is not { Length: > 0 }) return false;

        var previousPhoto = Path.Combine(webHostEnvironment.WebRootPath, "uploads\\images", path);

        if (!System.IO.File.Exists(previousPhoto)) return false;

        System.IO.File.Delete(previousPhoto);

        return true;
    }
}