using Gaspra.Scheduler.Common.Settings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common
{
    public interface IAsyncScheduledJob
    {
        public JobSettings Settings { get; }
        public Task Manage();
        public Task Execute();
    }
}
