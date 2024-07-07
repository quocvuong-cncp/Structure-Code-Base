
using Domain.Application.Abstractions.Interface.UnitofWorks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
namespace Domain.Application.Behaviors;

public sealed class TransactionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IUnitofWorkEF _unitOfWork; // SQL-SERVER-STRATEGY-2
    //private readonly ApplicationDbContext _context; // SQL-SERVER-STRATEGY-1

    public TransactionPipelineBehavior(IUnitofWorkEF unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand()) // In case TRequest is QueryRequest just ignore
            return await next();

        #region ============== SQL-SERVER-STRATEGY-1 ==============

        //// Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
        //// https://learn.microsoft.com/ef/core/miscellaneous/connection-resiliency
        var strategy = _unitOfWork.CreateStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var response = await next();
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitAsync();
                return response;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }

        });
        #endregion ============== SQL-SERVER-STRATEGY-1 ==============

        #region ============== SQL-SERVER-STRATEGY-2 ==============

        //IMPORTANT: passing "TransactionScopeAsyncFlowOption.Enabled" to the TransactionScope constructor. This is necessary to be able to use it with async/await.
        //await _unitOfWork.CreateTransaction();
        //var response = await next();
        //await _unitOfWork.CommitAsync();
        //return response;
        #endregion ============== SQL-SERVER-STRATEGY-2 ==============

    }

    private bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}
