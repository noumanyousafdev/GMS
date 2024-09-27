using GMS.Service.Dtos;
using GMS.Service.Dtos.Attendances;
using GMS.Service.Dtos.Feedback;
using GMS.Service.Dtos.Feedbacks;
using GMS.Service.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS.Service.Services.Feedbacks
{
    public interface IFeedbackService
    {
        Task<ServiceResponse<FeedbackDto>> CreateFeedbackAsync(FeedbackDto feedbackDto);
        Task<ServiceResponse<UpdateFeedback>> UpdateFeedbackAsync(UpdateFeedback feedbackDto);
        Task<ServiceResponse<bool>> DeleteFeedbackAsync(Guid feedbackId);
        Task<ServiceResponse<FeedbackDto>> GetFeedbackByIdAsync(Guid feedbackId);
        Task<ServiceResponse<IEnumerable<FeedbackDto>>> GetAllFeedbackAsync(FeedbackParametersDto queryParameters);
    }
}
