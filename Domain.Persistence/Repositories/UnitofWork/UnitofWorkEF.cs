using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Domain.Abstractions.Interface.Repositories;
using Domain.Domain.Abstractions.Interface.UnitofWorks;
using Domain.Domain.Abstractions.Repositories;
using Domain.Domain.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;
using static Domain.Persistence.Repositories.UnitofWork.UnitofWorkEF;

namespace Domain.Persistence.Repositories.UnitofWork;
public class UnitofWorkEF: IUnitofWorkEF
{
    private IDbContextTransaction _objTran;
    private readonly ApplicationDBContext _context;
    public IProductRepository ProductRepository { get;  }
    public IEventProjectRepository EventProjectRepository { get; }

    public UnitofWorkEF(ApplicationDBContext Context, IProductRepository productRepository, IEventProjectRepository eventProjectRepository)
    {
        _context = Context;
        ProductRepository = productRepository;
        EventProjectRepository = eventProjectRepository;
    } 
    public IExecutionStrategy CreateStrategy()
    {
        return _context.Database.CreateExecutionStrategy();
    }

        public async void Dispose()
        {
        await _context.DisposeAsync();
        GC.SuppressFinalize(this);
        }
        public async Task CreateTransaction()
        {
            _objTran = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitAsync()
        {
            await _objTran.CommitAsync();
            _objTran.Dispose();
        }
        public async Task RollbackAsync()
        {

            await _objTran.RollbackAsync();

            await _objTran.DisposeAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task Save()
        {
            _context.SaveChanges();
        }



}
