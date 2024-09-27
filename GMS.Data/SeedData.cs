using GMS.Models;
using GMS.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Data
{
    public static class SeedData
    {
        public static async Task SeedInitial(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Seed Roles if they don't exist
            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "5bd1711f-38cc-47ce-960b-242d82b86a18",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                },
                new IdentityRole
                {
                    Id = "7c1a9a70-8c4b-4c99-8d09-5e4c8b4e3e15",
                    Name = "Member",
                    NormalizedName = "MEMBER",
                },
                new IdentityRole
                {
                    Id = "8a7b1c90-19b6-4e72-b6c9-5a1c8c8e1f26",
                    Name = "Trainer",
                    NormalizedName = "TRAINER",
                }
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role.Name))
                {
                    await roleManager.CreateAsync(role);
                }
            }

            // Seed RoomTypes if they don't exist
            if (!context.RoomTypes.Any())
            {
                context.RoomTypes.AddRange(
                    new RoomType
                    {
                        Id = Guid.Parse("f88f18e6-f00d-49b5-96e8-77913af49974"),
                        Name = "Gym Room",
                        Description = "Room equipped for physical exercises."
                    },
                    new RoomType
                    {
                        Id = Guid.Parse("4d8f3cc4-b24c-48e2-b3ff-3072b27f6563"),
                        Name = "SPA",
                        Description = "Relaxation and wellness room."
                    }
                );
                await context.SaveChangesAsync();
            }

            // Seed Halls if they don't exist
            if (!context.Halls.Any())
            {
                context.Halls.AddRange(
                    new Hall
                    {
                        Id = Guid.NewGuid(),
                        Name = "Main Hall",
                        Capacity = "100",
                        Location = "Building A",
                        AvailabilityStatus = true,
                        RoomTypeId = Guid.Parse("f88f18e6-f00d-49b5-96e8-77913af49974")
                    }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
