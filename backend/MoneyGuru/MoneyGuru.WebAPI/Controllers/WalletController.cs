using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyGuru.WebAPI.Models;
using MoneyGuru.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoneyGuru.WebAPI;
using MoneyGuru.WebAPI.Contracts;


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
        public async Task<IActionResult> GetWallets()
        {
            // Предположим, что у вас есть сервис WalletService, который получает кошельки.
            var wallets = await _walletService.GetWalletsAsync();

            var wallet = wallets.Select(w => new Wallet
            {
                WalletName = w.WalletName,
                AmountOfMoney = w.AmountOfMoney
            });

            return Ok(wallet);
        }

        [HttpGet("getNames")]
        public async Task<IActionResult> GetWalletsAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();

            return Ok(wallets.Select(c => c.WalletName));

        }

        [HttpGet("{walletName}")]
        public async Task<IActionResult> GetWalletByNameAsync(string walletName)
        {
            var wallet = await _walletService.GetWalletByNameAsync(walletName);

            if (wallet == null)
            {
                return NotFound();
            }

            return Ok(wallet);
        }

        [HttpPut("{walletName}")]
        public async Task<IActionResult> UpdateWalletAsync(string walletName, [FromBody] UpdateWalletViewModel model)
        {
            var existingWallet = await _walletService.GetWalletByNameAsync(walletName);

            if (existingWallet == null)
            {
                return NotFound();
            }

            existingWallet.AmountOfMoney = model.AmountOfMoney;

            await _walletService.UpdateWalletAsync(existingWallet);

            return Ok(existingWallet);
        }

        [HttpGet("totalamount")]
        public async Task<IActionResult> GetTotalAmountAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();
            var totalAmount = wallets.Sum(w => w.AmountOfMoney);

            return Ok(totalAmount);
        }
    }
}