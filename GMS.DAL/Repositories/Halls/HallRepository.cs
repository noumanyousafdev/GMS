using GMS.DAL.Repositories.Attendances;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Halls
{
    public class HallRepository : GenericRepository<Hall>, IHallRepository
    {
        private readonly ApplicationDbContext dbContext;
        public HallRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
