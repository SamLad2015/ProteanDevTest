using System.Linq;
using DeveloperTest.Business.Interfaces;
using DeveloperTest.Database;
using DeveloperTest.Database.Models;
using DeveloperTest.Models;

namespace DeveloperTest.Business
{
    public class JobService : IJobService
    {
        private readonly ApplicationDbContext context;

        public JobService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public JobModel[] GetJobs()
        {
            return context.Jobs
                .Join(context.Customers, 
                    job => job.CustomerId,
                    customer => customer.CustomerId,
                    (j, c) => new JobModel
                    {
                        JobId = j.JobId,
                        Engineer = j.Engineer,
                        Customer = c.Name,
                        When = j.When
                    }
                ).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return context.Jobs
                .Join(context.Customers, 
                    job => job.CustomerId,
                    customer => customer.CustomerId,
                    (j, c) => new JobModel
                    {
                        JobId = j.JobId,
                        Engineer = j.Engineer,
                        Customer = c.Name,
                        When = j.When
                    }
                ).SingleOrDefault(j => j.JobId == jobId);
        }

        public JobModel CreateJob(BaseJobModel model)
        {
            var addedJob = context.Jobs.Add(new Job
            {
                Engineer = model.Engineer,
                CustomerId = model.CustomerId,
                When = model.When
            });

            context.SaveChanges();

            return new JobModel
            {
                JobId = addedJob.Entity.JobId,
                Engineer = addedJob.Entity.Engineer,
                When = addedJob.Entity.When
            };
        }
    }
}
