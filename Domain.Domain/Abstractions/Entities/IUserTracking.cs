using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Abstractions.Entities;
public interface IUserTracking<T>
{
    public T? CreateBy { get; set; }
    public T? UpdateBy { get; set; }
}
