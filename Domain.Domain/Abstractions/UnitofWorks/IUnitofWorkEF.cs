using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Interface.Repositories;
using Domain.Domain.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Domain.Domain.Abstractions.Interface.UnitofWorks;
public interface IUnitofWorkEF: IDisposable
{
    
     IProductRepository ProductRepository { get; }
     IEventProjectRepository EventProjectRepository { get; }
    //Start the database Transaction
     IExecutionStrategy CreateStrategy();
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
