using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    class IndexNotFoundException : BrainStormException
    {
        public IndexNotFoundException() { }
        public IndexNotFoundException(string message) : base(message) { }
        public IndexNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
