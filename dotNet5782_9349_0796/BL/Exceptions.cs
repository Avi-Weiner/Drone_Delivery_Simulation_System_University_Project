using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BL
{
    public class MessageException : Exception
    {
        public MessageException(string message) : base(message) { }

        //public override string ToString()
        //{
        //    return Message;
        //}
    }
}

