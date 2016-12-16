using BrainStorm.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainstorm
{
    class MIPSRegisters : RegisterSet
    {
        /// <summary>
        /// This initializes the sie of the register set
        /// </summary>
        /// <param name="size"></param>
        public MIPSRegisters(int size) : base(size)
        {
        }
    }
    class Test
    {
        public static void Main(string[] args)
        {
            MIPSRegisters reg = new MIPSRegisters(6);
            reg.MakeRegister(new string[] { "$r1" }, 0);
            reg.StoreToName("$r1", 45);

        }
    }
}
