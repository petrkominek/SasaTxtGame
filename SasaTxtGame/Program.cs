using System;
using System.Configuration;

namespace SasaTxtGame
{
    class Program
    {
        static Persona me;
        static Persona meOld;
        static Commands commands;
        static Background bgMap;
        static MapItems mapItems;
        static bool xx = true;
        static bool yy = true;
        static void Main(string[] args)
        {
            me = new Persona();
            me.x = 3;
            bgMap = new Background();
            mapItems = new MapItems(@"c:\Users\D046041\source\repos\GitProjects\SasaTxtGame\SasaTxtGame\Items.json");
            Console.WriteLine("Hello My friend what's your nick!");
            me.Name = Console.ReadLine();
            commands = new Commands(me);
            
            string command = string.Empty;
            while (command != "end")
            {
                //uloz starou pozici pro pripad problemu
                int oldX = me.x;
                int oldY = me.y;
                command = Console.ReadLine().ToLower();
                switch (command)
                {
                    case "forward": commands.GoForward(); yy = false; break;
                    case "backward": commands.GoBackward(); yy = true; break;
                    case "left": commands.GoLeft(); xx=false ; break;
                    case "right": commands.GoRight(); xx=true ; break;
                    case "w": commands.GoForward(); yy = true; break;
                    case "s": commands.GoBackward(); yy = false; break;
                    case "a": commands.GoLeft(); yy = false; break;
                    case "d": commands.GoRight(); yy = true; break;
                    case "status": GetStatus();  break;


                    case "help": PrintMessage(commands.Help()); break;
                    default: CheckCommand(command); break;
                }
                string kolize = ControlPosition();
                if (kolize != string.Empty)
                {
                    if (kolize.StartsWith("return"))
                    {
                        me.x = oldX;
                        me.y = oldY;
                    }
                    PrintMessage(kolize);
                    Item item = mapItems.GetItem(kolize);
                    if (item != null)
                    {
                        PrintMessage(mapItems.GetItem(kolize).Description);
                    }
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

        static void GetStatus() {
            string kolize = ControlPosition();
            if (kolize != string.Empty)
            {
                
                Item item = mapItems.GetItem(kolize);
                if (item != null)
                {
                    PrintMessage(string.Format("{0}: command {1}", mapItems.GetItem(kolize).Status, mapItems.GetItem(kolize).Command));
                }
            }
        }

        static void CheckCommand(string command)
        {
            string kolize = ControlPosition();
            if (kolize != string.Empty)
            {

                Item item = mapItems.GetItem(kolize);
                if (item != null)
                {
                    if (item.Command.Contains(command))
                    {
                        string[] comm = item.Command.Split('|');
                        if (comm[0] == "yy") {
                            if (yy)
                            {
                                me.y++;
                            }
                            else {
                                me.y--;
                            }
                        }
                        if (comm[0] == "xx")
                        {
                            if (xx)
                            {
                                me.x++;
                            }
                            else
                            {
                                me.x--;
                            }
                        }


                    }
                }
            }
        }
    }
}
