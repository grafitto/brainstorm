using BrainStorm.Base;
using BrainStorm.Events;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;

namespace BrainStorm.Processor.SP2000.Instructions
{
    abstract class SP2000Instruction : Instruction
    {
        private int cycles = 1;
        public new int Cycles { get { return cycles; } set { cycles = Cycles; } }
        public SP2000Instruction(string instruction) : base(instruction) { }

        /// <summary>
        /// This is where the instruction executes itself using the visitor partern
        /// </summary>
        /// <param name="processor"></param>

        virtual public void execute(SP2000Core core)
        {

        }
        protected void increamentPC(SP2000Core core, int? address = null )
        {

            SP2000Registers registers = (SP2000Registers)core.Registers;
            Register pc = registers.FetchRegister("PC");
            if(address == null)
            {
                pc.SetValue(pc.Value + 1);
            }
            else
            {
                pc.SetValue((int)address);
            }
        }
        protected virtual void OnConsoleWrite(ConsoleWriteEventArgs e)
        {
            this.ConsoleWrite(this, e);
        }
        protected virtual void OnConsoleRead(ConsoleReadEventArgs e)
        {
            this.ConsoleRead(this, e);
        }
        public event EventHandler<ConsoleWriteEventArgs> ConsoleWrite;
        public event EventHandler<ConsoleReadEventArgs> ConsoleRead;
    }
}
