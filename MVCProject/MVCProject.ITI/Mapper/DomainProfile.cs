using AutoMapper;
using MVCProject.ITI.DataAccessLayer.Entities;
using MVCProject.ITI.Models;

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
        }
    }
}
