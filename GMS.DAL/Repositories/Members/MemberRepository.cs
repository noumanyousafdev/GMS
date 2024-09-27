using GMS.Data;
using GMS.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using GMS.Models.Entities;
namespace GMS.DAL.Repositories.Members
{
    public class MemberRepository : GenericRepository<Member> , IMemberRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MemberRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }
        }
}
