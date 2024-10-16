using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Memory
    {
        public int[] DataMemory { get; private set; }
        public List<Instruction> CommandMemory { get; private set; }

        public Memory(int dataSize)
        {
            DataMemory = new int[dataSize];
            CommandMemory = new List<Instruction>();
        }

        public void LoadData(int[] initialData)
        {
            Array.Copy(initialData, DataMemory, initialData.Length);
        }

        public void LoadProgram(List<Instruction> program)
        {
            CommandMemory = program;
        }

        public Instruction FetchInstruction(int pc)
        {
            Instruction instruction = CommandMemory[pc];
            Console.WriteLine($"Извлечена команда: {instruction}");
            return instruction;
        }
    }
}
