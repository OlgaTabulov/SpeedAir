using Newtonsoft.Json;
using System.IO;


namespace SpeedAir
{
    public static class JsonFileReader
    {
        public static T Read<T>(string filePath)
        {
            string text = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}
