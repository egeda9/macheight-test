using System.IO;
using macheight.nba.model;
using Newtonsoft.Json;

namespace macheight.nba.test
{
    public class FileHelper
    {
        public static Players GetFileContent(string path, string testFile)
        {
            var resource = $"{path}/{testFile}";

            using (var r = new StreamReader(resource))
            {
                var json = r.ReadToEnd();
                return JsonConvert.DeserializeObject<Players>(json);
            }
        }
    }
}
