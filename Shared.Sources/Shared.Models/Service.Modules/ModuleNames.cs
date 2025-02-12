using Shared.Models.Annotation;

namespace Shared.Models.Service.Modules;

public enum ModuleNames
{
    [ModuleAnnotation(prefix: "/auth", context:  DatabaseContexts.AUTHENTICATION)]
    AUTHENTICATION,
    [ModuleAnnotation(prefix: "/personal", context:  DatabaseContexts.PERSONAL)]
    PERSONAL,
    [ModuleAnnotation(prefix: "/enterprises", context:  DatabaseContexts.ENTERPRISES)]
    ENTERPRISES,
    [ModuleAnnotation(prefix: "/financial", context:  DatabaseContexts.FINANCIAL)]
    FINANCIAL
}