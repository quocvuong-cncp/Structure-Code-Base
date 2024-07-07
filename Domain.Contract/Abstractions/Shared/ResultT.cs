using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract.Abstractions.Shared;
public class Result<TValue> : Result
{
    //public TValue Value => IsSuccess ? _value: throw new Exception("This transaction don't successful!", new Exception());
    public TValue Value;
    public Result(bool issuccess, Error error, TValue value) : base(issuccess, error)
    {
        Value = value;
    }


}
