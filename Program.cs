using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Методы_трансляций_2_лабораторная
{
    internal class Program
    {
        static void Main(string[] args)
        {
  
            string code =
                "программа Рез\n" +
                "функц Сложение(вещ x, вещ y)\n" +
                "нач\n" +
                "   возврат модуль(x - 5 * син(y);\n" +
                "кон\n" +
                "\n" +
                "нач вещ a, вещ b;\n" +
                "метка 1;\n" +
                "   a = 0.375;\n" +
                "   ввод b;\n" +
                "   c = модуль(факт(5) * син(0.5) * 1.2);\n" +
                "переход 1;\n" +
                "   вывод c;\n" +
                "кон\n";
            Console.WriteLine(code);
            Regex regex = new Regex(@"\([k,s,i,c],\d*\)");
            Regex[] Regex_k = new Regex[] {
                new Regex("программа"), new Regex("функц"), new Regex("вещ"),                         
                new Regex("нач"), new Regex("кон"), new Regex("возврат"),                          
                new Regex("ввод"), new Regex("вывод"), new Regex("переход"),                       
                new Regex("метка"),new Regex("модуль"),new Regex("программа"),new Regex("факт"),new Regex("син") };//ключевые слова
            char[] s = new char[] { '+', '-', '*', '_', '/', '(', ')','{', '}', '=', '<', '>', '[', ']',';',':',','};//резделители
            List<string> i = new List<string>(); //идентификаторы

            List<string> c = new List<string>(); //константы


            string newcode = "";
            foreach (var j in code)
            {
                if (s.Contains(j)) { newcode += $" (s,{Array.IndexOf(s, j)}) "; }
                else { newcode += j; }
            }
            for (int j = 0; j < Regex_k.Count(); j++)
            {
                if (Regex_k[j].Matches(newcode).Count != 0) { newcode = Regex_k[j].Replace(newcode, $" (k,{j}) "); }
            }

            List<string> sd = newcode.Split(' ').ToList();
            newcode = string.Join(" ", sd);

            for (var r = 0; r < sd.Count; r++)
            {
                if (sd[r].Trim(' ', '\n') != "")
                {
                    if (sd[r].Trim(' ', '\n')[0] != '(' && sd[r].Trim(' ', '\n')[0] != ')')
                    {
                        if (Regex.IsMatch(sd[r], @"('[^']*'|\d+)"))
                        {
                            c.Add(sd[r].Trim(' ', '\n'));
                            sd[r] = $"(c,{c.Count - 1})";
                        }
                        else if (Regex.IsMatch(sd[r], @"\w+"))
                        {
                            i.Add(sd[r].Trim(' ', '\n'));
                            for (int l = r; l < sd.Count; l++)
                            {
                                if (sd[l] == i.Last())
                                { sd[l] = Regex.Replace(sd[l], @"\b\w+\b", $"(i,{i.Count - 1})"); }
                            }
                        }
                    }
                }
            }
            newcode = string.Join(" ", sd);
            Console.WriteLine(newcode);
            void WriteElem(string name, List<string> Elem)
            {
                Console.WriteLine($"{name}: ");
                foreach (var j in Elem)
                {
                    Console.Write($"{j} ");
                }
                Console.WriteLine("\n");
            }
            WriteElem("Идентификаторы", i);
            WriteElem("Константы", c);

            Console.WriteLine($"Ключевые слова: ");
            foreach (var j in Regex_k)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine("\n");
            Console.WriteLine($"Разделители: ");
            foreach (var j in s)
            {
                Console.Write($"{j} ");
            }
            Console.WriteLine("\n");
        }
    }
}
