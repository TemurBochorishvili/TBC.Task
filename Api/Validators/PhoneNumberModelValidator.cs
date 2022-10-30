using Api.Controllers.Resources;
using Api.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

namespace Api.Validators;


public class PhoneNumberModelValidator : AbstractValidator<PhoneNumberResource>
{

    public PhoneNumberModelValidator(IStringLocalizer<Errors> localizer)
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage(localizer["InvalidPhoneType"]);
        RuleFor(x => x.Number).MinimumLength(4).WithMessage(string.Format(localizer["HasMinLengthOf"].Value, 4));
        RuleFor(x => x.Number).MaximumLength(50).WithMessage(string.Format(localizer["HasMaxLengthOf"].Value, 50));

    }
}
