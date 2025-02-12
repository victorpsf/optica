namespace Shared.Libraries;

public delegate void JobCaller();

public class BackgroundJob
{
    private Dictionary<string, JobCaller> jobs;
    private System.Timers.Timer? listen;

    public BackgroundJob()
    {
        this.jobs = new Dictionary<string, JobCaller>();
    }

    public void On(int interval)
    {
        this.listen = new System.Timers.Timer(interval);

        this.listen.Elapsed += (s, e) =>
        {
            this.listen.Enabled = false;
            foreach (string key in this.jobs.Keys)
                try
                { this.jobs[key].Invoke(); }
                catch (Exception ex)
                { Console.WriteLine($"JOB EXCEPTION: {key}\n{ex.Message}\n{ex.StackTrace}"); }
            this.listen.Enabled = true;
        };

        this.listen.Enabled = true;
    }

    public string RegistryJob(JobCaller caller)
    {
        var id = Guid.NewGuid().ToString();
        this.jobs.Add(id, caller);
        return id;
    }

    public void RemoveJob(string id)
        => this.jobs.Remove(id);

    public void Off()
    {
        if (this.listen is not null)
        {
            this.listen.Enabled = false;
            this.listen.Stop();
            this.listen.Dispose();
        }
    }
}