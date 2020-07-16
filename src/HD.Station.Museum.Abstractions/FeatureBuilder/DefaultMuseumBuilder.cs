using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum
{
    public class DefaultMuseumBuilder : IMuseumBuilder
    {
        public IServiceCollection Services { get; set; }
        public IConfiguration Configuration { get; set; }
        public DefaultMuseumBuilder(IServiceCollection services ,IConfiguration configuration)
        {
            Services = services;
            Configuration = configuration;
        }

    }
}
