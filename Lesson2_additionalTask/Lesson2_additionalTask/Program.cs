using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lesson2_additionalTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string str = "   Предложение один   Теперь предложение два     Предложение три  Четыре: 444   ";
            Console.WriteLine(str);

            RegexOptions options = RegexOptions.None;
            Regex myRegex = new Regex(@"\s+\b", options);
            var splittArr = myRegex.Split(str.Trim());
            var fullRes = splittArr.Aggregate(
                (x, y) =>
                Regex.Match(y, @"[А-Я]").Success ? x + ". " + y: x + " " + y);
            Console.WriteLine(fullRes);

            Console.ReadLine();
        }
    }
}
