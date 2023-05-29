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
    public interface ICategoryService
    {
        Task AddCategoryAsync(AddCategoryViewModel model);
        Task<List<Category>> GetCategoriesAsync();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCategoryAsync(AddCategoryViewModel model)
        {
            var category = new Category
            {
                Name = model.Name,
                TotalAmount = model.TotalAmount,
            };

            await _context.Categories.AddAsync(category);

            await _context.SaveChangesAsync();

        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
