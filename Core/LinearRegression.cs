using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core
{
    public class LinearRegression
    {
        /*
         * 假设方程:Y = X^2
         */

        /// <summary>
        /// 数据
        /// </summary>
        private List<double> YDataList = new List<double>();
        private Dictionary<double, int> YDataCountInfoDic = new Dictionary<double, int>();
        private Random random = new Random();

        private const int ZERO = 0;
        private const int ONE = 1;
        private const int TWO = 2;
        private const int THREE = 3;
        private const int FOUR = 4;
        private const int FIVE = 5;
        private const int TEN = 10;


        /// <summary>
        /// 生成YData
        /// </summary>
        /// <param name="num"></param>
        /// <param name="probability">区间(1-10)</param>
        public void GenerateYData(int num,int x ,int yDataAppearProbability, int mixMistake, int maxMistake)
        {
            for (int i = ONE; i <= num; i++)
            {
                double elf = ZERO;
                if (yDataAppearProbability >= random.Next(ONE, TEN + ONE))
                {
                    int variableParameter = ONE;
                    variableParameter = random.Next(ZERO, TEN) >= FIVE ? variableParameter : -variableParameter;

                    elf = Math.Pow(x, TWO) + random.Next(mixMistake, maxMistake) * variableParameter;
                }
                else
                {
                    elf = random.Next(ONE, num + ONE);
                }

                YDataList.Add(elf);

                if (YDataCountInfoDic.TryGetValue(elf, out int count))
                {
                    YDataCountInfoDic[elf] = count + ONE;
                }
                else
                {
                    YDataCountInfoDic[elf] = ONE;
                }
            }
        }


        /// <summary>
        /// 训练
        /// </summary>
        /// <param name="num">训练次数</param>
        /// <param name="accuracy">精度</param>
        public double Train(int num, double accuracy, double mixMistake, double maxMistake)
        {
            List<double> xList = new List<double>();
            List<double> duplicantList = new List<double>();

            for (int i = 0; i < num; i++)
            {
                foreach (var yData in YDataList) 
                {
                    double duplicant = ZERO;
                    double mistake = TEN * TEN * TEN;
                    while (true) 
                    {
                        duplicant += accuracy;

                        var tempMistake = Math.Abs(Math.Pow(duplicant, TWO) - yData);

                        if (tempMistake > mistake) 
                        {
                            duplicantList.Add(duplicant);
                            break;
                        }

                        mistake = tempMistake;
                    }
                }

                //探索X
                ValueTuple<int, double> duplicantCountTuple = new ValueTuple<int, double>(0, 0);

                foreach (var duplicant in duplicantList)
                {
                    int appearCount = 0;
                    var duplicantFloor = Math.Floor(duplicant);
                    foreach (var duplicantT in duplicantList)
                    {
                        if (duplicantFloor == Math.Floor(duplicantT))
                        {
                            appearCount++;
                        }
                    }

                    if (appearCount > duplicantCountTuple.Item1)
                    {
                        duplicantCountTuple.Item1 = appearCount;
                        duplicantCountTuple.Item2 = duplicantFloor;
                    }
                }

                ValueTuple<int, double> appearDuplicantCountTuple = new ValueTuple<int, double>(0, 0);
                foreach (var duplicant in duplicantList)
                {
                    if (duplicant >= duplicantCountTuple.Item2 - mixMistake && duplicant < duplicantCountTuple.Item2 + maxMistake)
                    {
                        appearDuplicantCountTuple.Item2 += duplicant;
                        appearDuplicantCountTuple.Item1++;
                    }
                }

                var x = appearDuplicantCountTuple.Item2 / appearDuplicantCountTuple.Item1;
                xList.Add(x);

                Console.WriteLine($"当前预测第{i}次 预测的X:{x}");
            }

            return xList.Sum() / num;
        }

        /// <summary>
        /// 打印YData
        /// </summary>
        public void PrintYData() 
        {
            int i = ZERO;
            foreach (var yData in YDataList) 
            {
                i++;

                Console.Write($"{yData} ");

                if (i % TEN == ZERO) 
                {
                    Console.WriteLine();
                }
            }
        }

    }
}
