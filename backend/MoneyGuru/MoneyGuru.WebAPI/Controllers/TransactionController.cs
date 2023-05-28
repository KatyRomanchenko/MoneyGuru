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
    [Authorize]
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            await _transactionService.AddTransactionAsync(model, userId.ToString());

            return Ok();
        }

        [HttpGet]

        public async Task<IActionResult> GetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new List<Guid> { Guid.NewGuid() });
        }
    }
}