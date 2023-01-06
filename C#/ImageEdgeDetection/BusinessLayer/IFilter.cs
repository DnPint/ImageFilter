using System;

namespace ImageEdgeDetection.BusinessLayer
{
    public interface IFilter
    {
        string getFilterName();
        void setFilterName(String name);
    }
}
