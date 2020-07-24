using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using HD.Station.Museum.Stores;
using HD.Station.Dashboard.SqlServer;
using HD.Station.Museum.SqlServer.Stores;
using HD.Station.Museum.Sqlserver.Stores;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SqlServerMuseumBuilderExtensions
    {
        public static IMuseumBuilder UseSqlServer(this IMuseumBuilder builder, IConfigurationRoot configuration)
        {
            builder.Services.Configure<StoreOption>(builder.Configuration.GetSection("SqlServer")); // for inject into other services
            var options = new StoreOption();
            builder.Configuration.GetSection("SqlServer").Bind(options);

            var connectionName = options?.ConnectionName ?? "HDStation";
            var connectionString = configuration.GetConnectionString(connectionName);

            builder.Services.AddDbContext<MuseumDbContext>(o => o.UseSqlServer(connectionString)); // add DBContext

            // add stores

            builder.Services.AddScoped<ICourseStore, CourseStore>();
            builder.Services.AddScoped<IStudentStore, StudentStore>();
            builder.Services.AddScoped<IEnrollmentStore, EnrollmentStore>();
            builder.Services.AddScoped<ICategoryStore, CategoryStore>();

            builder.Services.AddScoped<ICategoryMachineStore, CategoryMachineStore>();
            builder.Services.AddScoped<IMachineStore, MachineStore>();
            builder.Services.AddScoped<IMachineProduceStore, MachineProduceStore>();
            builder.Services.AddScoped<IMachineWareHouseStore, MachineWareHouseStore>();

            return builder;
        }
    }
}
