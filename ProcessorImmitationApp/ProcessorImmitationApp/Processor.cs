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
        private int pc = 0;  // Счетчик команд (Program Counter)

        private const uint LOAD = 0;
        private const uint STORE = 1;
        private const uint ADD = 2;
        private const uint HALT = 3;

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
                running = DecodeAndExecute(instruction);  // Декодирование и выполнение команды
                pc++;  // Переход к следующей команде
                Console.WriteLine();
            }
        }

        private bool DecodeAndExecute(Instruction instruction)
        {
            uint cmdType = instruction.CmdType;
            uint op1 = instruction.Operand1;
            uint op2 = instruction.Operand2;
            
            switch (cmdType)
            {
                case LOAD:
                    reg[op1] = memory.DataMemory[op2];  // Загрузка данных из памяти в регистр
                    break;
                case STORE:
                    memory.DataMemory[op1] = reg[op2];  // Сохранение данных из регистра в память
                    break;
                case ADD:
                    reg[op1] = reg[op1] + reg[op2];  // Сложение данных двух регистров
                    break;
                case HALT:
                    Console.WriteLine("Остановка программы");
                    return false;
                default:
                    Console.WriteLine($"Неизвестная команда: {cmdType}");
                    break;
            }

            // Вывод текущего состояния процессора
            Console.WriteLine($"PC: {pc}");
            Console.WriteLine("Регистры: " + string.Join(", ", reg));
            Console.WriteLine("Память данных: " + string.Join(", ", memory.DataMemory));

            return true;
        }
    }
}

