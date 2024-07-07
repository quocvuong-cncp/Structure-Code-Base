using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Entities.DomainEntities;
public class Product:BaseEntities<int>
{
    public string ProductName { set; get; }
}
