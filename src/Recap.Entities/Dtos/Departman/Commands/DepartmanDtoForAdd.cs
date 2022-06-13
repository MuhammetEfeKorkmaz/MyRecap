using Recap.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recap.Entities.Dtos.Departman.Commands
{
    public class DepartmanDtoForAdd:IDto
    {
        public string Name { get; set; }
        public string RootPath { get; set; }

        public string WinUserName { get; set; }
        public string WinPassword { get; set; }
    }
}
