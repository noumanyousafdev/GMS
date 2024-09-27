using AutoMapper;
using GMS.Models.Entities;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Diets;
using GMS.Service.Dtos.Feedback;
using GMS.Service.Dtos.Feedbacks;
using GMS.Service.Dtos.Halls;
using GMS.Service.Dtos.Inventory;
using GMS.Service.Dtos.Measurements;
using GMS.Service.Dtos.Members;
using GMS.Service.Dtos.MemberShips;
using GMS.Service.Dtos.Packages;
using GMS.Service.Dtos.Payment;
using GMS.Service.Dtos.Room;
using GMS.Service.Dtos.Trainers;
using GMS.Service.Dtos.Workout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Member mappings
            CreateMap<Member, MemberResponseDto>();
            CreateMap<MemberCreateDto, Member>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Map UserName to Email

            // Trainer mappings
            CreateMap<Trainer, TrainerResponseDto>();
            CreateMap<TrainerCreateDto, Trainer>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)); // Map UserName to Email

            CreateMap<Hall, HallDto>().ReverseMap();

            CreateMap<RoomType, RoomTypeDto>().ReverseMap();

            CreateMap<Diet, DietDto>().ReverseMap();

            CreateMap<Package, PackageResponseDto>();
            CreateMap<PackageDto, Package>();

            CreateMap<Payment, PaymentResponseDto>();
            CreateMap<PaymentCreateDto, Payment>();

            CreateMap<Membership, MembershipResponseDto>();
            CreateMap<MembershipCreateDto, Membership>();


            CreateMap<Feedback, FeedbackDto>().ReverseMap();
            CreateMap<Workout, WorkoutDto>().ReverseMap();
            CreateMap<Attendance, AttendanceDto>().ReverseMap();
            CreateMap<Measurement, MeasurementDto>().ReverseMap();
            CreateMap<InventoryItem, InventoyItemDto>().ReverseMap();
            CreateMap<Measurement, UpdateMeasurement>().ReverseMap();
            CreateMap<Attendance, UpdateAttendance>().ReverseMap();
            CreateMap<Feedback, UpdateFeedback>().ReverseMap();
            CreateMap<Workout, UpdateWorkout>().ReverseMap();
        }
    }
}
