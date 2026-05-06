using AutoMapper;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;
using MVCProject.ITI.ViewModels.Settings;

namespace MVCProject.ITI.Mapper
{
    public class DomainProfile:Profile
    {
        public DomainProfile()
        {
            CreateMap<Trip, TripCardViewModel>().
                ForMember(dst => dst.TripTotalCost, options => options.MapFrom(src => src.TripCostResult.TotalCost))
                .ForMember(dst=>dst.VehicleName,options=>options.MapFrom(src=>src.Vehicle.NickName))
                .ForMember(dst => dst.TripDate,options=>options.MapFrom(src=>src.TripDate.Date))
                .ReverseMap();

            CreateMap<ApplicationUser, ProfileViewModel>()
                .ForMember(dst => dst.Email, opt => opt.MapFrom(src => src.Email ?? string.Empty))
                .ReverseMap();

            CreateMap<VehicleInfoViewModel, Vehicle>()
                .ForMember(dst => dst.NickName, opt => opt.MapFrom(src => src.CarNickname))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.VehicleId));

            CreateMap<VehicleInfoViewModel, CarModel>()
                .ForMember(dst => dst.Make, opt => opt.MapFrom(src => src.Make))
                .ForMember(dst => dst.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dst => dst.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dst => dst.FuelType, opt => opt.MapFrom(src => src.FuelType))
                .ForMember(dst => dst.WltpMixed, opt => opt.MapFrom(src => src.WltpMixed));

            CreateMap<Trip, FavoriteTripViewModel>()
                .ForMember(dst => dst.FuelType, opt => opt.MapFrom(src => src.Vehicle.CarModel != null ? src.Vehicle.CarModel.FuelType : FuelTypeEnum.Gasoline))
                .ReverseMap();
        }
    }
}
