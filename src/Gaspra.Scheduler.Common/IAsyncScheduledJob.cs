using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gaspra.Scheduler.Common
{
    public interface IAsyncScheduledJob
    {
        public IDictionary<string, object> Context { get; set; }
        public Task Execute();
    }
}
