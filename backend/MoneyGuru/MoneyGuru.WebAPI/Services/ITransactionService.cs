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

namespace MoneyGuru.WebAPI.Services
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(AddTransactionViewModel model, string userId);
    }

    public class TransactionService : ITransactionService
    {
        private UserManager<User> _userManger;

        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context, UserManager<User> userManger)
        {
            _context = context;
            _userManger = userManger;
        }

        public async Task AddTransactionAsync(AddTransactionViewModel model, string userId)
        {
            var user = await _userManger.FindByIdAsync(userId);

            var transaction = new Transaction
            {
                TransactionType = model.TransactionType,
                User = user,
                Amount = model.Amount,
                Date = model.Date,
                Category = null
            };

            await _context.Transactions.AddAsync(transaction);

            await _context.SaveChangesAsync();
        }
    }
}
