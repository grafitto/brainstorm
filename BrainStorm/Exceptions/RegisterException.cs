using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    class RegisterException : MemoryException
    {
        public RegisterException() { }
        public RegisterException(string message) : base(message) { }
        public RegisterException(string message, Exception inner) : base(message, inner) { }
    }
}
