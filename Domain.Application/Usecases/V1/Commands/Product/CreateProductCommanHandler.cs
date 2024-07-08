using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Application.Usecases.V1.Queries.Product;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Abstractions.Shared;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Domain.Entities.DomainEntities;

namespace Domain.Application.Usecases.V1.Commands.Product;
public sealed class CreateProductCommanHandler : ICommandHandler<CreateProductCommand, ProductModel<Guid>>
{
    private readonly IMapper _mapper;
    private readonly IUnitofWorkEF _unitofWorkEF;

    public CreateProductCommanHandler(IUnitofWorkEF unitofWorkEF,IMapper mapper)
    {
        _mapper = mapper;
        _unitofWorkEF = unitofWorkEF;
    }
    public async Task<Result<ProductModel<Guid>>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Domain.Entities.DomainEntities.Product.CreateProduct(Guid.NewGuid(), "Nuoc");
        var status = await _unitofWorkEF.ProductRepository.AddAsync(product);
        return new Result<ProductModel<Guid>>(status, null, _mapper.Map<ProductModel<Guid>>(product));
    }
}
