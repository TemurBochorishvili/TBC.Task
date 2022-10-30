using Api.Controllers.Resources;
using Api.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace Api.Validators;


public class SavePhysicalPersonModelValidator : AbstractValidator<SavePhysicalPersonResource>
{

    public SavePhysicalPersonModelValidator(IStringLocalizer<Errors> localizer)
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(localizer["RequiredField"].Value);
        RuleFor(x => x.Name).Must(x => !string.IsNullOrWhiteSpace(x) && (Regex.IsMatch(x, "^[a-zA-Z]*$") || Regex.IsMatch(x, "^[ა-ჰ]*$"))).WithMessage(localizer["IncorrectCharacters"].Value);
        RuleFor(x => x.Name).MinimumLength(2).WithMessage(string.Format(localizer["HasMinLengthOf"].Value, 2));
        RuleFor(x => x.Name).MaximumLength(50).WithMessage(string.Format(localizer["HasMaxLengthOf"].Value, 50));

        RuleFor(x => x.LastName).NotEmpty().WithMessage(localizer["RequiredField"].Value);
        RuleFor(x => x.LastName).Must(x => !string.IsNullOrWhiteSpace(x) && (Regex.IsMatch(x, "^[a-zA-Z]*$") || Regex.IsMatch(x, "^[ა-ჰ]*$"))).WithMessage(localizer["IncorrectCharacters"].Value);
        RuleFor(x => x.LastName).MinimumLength(2).WithMessage(string.Format(localizer["HasMinLengthOf"].Value, 2));
        RuleFor(x => x.LastName).MaximumLength(50).WithMessage(string.Format(localizer["HasMaxLengthOf"].Value, 50));

        RuleFor(x => x.PersonalNumber).NotEmpty().WithMessage(localizer["RequiredField"].Value);
        RuleFor(x => x.PersonalNumber).Length(11).WithMessage(string.Format(localizer["HasFixedLengthOf"].Value, 11));
        RuleFor(x => x.PersonalNumber).Matches("^[0-9]{11}$").WithMessage(localizer["OnlyDigits"].Value);

        RuleFor(x => x.Gender).IsInEnum().WithMessage(localizer["InvalidGenderType"]);

        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(localizer["RequiredField"].Value);
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.Now.Date.AddYears(-18)).WithMessage(string.Format(localizer["HasMinAgeRestrictionOf"].Value, 18));
    }
}
