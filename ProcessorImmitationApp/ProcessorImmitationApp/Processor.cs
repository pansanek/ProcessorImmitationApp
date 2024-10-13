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
        private int ACC = 0;  // Аккумулятор для промежуточных вычислений
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
                Console.WriteLine();  // Разделение между шагами
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
                    ACC = memory.DataMemory[addr2];  // Загрузка данных из памяти в аккумулятор
                    reg[addr1] = ACC;   // Загрузка данных из аккумулятора в регистр
                    Console.WriteLine($"LOAD: Загрузить dmem[{addr2}] в reg[{addr1}], значение: {ACC}");
                    break;
                case STORE:
                    ACC = reg[addr2];   // Загрузка данных из регистра в аккумулятор
                    memory.DataMemory[addr1] = ACC;  // Сохранение данных из аккумулятора в память
                    Console.WriteLine($"STORE: Сохранить reg[{addr2}] в dmem[{addr1}], значение: {ACC}");
                    break;
                case ADD:
                    ACC = reg[addr1] + reg[addr2];  // Сложение данных в аккумуляторе
                    reg[addr1] = ACC;   // Сохранение результата в регистр
                    Console.WriteLine($"ADD: reg[{addr1}] = reg[{addr1}] + reg[{addr2}], результат: {ACC}");
                    break;
                case SUB:
                    ACC = reg[addr1] - reg[addr2];  // Вычитание данных в аккумуляторе
                    reg[addr1] = ACC;   // Сохранение результата в регистр
                    Console.WriteLine($"SUB: reg[{addr1}] = reg[{addr1}] - reg[{addr2}], результат: {ACC}");
                    break;
                case HALT:
                    Console.WriteLine("HALT: Остановка программы");
                    return false;
                default:
                    Console.WriteLine($"Неизвестная команда: {op}");
                    break;
            }

            // Вывод состояния после выполнения команды
            Console.WriteLine($"PC: {pc}, Аккумулятор (ACC): {ACC}");
            Console.WriteLine("Регистры: " + string.Join(", ", reg));
            Console.WriteLine("Память данных: " + string.Join(", ", memory.DataMemory));

            return true;
        }
    }
}

