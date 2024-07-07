using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Domain.Domain.Exceptions;
public  class ValidationException : DomainException
{
    public object Request { get; }
    public ValidationException(IReadOnlyCollection<ValidationError> errors, object request)
        : base("Validation Failure", "One or more validation errors occurred")
    {
        Errors = errors;
        Request = request;
        
    }

    public IReadOnlyCollection<ValidationError> Errors { get; }

}

public record ValidationError(string PropertyName, string ErrorMessage);
//public sealed class ValidationException<T> : ValidationException where T : IRequest
//{
//    public  T Request { get; }

//    public ValidationException(IReadOnlyCollection<ValidationError> errors, T request): base(errors)
//    {
//        Request = request;
//    }
//}
