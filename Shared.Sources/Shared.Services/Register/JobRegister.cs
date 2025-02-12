using Shared.Libraries;

namespace Shared.Services.Register;

public class JobRegister
{
    public BackgroundJob Job { get; private set; } 
    public string Id { get; private set; }

    public JobRegister(BackgroundJob job, string id)
    {
        this.Job = job;
        this.Id = id;
    }

    public static JobRegister Create(BackgroundJob job, string id)
        => new JobRegister(job, id);
}