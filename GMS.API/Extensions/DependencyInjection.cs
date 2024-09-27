using AutoMapper;
using GMS.DAL.Repositories.Attendances;
using GMS.DAL.Repositories.Diets;
using GMS.DAL.Repositories.Feedbacks;
using GMS.DAL.Repositories.Halls;
using GMS.DAL.Repositories.InventoryItems;
using GMS.DAL.Repositories.Measurements;
using GMS.DAL.Repositories.Members;
using GMS.DAL.Repositories.Memberships;
using GMS.DAL.Repositories.Packages;
using GMS.DAL.Repositories.Payments;
using GMS.DAL.Repositories.Room;
using GMS.DAL.Repositories.Trainers;
using GMS.DAL.Repositories.Workouts;
using GMS.GenericRepository;
using GMS.Service.Services.Attendances;
using GMS.Service.Services.Authentication;
using GMS.Service.Services.Diets;
using GMS.Service.Services.Feedbacks;
using GMS.Service.Services.Halls;
using GMS.Service.Services.Inventory;
using GMS.Service.Services.Mapping;
using GMS.Service.Services.Measurements;
using GMS.Service.Services.Members;
using GMS.Service.Services.Memberships;
using GMS.Service.Services.Packages;
using GMS.Service.Services.Payments;
using GMS.Service.Services.Rooms;
using GMS.Service.Services.Trainers;
using GMS.Service.Services.Workouts;
using System.ComponentModel.Design;

namespace GMS.API.Extensions
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            // Register All Services


            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IDietService, DietService>();
            services.AddScoped<IRoomTypeService, RoomTypeService>();
            services.AddScoped<IHallService, HallService>();
            services.AddScoped<ITrainerService, TrainerService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<IPackageService, PackageService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IPaymentService , PaymentService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IWorkoutService, WorkoutService>();
            services.AddScoped<IMeasurementService, MeasurementService>();
            services.AddScoped<IInventoryService , InventoryService>();
            services.AddScoped<IAttendanceService , AttendanceService>();



            services.AddLogging();



        }

        public static void AddRepos(this IServiceCollection services)
        {
            // Register all repositories
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<ITrainerRepository, TrainerRepository>();
            services.AddScoped<IMembershipRepository, MembershipRepository>();
            services.AddScoped<IWorkoutRepository, WorkoutRepository>();
            services.AddScoped<IDietRepository, DietRepository>();
            services.AddScoped<IHallRepository, HallRepository>();
            services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
            services.AddScoped<IMeasurementRepository, MeasurementRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IPackageRepository, PackageRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();

           
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            // Manually configure AutoMapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile()); // Register your profiles here
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper); // Register IMapper as singleton
        }
    }
}
