using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEdgeDetection.BusinessLayer
{
    public class Filter : IFilter
    {
        private String filterName;

        public string getFilterName()
        {
            return filterName;
        }

        public void setFilterName(String name)
        {
            filterName = name;
        }
    }
}
