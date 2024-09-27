using GMS.Service.Dtos;
using GMS.Service.Dtos.MemberShips;
using GMS.Service.Dtos.Payment;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Payments
{
    public interface IPaymentService
    {
        Task<ServiceResponse<PaymentResponseDto>> CreatePaymentAsync(PaymentCreateDto paymentCreateDto);
        Task<ServiceResponse<PaymentResponseDto>> UpdatePaymentAsync(Guid id, PaymentCreateDto paymentCreateDto);
        Task<ServiceResponse<bool>> DeletePaymentAsync(Guid id);  
        Task<ServiceResponse<PaymentResponseDto>> GetPaymentByIdAsync(Guid id);
        Task<ServiceResponse<IEnumerable<PaymentResponseDto>>> GetAllPaymentAsync(PaymentParametersDto queryParameters);
    }
}
