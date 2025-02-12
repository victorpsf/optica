
namespace Authentication.Server;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHost(a =>
            {
                a.UseStartup<Startup>();
                a.UseKestrel();
            });
}