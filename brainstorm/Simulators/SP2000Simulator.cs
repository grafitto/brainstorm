using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BrainStorm.Simulators
{
    class SP2000Executor : Simulator
    {
        private static object executionLock = new object();
        SP2000InstructionMemory Program;
        SP2000Processor Processor;
        private int startAddress, instructionSize;
        public bool IsPaused;
        public bool IsStopped;

        public SP2000Executor(int startAddress, SP2000InstructionMemory program, int instructionSize)
        {
            this.startAddress = startAddress;
            this.instructionSize = instructionSize;
            this.Program = program;
            this.Processor = new SP2000Processor(program);

            //Load start address to the processor
            try
            {
                Processor.Chips[0].Registers.FetchRegister("PC").SetValue(startAddress);
            }catch(RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public virtual void Terminate()
        {
        }

        public virtual void Step()
        {
            lock (executionLock)
            {
                IsPaused = false;
                if (!IsPaused && !IsStopped)
                {
                    Processor.Tick();
                    IsPaused = true;
                }
            }
        }

        public virtual void Run(int delay)
        {
            lock (executionLock)
            {
                int currentAddress = Processor.Chips[0].Registers.FetchRegister("PC").GetValue();
                while ((!IsStopped || !IsPaused) && currentAddress < instructionSize)
                {
                    Processor.Tick();
                    currentAddress = Processor.Chips[0].Registers.FetchRegister("PC").GetValue();
                    //Pause for delay time
                    Thread.Sleep(delay);
                }
            }
        }

        public virtual void Stop()
        {
            lock (executionLock)
            {
                IsStopped = true;
                //Reset the PC address
                try
                {
                    Processor.Chips[0].Registers.FetchRegister("PC").SetValue(startAddress);
                }
                catch (RegisterException e)
                {
                    Console.WriteLine(e.Message);
                }
            }        
        }

        public virtual void Pause()
        {
            lock (executionLock)
            {
                if (!IsPaused || !IsStopped)
                    IsPaused = true;
            }
        }
    }

    class SP2000Simulator : SP2000Executor
    {
        public SP2000Simulator(int startAddress, SP2000InstructionMemory program, int instructionSize) : base(startAddress, program, instructionSize)
        {
        }
        public override void Run(int delay = 100)
        {
            new Thread(() => base.Run(delay)).Start();
        }
        public override void Step()
        {
            new Thread(() => base.Step()).Start();
        }
        public override void Pause()
        {
            new Thread(() => base.Pause()).Start();
        }
        public override void Stop()
        {
            new Thread(() => base.Stop()).Start();
        }
    }
}
