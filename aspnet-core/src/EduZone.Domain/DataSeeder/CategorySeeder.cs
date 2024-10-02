using EduZone.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace EduZone.DataSeeder
{
    public class CategorySeeder : ITransientDependency
    {
        private readonly IRepository<Category> _categoryRepo;
        private readonly IGuidGenerator _guidGenerator;

        public CategorySeeder(IRepository<Category> categoryRepo, IGuidGenerator guidGenerator)
        {
            _categoryRepo = categoryRepo;
            _guidGenerator = guidGenerator;
        }

        public async Task Seed()
        {
            await CreateMainCategory();
        }

        private async Task CreateMainCategory()
        {
            if (!await _categoryRepo.AnyAsync())
            {
                List<Category> categories = new List<Category> { 
                    new Category(_guidGenerator.Create(),"Computer Science","Explore the fundamentals of programming, algorithms, data structures, and software development. Ideal for aspiring developers and tech enthusiasts."),
                    new Category(_guidGenerator.Create(),"Business & Management","Learn essential business principles, management strategies, and entrepreneurship skills. Perfect for future leaders and innovators in the corporate world."),
                    new Category(_guidGenerator.Create(), "Health & Wellness","Discover topics in nutrition, mental health, fitness, and holistic wellness. Designed for those looking to enhance their personal well-being and professional skills in health."),
                    new Category(_guidGenerator.Create(), "Creative Arts","Dive into the world of visual arts, music, theater, and creative writing. This category fosters creativity and self-expression through various artistic mediums."),
                    new Category(_guidGenerator.Create(), "Science & Engineering","Gain insights into the principles of physics, chemistry, biology, and engineering. Ideal for students and professionals seeking a deeper understanding of scientific concepts."),
                    new Category(_guidGenerator.Create(), "Social Sciences","Examine human behavior, society, and relationships through sociology, psychology, and anthropology. Great for those interested in social dynamics and cultural studies."),
                    new Category(_guidGenerator.Create(), "Languages & Communication","Improve your language skills and communication techniques. This category includes courses in foreign languages, public speaking, and effective writing."),
                    new Category(_guidGenerator.Create(), "Education & Teaching","Focus on teaching methodologies, educational psychology, and curriculum development. Designed for current and aspiring educators looking to enhance their skills."),
                    new Category(_guidGenerator.Create(), "Information Technology","Delve into IT systems, cybersecurity, and network management. Perfect for tech-savvy individuals aiming for a career in the fast-paced tech industry."),
                    new Category(_guidGenerator.Create(), "Personal Development","Enhance your life skills, productivity, and emotional intelligence. This category is for anyone looking to grow personally and professionally."),
                };

                await _categoryRepo.InsertManyAsync(categories,true);

            }
        }

    }
}
