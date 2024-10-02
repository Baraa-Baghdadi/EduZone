using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace EduZone.StartupTasks
{
    public interface IStartupTask
    {
        Task Excute(IHost host);
    }
}
