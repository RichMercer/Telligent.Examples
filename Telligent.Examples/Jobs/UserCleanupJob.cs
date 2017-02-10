using System;
using Telligent.Evolution.Extensibility.Api.Version1;
using Telligent.Evolution.Extensibility.Jobs.Version1;

namespace UserCleanupJob
{
    public class UserCleanupJob : IRecurringEvolutionJobPlugin
    {
        public string Name
        {
            get { return "User Cleanup Job";  }
        }

        public string Description 
        {
            get { return "A scheduled job that will find users and based on a set criteria will delete them."; }
        }
        public void Initialize()
        {
        }

        public Guid JobTypeId
        {
            get { return Guid.Parse("92451a8c3a444e89ad8253efbc9220b6"); }
        }

        public JobSchedule DefaultSchedule
        {
            get { return JobSchedule.Daily(DateTime.Now.Date.AddHours(2.0)); }
        }

        public JobContext SupportedContext
        {
            get { return JobContext.Service; }
        }

        public void Execute(JobData jobData)
        {
            var moreRecords = true;
            var pageIndex = 0;
            var pageSize = 100;

            do
            {
                var users = PublicApi.Users.List(new UsersListOptions() {PageSize = pageSize, PageIndex = pageIndex});
                moreRecords = users.TotalCount == pageSize;

                foreach (var user in users)
                {
                    if (user.LastLoginDate < DateTime.Now.AddYears(-5) && user.TotalPosts == 0)
                    {
                        PublicApi.Users.Delete(new UsersDeleteOptions
                        {
                            Id = user.Id,
                            ReassignedUsername = PublicApi.Users.AnonymousUserName
                        });
                    }
                }

                pageIndex++;
            } while (moreRecords);
        }
    }
}
