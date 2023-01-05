using System;

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
