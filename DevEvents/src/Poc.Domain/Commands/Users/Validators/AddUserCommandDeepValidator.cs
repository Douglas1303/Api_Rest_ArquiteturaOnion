using FluentValidation;
using Infra.CrossCutting.Core.IPipelineBehavior;
using Microsoft.Extensions.Localization;
using Poc.Domain.Commands.BaseValidators;
using Poc.Domain.Interface.Repository;
using Poc.Domain.Resources;

namespace Poc.Domain.Commands.Users.Validators
{
    internal class AddUserCommandDeepValidator : BaseValidator<AddUserCommand, AddUserRsc>, IDeepValidator<AddUserCommand>
    {
        private const string NameInvalidError = "NameInvalidError";
        private readonly IUserRepository _userRepository;

        public AddUserCommandDeepValidator(IStringLocalizer<AddUserRsc> localizer, IUserRepository userRepository) : base(localizer)
        {
            _userRepository = userRepository;
            Validations();
        }

        private void Validations()
        {
            RuleFor(x => x.NomeCompleto)
             .Must(CheckUserExists)
             .WithErrorCode(NameInvalidError)
             .WithMessage(x => GetMessage(NameInvalidError));
        }

        private bool CheckUserExists(string userName)
        {
            return _userRepository.UserExists(userName);
        }
    }
}