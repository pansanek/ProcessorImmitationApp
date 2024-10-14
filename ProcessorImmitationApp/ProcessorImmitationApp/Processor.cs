using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Processor
    {
        private int[] reg = new int[8];  // 8 регистров общего назначения
        private int pc = 0;  // Счетчик команд

        private const int LOAD = 1;
        private const int STORE = 2;
        private const int ADD = 3;
        private const int SUB = 4;
        private const int HALT = 99;

        private Memory memory;

        public Processor(Memory mem)
        {
            memory = mem;
        }

        public void ExecuteProgram()
        {
            bool running = true;
            while (running)
            {
                Instruction instruction = memory.FetchInstruction(pc);  // Извлечение команды
                running = DecodeAndExecute(instruction);  // Декодирование и выполнение
                pc++;  // Переход к следующей команде
                Console.WriteLine();
            }
        }

        private bool DecodeAndExecute(Instruction instruction)
        {
            int op = instruction.OpCode;
            int addr1 = instruction.Operand1;
            int addr2 = instruction.Operand2;

            switch (op)
            {
                case LOAD:
                    reg[addr1] = memory.DataMemory[addr2];  // Загрузка данных из памяти в регистр
                    break;
                case STORE:
                    memory.DataMemory[addr1] = reg[addr2];  // Сохранение данных из регистра в память
                    break;
                case ADD:
                    reg[addr1] = reg[addr1] + reg[addr2];  // Сложение данных двух регистров
                    break;
                case SUB:
                    reg[addr1] = reg[addr1] - reg[addr2];  // Вычитание данных двух регистров
                    break;
                case HALT:
                    Console.WriteLine("Остановка программы");
                    return false;
                default:
                    Console.WriteLine($"Неизвестная команда: {op}");
                    break;
            }

            // Вывод состояния после выполнения команды
            Console.WriteLine($"PC: {pc}");
            Console.WriteLine("Регистры: " + string.Join(", ", reg));
            Console.WriteLine("Память данных: " + string.Join(", ", memory.DataMemory));

            return true;
        }
    }
}

