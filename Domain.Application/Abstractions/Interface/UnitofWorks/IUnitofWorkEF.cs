using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Application.Abstractions.Interface.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Application.Abstractions.Interface.UnitofWorks;
public interface IUnitofWorkEF: IDisposable
{
    
    public IProductRepository ProductRepository { get; }
    //Start the database Transaction
    public IExecutionStrategy CreateStrategy();
    Task CreateTransaction();
    //Commit the database Transaction
    Task CommitAsync();
    //Rollback the database Transaction
    Task RollbackAsync();
    //DbContext Class SaveChanges method
    Task Save();
    //DbContext Class SaveChanges method
    Task SaveAsync();
}
