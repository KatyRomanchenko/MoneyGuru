using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyGuru.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyGuru.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            transactions.Reverse();

            return Ok(transactions);
        }

        [HttpGet("totalEarned")]
        public async Task<IActionResult> GetTotalEarnedAsync()
        {
            var transactions = await _transactionService.GetTransactionsAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            var totalEarned = transactions
                .Where(t => t.TransactionType == "Income")
                .Sum(t => t.Amount);

            return Ok(Math.Round(totalEarned));
        }

        [HttpGet("totalSpent")]
        public async Task<IActionResult> GetTotalSpentAsync()
        {
            var transactions = await _transactionService.GetTransactionsAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            var totalSpent = transactions
                .Where(t => t.TransactionType == "Expense")
                .Sum(t => t.Amount);

            return Ok(Math.Round(totalSpent));
        }
        [HttpGet("totalSpentByCategory")]
        public async Task<IActionResult> GetTotalSpentByCategoryAsync()
        {
            var transactions = await _transactionService.GetTransactionsAsync();

            if (transactions == null)
            {
                return NotFound();
            }

            var totalSpentByCategory = transactions
                .Where(t => t.TransactionType == "Expense")
                .GroupBy(t => t.Category)
                .Select(group => new
                {
                    Category = group.Key,
                    TotalSpent = Math.Round(group.Sum(t => t.Amount))
                })
                .ToList();

            return Ok(totalSpentByCategory);
        }
    }
}