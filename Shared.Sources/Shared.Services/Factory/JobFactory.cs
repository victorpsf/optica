using Shared.Libraries;
using Shared.Services.Register;

namespace Shared.Services.Factory;

public class JobFactory
{
    public static JobRegister CreateJob(JobCaller caller, int time)
    {
        var job = new BackgroundJob();
        var id = job.RegistryJob(caller);
        job.On(time);

        return JobRegister.Create(job, id);
    }
}