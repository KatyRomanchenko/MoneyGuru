using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MoneyGuru.WebAPI.Contracts;
using MoneyGuru.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MoneyGuru.WebAPI.Services
{
    public interface IWalletService
    {
        Task AddWalletAsync(AddWalletViewModel model);
        Task<List<Wallet>> GetWalletsAsync();
        Task<Wallet> GetWalletByNameAsync(string walletName);
        Task UpdateWalletAsync(Wallet wallet);
    }

    public class WalletService : IWalletService
    {
        private readonly ApplicationDbContext _context;

        public WalletService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddWalletAsync(AddWalletViewModel model)
        {
            var wallet = new Wallet
            {
                Type = model.Type,
                AmountOfMoney = model.AmountOfMoney,
                WalletName = model.WalletName,
            };

            await _context.Wallets.AddAsync(wallet);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Wallet>> GetWalletsAsync()
        {
            return await _context.Wallets.ToListAsync();
        }
        public async Task<Wallet> GetWalletByNameAsync(string walletName)
        {
            return await _context.Wallets.FirstOrDefaultAsync(w => w.WalletName == walletName);
        }

        public async Task UpdateWalletAsync(Wallet wallet)
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync();
        }
    }
}
