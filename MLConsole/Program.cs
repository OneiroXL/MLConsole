using Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MLConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            LinearRegression linearRegression = new LinearRegression();
            linearRegression.GenerateYData(1000,10, 3, 0, 10);
            linearRegression.PrintYData();
            Console.WriteLine($"最终预测X值为:{linearRegression.Train(10, 0.0001, 0.5, 0.5)}");
        }
    }
}
