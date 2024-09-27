using GMS.Service.Dtos;
using GMS.Service.Dtos.Packages;
using GMS.Service.Services.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : BaseController
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPackage([FromBody] PackageDto packageDto)
        {
            var response = await _packageService.AddPackageAsync(packageDto);
            return ReturnFormattedResponse(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePackage(Guid id, [FromBody] PackageDto packageDto)
        {
            var response = await _packageService.UpdatePackageAsync(id, packageDto);
            return ReturnFormattedResponse(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePackage(Guid id)
        {
            var response = await _packageService.DeletePackageAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPackageById(Guid id)
        {
            var response = await _packageService.GetPackageByIdAsync(id);
            return ReturnFormattedResponse(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPackages([FromQuery] PackageParametersDto queryParameters)
        {
            var response = await _packageService.GetAllPackageAsync(queryParameters);
            return ReturnFormattedResponse(response);
        }
    }
}
