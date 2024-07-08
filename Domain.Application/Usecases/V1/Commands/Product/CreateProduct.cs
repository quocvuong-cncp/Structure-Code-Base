using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Domain.Application.Usecases.V1.Queries.Product;
using Domain.Contract.Abstractions.Message;
using MediatR;

namespace Domain.Application.Usecases.V1.Commands.Product;
public sealed class CreateProductCommand:ICommand<ProductModel<Guid>>
{
    public CreateProductCommand(Guid id, string name)
    {
        Id = id;
        ProductName = name; 
    }
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public static CreateProductCommand Create(Guid id, string productName) => new(id, productName);
}
