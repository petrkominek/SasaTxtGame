using System;

namespace SasaTxtGame
{
    class Program
    {
        static Persona me;
        static Persona meOld;
        static Commands commands;
        static Background bgMap;
        static void Main(string[] args)
        {
            me = new Persona();
            me.x = 3;
            bgMap = new Background();
            Console.WriteLine("Hello My friend what's your nick!");
            me.Name = Console.ReadLine();
            commands = new Commands(me);

            string command = string.Empty;
            while (command != "end")
            {
                //uloz starou pozici pro pripad problemu
                meOld = me;
                command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "forward": commands.GoForward(); break;
                    case "backward": commands.GoBackward(); break;
                    case "left": commands.GoLeft(); break;
                    case "right": commands.GoRight(); break;
                    case "w": commands.GoForward(); break;
                    case "s": commands.GoBackward(); break;
                    case "a": commands.GoLeft(); break;
                    case "d": commands.GoRight(); break;

                    case "help": PrintMessage(commands.Help()); break;
                    default: break;
                }
                string kolize = ControlPosition();
                if (kolize != string.Empty)
                {
                    PrintMessage(kolize);
                }
                PrintMessage(string.Format("You are at {0}:{1}", me.x, me.y));
            }
        }

        static void PrintMessage(string message)
        {

            Console.WriteLine(message);
        }

        static string ControlPosition()
        {
            return bgMap.CheckPosition(me.x, me.y);
        }

    }
}
