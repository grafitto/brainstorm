using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrainStorm.Processors.SP2000.Processor;
using BrainStorm.Processors.SP2000.Memory;
using brainstorm.Exceptions;
using brainstorm.Base;

namespace BrainStorm.Processors.SP2000.Instructions.Memory
{
    class SwInstruction : TwoOperandInstruction
    {
        private string regSource;
        private int ramDest;
        private string destRegister;

        private bool IS_ADDRESSED = false;
        private string addressRegister;
        private int addressOffset = 0;

        public SwInstruction(string instruction, string regSource, int ramDest) : base(instruction, regSource, ramDest.ToString())
        {
            IS_ADDRESSED = false;
            this.regSource = regSource;
            this.ramDest = ramDest;
        }

        public SwInstruction(string instruction, string regSource, string ramDest): base(instruction, regSource, ramDest)
        {
            this.IS_ADDRESSED = true;
            this.regSource = regSource;
            //regAddress looks like ($t0) or 5($t0)
            string[] offset = ramDest.Split(new char[] { '(' });
            if (offset[0] != String.Empty)
            {
                try
                {
                    addressOffset = Convert.ToInt32(offset[0]);
                }
                catch (ArgumentException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            addressRegister = offset[1].Trim(new char[] { ')' });
        }

        public override void execute(SP2000Core core)
        {
            SP2000Registers registers = (SP2000Registers)core.Registers;
            try
            {
                Register register = registers.FetchRegister(regSource);
                SP2000DataMemory memory = SP2000DataMemory.Instance;

                if (IS_ADDRESSED)
                {
                    Register addressReg = registers.FetchRegister(addressRegister);
                    int finalAddress = addressOffset + addressReg.GetValue();

                    memory.StoreWord(register.GetValue(), finalAddress);
                }
                else
                {
                    memory.StoreWord(register.GetValue(), ramDest);
                }
               
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
