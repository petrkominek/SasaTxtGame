using System;
using System.Configuration;
//using Microsoft.Extensions.Configuration;

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
            me.y = 0;
            bgMap = new Background();
            mapItems = new MapItems(@"Items.json");
            Console.WriteLine("Hello My friend what's your nick!");
            me.Name = Console.ReadLine();
            commands = new Commands(me, mapItems);
            
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
                    case "s": commands.GoForward(); yy = true; break;
                    case "w": commands.GoBackward(); yy = false; break;
                    case "a": commands.GoLeft(); yy = false; break;
                    case "d": commands.GoRight(); yy = true; break;
                    case "m": commands.GetMap(); break;
                    case "map": commands.GetMap();  break;
                    case "status": GetStatus();  break;
                    case "take": commands.TakeItem(); break;
                    case "inventory": commands.ItemList(); break;
                    case "i": commands.ItemList(); break;
                    case "light": commands.Light(); break;
                    case "l": commands.Light(); break;


                    case "help": PrintMessage(commands.Help()); break;
                    default: commands.CheckCommand(command); break;
                }
                string kolize = ControlPosition();
                if (kolize != string.Empty)
                {
                    if (kolize.ToLower().StartsWith("return"))
                    {
                        me.x = oldX;
                        me.y = oldY;
                        PrintMessage(kolize.Replace("Return|",string.Empty));
                    }
                    //PrintMessage(kolize);
                    Item item = mapItems.GetItem(kolize);
                    if (item != null)
                    {
                        PrintMessage(mapItems.GetItem(kolize).Description);
                        if (item.Command == "start")
                        {
                            me.x = 3;
                            me.y = 0;
                            Console.WriteLine(string.Format("{0} you are starting from the beginning", me.Name));

                        }
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

       
    }
}
