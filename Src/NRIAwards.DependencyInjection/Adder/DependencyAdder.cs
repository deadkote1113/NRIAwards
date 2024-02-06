using Microsoft.Extensions.DependencyInjection;
using NRIAwards.BL.Service;
using NRIAwards.DAL.Repository;

namespace NRIAwards.DependencyInjection.Adder;

public abstract class DependencyAdder
{
    protected readonly IServiceCollection _services;

    public DependencyAdder(IServiceCollection services)
    {
        _services = services;
    }

    public virtual void AddDalInterfaces()
    {
        var assemblyProdDal = typeof(DAL.Repository.AssemblyRunner).Assembly;
        var dals = assemblyProdDal.GetTypes().Where(item => item.Name.EndsWith("Repository")).ToList();
        foreach (var dal in dals)
        {
            foreach (var @interface in dal.GetInterfaces())
            {
                _services.AddScoped(@interface, dal);
            }
        }
    }

    public virtual void AddBlInterfaces()
    {
        var assemblyBl = typeof(BL.Service.AssemblyRunner).Assembly;
        var bls = assemblyBl.GetTypes().Where(item => item.Name.EndsWith("Service")).ToList();
        foreach (var bl in bls)
        {
            foreach (var @interface in bl.GetInterfaces())
            {
                _services.AddScoped(@interface, bl);
            }
        }
    }

    public virtual void ExternalDependencies()
    {

    }
}
