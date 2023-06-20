using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalVessels.Repositories;

public class VesselRepository : IRepository<IVessel>
{
    private List<IVessel> models;

    public VesselRepository()
    {
        this.models = new List<IVessel>();
    }
    public IReadOnlyCollection<IVessel> Models => this.models.AsReadOnly();

    public void Add(IVessel model)
    {
        models.Add(model);
    }

    public IVessel FindByName(string name) => this.models.FirstOrDefault(x => x.Name == name);

    public bool Remove(IVessel model)
    {
        return this.models.Remove(model);
    }
}
