using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Shared;
using MediatR;

namespace Domain.Contract.Abstractions.Message;
public interface ICommandHandler<TCommand>: IRequestHandler<TCommand, Result> where TCommand : ICommand
{
    
}
public interface ICommandHandler<TCommand, TReponse> : IRequestHandler<TCommand, Result<TReponse>> 
    where TCommand : ICommand<TReponse>
{

}
