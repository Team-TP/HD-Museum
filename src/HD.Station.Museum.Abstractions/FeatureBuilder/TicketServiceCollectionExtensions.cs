using HD.Station.Museum;
using HD.Station.Museum.Sevices;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MuseumServiceCollectionExtensions
    {
        public static IMuseumBuilder AddMuseumFeature (this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MuseumOption>(configuration);
            return new DefaultMuseumBuilder(services, configuration);
        }
        public static IMuseumBuilder AddManagers (this IMuseumBuilder buider)
        {
            buider.Services.AddScoped<ICourseManager, CourseManager>();
            buider.Services.AddScoped<IStudentManager, StudentManager>();
            buider.Services.AddScoped<IEnrollmentManager, EnrollmentManager>();
            buider.Services.AddScoped<ICategoryManager, CategoryManager>();
            return buider;
        }
    }
}
