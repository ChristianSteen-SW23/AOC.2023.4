using System;

namespace AOC._2023._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "DataBig.txt";

            string[] lines = ReadFile(filePath);

            int firstWonNum = lines[0].IndexOf(":") + 3;
            int lastWonNum = lines[0].IndexOf("|") - 2;

            int firstGueNum = lines[0].IndexOf("|") + 3;
            int lastGueNum = lines[0].Length;

            //Console.WriteLine(firstWonNum+" "+ lastWonNum);

            int[] wonNums = new int[(lastWonNum-firstWonNum) / 3 + 1];
            int[] gueNums = new int[(lastGueNum - firstGueNum) / 3 + 1];

            int[] cardAmount = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                cardAmount[i] = 1;
            }

            //Console.WriteLine(wonNums.Length + " " + gueNums.Length);
            int count = 0;
            for (int h = 0; h < lines.Length; h++)
            {
                int index = 0;
                for (int i = firstWonNum; i <= lastWonNum; i += 3)
                {
                    wonNums[index] = Convert.ToInt32(FindNumberFromChar(lines[h], i));
                    //Console.WriteLine(wonNums[index]);
                    index++;
                }

                index = 0;
                for (int i = firstGueNum; i < lastGueNum; i += 3)
                {
                    gueNums[index] = Convert.ToInt32(FindNumberFromChar(lines[h], i));
                    Console.Write(gueNums[index]+" ");
                    index++;
                }

                int smallCount = 0;
                for (int gue = 0; gue < gueNums.Length; gue++)
                {
                    for (int won = 0; won < wonNums.Length; won++)
                    {
                        if (wonNums[won] == gueNums[gue])
                        {
                            smallCount++;
                        }
                    }
                }

                for (int i = h+1; i <= smallCount+h; i++)
                {
                    cardAmount[i] += cardAmount[h];
                }
                Console.WriteLine("Smallcount: "+smallCount);
                count += Convert.ToInt32(Math.Pow(2, smallCount))/2;
            }

            int countpart2 = 0;
            for(int i = 0; i < cardAmount.Length; i++)
            {
                countpart2 += cardAmount[i];
            }

            Console.WriteLine("\nPart 1: " + count);
            Console.WriteLine("\nPart 2: " + countpart2);
        }

        static string[] ReadFile(string filePath)
        {
            try
            {
                int lineCount = 0;
                // Finds the amount of lines in a the data file
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (sr.ReadLine() != null)
                    {
                        lineCount++;
                    }
                }

                Console.WriteLine("Line count is: " + lineCount);

                using (StreamReader sr = new StreamReader(filePath))
                {
                    // Sets up the array to storge the data
                    string[] lines = new string[lineCount];

                    // Makes a forloop to scan in all the data
                    for (int i = 0; i < lineCount; i++)
                    {
                        lines[i] = sr.ReadLine();
                    }
                    /*foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }*/
                    return lines;
                }

            }
            catch
            {
                Console.WriteLine("Did not find the file");
                Environment.Exit(-1);
                return null;
            }
        }

        static string FindNumberFromChar(string line, int ch)
        {
            if (!CheckForNumberChar(line[ch])) return "0";

            int leftCount = 0;
            for (int h = 0; ch - h >= 0 && ch + h <= 139 && CheckForNumberChar(line[ch - h]); h++)
            {
                leftCount = h;
            }

            int rightCount = 0;
            for (int h = 0; ch - h >= 0 && ch + h <= line.Length-1 && CheckForNumberChar(line[ch + h]); h++)
            {
                rightCount = h;
            }

            string number = line[ch].ToString();
            for (int h = 1; h < leftCount + 1; h++)
            {
                number = string.Concat(line[ch - h].ToString(), number);
            }
            for (int h = 1; h < rightCount + 1; h++)
            {
                number = string.Concat(number, line[ch + h].ToString());
            }

            return number;
        }
        static bool CheckForNumberChar(char ch)
        {
            switch (ch)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
                    break;
                default:
                    return false;
                    break;
            }
        }
    }
}