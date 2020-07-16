using HD.Station.Museum.Stores;
using System;
using System.Collections.Generic;
using System.Text;

namespace HD.Station.Museum.Sevices
{
    public class CategoryManager : ManagerBase<Categories, Guid>, ICategoryManager
    {
        public CategoryManager(IServiceProvider serviceProvider, ICategoryStore store) : base(serviceProvider, store)
        {
        }
    }
}
