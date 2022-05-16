﻿using FluentValidation;
using Photogram.Models;

namespace Photogram.Data
{
    public class RegisterValidator :AbstractValidator<Users>
    {
        public RegisterValidator(PhotogramDbContext dbcontext)
        {
            RuleFor(x=>x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .MinimumLength(8);
            RuleFor(x => x.Email)
               .Custom((value, context) =>
               {
                   var emailInUse = dbcontext.Users.Any(u => u.Email == value);
                   if (emailInUse)
                   {
                       context.AddFailure("Email", "That email is taken");
                   }
               });
        }
    }
}
