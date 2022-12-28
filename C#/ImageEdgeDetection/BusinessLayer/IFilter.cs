using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageEdgeDetection.BusinessLayer
{
    public interface IFilter
    {
        string getFilterName();
        void setFilterName(String name);
    }
}
