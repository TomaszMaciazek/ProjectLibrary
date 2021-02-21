using Application.Dto;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModels.AddVM;
using Application.ViewModels.UpdateVM;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<CategoryDto>> GetAllCategoriesAsync()
            => _mapper.Map<ICollection<CategoryDto>>(await _categoryRepository.GetAllCategoriesAsync());
        public async Task<CategoryWithBooksDto> GetCategoryByIdAsync(int id)
            => _mapper.Map<CategoryWithBooksDto>(await _categoryRepository.GetCategoryByIdAsync(id));
        public async Task<CategoryDto> AddCategoryAsync(AddCategoryVM category)
        {
            try
            {
                var mappedCategory = _mapper.Map<Category>(category);
                await _categoryRepository.AddAsync(mappedCategory);
                return _mapper.Map<CategoryDto>(mappedCategory);
            }
            catch (Exception)
            {
                throw new AddOperationFailedException();
            }
        }
        public async Task<bool> UpdateCategoryAsync(UpdateCategoryVM category)
        {
            try
            {
                var entity = await _categoryRepository.GetCategoryByIdAsync(category.Id);
                if(entity == null)
                {
                    return false;
                }
                entity.Name = category.Name;
                entity.ModificationDate = category.ModyficationDate;
                entity.ModifiedBy = category.ModifiedBy;
                await _categoryRepository.UpdateAsync(entity);
                return true;
            }
            catch (Exception)
            {
                throw new UpdateOperationFailedException();
            }
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if(category == null)
                {
                    return false;
                }
                await _categoryRepository.DeleteAsync(category);
                return true;
            }
            catch (InvalidOperationException)
            {
                throw new DeleteIsForbiddenException();
            }
            catch (Exception)
            {
                throw new DeleteOperationFailedException();
            }
        }
    }
}
