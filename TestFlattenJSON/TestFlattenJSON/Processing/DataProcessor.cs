using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace TestFlattenJSON.Processing
{
    public class DataProcessor
    {

        /// <summary>
        /// Recursively crawl the json token to flatten everything into dot notated kvp
        /// </summary>
        /// <param name="dict">the dictinoary to hold the flattened data</param>
        /// <param name="token">the parsed json token</param>
        /// <param name="prefix">the hierarchy prefix of the data</param>
        private void FillDictionaryFromJSON(Dictionary<string, object> dict, JToken token, string prefix)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach(var prop in token.Children<JProperty>())
                    {
                        FillDictionaryFromJSON(dict, prop.Value, Join(prefix, prop.Name));
                    }
                    break;
                case JTokenType.Array:
                    var index = 0;
                    foreach (var value in token.Children())
                    {
                        FillDictionaryFromJSON(dict, value, Join(prefix, index.ToString()));
                        index++;
                    }
                    break;
                default:
                    dict.Add(prefix, ((JValue)token).Value);
                    break;
            }


        }

        /// <summary>
        /// Join the strings with dot notation
        /// </summary>
        private string Join(string prefix, string name)
        {
            return string.IsNullOrEmpty(prefix) ? name : $"{prefix}.{name}";
        }


        /// <summary>
        /// Parse json data and convert it to a flattened dicitonary of key value pairs
        /// </summary>
        /// <param name="jsonData">the json data in a string form</param>
        /// <returns>Dictionary<string, object></string></returns>
        public Dictionary<string, object> FlattenJsonData(string jsonData)
        {
            var result = new Dictionary<string, object>();
            var token = JToken.Parse(jsonData);
            FillDictionaryFromJSON(result, token, string.Empty);
            return result;
        }
    }
}
