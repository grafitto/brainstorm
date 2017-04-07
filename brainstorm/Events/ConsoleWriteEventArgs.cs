using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Events
{
    public class ConsoleWriteEventArgs : EventArgs
    {
        public string Message { get; }
        public ConsoleWriteEventArgs(string message)
        {
            Message = message;
        }
    }
}
