using Domain.Enums;

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
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        string fileExtension = new FileInfo(picture.FileName).Extension;
        int filesCount = Directory.GetFiles(path).Length;
        string fileName = $"_{Guid.NewGuid()}_Axe{filesCount}{fileExtension}";

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
    /// <exception cref="FileNotFoundException"></exception>
    public static bool DeletePicture(this string pictureName, PictureType pictureType, long parentId)
    {
        string path = CreatePicturePath(pictureType, parentId);
        if (string.IsNullOrWhiteSpace(path))
            throw new FileNotFoundException();

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
    /// <exception cref="FileNotFoundException"></exception>
    public static string GetPicture(this string pictureName, PictureType pictureType, long parentId)
    {
        string path = CreatePicturePath(pictureType, parentId);
        if (string.IsNullOrWhiteSpace(path))
            throw new FileNotFoundException();

        return Path.Combine(path, pictureName);
    }

    /// <summary>
    /// ساخت آدرس عکس
    /// </summary>
    /// <param name="pictureType">نوع عکس</param>
    /// <param name="parentId">آیدی صاحب عکس</param>
    /// <returns>آدرس محل ذخیره عکس</returns>
    /// <exception cref="FileNotFoundException"></exception>
    private static string CreatePicturePath(PictureType pictureType, long parentId)
    {
        if (parentId == 0)
            throw new FileNotFoundException();

        string pctureTypePath = EnumExtension.GetEnumDescriptions(typeof(PictureType))
            .FirstOrDefault(x => x.Id == (int)pictureType).Description
            ?? throw new FileNotFoundException();

        string mainFolder = $"{pctureTypePath} با آیدی {parentId}";
        return Path.Combine(Directory.GetCurrentDirectory(), FileHelper.MainPath, pctureTypePath, mainFolder);
    }
}
