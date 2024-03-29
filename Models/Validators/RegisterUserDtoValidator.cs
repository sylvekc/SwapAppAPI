﻿using FluentValidation;
using SwapApp.Entities;

namespace SwapApp.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {

        public RegisterUserDtoValidator(ItemDbContext dbContext)
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password).MinimumLength(8);
            RuleFor(x=>x.ConfirmPassword).Equal(e=>e.Password).WithMessage("Passwords doesn't match");
            RuleFor(x => x.Email).Custom((value, context) =>
              {
                  var emailInUse = dbContext.Users.Any(u => u.Email == value);
                  if (emailInUse)
                  {
                      context.AddFailure("Email", "That email is taken");
                  }
              });
        }
    }
}
