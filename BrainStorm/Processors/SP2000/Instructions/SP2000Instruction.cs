using BrainStorm.Base;
using System;

namespace BrainStorm.Processor.SP2000.Instructions
{
    class SP2000Instruction : Instruction
    {
        public SP2000Instruction(string instruction) : base(instruction) { }

        public override void execute(Base.Processor processor)
        {
            throw new NotImplementedException();
        }
    }
}
