using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Application.Usecases.V1.Queries.Product;
public class ProductModel<TKey>
{
    public TKey Id { get; set; }
    public string ProductName { get; set; }
}
