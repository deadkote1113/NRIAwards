using NRIAwards.DependencyInjection.Adder;
using Microsoft.Extensions.DependencyInjection;

namespace NRIAwards.DependencyInjection;

public class Di
{
    private readonly DependencyAdder _dependencyAdder;

    public Di(ProjectConfiguration projectConfiguration, IServiceCollection services)
    {
        switch (projectConfiguration)
        {
            case ProjectConfiguration.Prod:
                _dependencyAdder = new ProdDependencyAdder(services);
                break;
            case ProjectConfiguration.Dev:
                _dependencyAdder = new DevDependencyAdder(services);
                break;
        }
    }

    public void Add()
    {
        _dependencyAdder.AddDalInterfaces();
        _dependencyAdder.AddBlInterfaces();
        _dependencyAdder.ExternalDependencies();
    }
}