using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace SasaTxtGame
{
    public class MapItems
    {

        //private List<KeyValuePair<string, Item>> list = new List<KeyValuePair<string, Item>>();
        private Dictionary<string, Item> dict = new Dictionary<string, Item>();
       
        public MapItems(string filepath) {
            using (TextReader reader = File.OpenText(filepath))
            {

                string itemTxt = reader.ReadToEnd();
                


                JArray a = JArray.Parse(itemTxt);

                foreach (JObject o in a.Children<JObject>())
                {
                    Item item = ParseItem(o);
                    dict.Add(item.Key, item);
                }


            }
        }

        private Item ParseItem(JObject o) {

            Item item = new Item();
            
            foreach (JProperty p in o.Properties())
            {

                string key = p.Name;
                //string value = (string)p.Value;

                switch (key)
                {
                    case "key": item.Key = (string)p.Value; break;
                    case "name": item.Name = (string)p.Value; break;
                    case "description": item.Description = (string)p.Value; break;
                    case "status": item.Status = (string)p.Value; break;
                    case "status_param": item.StatusParam = (string)p.Value; break;
                    case "can_pass": item.CanPass = bool.Parse((string)p.Value); break;
                    case "pass_direction": item.Direction = (string)p.Value; break;
                    case "command": item.Command = (string)p.Value;  break;
                    case "subitem": item.subitem = ParseItem((JObject)p.Value);


                        break;

                }


            }

            return item;
        }

        public Item GetItem(string key){
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return null;
        }
        public void RemoveItem(string key)
        {
            if (dict.ContainsKey(key))
            {
                dict.Remove(key);
            }
          
        }
    }
}
