using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    interface Simulator
    {
        /// <summary>
        /// A single processor tick
        /// </summary>
        void Step();

        /// <summary>
        /// Run a processor with a delay between processor ticks. Default delay: 10ms
        /// </summary>
        /// <param name="delay"></param>
        void Run(int delay = 10);

        /// <summary>
        /// Stops the processor completely, resets the PC to entryAddress
        /// </summary>
        void Stop();

        /// <summary>
        /// Suspends the processor after the current instruction
        /// </summary>
        void Pause();
    }
}
