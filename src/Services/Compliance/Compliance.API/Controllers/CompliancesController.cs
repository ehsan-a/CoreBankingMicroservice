using Compliance.Application.DTOs;
using Compliance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Compliance.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompliancesController : ControllerBase
    {
        private readonly IComplianceService _complianceService;

        public CompliancesController(IComplianceService complianceService)
        {
            _complianceService = complianceService;
        }

        [HttpGet("{nationalCode}")]
        //[Authorize(Policy = "Accessibility")]
        public async Task<ActionResult<ComplianceResponseDto>> GetCompliance(string nationalCode, CancellationToken cancellationToken)
        {
            return Ok(await _complianceService.GetInquiryAsync(nationalCode, cancellationToken));
        }

        [HttpPost]
        //[Authorize(Policy = "Accessibility")]
        public async Task<ActionResult<RegisteredComplianceResponseDto>> PostCompliances(CreateComplianceRequest request, CancellationToken cancellationToken)
        {
            var result = await _complianceService.CreateAsync(request, User, cancellationToken);

            return CreatedAtAction("GetCompliance", new { nationalCode = request.NationalCode }, result);
        }
    }
}
