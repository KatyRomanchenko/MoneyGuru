using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyGuru.WebAPI.Models;
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
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _walletService;
        public WalletController(IWalletService walletService)
        {
            _walletService = walletService;
        }

        [HttpPost] 
        public async Task<IActionResult> AddWalletAsync([FromBody] AddWalletViewModel model)
        {
            await _walletService.AddWalletAsync(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletsAsync()
        {
            var wallet = await _walletService.GetWalletsAsync();

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }
        [HttpPut("{walletName}")]
        public async Task<IActionResult> UpdateWalletAsync(string walletName, [FromBody] Wallet wallet)
        {
            // Make sure the wallet name in the URL matches the name in the body
            if (walletName != wallet.WalletName)
            {
                return BadRequest();
            }

            try
            {
                await _walletService.UpdateWalletAsync(wallet);
                return Ok();
            }
            catch
            {
                // If an exception is thrown, return a 500 status
                return StatusCode(500);
            }
        }
    }
}