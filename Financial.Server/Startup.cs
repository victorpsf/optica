using Shared.Configuration;
using Shared.Models.Service.Modules;

namespace Financial.Server;

public class Startup: StartupCoreConfiguration
{
    public Startup(IConfiguration configuration): base(configuration, ModuleNames.FINANCIAL) 
    { }
}