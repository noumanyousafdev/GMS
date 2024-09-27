using GMS.DAL.Repositories.Members;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Attendances
{
    public class AttendanceRepository : GenericRepository<Attendance>, IAttendanceRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AttendanceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
