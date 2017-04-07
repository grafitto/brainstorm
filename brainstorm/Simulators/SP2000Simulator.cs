using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Threading;
using BrainStorm.Events;

namespace BrainStorm.Simulators
{
    public class SP2000Executor : Simulator
    {
        private static object executionLock = new object();
        public SP2000InstructionMemory Program;
        public SP2000Processor Processor;
        private int startAddress, instructionSize;
        public bool IsPaused;
        public bool IsStopped;

        public SP2000Executor(int startAddress, SP2000InstructionMemory program, int instructionSize)
        {
            this.startAddress = startAddress;
            this.instructionSize = instructionSize;
            this.Program = program;
            this.Processor = new SP2000Processor(program);

            //Register any print event received
            Processor.ConsoleWrite += OnConsoleWrite;
            Processor.ConsoleRead += OnConsoleRead;
            //Load start address to the processor
            try
            {
                Processor.Chips[0].Registers.FetchRegister("PC").SetValue(startAddress);
            }catch(RegisterException e)
            {
                throw new RegisterException("Error setting the PC register.", e);
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
                int temp = startAddress;
                while (temp < Program.Size())
                {
                    this.Step();
                    temp++;
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
                    IsStopped = false;
                }
                catch (RegisterException e)
                {
                    throw new RegisterException("Error resetting PC register.", e);
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
        /// <summary>
        /// Returns all the registers of the CPU being Simulated
        /// </summary>
        /// <returns></returns>
        public Register[] GetRegisters()
        {
            return Processor.GetRegisters();
        }
        /// <summary>
        /// Console Write event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnConsoleWrite(object sender, ConsoleWriteEventArgs e)
        {
            //Bubble a print event
            ConsoleWrite(sender, e);
        }
        public void OnConsoleRead(object sender, ConsoleReadEventArgs e)
        {
            //Bubble a print event
            ConsoleRead(sender, e);
        }

        /// <summary>
        /// Console write event
        /// </summary>
        public event EventHandler<ConsoleWriteEventArgs> ConsoleWrite;
        public event EventHandler<ConsoleReadEventArgs> ConsoleRead;
    }

    public class SP2000Simulator : SP2000Executor
    {
        public SP2000Simulator(int startAddress, SP2000InstructionMemory program, int instructionSize) : base(startAddress, program, instructionSize)
        {
        }
        /// <summary>
        /// Runs all the instructions in the instruction memory
        /// </summary>
        /// <param name="delay"></param>
        public override void Run(int delay = 100)
        {
            new Thread(() => base.Run(delay)).Start();
            //base.Run(delay);
        }
        /// <summary>
        /// Executes the next instruction in the PC
        /// </summary>
        public override void Step()
        {
            //new Thread(() => base.Step()).Start();
            base.Step();
        }
        /// <summary>
        /// Pauses a CPU if it is executing
        /// </summary>
        public override void Pause()
        {
            //new Thread(() => base.Pause()).Start();
            base.Pause();
        }
        /// <summary>
        /// Stops a CPU if it is in excution
        /// </summary>
        public override void Stop()
        {
            //new Thread(() => base.Stop()).Start();
            base.Stop();
        }
        /// <summary>
        /// Gets all the registers of the CPU bein emulated
        /// </summary>
        /// <returns></returns>
        public new Register[] GetRegisters()
        {
            return base.GetRegisters();
        }
    }
}
