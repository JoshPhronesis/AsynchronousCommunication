using AutoMapper;
using Orders.Api.Dtos;
using Orders.Api.Entities;

namespace Orders.Api;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Order, OrderForCreateDto>().ReverseMap();
        CreateMap<Order, OrderForUpdateDto>().ReverseMap();
        CreateMap<Order, OrderForReturnDto>().ReverseMap();
    }
}