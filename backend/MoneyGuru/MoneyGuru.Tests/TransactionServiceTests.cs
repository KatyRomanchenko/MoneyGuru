using Microsoft.EntityFrameworkCore;
using MoneyGuru.WebAPI;
using MoneyGuru.WebAPI.Models;
using MoneyGuru.WebAPI.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using Xunit;

namespace MoneyGuru.Tests
{
    public class TransactionServiceTests
    {
        private ApplicationDbContext _context;

        public TransactionServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);
            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task AddTransactionAsync_AddsTransactionToDatabase()
        {
            // Arrange
            var testService = new TransactionService(_context);
            var testModel = new AddTransactionViewModel
            {
                TransactionType = "test type",
                TransactionName = "test name",
                Amount = 100,
                Date = DateTime.Now,
                Category = "test category",
                Wallet = "test wallet",
            };

            // Act
            await testService.AddTransactionAsync(testModel);

            // Assert
            Assert.NotEmpty(_context.Transactions);
            var transactionInDb = _context.Transactions.First();
            Assert.Equal(testModel.TransactionType, transactionInDb.TransactionType);
            Assert.Equal(testModel.TransactionName, transactionInDb.TransactionName);
            Assert.Equal(testModel.Amount, transactionInDb.Amount);
            Assert.Equal(testModel.Date, transactionInDb.Date);
            Assert.Equal(testModel.Category, transactionInDb.Category);
            Assert.Equal(testModel.Wallet, transactionInDb.Wallet);
        }
    }
}
