using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private List<ISupplement> models;

        public SupplementRepository()
        {
            this.models = new List<ISupplement>();
        }
        public void AddNew(ISupplement model)
        {
            models.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return models.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        }

        public IReadOnlyCollection<ISupplement> Models() => this.models.AsReadOnly();

        public bool RemoveByName(string typeName)
        {
            ISupplement supplement = models.FirstOrDefault(x => x.GetType().Name == typeName);
            return models.Remove(supplement);
        }
    }
}
