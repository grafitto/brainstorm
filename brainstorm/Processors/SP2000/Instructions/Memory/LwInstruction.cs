using brainstorm.Base;
using brainstorm.Exceptions;
using BrainStorm.Processor.SP2000.Instructions;
using BrainStorm.Processors.SP2000.Memory;
using BrainStorm.Processors.SP2000.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BrainStorm.Processors.SP2000.Instructions.Memory
{
    class LwInstruction : TwoOperandInstruction
    {
        private string destRegister;
        private int ramSource;

        private bool IS_ADDRESSED = false;
        private string addressRegister;
        private int addressOffset = 0;

        public LwInstruction(string instruction, string destRegister, int ramSource) : base(instruction, destRegister, ramSource.ToString())
        {
            this.IS_ADDRESSED = false;
            this.destRegister = destRegister;
            this.ramSource = ramSource;
        }
        public LwInstruction(string instruction, string destRegister, string regAddress): base(instruction, destRegister, regAddress)
        {
            this.IS_ADDRESSED = true;
            this.destRegister = destRegister;
            //regAddress looks like ($t0) or 5($t0)
            string[] offset = regAddress.Split(new char[] { '(' });
            if(offset[0] != String.Empty)
            {
                try
                {
                    addressOffset = Convert.ToInt32(offset[0]);
                } catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            addressRegister = offset[1].Trim(new char[] { ')' });
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
                Register register = registers.FetchRegister(destRegister);
                SP2000DataMemory memory = SP2000DataMemory.Instance;
                int finalOffset = 0;

                if (IS_ADDRESSED)
                {
                    Register address = registers.FetchRegister(this.addressRegister);
                    finalOffset = addressOffset + address.GetValue();
                }

                int data = memory.LoadWord(ramSource + finalOffset);
                register.SetValue(data);
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
