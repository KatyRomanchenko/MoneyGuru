using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost] 
        public async Task<IActionResult> AddCategoryAsync([FromBody] AddCategoryViewModel model)
        {
            await _categoryService.AddCategoryAsync(model);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();

            return Ok(categories.Select(c => c.Name));
        }
    }
}