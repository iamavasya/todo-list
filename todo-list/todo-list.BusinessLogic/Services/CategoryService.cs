using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo_list.BusinessLogic.Interfaces;
using todo_list.BusinessLogic.Dtos;
using todo_list.Infrastructure.Interfaces;
using todo_list.Infrastructure.Models;

namespace todo_list.BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllCategoriesAsync();
            return categories.Select(MapCategoryToDto).ToList();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _categoryRepository.GetCategoryByIdAsync(id);
        }

        public async Task<CategoryDto> GetCategoryDtoByIdAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            return MapCategoryToDto(category);
        }

        public async Task<Category> AddCategoryAsync(CategoryDto category)
        {
            var categoryEntity = MapDtoToCategory(category);
            await _categoryRepository.AddCategoryAsync(categoryEntity);
            return categoryEntity;
        }

        public async Task UpdateCategoryAsync(CategoryDto category, int id)
        {
            var oldCategoryEntity = await GetCategoryByIdAsync(id);
            var categoryEntity = MapDtoToCategory(category, oldCategoryEntity);
            await _categoryRepository.UpdateCategoryAsync(categoryEntity);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
        }

        public CategoryDto MapCategoryToDto(Category category)
        {
            return new CategoryDto { Name = category.Name };
        }

        public Category MapDtoToCategory(CategoryDto categoryDto, Category? category = null)
        {
            if (category != null)
            {
                category.Name = categoryDto.Name;
                return category;
            }
            else
            {
                return new Category { Name = categoryDto.Name };
            }
        }
    }
}
