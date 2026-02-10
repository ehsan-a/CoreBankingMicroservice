using Account.Application.DTOs;
using Account.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Account.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "Accessibility")]
    public class BankAccountsController : ControllerBase
    {
        private readonly IBankAccountService _bankAccountService;

        public BankAccountsController(IBankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        // GET: api/BankAccounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BankAccountResponseDto>>> GetBankAccounts(CancellationToken cancellationToken, int limit = 25, int offset = 0)
        {
            var bankAccount = await _bankAccountService.GetAllAsync(limit, offset, cancellationToken);
            return Ok(bankAccount);
        }

        // GET: api/BankAccounts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BankAccountResponseDto>> GetBankAccount(Guid id, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountService.GetByIdAsync(id, cancellationToken);


            if (bankAccount == null)
            {
                return NotFound();
            }

            return Ok(bankAccount);
        }

        // PUT: api/BankAccounts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBankAccount(Guid id, UpdateBankAccountRequestDto dto, CancellationToken cancellationToken)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }
            try
            {
                //var account = await _bankAccountService.GetByIdAsync(id, cancellationToken);
                await _bankAccountService.UpdateAsync(dto, User, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BankAccountExists(id, cancellationToken))
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

        // POST: api/BankAccounts
        [HttpPost]
        public async Task<ActionResult<BankAccountResponseDto>> PostBankAccount(CreateBankAccountRequestDto dto, [FromHeader(Name = "x-requestid")] Guid requestId, CancellationToken cancellationToken)
        {
            if (requestId == Guid.Empty)
            {
                return BadRequest("Empty GUID is not valid for request ID");
            }

            var result = await _bankAccountService.CreateAsync(dto, requestId, User, cancellationToken);

            return Created();

            //return CreatedAtAction("GetBankAccount", new { id = bankAccount.Id }, bankAccount);
        }

        // DELETE: api/BankAccounts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBankAccount(Guid id, CancellationToken cancellationToken)
        {
            var bankAccount = await _bankAccountService.GetByIdAsync(id, cancellationToken);
            if (bankAccount == null)
            {
                return NotFound();
            }

            await _bankAccountService.DeleteAsync(id, User, cancellationToken);

            return NoContent();
        }

        private async Task<bool> BankAccountExists(Guid id, CancellationToken cancellationToken)
        {
            return await _bankAccountService.ExistsAsync(id, cancellationToken);
        }
    }
}
