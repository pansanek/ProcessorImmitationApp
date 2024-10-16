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
            Memory memory = new Memory(9);

            // Инициализация данных в памяти (массив может быть любого размера)
            int[] initialData = { 1, 1, 1, 1, 1, 1, 1, 1, 9 };  // Массив для суммирования
            memory.LoadData(initialData);

            // Пример 11-битных команд
            List<uint> program = new List<uint>
        {
            0b000_0000_0000u,  // LOAD R0, dmem[0]    (Загрузить первый элемент в R0)
            0b000_0001_0000u,  // LOAD R2, 0          (Инициализация счётчика R2 = 0)
            0b000_0010_1000u,  // LOAD R3, 9          (Размер массива в R3 = 9)
            // Метка начала цикла
            0b000_0001_0010u,  // LOAD R1, dmem[R2]   (Загрузить текущий элемент массива в R1)
            0b010_0000_0001u,  // ADD R0, R1          (Добавить элемент к сумме в R0)
            0b010_0001_0000u,  // ADD R2, 1           (Увеличить счётчик позиций в R2)
            0b011_0001_0010u,  // JUMP_IF R2 == R3, END (Если достигли конца массива, переход к метке END)
            0b100_0000_0011u,  // JUMP START          (Переход к началу цикла)
            // Метка конца программы
            0b001_0000_0000u,  // STORE R0, dmem[9]   (Сохранить сумму в dmem[9])
            0b10100000000u   // HALT                (Завершить выполнение программы)
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
