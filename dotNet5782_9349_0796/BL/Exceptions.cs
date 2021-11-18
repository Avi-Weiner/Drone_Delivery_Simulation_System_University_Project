using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBL
{
    namespace BO
    {
        public class MessageException : Exception
        {
            public MessageException(string message) : base(message) { }

        }
    }
}
