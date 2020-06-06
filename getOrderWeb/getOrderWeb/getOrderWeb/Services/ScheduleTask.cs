using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace getOrderWeb.Services
{
    public class ScheduleTask : IHostedService, IDisposable
    {
        Timer timer;
        readonly IServiceScopeFactory serviceScopeFactory;
        readonly ILogger<ScheduleTask> logger;
        bool isRunning;
        public ScheduleTask(IServiceScopeFactory _serviceScopeFactory , ILogger<ScheduleTask> _logger)
        {
            serviceScopeFactory = _serviceScopeFactory;
            logger = _logger;
            isRunning = false;
        }
        public void Dispose()
        {
            timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(OurTasks, null, TimeSpan.Zero, TimeSpan.FromSeconds(3600));
            return Task.CompletedTask;
        }
        private void OurTasks(object state)
        {
            if (isRunning)
                return;
            isRunning = true;
            logger.LogInformation("task start");
            using var scope = serviceScopeFactory.CreateScope();
            //do this operation in schedule time can be add more  
            var orderServices = scope.ServiceProvider.GetRequiredService<IOrderServices>();
            var count = orderServices.validateOrders();
            isRunning = false;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer.Change(int.MaxValue, 0);
            return Task.CompletedTask;
        }
    }
}
