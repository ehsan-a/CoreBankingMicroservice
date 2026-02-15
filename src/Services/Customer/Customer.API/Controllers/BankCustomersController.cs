using Customer.Application.DTOs;
using Customer.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "Accessibility")]
    public class BankCustomersController : ControllerBase
    {
        private readonly IBankCustomerService _bankCustomerService;

        public BankCustomersController(IBankCustomerService bankCustomerService)
        {
            _bankCustomerService = bankCustomerService;
        }

        // GET: api/BankCustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankCustomerResponseDto>>> GetBankCustomers(CancellationToken cancellationToken)
        {
            var bankCustomers = await _bankCustomerService.GetAllAsync(cancellationToken);
            return Ok(bankCustomers);
        }

        // GET: api/BankCustomers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankCustomerResponseDto>> GetBankCustomer(Guid id, CancellationToken cancellationToken)
        {
            var bankCustomer = await _bankCustomerService.GetByIdAsync(id, cancellationToken);

            if (bankCustomer == null)
            {
                return NotFound();
            }

            return Ok(bankCustomer);
        }

        // PUT: api/BankCustomers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankCustomer(Guid id, UpdateBankCustomerRequest request, CancellationToken cancellationToken)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            try
            {
                //var customer = await _bankCustomerService.GetByIdAsync(id, cancellationToken);
                await _bankCustomerService.UpdateAsync(request, User, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BankCustomerExists(id, cancellationToken))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BankCustomers
        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<BankCustomerResponseDto>> PostBankCustomer(CreateBankCustomerRequest request, CancellationToken cancellationToken)
        {

            var bankCustomer = await _bankCustomerService.CreateAsync(request, User, cancellationToken);
            return CreatedAtAction("GetBankCustomer", new { id = bankCustomer.Id }, bankCustomer);
        }

        // DELETE: api/BankCustomers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankCustomer(Guid id, CancellationToken cancellationToken)
        {
            var bankCustomer = await _bankCustomerService.GetByIdAsync(id, cancellationToken);
            if (bankCustomer == null)
            {
                return NotFound();
            }

            await _bankCustomerService.DeleteAsync(bankCustomer.Id, User, cancellationToken);

            return NoContent();
        }

        private async Task<bool> BankCustomerExists(Guid id, CancellationToken cancellationToken)
        {
            return await _bankCustomerService.ExistsAsync(id, cancellationToken);
        }
    }
}
