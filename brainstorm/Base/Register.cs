using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace brainstorm.Base
{
    class Register
    {
        private string[] names;
        public string[] Names
        {
            get { return names;  }
            set { names = Names; }
        }
        private int value;
        public int Value
        {
            get { return value; }
            set { this.value = Value; }
        }
        public string text;
        public string Text
        {
            get { return text; }
            set { text = Text; }
        }

        public Register() { }
        public Register(string[] names, int value, string text)
        {
            this.names = names;
            this.value = value;
            this.text = text;
        }
        public void SetValue(int value)
        {
            this.value = value;
        }
        public override string ToString()
        {
            return text;
        }

        public static int operator + (Register self, Register other)
        {
            return self.Value + other.Value;
        }

        public static int operator -(Register self, Register other)
        {
            return self.Value - other.Value;
        }
        public static int operator *(Register self, Register other)
        {
            return self.Value * other.Value;
        }
        public static int operator &(Register self, Register other)
        {
            return self.Value & self.Value;
        }
        public static int operator |(Register self, Register other)
        {
            return self.Value | other.Value;
        }
        public static int operator ^(Register self, Register other)
        {
            return self.Value ^ other.Value;
        }
    }
}
