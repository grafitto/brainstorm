﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Exceptions
{
    public class MemoryException : RuntimeSimException
    {
        public MemoryException() { }
        public MemoryException(string message) : base(message) { }
        public MemoryException(string message, Exception inner) : base(message, inner) { }
    }
}
