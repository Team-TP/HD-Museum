using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IMuseumBuilder
    {
        IConfiguration Configuration { get; set; }
        IServiceCollection Services { get; set; }
    }
}
