using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract.Abstractions.Shared;
public class Result
{
    public bool IsSuccess { get; set; }
    public bool IsFailure=> !IsSuccess;
    public Error Error { get; set; }
    protected internal Result(bool issuccess, Error error)
    {
        IsSuccess = issuccess;
        Error = error;
    }
    public static Result Failure (Error error) => new (false, error);
    public static Result<TValue> Failure<TValue>(Error error) => new(false, error, default);
    public static Result Success ()=> new (true,Error.None);
    public static Result<TValue> Success<TValue>(TValue value) => new(true, Error.None, value);
}
