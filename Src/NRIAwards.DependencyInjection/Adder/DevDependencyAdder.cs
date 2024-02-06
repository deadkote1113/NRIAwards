using Microsoft.Extensions.DependencyInjection;

namespace NRIAwards.DependencyInjection.Adder;

internal class DevDependencyAdder : DependencyAdder
{
    public DevDependencyAdder(IServiceCollection services) : base(services)
    {
    }
}
