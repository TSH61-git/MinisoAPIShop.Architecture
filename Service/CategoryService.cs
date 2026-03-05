using AutoMapper;
using DTOs;
using Entities;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository _categoryrepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryrepository, IMapper mapper)
        {
            _categoryrepository = categoryrepository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDTO>> GetCategories()
        {
            var categories = await _categoryrepository.GetCategories();
            var categoriesDto = _mapper.Map<List<CategoryDTO>>(categories);
            return categoriesDto;
        }

    }
}
