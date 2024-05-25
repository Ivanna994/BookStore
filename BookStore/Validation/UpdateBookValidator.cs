using BookStore.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Validation
{
    public class UpdateBookValidator: AbstractValidator<BookForUpdateDTO>
    {
        public UpdateBookValidator()
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("You should provide a title value for book.")               
                .Length(3, 100).WithMessage("Title should have from 3 to 100 characters.");

            RuleFor(p => p.SubTitle)
                .Length(3, 100).WithMessage("SubTitle should have from 3 to 100 characters.");
        }
    }
}
