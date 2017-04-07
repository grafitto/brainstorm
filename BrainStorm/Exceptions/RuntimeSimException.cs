using BrainStorm.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    public class RuntimeSimException : SimulationException
    {
        public RuntimeSimException() { }
        public RuntimeSimException(string message) : base(message) { }
        public RuntimeSimException(string message, Exception inner) : base(message, inner) { }
    }
}
