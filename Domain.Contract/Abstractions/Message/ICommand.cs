using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Abstractions.Shared;
using MassTransit.Topology;
using MediatR;

namespace Domain.Contract.Abstractions.Message;
[ExcludeFromTopology]
public interface ICommand: IRequest<Result>
{
}
[ExcludeFromTopology]
public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}
