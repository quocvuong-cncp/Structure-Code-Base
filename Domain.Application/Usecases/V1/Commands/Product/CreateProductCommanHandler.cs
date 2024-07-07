using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Application.Abstractions.Interface.UnitofWorks;
using Domain.Application.Usecases.V1.Queries.Product;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Abstractions.Shared;

namespace Domain.Application.Usecases.V1.Commands.Product;
public sealed class CreateProductCommanHandler : ICommandHandler<CreateProductCommand, ProductModel<int>>
{
    private readonly IMapper _mapper;
    private readonly IUnitofWorkEF _unitofWorkEF;

    public CreateProductCommanHandler(IUnitofWorkEF unitofWorkEF,IMapper mapper)
    {
        _mapper = mapper;
        _unitofWorkEF = unitofWorkEF;
    }
    public async Task<Result<ProductModel<int>>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var data = new Domain.Entities.DomainEntities.Product() { ProductName = request.ProductName, CreateBy="Vuong", CreateDate=DateTime.Now };
        var data1 = new Domain.Entities.DomainEntities.Product() { ProductName = request.ProductName, CreateBy = "Vuong", CreateDate = DateTime.Now};
        var isStatus = await _unitofWorkEF.ProductRepository.AddAsync(data);
        throw new Exception("kkk");
        var isStatus1 = await _unitofWorkEF.ProductRepository.AddAsync(data1);

        return new Result<ProductModel<int>>(isStatus, null, _mapper.Map<ProductModel<int>>(data));
    }
}
