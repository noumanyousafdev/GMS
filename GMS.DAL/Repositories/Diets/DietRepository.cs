using GMS.DAL.Repositories.Attendances;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Diets
{
    public class DietRepository : GenericRepository<Diet>, IDietRepository
    {
        private readonly ApplicationDbContext dbContext;
        public DietRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
