using GMS.DAL.Repositories.Trainers;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Workouts
{
    public class WorkoutRepository : GenericRepository<Workout>, IWorkoutRepository
    {
        private readonly ApplicationDbContext dbContext;
        public WorkoutRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}