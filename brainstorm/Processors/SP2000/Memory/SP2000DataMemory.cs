using BrainStorm.Memory;
using System;

namespace BrainStorm.Processors.SP2000.Memory
{
    class SP2000DataMemory : DataMemory
    {
        private static volatile SP2000DataMemory instance;
        private static object syncRoot = new object();
        private SP2000DataMemory(int size = 1024) : base(size) { }
        public SP2000DataMemory(): base(1024) { }
        public static SP2000DataMemory Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncRoot)
                    {
                        if(instance == null)
                        {
                            instance = new SP2000DataMemory();
                        }
                    }
                }
                return instance;
            }
        }

        public void ShowMemory()
        {
            Console.WriteLine("============Memory============");
            for(int i = 0; i < this.memory.Length; i++)
            {
                if(this.memory[i] != byte.MinValue)
                {
                    Console.WriteLine(i + " => " + this.memory[i]);
                }
            }
        }
        
    }
}
