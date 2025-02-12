using Shared.Models.Service.Modules;

namespace Shared.Models.Annotation;

[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
public class ModuleAnnotation: Attribute
{
    public string Prefix { get; private set; }
    public DatabaseContexts Context { get; private set; }

    public ModuleAnnotation(string prefix, DatabaseContexts context)
    {
        this.Prefix = prefix;
        this.Context = context;
    }
}