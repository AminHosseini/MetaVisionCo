namespace Api.Extensions;

/// <summary>
/// کلاس کار با فایل ها
/// </summary>
public static class FileHandlerExtension
{
    /// <summary>
    /// گرفتن عکس و ساختن آن
    /// </summary>
    /// <param name="picture">عکس</param>
    /// <param name="pictureType">نوع عکس</param>
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <returns>نام فایل</returns>
    public static string HandlePicture(this IFormFile picture, PictureType pictureType, long parentId)
    {
        string path = CreatePicturePath(pictureType, parentId);
        path = Path.Combine(Directory.GetCurrentDirectory(), path);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fileExtension = new FileInfo(picture.FileName).Extension;
        int filesCount = Directory.GetFiles(path).Length;
        string fileName = $"_{Guid.NewGuid()}_Axe_{filesCount}{fileExtension}";

        string fileNameWithPath = Path.Combine(path, fileName);
        using var stream = new FileStream(fileNameWithPath, FileMode.Create);
        picture.CopyTo(stream);

        return fileName;
    }

    /// <summary>
    /// حذف عکس
    /// </summary>
    /// <param name="pictureName">نام عکس</param>
    /// <param name="pictureType">نوع عکس</param>
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <returns>آیا عملیات حذف موفق بود؟</returns>
    public static bool DeletePicture(this string pictureName, PictureType pictureType, long parentId)
    {
        string path = CreatePicturePath(pictureType, parentId);
        path = Path.Combine(Directory.GetCurrentDirectory(), path);

        if (string.IsNullOrWhiteSpace(path))
            throw new RecordNotFoundException();

        var picturePath = Path.Combine(path, pictureName);
        if (File.Exists(picturePath))
        {
            File.Delete(picturePath);
            return true;
        }
        else
            return false;
    }

    /// <summary>
    /// گرفتن عکس
    /// </summary>
    /// <param name="pictureName">نام عکس</param>
    /// <param name="pictureType">نوع عکس</param>
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <returns>آدرس عکس</returns>
    public static PictureInfoDto GetPicture(this string pictureName, PictureType pictureType, long parentId)
    {
        string path = CreatePicturePath(pictureType, parentId);
        var localPath = Path.Combine(Directory.GetCurrentDirectory(), path);

        if (string.IsNullOrWhiteSpace(localPath))
            throw new RecordNotFoundException();

        localPath = Path.Combine(localPath, pictureName);
        if (!File.Exists(localPath))
            throw new RecordNotFoundException();

        using FileStream stream = File.OpenRead(localPath);

        path = Path.Combine("https://localhost:7191", path, pictureName);
        return new PictureInfoDto() { PictureName = pictureName, PicturePath = path, PictureSize = stream.Length };
    }

    /// <summary>
    /// ساخت آدرس عکس
    /// </summary>
    /// <param name="pictureType">نوع عکس</param>
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <returns>آدرس محل ذخیره عکس</returns>
    private static string CreatePicturePath(PictureType pictureType, long parentId)
    {
        if (parentId == 0)
            throw new RecordNotFoundException();

        string pctureTypePath = EnumExtension.GetEnumDescriptions(typeof(PictureType))
            .FirstOrDefault(x => x.Id == (int)pictureType).Name//Description
            ?? throw new RecordNotFoundException();

        string mainFolder = $"{pctureTypePath}_With_Id_{parentId}";
        //return Path.Combine(Directory.GetCurrentDirectory(), FileHelper.MainPath, pctureTypePath, mainFolder);
        return Path.Combine(FileHelper.MainPath, pctureTypePath, mainFolder);
    }
}
