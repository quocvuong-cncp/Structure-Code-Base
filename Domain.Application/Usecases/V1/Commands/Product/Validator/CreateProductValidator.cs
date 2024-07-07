using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Application.Usecases.V1.Queries.Product;
using FluentValidation;

namespace Domain.Application.Usecases.V1.Commands.Product.Validator;
public class CreateProductValidator: AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(c => c.ProductName).NotNull();

    }
}
public class CreateProductValidator1 : AbstractValidator<CreateProductCommand>
{
    public CreateProductValidator1()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than zero.");
    }
}
public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
{
    public GetProductQueryValidator()
    {
        RuleFor(x => x.Quantity).GreaterThan(10).WithMessage("Quantity must be greater than zero.");
    }
}
