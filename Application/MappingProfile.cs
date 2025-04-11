using Application.CQRS.Notifications;
using Application.CQRS.ProductCommandQuery.Command;
using Application.CQRS.ProductCommandQuery.Query;
using AutoMapper;
using Core.Entities;
using Infrastructure.Dto;
using Infrastructure.Utility;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       
        CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => src.Price.ToString("N0"))); 
        
        CreateMap<ProductDto, Product>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => long.Parse((src.PriceWithComma ?? "0").Replace(",", ""))));
        //CreateMap<SaveProductCommand, Product>()
        //    .ForMember(dest => dest.Thumbnail,opt=>opt.MapFrom(src=> FileUtility.ConvertToByteArray(src.Thumbnail)))

        CreateMap<Product, GetProductQueryResponse>()
            .ForMember(dest => dest.PriceWithComma, opt => opt.MapFrom(src => src.Price.ToString("N0")))
            .ForMember(dest=>dest.Title, opt=>opt.MapFrom(src =>src.ProductName));

        CreateMap<AddRefreshTokenNotification, UserRefreshToken>()
             .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(src => DateTime.UtcNow))
             .ForMember(dest=> dest.IsValid,opt => opt.MapFrom(src=>true ));
    }
}