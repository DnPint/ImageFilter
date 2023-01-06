using System;

namespace ImageEdgeDetection.BusinessLayer
{
    public class Filter : IFilter
    {
        private string filterName;

        public string getFilterName()
        {
            return filterName;
        }

        public void setFilterName(string name)
        {
            filterName = name;
        }
    }
}
