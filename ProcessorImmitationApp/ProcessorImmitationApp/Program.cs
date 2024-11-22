using System;
using System.Collections.Generic;
using System.IO;
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
            string[] programText = File.ReadAllLines("C:\\Users\\alexp\\Source\\Repos\\pansanek\\ProcessorImmitationApp\\ProcessorImmitationApp\\ProcessorImmitationApp\\program.txt");

            // Создание ассемблера
            Assembler assembler = new Assembler();

            // Преобразование текста в машинный код
            List<uint> machineCode = assembler.Assemble(programText);

            Memory memory = new Memory(11); 

            // Инициализация данных в памяти 
            int[] initialData = { 9, 1, 2, 3, 4, 5, 6, 7, 8, 9};  
            memory.LoadData(initialData);

            // Загрузка программы
            memory.LoadProgram(machineCode);

            // Создание процессора и выполнение программы
            Processor processor = new Processor(memory);
            processor.ExecuteProgram();
            Console.ReadLine();
        }
    }
}
