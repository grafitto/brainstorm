using BrainStorm.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrainStorm.Base
{
    abstract class RegisterSet
    {
        private Dictionary<String, int> indices;
        //nullable int
        private int?[] registers;

        /**
         * Initialization if registers and indices
         **/
        public RegisterSet(int size)
        {
            this.registers = new int?[size];
            this.indices = new Dictionary<string, int>();
        }
        /**
         * This looks for an index with the provided name, throws KeyNotFoundEsception if the key is not found
         **/
        public int Lookup(String name)
        {
            if (this.indices.ContainsKey(name)) {

                return this.indices[name];
            } else {

                Exceptions.KeyNotFoundException up = new Exceptions.KeyNotFoundException("Name lookup in Registers was not successfull :-(");
                throw up;
            }      
        }
        /**
         * This fetches a register value using an index
         * <return>Nullable int</return>
         * <parrams>int index</parrams>
         * <example> 
         * RegisterSet r = new RegisterSet(12);
         * r.FetchRegister(3)
         * </example>
         **/
        public int? FetchRegister(int index)
        {
            if (this.registers.Length > index) { 

                    return this.registers[index];
            } else {

                Exceptions.IndexNotFoundException up = new Exceptions.IndexNotFoundException("Index out of Bound");
                throw up;
            }
        }
        /**
         * Returns a register referred to with the name provided
         **/
        public int? FetchRegister(string name) {

                int index = this.Lookup(name);
                int? value = this.FetchRegister(name);
                return value;
        }
        /**
         * This names a register using desirable names, many names can be assigned to a single register
         **/
        public void MakeRegister(string[] names, int index){

            foreach(var name in names) {

                this.indices.Add(name, index);
            }
        }
        public void MakeRegisters(string[] names) {

            for(int i = 0; i < names.Length; i++)
            {
                this.MakeRegister(new string[] { names[i] }, i);
            }
        }
        public void StoreToName(string name, int value)
        {
            int index = this.Lookup(name);
            this.registers[index] = value;
        }
    }
}
