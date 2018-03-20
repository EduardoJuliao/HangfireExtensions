using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangfireExtensions
{
    public interface IJob
    {
        void Integrate(PerformContext context, object[] parameters);
    }
}
