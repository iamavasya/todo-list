using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.Infrastructure.Models;
using todo_list.BusinessLogic.Dtos;

namespace todo_list.BusinessLogic.Interfaces
{
    public interface ICategoryService 
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<CategoryDto> GetCategoryDtoByIdAsync(int id);
        Task<Category> AddCategoryAsync(CategoryDto category);
        Task UpdateCategoryAsync(CategoryDto category, int id);
        Task DeleteCategoryAsync(int id);
        CategoryDto MapCategoryToDto(Category category);
        Category MapDtoToCategory(CategoryDto categoryDto, Category? category = null);
    }
}
