using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract.Services.Product.DomainEvent;
using Domain.Domain.Abstractions.Entities;

namespace Domain.Domain.Entities.DomainEntities;
public class Product: BaseEntities<Guid>
{
    public string ProductName { set; get; }
    public Product( Guid id,string productName )
    {
        Id = id;
        ProductName = productName;
    }
    public static Product CreateProduct(Guid id, string productName)
    {
        var product = new Product(id, productName);
        product.RaiseDomainEvent(new DomainEvent.ProductCreated(id, id, productName));
        return product;
    }
    public  void UpdateProduct(Guid id, string productName)
    {
        Id = id;
        ProductName = productName;
        RaiseDomainEvent(new DomainEvent.ProductUpdated(Id, Id, productName));
    }
    public void  DeleteProduct(Guid id)
    {
        RaiseDomainEvent(new DomainEvent.ProductDeleted(id, id));

    }

}
