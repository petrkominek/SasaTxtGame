using System;
using System.Collections.Generic;
using System.Text;

namespace SasaTxtGame
{
    public class Background
    {

        char[,] arr = new char[100, 100];

        public Background() {
            string[] lines = System.IO.File.ReadAllLines(@"C:\Users\D046041\source\repos\SasaGame\SasaGame\GameMap.txt");
            int x = 0;
            int y = 0;
            foreach (string line in lines)
            {
                foreach(char s in line)
                {
                    arr[x, y] = s;
                    x += 1;
                }
                y += 1;
                x = 0;
            }
        }

        public string CheckPosition(int x, int y) {
            string answer = string.Empty;
            char valueFromMap = arr[x, y];

            if (valueFromMap.ToString() == string.Empty)
                return string.Empty;

            switch (valueFromMap)
              {
                 case '|': answer = "you hit the wall. Can't move"; break;
                 case '-': answer = "you hit the wall. Can't move"; break;
              }

            if (valueFromMap.ToString() == "|" | valueFromMap.ToString() == "-") {
                return answer;
            }
            else if (valueFromMap.ToString() != " ") { 
                if (x > 0)
                {
                    answer += arr[x - 1, y];
                }
                answer += valueFromMap;
                if (x < 99)
                    answer += arr[x + 1, y];

                answer = answer.Replace("|", "").Replace("-", "").Replace(" ","");
            }
         

            return answer;

        }
    }
}
