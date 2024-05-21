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
        RuleFor(p => p.PictureName)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .NotNull().WithMessage(ValidationMessages.NullMessage)
            .Must(x => x!.ContentType.Equals("image/jpeg") || x.ContentType.Equals("image/jpg") || x.ContentType.Equals("image/png"))
            .WithMessage("فرمت اشتباه");

        RuleFor(p => p.PictureAlt)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));

        RuleFor(p => p.PictureTitle)
            .NotEmpty().WithMessage(ValidationMessages.NullMessage)
            .MaximumLength(200).WithMessage(ValidationMessages.MaximumCharactersMessage(200));
    }
}
