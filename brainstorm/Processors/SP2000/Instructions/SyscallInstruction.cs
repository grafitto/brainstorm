
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Processor;

namespace BrainStorm.Processors.SP2000.Instructions
{
    
    class SyscallInstruction : SP2000Instruction
    {
        const int PRINT_STRING = 4;
        const int EXIT = 10;
        public SyscallInstruction(string instruction) : base(instruction) { }
        public SyscallInstruction(): base("Syscall") { }

        public override void execute(SP2000Core core)
        {
            int value = core.Registers.FetchRegister("$v0").GetValue();
            switch (value)
            {
                case PRINT_STRING:
                    break;
                case EXIT:
                    break;
                
            }
            base.execute(core);
        }
    }
}
