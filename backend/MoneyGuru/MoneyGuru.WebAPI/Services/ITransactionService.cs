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
using System.Linq;

namespace MoneyGuru.WebAPI.Services
{
    public interface ITransactionService
    {
        Task AddTransactionAsync(AddTransactionViewModel model);
        Task<List<AddTransactionViewModel>> GetTransactionsAsync();

    }

    public class TransactionService : ITransactionService
    {
        private readonly ApplicationDbContext _context;

        public TransactionService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddTransactionAsync(AddTransactionViewModel model)
        {

            var transaction = new Transaction
            {
                TransactionType = model.TransactionType,
                TransactionName = model.TransactionName,
                Amount = model.Amount,
                Date = model.Date,
                Category = model.Category
            };

            await _context.Transactions.AddAsync(transaction);

            await _context.SaveChangesAsync();
        }
        public async Task<List<AddTransactionViewModel>> GetTransactionsAsync()
        {
            return await _context.Transactions
                .Select(t => new AddTransactionViewModel
                {
                    TransactionName = t.TransactionName,
                    Amount = t.Amount,
                    Date = t.Date,
                    Category = t.Category
                })
                .ToListAsync();
        }
    }
}
