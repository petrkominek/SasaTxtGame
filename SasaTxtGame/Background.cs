using System;
using System.Collections.Generic;
using System.Text;

namespace SasaTxtGame
{
    public class Background
    {

        char[,] arr = new char[100, 100];

        public Background()
        {
            string[] lines = System.IO.File.ReadAllLines(@"GameMap.txt");
            int x = 0;
            int y = 0;
            foreach (string line in lines)
            {
                foreach (char s in line)
                {
                    arr[x, y] = s;
                    x += 1;
                }
                y += 1;
                x = 0;
            }
        }

        public string CheckPosition(int x, int y)
        {
            if(x<0 || y<0) return "Return|you hit a wall. Can't move";

            string answer = string.Empty;
            char valueFromMap = arr[x, y];

            if (valueFromMap.ToString() == string.Empty || valueFromMap == '\0')
                return string.Empty;

            switch (valueFromMap)
            {
                case '|': answer = "Return|you hit a wall. Can't move"; break;
                case '-': answer = "Return|you hit a wall. Can't move"; break;
            }

            if (valueFromMap.ToString() == "|" | valueFromMap.ToString() == "-")
            {
                return answer;
            }
            else if (valueFromMap.ToString() != " ")
            {
                if (x > 0)
                {
                    answer += arr[x - 1, y];
                }
                answer += valueFromMap;
                if (x < 99)
                    answer += arr[x + 1, y];

                answer = answer.Replace("|", "").Replace("-", "").Replace(" ", "");
            }


            return answer;

        }

        public void PrintMap(int x, int y)
        {
            Console.WriteLine("X is your position");
            char[,] arrMap = (char[,])arr.Clone();
            arrMap[x, y] = 'X';

            for (int y1 = 0; y1 < 100; y1++) {
                string line = string.Empty;
                for (int x1=0;x1<100;x1++)
                {
                    if (!arrMap[x1, y1].Equals('\0'))
                    {
                        line = line + arrMap[x1, y1].ToString();
                    }
                }
                if (line.Length > 0)
                {
                    Console.WriteLine(line);
                }
            }

        }

        public void LightMatches(int x, int y)
        {
            Console.WriteLine("X is your position");
            char[,] arrMap = arr;
            arrMap[x, y] = 'X';

            int xbegin = x < 3 ? 0 : x - 3;
            int ybegin = y < 3 ? 0 : x - y;

            for (int y1 = ybegin; y1 < ybegin+6; y1++)
            {
                string line = string.Empty;
                for (int x1 = xbegin; x1 < xbegin+6; x1++)
                {
                    if (!arrMap[x1, y1].Equals('\0'))
                    {
                        line = line + arrMap[x1, y1].ToString();
                    }
                }
                if (line.Length > 0)
                {
                    Console.WriteLine(line);
                }
            }

        }

        public void RemoveTakenItem(string kolize)
        {
            for (int y1 = 0; y1 < 99; y1++)
            {
                
                for (int x1 = 0; x1 < 99; x1++)
                {
                    string line = arr[x1, y1].ToString() + arr[x1 + 1, y1].ToString();
                    if(line.ToLower()==kolize.ToLower())
                    {
                        arr[x1, y1] = (char)'\0';
                        arr[x1+1, y1] = (char)'\0';
                    }
                }
                
            }
        }

    }
}
