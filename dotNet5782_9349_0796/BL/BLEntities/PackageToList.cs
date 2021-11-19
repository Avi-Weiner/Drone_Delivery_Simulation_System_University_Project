using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        class PackageToList
        {
            public int Id { get; set; }
            public string SenderName { get; set; }
            public string ReceiverName { get; set; }
            public IDAL.DO.WeightCategory Weight { get; set; }
            public IDAL.DO.Priority Priority { get; set; }
            public PackageStatus PackageStatus { get; set; }
        }
    }
}

