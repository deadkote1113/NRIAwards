using Microsoft.Extensions.DependencyInjection;

namespace NRIAwards.DependencyInjection.Adder;

internal class ProdDependencyAdder : DependencyAdder
{
    public ProdDependencyAdder(IServiceCollection services) : base(services)
    {
    }
}
