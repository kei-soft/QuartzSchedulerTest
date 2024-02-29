using Quartz;
using Quartz.Impl;
using Quartz.Logging;

namespace QuartzSchedulerTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        public async void Start()
        {
            LogProvider.SetCurrentLogProvider(new ConsoleLogProvider());

            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // Job 목록 생성
            List<JobInfo> jobList = new List<JobInfo>();

            jobList.Add(new JobInfo() { Key = "1", CronExpression = "0/5 * * * * ?", StartTime = DateTime.Now, EndTime = DateTime.Now.AddSeconds(30) });
            jobList.Add(new JobInfo() { Key = "2", CronExpression = "0/10 * * * * ?", StartTime = DateTime.Now, EndTime = DateTime.Now.AddSeconds(30) });
            jobList.Add(new JobInfo() { Key = "3", CronExpression = "0/15 * * * * ?", StartTime = DateTime.Now, EndTime = DateTime.Now.AddSeconds(30) });

            foreach (var job in jobList)
            {
                // Job 정의
                IJobDetail jobdetail = JobBuilder.Create<TestJob>()
                             .WithIdentity(job.Key)
                             .Build();

                // Job 주기 정의
                ITrigger trigger = TriggerBuilder.Create()
                                    .WithIdentity($"{job.Key}_trigger")
                                    .StartNow()
                                    .WithCronSchedule(job.CronExpression)
                                    .Build();

                // Scheduler 에 Job 추가
                await scheduler.ScheduleJob(jobdetail, trigger);
            }

            // Scheduler 시작
            await scheduler.Start();

            Console.ReadLine();
        }
    }
}
