using Api.Helpers;

namespace Api.Extensions;

public static class FileHandlerExtension
{
    public static string HandleFile(this IFormFile file, string parentSlug)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), FilePath.MainPath, parentSlug);
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileExtension = new FileInfo(file.FileName).Extension;
        int filesCount = Directory.GetFiles(path).Length;
        string fileName = $"_{Guid.NewGuid()}_Axe{filesCount}{fileExtension}";

        string fileNameWithPath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        file.CopyTo(stream);

        string dbName = Path.Combine(parentSlug, fileName);
        return dbName;
    }
}
