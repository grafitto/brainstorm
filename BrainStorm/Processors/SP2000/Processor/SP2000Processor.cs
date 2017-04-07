using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Events;
using BrainStorm.Base;
using System;

namespace BrainStorm.Processors.SP2000.Processor
{
    public class SP2000Processor : Base.Processor
    {
        private SP2000Core[] cores;
        public SP2000Core[] Chips
        {
            get
            {
                return this.cores;
            }
        }
        private int context = 0;
        private static int DEFAULT_CONTEXT = 0;
        //private static int INC_CONTEXT_PC = 0;

        public SP2000Processor(SP2000InstructionMemory program, int cores = 1) : base(program, cores)
        {
            this.cores = new SP2000Core[cores];
            for(int i = 0; i < cores; i++)
            {
                this.cores[i] = new SP2000Core(program, context);
                //Contexts cxt = Contexts.Instance;
                //cxt.AddCotext(i);
            }
        }
        public override void Tick()
        {
            this.cores[DEFAULT_CONTEXT].execute();
            //Handle any print event bubbled
            this.cores[DEFAULT_CONTEXT].ConsoleWrite += OnConsoleWrite;
            this.cores[DEFAULT_CONTEXT].ConsoleRead += OnConsoleRead;
        }
        public Register[] GetRegisters()
        {
            return this.cores[DEFAULT_CONTEXT].GetRegisters();
        }

        public void OnConsoleWrite(object sender, ConsoleWriteEventArgs e)
        {
            //Bubble any print event received
            ConsoleWrite(sender, e);
        }
        public void OnConsoleRead(object sender, ConsoleReadEventArgs e)
        {
            //Bubble any print event received
            ConsoleRead(sender, e);
        }

        public event EventHandler<ConsoleWriteEventArgs> ConsoleWrite;
        public event EventHandler<ConsoleReadEventArgs> ConsoleRead;
    }
}
