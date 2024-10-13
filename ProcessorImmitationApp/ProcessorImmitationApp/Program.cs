using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessorImmitationApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            // Создание памяти
            Memory memory = new Memory(10);

            // Инициализация данных в памяти
            int[] initialData = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 0 };  // Последняя ячейка для суммы
            memory.LoadData(initialData);

            // Программа для суммирования всех элементов массива
            List<Instruction> program = new List<Instruction>
            {
            new Instruction(1, 0, 0),  // Загрузить значение dmem[0] в R0 через ACC
            new Instruction(1, 1, 1),  // Загрузить значение dmem[1] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 2),  // Загрузить значение dmem[2] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 3),  // Загрузить значение dmem[3] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 4),  // Загрузить значение dmem[4] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 5),  // Загрузить значение dmem[5] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 6),  // Загрузить значение dmem[6] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 7),  // Загрузить значение dmem[7] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(1, 1, 8),  // Загрузить значение dmem[8] в R1 через ACC
            new Instruction(3, 0, 1),  // Сложить R0 и R1, результат в R0 через ACC

            new Instruction(2, 9, 0),  // Сохранить значение R0 (сумма) в dmem[9] через ACC
            new Instruction(99, 0, 0)  // Остановить выполнение программы
            };
            memory.LoadProgram(program);

            // Создание процессора и выполнение программы
            Processor processor = new Processor(memory);
            processor.ExecuteProgram();
            Console.ReadLine();
            // Ожидаемый результат:
            // Значение dmem[9] = 9 (сумма всех элементов массива dmem[0] - dmem[8])
        }
    }
}
