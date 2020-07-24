using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class CategoryMachineManager : ManagerBase<CategoryMachines, Guid>, ICategoryMachineManager
    {
        public CategoryMachineManager(IServiceProvider serviceProvider, ICategoryMachineStore store) : base(serviceProvider, store)
        {
        }
    }
}
