using BrainStorm.Base;

namespace BrainStorm.Processors.SP2000.Memory
{
    class SP2000Registers : RegisterSet
    {
        public SP2000Registers() : base(35)
        {
            this.MakeRegister(new string[] { "zero" }, 0, "This register is hardwired to ZERO and cannot be changed"); //Always ZERO
            this.MakeRegister(new string[] { "$at" }, 1);

            //Registers of values from expression evaluation and function results
            this.MakeRegisters(new string[] { "$v0", "$v1" }, new int[] { 2, 3 });

            //Registers storing the first four parameters for subroutines
            //The values are not reserved across multiple function calls
            string[] param_names = new string[] { "$a0", "$a1", "$a2", "$a3" };
            int[] param_values = new int[] { 4, 5, 6, 7 };
            this.MakeRegisters(param_names, param_values);

            //Temporary registers where caller is saven when needed
            //Subroutines can use with/out saving
            //Not preserved across procedure calls
            string[] temp_names = new string[] { "$t0", "$t1", "$t2", "$t3", "$t4", "$t5", "$t6", "$t7" };
            int[] temp_values = new int[] { 8, 9, 10, 11, 12, 13, 14, 15 };
            this.MakeRegisters(temp_names, temp_values);

            //Saved values registers. Callee saved
            //Subroutines using one of these must save the original and restore it before exiting
            //Preserved across procedure calls
            string[] saved_names = new string[] { "$s0", "$s1", "$s2", "$s3", "$s4", "$s5", "$s6", "$s7" };
            int[] saved_values = new int[] { 16, 17, 18, 19, 20, 21, 22, 23 };
            this.MakeRegisters(saved_names, saved_values);

            //Temporary registers where caller is saven when needed
            //Subroutines can use with/out saving
            //Not preserved across procedure calls
            this.MakeRegisters(new string[] { "$t8", "$t9" }, new int[] { 24, 25 });

            //Reserved for use by the interrupt/trap handler
            this.MakeRegisters(new string[] { "$k0", "$k1" }, new int[] { 26, 27 });

            //Global pointer
            //points to the middle of the 64K block of memory in the static data segment
            this.MakeRegister(new string[] { "$gp" }, 28);

            //Stack pointer
            //Points to the last location on the stack
            this.MakeRegister(new string[] { "$sp" }, 29);

            //Saved value, Frame pointer
            //Preserved across procedure calls
            this.MakeRegister(new string[] { "$s8", "$fp" }, 30);

            //Return address
            this.MakeRegister(new string[] { "$ra" }, 31);

            //These addresses are UNTOUCHABLE
            this.MakeRegisters(new string[] { "PC", "HI", "LO" }, new int[] { 32, 33, 34 });
        }

        internal void StoreToName(string destination, uint result)
        {
            this.StoreToName(destination, (int)result);
        }
        internal new void StoreToName(string destination, int result)
        {
            if (destination != "zero")
            {
                base.StoreToName(destination, result);
            }
        }
    }
}
