using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyGuru.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyGuru.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddTransactionAsync([FromBody] AddTransactionViewModel model)
        {
            await _transactionService.AddTransactionAsync(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsAsync()
        {
            var transactions = await _transactionService.GetTransactionsAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            return Ok(transactions);
        }
    }
}