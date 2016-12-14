using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    abstract class BrainStormException : Exception
    {
        public BrainStormException() { }
        public BrainStormException(string message) : base(message) { }
        public BrainStormException(string message, Exception inner) : base(message, inner) { }
    }
}
