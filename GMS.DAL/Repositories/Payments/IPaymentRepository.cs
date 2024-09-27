using GMS.GenericRepository;
using GMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.DAL.Repositories.Payments
{
    public interface IPaymentRepository : IGenericRepository<Payment>
    {
    }
}
