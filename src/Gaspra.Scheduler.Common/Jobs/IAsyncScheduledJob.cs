using Gaspra.Scheduler.Common.Settings;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common.Jobs
{
    public interface IAsyncScheduledJob
    {
        public Task Manage(JobSettings jobSettings);
        public Task Execute(JobSettings jobSettings);
    }
}
