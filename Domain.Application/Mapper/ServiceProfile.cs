using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Application.Usecases.V1.Queries.Product;
using Domain.Domain.Entities.DomainEntities;

namespace Domain.Application.Mapper;
public class ServiceProfile:Profile
{
    public ServiceProfile()
    {
        CreateMap<Product, ProductModel<Guid>>().ReverseMap();
    }

}
