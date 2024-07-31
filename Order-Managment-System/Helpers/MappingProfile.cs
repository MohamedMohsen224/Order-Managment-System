using AutoMapper;
using Core.Models;
using Order_Managment_System.Dtos;

namespace Order_Managment_System.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ForMember(d=>d.CustomerName,o=>o.MapFrom(d=>d.Customer.Name));
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
