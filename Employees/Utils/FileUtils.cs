using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Employees.Utils
{
    class FileUtils
    {
        public static async Task SerializeToFile(object data, string path)
        {
            var jsonString = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(path, jsonString);
        }

        public static async Task<T?> DeserializeFromFile<T>(string path)
        {
            var jsonString = await File.ReadAllTextAsync(path);
            var deserializedData = JsonSerializer.Deserialize<T>(jsonString);

            return deserializedData;
        }
    }
}
