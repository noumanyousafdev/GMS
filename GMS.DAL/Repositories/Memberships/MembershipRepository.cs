using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Memberships
{
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly ApplicationDbContext dbContext;
        public MembershipRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
