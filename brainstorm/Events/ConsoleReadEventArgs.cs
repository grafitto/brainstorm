using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Events
{
    public class ConsoleReadEventArgs
    {
        public string Message { get; }
        public ConsoleReadEventArgs(string message)
        {
            Message = message;
        }
    }
}
