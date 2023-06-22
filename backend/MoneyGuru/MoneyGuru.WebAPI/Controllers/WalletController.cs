using MoneyGuru.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MoneyGuru.WebAPI;
using MoneyGuru.WebAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using MoneyGuru.WebAPI.Models;

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

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();
            var total = wallets.Sum(w => w.AmountOfMoney);

            return Ok(Math.Round(total));
        }

        [HttpGet("totalwallets")]
        public async Task<IActionResult> GetTotalWalletsAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();

            var walletsAmount = wallets
                .Where(w => w.Type == "Card" || w.Type == "Cash")
                .Sum(w => w.AmountOfMoney);

            return Ok(Math.Round(walletsAmount));
        }

        [HttpGet("totalsaved")]
        public async Task<IActionResult> GetTotalSavedAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();

            var totalSaved = wallets
                .Where(w => w.Type == "Saving")
                .Sum(w => w.AmountOfMoney);

            return Ok(Math.Round(totalSaved));
        }

        [HttpGet("details")]
        public async Task<IActionResult> GetWalletDetailsAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();

            var walletDetails = wallets.Select(w => new WalletData
            {
                WalletName = w.WalletName,
                WalletAmount = w.AmountOfMoney
            });

            return Ok(walletDetails);
        }

        [HttpGet("formainpage")]
        public async Task<IActionResult> GetWalletForMainPageAsync()
        {
            var wallets = await _walletService.GetWalletsAsync();

            var walletDetails = wallets
                .Where(w => w.Type != "Saving")
                .Select(w => new WalletData
                {
                    WalletName = w.WalletName,
                    WalletAmount = w.AmountOfMoney
                });

            return Ok(walletDetails);
        }



        public class WalletData
        {
            public string WalletName { get; set; }
            public decimal WalletAmount { get; set; }
        }

    }
}