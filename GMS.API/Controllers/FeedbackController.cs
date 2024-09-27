using GMS.Service.Dtos;
using GMS.Service.Dtos.Feedback;
using GMS.Service.Dtos.Feedbacks;
using GMS.Service.Services.Feedbacks;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromBody] FeedbackDto feedbackDto)
        {
            var response = await _feedbackService.CreateFeedbackAsync(feedbackDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateFeedback( [FromBody] UpdateFeedback requestDto)
        {
            var response = await _feedbackService.UpdateFeedbackAsync(requestDto);
            return ReturnFormattedResponse(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(Guid Id)
        {
            var response = await _feedbackService.DeleteFeedbackAsync(Id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedbackById(Guid id)
        {
            var response = await _feedbackService.GetFeedbackByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback([FromQuery] FeedbackParametersDto queryParameters)
        {
            var response = await _feedbackService.GetAllFeedbackAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
