using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Events;

namespace BrainStorm.Processors.SP2000.Instructions
{
    
    class SyscallInstruction : SP2000Instruction
    {
        const int PRINT_INT = 1;
        const int PRINT_STRING = 4; //
        const int READ_INTEGER = 5; //
        const int READ_STRING = 8; //
        const int PRINT_REGISTERS = 6;//
        const int EXIT_0 = 10;//
        const int EXIT_INT = 17;//
        
        public SyscallInstruction(string instruction) : base(instruction) { }
        public SyscallInstruction(): base("Syscall") { }

        public override void execute(SP2000Core core)
        {
            int value = core.Registers.FetchRegister("$v0").GetValue();
            switch (value)
            {
                case PRINT_STRING:
                    SP2000DataMemory memory = SP2000DataMemory.Instance;
                    int address = core.Registers.FetchRegister("$a0").GetValue();
                    string memString = memory.LoadString(address).ToString();
                    //Bubble up a print event
                    ConsoleWriteEventArgs args = new ConsoleWriteEventArgs(memString);
                    OnConsoleWrite(args);
                    break;
                case PRINT_REGISTERS:
                    //Bubble up a print event
                    ConsoleWriteEventArgs printArgs = new ConsoleWriteEventArgs(core.Registers.ToString());
                    OnConsoleWrite(printArgs);
                    break;
                case PRINT_INT:
                    //Bubble up a print_int event
                    int printVal = core.Registers.FetchRegister("$a0").GetValue();
                    ConsoleWriteEventArgs printIntArgs = new ConsoleWriteEventArgs(printVal.ToString());
                    OnConsoleWrite(printIntArgs);
                    break;
                case READ_INTEGER:
                    //Bubble up a read_int event
                    ConsoleReadEventArgs readIntArgs = new ConsoleReadEventArgs("Integer");
                    OnConsoleRead(readIntArgs);
                    break;
                case READ_STRING:
                    //Bubble up a read_string event
                    ConsoleReadEventArgs readStringArgs = new ConsoleReadEventArgs("String");
                    OnConsoleRead(readStringArgs);
                    break;
                case EXIT_0:
                    //Bubble up an exit request
                    ConsoleWriteEventArgs e = new ConsoleWriteEventArgs("Exit request(0)");
                    OnConsoleWrite(e);
                    break;
                case EXIT_INT:
                    int exitValue = core.Registers.FetchRegister("$a0").GetValue();
                    //Bubble up an exit request
                    ConsoleWriteEventArgs exitArgs = new ConsoleWriteEventArgs("Exit request(" + exitValue + ")");
                    OnConsoleWrite(exitArgs);
                    break;
                
            }
            this.increamentPC(core);
            core.Cycles += this.Cycles;
        }
    }
}
