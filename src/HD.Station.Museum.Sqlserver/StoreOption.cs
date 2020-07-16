using System;

namespace HD.Station.Dashboard.SqlServer
{
    public class StoreOption
    {
        public string ConnectionName { get; set; } = "HDStation";
        public string Schema { get; set; } = "Artifacts";
        public string SchemaApp { get; set; } = "Train";
    }
}
