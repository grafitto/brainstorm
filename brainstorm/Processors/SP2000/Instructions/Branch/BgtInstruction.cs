﻿using BrainStorm.Processors.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Base;
using BrainStorm.Exceptions;
using System;

namespace BrainStorm.Processors.SP2000.Instructions.Branch
{
    class BgtInstruction : ThreeOperandInstruction
    {
        private string firstRegister;
        private string secondRegister;
        private int address;
        public BgtInstruction(string instruction, string firstRegister, string secondRegister, int address) : base(instruction, firstRegister, secondRegister, address.ToString())
        {
            this.firstRegister = firstRegister;
            this.secondRegister = secondRegister;
            this.address = address;
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;

            try
            {
                Register first = registers.FetchRegister(this.firstRegister);
                Register second = registers.FetchRegister(this.secondRegister);

                if (first.GetValue() > second.GetValue())
                {
                    Register pc = registers.FetchRegister("PC");
                    pc.SetValue(this.address);
                }
            }
            catch (RegisterException e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                core.Cycles += Cycles;
            }
        }
    }
}
