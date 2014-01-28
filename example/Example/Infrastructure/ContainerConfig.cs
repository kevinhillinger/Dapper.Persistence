using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StructureMap;

namespace Example.Infrastructure
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            ObjectFactory.Initialize(c => c.AddRegistry<ProgramRegistry>());
        }
    }
}
