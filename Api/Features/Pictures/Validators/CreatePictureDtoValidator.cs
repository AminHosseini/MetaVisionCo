namespace Api.Features.Pictures.Validators;

/// <summary>
/// کلاس اعتبار سنجی اطلاعات عکس وارد شده توسط کاربر برای ایجاد
/// </summary>
public class CreatePictureDtoValidator : AbstractValidator<CreatePictureDto>
{
    /// <summary>
    /// سازنده کلاس به همراه قوانین اعتبار سنجی
    /// </summary>
    public CreatePictureDtoValidator()
    {
        RuleFor(p => p.PictureFile)
            .NotNull().WithMessage(ValidationMessages.NullMessage)
            .Must(p => p?.Length < 3000000).WithMessage(ValidationMessages.MaximumFileSize(3))
            .Must(p => p is not null && (p.ContentType.Equals(FileHelper.Jpeg)
                || p.ContentType.Equals(FileHelper.Jpg)
                || p.ContentType.Equals(FileHelper.Png)))
                .WithMessage(ValidationMessages.AllowedFileFormats("Jpeg, Jpg, Png"));

        RuleFor(p => p.PictureFile!.Length)
            .ExclusiveBetween(0, 3000000).WithMessage(ValidationMessages.MaximumFileSize(3))
            .When(p => p is not null && p.PictureFile is not null && p.PictureFile.Length is not 0);

        RuleFor(p => p.PictureAlt)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .NotEqual("null").WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(p => p.PictureTitle)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .NotEqual("null").WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));
    }
}
