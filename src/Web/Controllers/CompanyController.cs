using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Application.DTOs.Company.Request;
using Microsoft.AspNetCore.Authorization;
using Domain.Exceptions;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _companyService
                    .GetAllAsync());
        }

        [Authorize(Roles = "Recruiter,Admin,Candidate")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var company = await _companyService.GetByIdAsync(id);

            return Ok(company);
        }


        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(
        CreateCompanyRequestDto request)
        {
            return Ok(
                await _companyService
                    .CreateAsync(request));
        }

        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
        int id,
        UpdateCompanyRequestDto request)
        {
            await _companyService
                .UpdateAsync(id, request);

            return NoContent();
        }


        [Authorize(Roles = "Recruiter,Admin")]
        [HttpPost("{id:int}/logo")]
        public async Task<IActionResult> UploadLogo(
        int id,
        IFormFile file)
        {
            using var stream =
                file.OpenReadStream();

            await _companyService
                .UploadLogoAsync(
                    id,
                    stream,
                    file.FileName);

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id)
        {
            await _companyService
                .DeleteAsync(id);

            return NoContent();
        }


        [Authorize(Roles = "Admin")]
        [HttpPatch("{id:int}/approve")]
        public async Task<IActionResult> Approve(int id, ApproveCompanyRequestDto request)
        {
            await _companyService
                .ApproveAsync(
                    id,
                    request.AdminId);

            return NoContent();
        }
    }
}