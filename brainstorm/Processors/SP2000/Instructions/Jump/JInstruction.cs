
using brainstorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;

namespace BrainStorm.Processors.SP2000.Instructions.Jump
{
    class JInstruction : OneOperandInstruction
    {
        private int address;
        public JInstruction(string instruction, int address) : base(instruction, address.ToString())
        {
            this.address = address;
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;

            try
            {
                registers.FetchRegister("PC").SetValue(address);
            }catch(RegisterException e)
            {
                System.Console.WriteLine(e.Message);
            }finally
            {
                core.Cycles += Cycles;
            }
        }
    }
}
