using AutoMapper;
using MasterBooking.BLL.DTO.AvailabilityDto;
using MasterBooking.BLL.DTO.BlockedSlotsDto;
using MasterBooking.BLL.DTO.ReviewsDto;
using MasterBooking.BLL.DTO.ServicesDto;
using MasterBooking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterBooking.BLL.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service, ServiceDto>();

            CreateMap<CreateServiceDto, Service>();

            CreateMap<UpdateServiceDto, Service>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<Availability, AvailabilityDto>();
            CreateMap<CreateAvailabilityDto, Availability>();
            CreateMap<Review, ReviewDto>();
            CreateMap<CreateReviewDto, Review>();
            CreateMap<BlockedSlot, BlockedSlotDto>();
            CreateMap<CreateBlockedSlotDto, BlockedSlot>();
        }
    }
}
