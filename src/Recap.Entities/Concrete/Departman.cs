

using Recap.Core.Entities;

namespace Recap.Entities.Concrete
{
    public class Departman : BaseModel, IEntity
    {
        public string Name { get; set; }
        public string RootPath { get; set; }

        public string WinUserName { get; set; }
        public string WinPassword { get; set; }


    }
}
