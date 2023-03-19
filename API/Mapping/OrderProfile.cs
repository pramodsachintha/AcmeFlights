using API.Application.Commands;
using API.Application.Dto;
using API.Application.ViewModels;
using AutoMapper;
using Domain.Aggregates.OrderAggregate;

namespace API.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDraftDto>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(s => s.GetTotal()))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(s => s.OrderStatus.Name));


            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(s => s.GetUnitPrice()))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(s => s.GetUnits()))
                .ForMember(dest => dest.OriginAirport, opt => opt.MapFrom(s => s.GetOriginAirport()))
                .ForMember(dest => dest.DestinationAirport, opt => opt.MapFrom(s => s.GetDestinationAirport()));

            CreateMap<Order, OrderViewModel>()
               .ForMember(dest => dest.Total, opt => opt.MapFrom(s => s.GetTotal()))
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(s => s.Id));

            CreateMap<OrderItem, OrderItemViewModel>()
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(s => s.GetUnitPrice()))
                .ForMember(dest => dest.Units, opt => opt.MapFrom(s => s.GetUnits()))
                .ForMember(dest => dest.OriginAirport, opt => opt.MapFrom(s => s.GetOriginAirport()))
                .ForMember(dest => dest.DestinationAirport, opt => opt.MapFrom(s => s.GetDestinationAirport()))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(s => s.GetItemDescription()));

            CreateMap<BookingItem, OrderItemDto>();
        }
    }
}
