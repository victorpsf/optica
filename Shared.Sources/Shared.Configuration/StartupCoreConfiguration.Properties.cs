using Microsoft.Extensions.Configuration;
using Shared.Models.Annotation;
using Shared.Models.Service.Modules;

namespace Shared.Configuration;

public partial class StartupCoreConfiguration
{
    protected IConfiguration Configuration { get; private set; }
    protected ModuleNames Module { get; private set; }
    protected ModuleAnnotation? Annotation { get; private set; }
}