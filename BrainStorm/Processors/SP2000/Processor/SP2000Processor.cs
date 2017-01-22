using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Base;
using System.Threading;

namespace BrainStorm.Processors.SP2000.Processor
{
    class SP2000Processor : Base.Processor
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
            /*
            for(int i = 0; i < this.cores.Length; i++)
            {
                Contexts contexts = Contexts.Instance;
                if(i == DEFAULT_CONTEXT)
                {
                    if(contexts.GetContextAddress(DEFAULT_CONTEXT) != -1)
                    {
                        SP2000Registers registers = (SP2000Registers)this.cores[DEFAULT_CONTEXT].Registers;
                        Register pc = registers.FetchRegister("PC");
                        pc.SetValue(contexts.GetContextAddress(DEFAULT_CONTEXT));
                        contexts.SetContextAddress(DEFAULT_CONTEXT, -1);
                    }
                    this.cores[DEFAULT_CONTEXT].execute();
                }
                else
                {
                    if (contexts.GetContextAddress(i) != DEFAULT_CONTEXT)
                    {
                        if(contexts.GetContextAddress(i) != INC_CONTEXT_PC)
                        {
                            SP2000Registers registers = (SP2000Registers)this.cores[i].Registers;
                            Register pc = registers.FetchRegister("PC");
                            pc.SetValue(contexts.GetContextAddress(i));
                            contexts.SetContextAddress(i, INC_CONTEXT_PC);
                        }
                        else
                        {
                            new Thread(new ThreadStart(this.cores[i].execute)).Start();
                        }
                    }
                }
            }
            */
        }
        public void ShowRegisters()
        {
            this.cores[DEFAULT_CONTEXT].Registers.ShowRegisters();
        }
    }
}
