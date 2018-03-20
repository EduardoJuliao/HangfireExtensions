using Hangfire;
using Hangfire.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireExtensions
{
    class JobsInitializer
    {
        public static void ClearJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }

        public static void RegisterJobs()
        {
            foreach (JobsConfigElement job in JobsConfig.GetConfig().Jobs)
            {
                var type = Type.GetType(job.JobClass);

                var parameters = new List<object>();

                if (!string.IsNullOrWhiteSpace(job.JobParameters))
                    foreach (var p in job.JobParameters.Split(','))
                        parameters.Add(p);

                var instance = Activator.CreateInstance(type) as IJob;
                if (instance != null)
                {
                    RecurringJob.AddOrUpdate(job.JobName,
                    () => instance.Integrate(null, parameters.ToArray()), job.JobCron,
                    TimeZoneInfo.FindSystemTimeZoneById(job.JobTimeZone));
                }

            }

        }
    }
}
