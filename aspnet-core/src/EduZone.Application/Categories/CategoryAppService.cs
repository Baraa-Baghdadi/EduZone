using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace EduZone.Categories
{
    public class CategoryAppService : EduZoneAppService, ICategoryAppService
    {
        private readonly IRepository<Category> _categoryRepos;

        public CategoryAppService(IRepository<Category> categoryRepos)
        {
            _categoryRepos = categoryRepos;
        }

        public async Task<List<CategoryDto>> GetCategoriesForDrop()
        {
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(await _categoryRepos.GetListAsync());
        }

        public async Task<CategoryDto> GetGetgory([Required] Guid id)
        {
            return ObjectMapper.Map<Category, CategoryDto>(await _categoryRepos.GetAsync(row => row.Id == id));
        }
    }
}
