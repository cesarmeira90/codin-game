using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class ConwaySequence
{
    static class ConwaySequenceModel
    {
        private static string GetConwaySequenceOf(string conwaySequence)
        {
            string[] numbers = conwaySequence.Split(' ');

            StringBuilder answer = new StringBuilder();

            int counter = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                string currentNumber = numbers[i];
                counter++;

                // last number
                if (i == numbers.Length - 1)
                {
                    answer.Append(string.Format("{0} {1} ", counter, currentNumber));
                }
                else
                {
                    string nextNumber = numbers[i + 1];
                    if (nextNumber != currentNumber)
                    {
                        answer.Append(string.Format("{0} {1} ", counter, currentNumber));
                        counter = 0;
                    }
                }
            }

            return answer.ToString().Trim();
        }

        public static string GetLine(int number, int line)
        {
            string answer = number.ToString();

            for (int i = 2; i <= line; i++)
            {
                answer = GetConwaySequenceOf(answer);
            }

            return answer;
        }
    }

    static void Main(string[] args)
    {
        int R = int.Parse(Console.ReadLine());
        int L = int.Parse(Console.ReadLine());
        
        Console.WriteLine(ConwaySequenceModel.GetLine(R, L));
    }
}