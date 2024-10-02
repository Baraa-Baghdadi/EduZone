using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EduZone.Categories
{
    public interface ICategoryAppService : IApplicationService
    {
        Task<List<CategoryDto>> GetCategoriesForDrop();
        Task<CategoryDto> GetGetgory([Required] Guid id);
    }
}
