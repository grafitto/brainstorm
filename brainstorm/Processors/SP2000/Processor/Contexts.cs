using System.Collections.Generic;

namespace BrainStorm.Processors.SP2000.Processor
{
    sealed class Contexts
    {
        private Dictionary<int, int> buffer;
        private static volatile Contexts instance;
        private static object syncRoot = new object();
        
        public Contexts(){ this.buffer = new Dictionary<int, int>();  }

        /// <summary>
        /// Checks if a context exists
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool ContextExists(int context)
        {
            return buffer.ContainsKey(context);
        }

        /// <summary>
        /// Gets an Instruction address from a CPU context
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public int GetContextAddress(int context)
        {
            return buffer[context];
        }

        /// <summary>
        /// Sets an Instruction Address to a CPU context for execution
        /// </summary>
        /// <param name="context"></param>
        /// <param name="address"></param>
        public void SetContextAddress(int context, int address)
        {
            if (this.ContextExists(context))
            {
                buffer[context] = address;
            }
        }

        /// <summary>
        /// Adds a CPU context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="address"></param>
        public void AddCotext(int context, int? address = null)
        {
            buffer.Add(context, address!=null?(int)address:-1 );
        }

        /// <summary>
        /// Makes sure only one instance of the Contexts table exists
        /// </summary>
        public static Contexts Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncRoot)
                    {
                        if(instance == null)
                        {
                            instance = new Contexts();
                        }
                    }
                }
                return instance;
            }
        }

    }
}
