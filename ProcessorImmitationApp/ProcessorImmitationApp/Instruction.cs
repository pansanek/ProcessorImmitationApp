using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Instruction
    {
        public int OpCode { get; }
        public int Operand1 { get; }
        public int Operand2 { get; }

        public Instruction(int opCode, int operand1, int operand2)
        {
            OpCode = opCode;
            Operand1 = operand1;
            Operand2 = operand2;
        }

        public override string ToString()
        {
            return $"[{OpCode}, {Operand1}, {Operand2}]";
        }
    }
}
