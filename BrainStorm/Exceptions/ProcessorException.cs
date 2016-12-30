using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    class ProcessorException : RuntimeSimException
    {
        public ProcessorException() { }
        public ProcessorException(string message) : base(message) { }
        public ProcessorException(string message, Exception inner) : base(message, inner) { }
    }
}
