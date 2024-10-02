using EduZone.DataSeeder;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace EduZone.StartupTasks
{
    public class CreateDatabaseStartupTask : IStartupTask
    {
        private readonly SeederService _seederService;

        public CreateDatabaseStartupTask(SeederService seederService)
        {
            _seederService = seederService;
        }

        public async Task Excute(IHost host)
        {
            try
            {
                await _seederService.SeedAsync();
            }
            catch
            {
                
            }
        }
    }
}
