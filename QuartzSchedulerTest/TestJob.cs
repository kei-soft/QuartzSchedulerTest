using Quartz;

namespace QuartzSchedulerTest
{
    internal class TestJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Console.Out.WriteLineAsync($"{DateTime.Now} | Execute Job : [{context.JobDetail.Key.Name}]");
        }
    }
}
