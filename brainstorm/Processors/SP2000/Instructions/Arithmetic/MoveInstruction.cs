﻿using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Arithmetic
{
    class MoveInstruction : ArithmeticInstruction
    {
        public MoveInstruction(string instruction, string destination, string secondOperand) : base(instruction, destination, "0", secondOperand){}

        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            int result = 0;
            try
            {
                Register value = registers.FetchRegister(secondOperand);
                result = 0 + value.Value;
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                registers.StoreToName(destination, result);
                this.increamentPC(core);
                core.Cycles += Cycles;
            }
        }
    }
}
