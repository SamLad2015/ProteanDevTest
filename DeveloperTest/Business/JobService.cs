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
            return (from j in context.Jobs
                join c in context.Customers 
                    on j.CustomerId equals c.CustomerId into cJoin
                from cj in cJoin.DefaultIfEmpty()
                select new JobModel
                {
                    JobId = j.JobId,
                    Engineer = j.Engineer,
                    Customer = j.CustomerId.HasValue ? cj.Name : "Unknown",
                    When = j.When
                }).ToArray();
        }

        public JobModel GetJob(int jobId)
        {
            return (from j in context.Jobs
                join c in context.Customers 
                    on j.CustomerId equals c.CustomerId into cJoin
                from cj in cJoin.DefaultIfEmpty()
                join t in context.CustomerTypes
                    on cj.CustomerTypeId equals t.CustomerTypeId into ctJoin
                from cjt in ctJoin.DefaultIfEmpty()
                select new JobModel
                {
                    JobId = j.JobId,
                    Engineer = j.Engineer,
                    Customer = j.CustomerId.HasValue ? cj.Name : "Unknown",
                    CustomerType = cjt.Description,
                    When = j.When
                }).SingleOrDefault(j => j.JobId == jobId);
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
