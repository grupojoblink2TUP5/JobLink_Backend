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

        [HttpGet]
        public IActionResult GetAll()
        {
            var companies = _companyService.GetAllCompanies();

            return Ok(companies);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        {


            var company = _companyService.GetCompanyById(id);


            return Ok(company);


        }

        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] CreateCompanyRequest request)
        {
            var result = _companyService.CreateCompany(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] UpdateCompanyRequest request
        )
        {
            try
            {
                var updatedCompany = _companyService.UpdateCompany(id, request);

                return Ok(updatedCompany);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:int}/approve")]
        public IActionResult Approve([FromRoute] int id)
        {
            try
            {
                var approvedCompany = _companyService.ApproveCompany(id);

                return Ok(approvedCompany);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPatch("{id:int}/reject")]
        public IActionResult Reject([FromRoute] int id)
        {
            try
            {
                var rejectedCompany = _companyService.RejectCompany(id);

                return Ok(rejectedCompany);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var deleted = _companyService.DeleteCompany(id);

            if (!deleted)
            {
                return NotFound($"Company with id {id} not found");
            }

            return NoContent();
        }
    }
}