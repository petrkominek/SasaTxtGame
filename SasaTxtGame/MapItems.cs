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
                    Item item = new Item();
                    string kkey = "";
                    foreach (JProperty p in o.Properties())
                    {

                        string key = p.Name;
                        string value = (string)p.Value;
                        
                        switch (key) {
                            case "key": kkey = value; break;
                            case "name": item.Name = value; break;
                            case "description": item.Description = value; break;
                            case "status": item.Status = value; break;
                            case "status_param": item.StatusParam = value; break;

                        }

                        
                    }

                    dict.Add(kkey, item);
                }


            }
        } 

        public Item GetItem(string key){
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            return null;
        }
    }
}
