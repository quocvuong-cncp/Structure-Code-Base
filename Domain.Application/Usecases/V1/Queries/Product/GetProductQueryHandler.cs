using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Contract.Abstractions.Message;
using Domain.Contract.Abstractions.Shared;
using Microsoft.EntityFrameworkCore;

namespace Domain.Application.Usecases.V1.Queries.Product;
public sealed class  GetProductQueryHandler : IQueryHandler<GetProductQuery, int>
{
    private readonly IUnitofWorkEF _unitofWorkEF;

    public GetProductQueryHandler(IUnitofWorkEF unitofWorkEF)
    {
        _unitofWorkEF = unitofWorkEF;
    }
    public async Task<Result<int>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        Task<Result<int>> task = new Task<Result<int>>(() =>
        {
            return Result.Success<int>(10000000);
        });
        task.Start();
        var a = await task;
        var data = await _unitofWorkEF.ProductRepository.FindAll().ToListAsync();
        return a;
    }
}
