using BrainStorm.Base;
using BrainStorm.Memory;
using BrainStorm.Base;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;

namespace BrainStorm.Processors.SP2000.Processor
{
    class SP2000Core : Core
    {
        private SP2000RegisterSet registers;
        public new SP2000RegisterSet Registers
        {
            get { return registers; }
        }
        private int context;
        public int Context
        {
            get { return context; }
        }
        public int Cycles { get; set; }
        public SP2000Core(InstructionMemory program, int context) : base(program)
        {
            registers = new SP2000Registers();
            this.context = context;
        }

        /// <summary>
        /// Executes an instruction found in the specified address if any, else fetches the from PC
        /// </summary>
        /// <param name="address"></param>
        public override void execute()
        {
                Register register = registers.FetchRegister("PC");
                SP2000Instruction current = this.getInstruction(register.Value);
                current.execute(this);
                Registers.ShowRegisters(); //Will change in future
        }

        /// <summary>
        /// Gets an instruction in an address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        private SP2000Instruction getInstruction(int address)
        {
            
            return (SP2000Instruction)Program.Pop((int)address);
        }
    }
}
