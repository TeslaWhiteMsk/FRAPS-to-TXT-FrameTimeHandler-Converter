using System;
using System.IO;
using System.Collections.Generic;

/* Fraps to TXT FrameTimeHandler Converter 
 * Version 0.0.1 
   Данная утилита конвертирует файлы фреймтайма бенчмарка Fraps формата:
      n,             Frametime(n)
    n+1,       Frametime(n+(n+1))
    ...
  n+..., Frametime(n+...+(n+...))
  
  В формат утилиты TXT FrameTimeHandler:
  "FrameTime(n)"
  "FrameTime(n+1)"
  ...
  "FrameTime(n+...)"

  Где: FrameTime(n) - Время кадра n.
     */

namespace Fraps_to_FrameHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Fraps to TXT FrameTimeHandler Converter \nVer. 0.0.1");
            string filename = @"001.csv";
            StreamReader srinput = null;
            try
            {
                srinput = File.OpenText(filename);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"File {filename} not found \nPress any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            if (srinput.ReadLine() == "Frame, Time (ms)")
            {
                Console.WriteLine($"{filename} is FRAPS frametime file");
                double[] frametimesarr = Reader(ref srinput);
                ArrOutput(frametimesarr);
            }
            else
            {
                Console.WriteLine($"{filename} is not FRAPS frametime file");

            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            srinput.Close();
        }

        public static double[] Reader(ref StreamReader stream)
        {
            LinkedList<double> temp = new LinkedList<double>();
            string tempstr = null;
            Console.WriteLine("Reading...");
            while (stream.EndOfStream != true)
            {
                //Запись в связный список значений прочитанных из файла
                tempstr = stream.ReadLine();
                tempstr = tempstr.Remove(0, 6);
                temp.AddLast(Convert.ToDouble(tempstr));
            }
            double[] result = new double[temp.Count - 1];
            LinkedListNode<double> prev = temp.First;
            LinkedListNode<double> current = temp.First.Next;
            Console.WriteLine("Converting...");
            for (int i = 0; i < temp.Count - 2; i++)
            {
                //Конвертируем прочитанный связный список в массив, округляя значения
                result[i] = Math.Round(current.Value - prev.Value, 3, MidpointRounding.AwayFromZero);
                if (current.Next != null)
                {
                    prev = prev.Next;
                    current = current.Next;
                }
            }
            return result;
        }

        public static void ArrOutput(double[] arr)
        {
            string filename = @"001.txt";
            StreamWriter swoutput = File.CreateText(filename);
            for (int i = 0; i < arr.Length - 1; i++)
            {
                //Вывод массива в файл
                swoutput.WriteLine(arr[i].ToString());
            }
            Console.WriteLine($"{arr.Length - 1} frametime values write in {filename}");
            swoutput.Close();
        }


    }
}
