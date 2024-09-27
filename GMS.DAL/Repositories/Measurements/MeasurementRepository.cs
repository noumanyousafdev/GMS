using GMS.DAL.Repositories.InventoryItems;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Measurements
{
    public class MeasurementRepository : GenericRepository<Measurement>, IMeasurementRepository
    {
        private readonly ApplicationDbContext dbContext;
        public MeasurementRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
