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
            var wallets = await _walletService.GetWalletsAsync();

            return Ok(wallets.Select(c => c.WalletName));

        }
    }
}