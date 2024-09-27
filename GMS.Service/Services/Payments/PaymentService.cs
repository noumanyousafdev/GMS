using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMS.DAL;
using GMS.DAL.Repositories.Payments;
using GMS.Data;
using GMS.GenericRepository;
using GMS.Models.Entities;
using GMS.Service.Dtos;
using GMS.Service.Dtos.Payment;
using GMS.Service.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext, IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<PaymentResponseDto>> CreatePaymentAsync(PaymentCreateDto paymentCreateDto)
        {
            try
            {
                // Map DTO to entity
                var payment = _mapper.Map<Payment>(paymentCreateDto);

                // Add the new payment
                 _paymentRepository.Add(payment);
                await _paymentRepository.SaveAsync();

                // Map entity back to DTO
                var paymentDto = _mapper.Map<PaymentResponseDto>(payment);
                return ServiceResponse<PaymentResponseDto>.ReturnResultWith201(paymentDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PaymentResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<PaymentResponseDto>> UpdatePaymentAsync(Guid id, PaymentCreateDto paymentCreateDto)
        {
            try
            {
                var payment = await _paymentRepository.FindAsync(id);
                if (payment == null)
                {
                    return ServiceResponse<PaymentResponseDto>.Return404("Payment not found.");
                }

                // Map updated properties
                _mapper.Map(paymentCreateDto, payment);

                // Update the payment
                _paymentRepository.Update(payment);
                await _paymentRepository.SaveAsync();

                var paymentDto = _mapper.Map<PaymentResponseDto>(payment);
                return ServiceResponse<PaymentResponseDto>.ReturnResultWith200(paymentDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PaymentResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<bool>> DeletePaymentAsync(Guid id)
        {
            try
            {
                var payment = await _paymentRepository.FindAsync(id);
                if (payment == null)
                {
                    return ServiceResponse<bool>.Return404("Payment not found.");
                }

                _paymentRepository.Remove(payment);
                await _paymentRepository.SaveAsync();

                return ServiceResponse<bool>.ReturnResultWith204();
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<PaymentResponseDto>> GetPaymentByIdAsync(Guid id)
        {
            try
            {
                var payment = await _paymentRepository.FindAsync(id);
                if (payment == null)
                {
                    return ServiceResponse<PaymentResponseDto>.Return404("Payment not found.");
                }

                var paymentDto = _mapper.Map<PaymentResponseDto>(payment);
                return ServiceResponse<PaymentResponseDto>.ReturnResultWith200(paymentDto);
            }
            catch (Exception ex)
            {
                return ServiceResponse<PaymentResponseDto>.ReturnException(ex);
            }
        }

        public async Task<ServiceResponse<IEnumerable<PaymentResponseDto>>> GetAllPaymentAsync(PaymentParametersDto queryParameters)
        {
            try
            {
                // Start: Retrieve payments
                IQueryable<Payment> paymentQuery = _dbContext.Payments; // Direct access to DbSet

                // Filtering
                if (!string.IsNullOrWhiteSpace(queryParameters.PaymentMethod))
                {
                    paymentQuery = paymentQuery.Where(p => p.PaymentMethod.Contains(queryParameters.PaymentMethod));
                }

                if (queryParameters.PaymentDate.HasValue)
                {
                    paymentQuery = paymentQuery.Where(p => p.PaymentDate.Date == queryParameters.PaymentDate.Value.Date);
                }

                if (!string.IsNullOrWhiteSpace(queryParameters.PaymentStatus))
                {
                    paymentQuery = paymentQuery.Where(p => p.PaymentStatus.Contains(queryParameters.PaymentStatus));
                }

                // Pagination and Sorting
                var totalItems = await paymentQuery.CountAsync(); // Get total count before paging
                var paginatedPayments = await paymentQuery
                    .ProjectTo<PaymentResponseDto>(_mapper.ConfigurationProvider) // Use AutoMapper projection
                    .OrderByCustom(queryParameters.SortBy, queryParameters.IsAscending) // Custom sorting
                    .Paginate(queryParameters.PageNumber, queryParameters.PageSize) // Paginate the results
                    .ToListAsync();

                // Return success response
                return ServiceResponse<IEnumerable<PaymentResponseDto>>.ReturnResultWith200(paginatedPayments);
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<PaymentResponseDto>>.ReturnException(ex);
            }
        }
    }
}
