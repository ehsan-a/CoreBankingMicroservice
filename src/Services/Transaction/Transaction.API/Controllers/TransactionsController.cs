using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Transaction.Application.DTOs;
using Transaction.Application.Interfaces;

namespace Transaction.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Policy = "Accessibility")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionResponseDto>>> GetTransactions(CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetAllAsync(cancellationToken);
            return Ok(transaction);
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionResponseDto>> GetTransaction(Guid id, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.GetByIdAsync(id, cancellationToken);

            if (transaction == null)
            {
                return NotFound();
            }

            return Ok(transaction);
        }

        // POST: api/Transactions
        [HttpPost]
        public async Task<ActionResult<TransactionResponseDto>> PostTransaction(CreateTransactionRequestDto createTransactionRequestDto, CancellationToken cancellationToken)
        {
            var transaction = await _transactionService.CreateAsync(createTransactionRequestDto, User, cancellationToken);

            return CreatedAtAction("GetTransaction", new { id = transaction.Id }, transaction);
        }
    }
}
