using GMS.DAL.Repositories.Payments;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Trainers
{
    public class TrainerRepository : GenericRepository<Trainer>, ITrainerRepository
    {
        private readonly ApplicationDbContext dbContext;
        public TrainerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
