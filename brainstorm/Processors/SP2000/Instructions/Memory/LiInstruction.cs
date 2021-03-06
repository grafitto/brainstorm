﻿using BrainStorm.Base;
using BrainStorm.Exceptions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Memory
{
    class LiInstruction : TwoOperandInstruction
    {
        private int immidiate;
        public LiInstruction(string instruction, string firstOperand, int secondOperand) : base(instruction, firstOperand, secondOperand.ToString())
        {
            immidiate = secondOperand;
        }
        public LiInstruction(string instruction, string firstOperand, string secondOperand) : base(instruction, firstOperand, secondOperand)
        {
            try
            {
                immidiate = Convert.ToInt32(secondOperand);
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// This excecuted the instruction and changes the CPU register values
        /// </summary>
        /// <param name="processor"></param>
        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            try
            {
                Register destination = registers.FetchRegister(firstOperand);
                destination.SetValue(immidiate);
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.increamentPC(core);
                core.Cycles += Cycles;
            }
        }
    }
}
