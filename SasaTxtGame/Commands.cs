using System;
using System.Collections.Generic;
using System.Text;


namespace SasaTxtGame
{
    public class Commands
    {
        Background bgMap;
        Persona user;
        MapItems mapItems;
        public Commands(Persona p, MapItems _mapItems) {
            user = p;
            bgMap = new Background();
            mapItems = _mapItems;
        }

        public void GoForward() {
            if (CanProceed("w"))
            {
                user.OldY = user.y;
                user.y += 1;
            }
            else {
                Console.WriteLine("You can't proceed");
            }
        }

        public void GoBackward()
        {
            if (CanProceed("s"))
            {
                user.OldY = user.y;
                user.y -= 1;
            }
            else
            {
                Console.WriteLine("You can't proceed");
            }
        }
        public void GoLeft()
        {
            if (CanProceed("a")){
                user.OldX = user.x;
                user.x -= 1;
            }
            else
            {
                Console.WriteLine("You can't proceed");
            }
        }

        public void GoRight()
        {
            if (CanProceed("d"))
            {
                user.OldX = user.x;
                user.x += 1;
            }
            else
            {
                Console.WriteLine("You can't proceed");
            }
        }
        public void TakeItem()
        {
            string kolize = bgMap.CheckPosition(user.x, user.y);
            if (kolize != string.Empty)
            {

                Item item = mapItems.GetItem(kolize);
                if (item.subitem != null)
                {
                    user.AddToRuksak(item.subitem);
                    Console.WriteLine(string.Format("{0}: has been add to your inventory", item.subitem.Name));
                    bgMap.RemoveTakenItem(kolize);
                    mapItems.RemoveItem(kolize);
                    return;
                }
                if (item != null)
                {
                    user.AddToRuksak(item);
                    Console.WriteLine(string.Format("{0}: has been added to your inventory", item.Name));
                    bgMap.RemoveTakenItem(kolize);
                    mapItems.RemoveItem(kolize);
                }
            }
        }

        public void ItemList()
        {
            Console.WriteLine("Items in inventory");
            foreach (Item item in user.Ruksak)
            {
               Console.WriteLine(item.Name);
            }  
        }

        public void ReleaseItem()
        {
            
        }

        public string Help()
        {
            string text = System.IO.File.ReadAllText("Help.txt");
            return text;
        }

        private bool CanProceed(string directionCOmmand) {

            string kolize = ControlPosition();
            if (kolize != string.Empty)
            {
                Item i = mapItems.GetItem(kolize);
                if (i!=null) { 
                if (!i.CanPass)
                {
                    if (i.Direction == "x" && directionCOmmand == "d" && user.OldX - user.x > 0) return false;
                    if (i.Direction == "x" && directionCOmmand == "a" && user.x - user.OldX > 0) return false;
                    if (i.Direction == "y" && directionCOmmand == "w" && user.y - user.OldY > 0) return false;
                    if (i.Direction == "y" && directionCOmmand == "s" && user.OldY - user.y > 0) return false;
                }
                }

                switch (kolize)
                {
                    case "|": Console.WriteLine("You hit a wall. Can't move"); return false; 
                    case "-": Console.WriteLine("You hit a wall. Can't move"); return false; 
                }

            }

           

            return true;
        }

         private string ControlPosition()
        {
            return bgMap.CheckPosition(user.x, user.y);
        }

        public void CheckCommand(string command)
        {
            string kolize = ControlPosition();
            if (kolize != string.Empty)
            {

                Item item = mapItems.GetItem(kolize);
                if (item != null)
                {
                    if (item.Command.Contains(command))
                    {
                        if (kolize == "EX")
                        {
                            bool red = false;
                            bool blue = false;
                            bool green = false;
                            int count = 0;
                            foreach (Item i in user.Ruksak) {
                                if (i.Key == "k1") { red = true; count++; }
                                if (i.Key == "k2") { blue = true; count++; }
                                if (i.Key == "k3") { green = true; count++; }
                            }

                            if (!(red && blue && green)) {
                                Console.WriteLine(string.Format("{0}, some keys are missing! Go and find them...({1}/3  keys)", user.Name,count ));
                                return;
                            }
                        }

                        if (item.Direction == "y" && user.y - user.OldY > 0)
                        {
                            user.OldY = user.y;
                            user.y++;
                            if (item.StatusParam == "deadly")
                            {
                                string[] answer = item.Status.Split('|');
                                Console.Write(answer[0]);
                            }
                            return;
                        }
                        if (item.Direction == "y" && user.OldY - user.y > 0)
                        {
                            user.OldY = user.y;
                            user.y--;
                            if (item.StatusParam == "deadly")
                            {
                                string[] answer = item.Status.Split('|');
                                Console.WriteLine(answer[0]);
                            }
                            return;
                        }

                        if (item.Direction == "x" && user.x - user.OldX > 0)
                        {
                            user.OldX = user.x;
                            user.x = user.x + 2;
                            if (item.StatusParam == "deadly")
                            {
                                string[] answer = item.Status.Split('|');
                                Console.WriteLine(answer[0]);
                            }
                            return;
                        }
                        if (item.Direction == "x" && user.OldX - user.x > 0)
                        {
                            user.OldX = user.x;
                            user.x = user.x - 2;
                            if (item.StatusParam == "deadly")
                            {
                                string[] answer = item.Status.Split('|');
                                Console.WriteLine(answer[0]);
                            }
                            return;
                        }

                        Console.WriteLine(item.Direction);

                    }
                    else if (item.StatusParam == "deadly")
                    {
                        string[] answer = item.Status.Split('|');
                        Console.WriteLine(answer[1]);
                        user.x = 3;
                        user.y = 0;
                        Console.WriteLine(string.Format("{0} you are staring from the beginning", user.Name));
                    }

                }
            }
        }

        public void GetMap()
        {

            bgMap.PrintMap(user.x,user.y);
        }
        public void Light()
        {
            foreach (Item i in user.Ruksak)
            {
                if (i.Name == "Matches")
                {
                   
                    string[] s = i.StatusParam.Split('|');
                    int count = 0;
                    int.TryParse(s[1], out count);
                    if (count > 0)
                    {
                        count--;
                        Console.WriteLine(string.Format("{0} left",count));
                        bgMap.LightMatches(user.x, user.y);
                        i.StatusParam = "int|" + count.ToString();
                    }
                    else {
                        Console.WriteLine("Sorry no match left");
                    }

                }
            }
            
        }
    }
}
