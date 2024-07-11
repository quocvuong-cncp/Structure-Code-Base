using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Abstractions.Entities;
public class Entity<T>: IEntity<T>
{
    public T Id { set; get; }
}
