using BookStore.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Validation
{
    public class CreateAuthorValidatior : AbstractValidator<AuthorForCreationDTO>
    {
        public CreateAuthorValidatior()
        {
            RuleFor(b => b.Name)
             .NotEmpty().WithMessage("You should provide name value for an author.")
             .Length(3, 100).WithMessage("Name should have from 3 to 100 characters.");
       
        }

    }
}
