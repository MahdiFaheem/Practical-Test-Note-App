using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.JsonFileService
{
    public interface IJsonFileService
    {
        Task<T> ReadJsonFile<T>(string filePath);
        Task WriteJsonFile<T>(string filePath, T content);
    }

    class JsonFileService : IJsonFileService
    {
        private static async Task CreateFileIfNotExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                var dirPath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                using (var sw = File.CreateText(filePath))
                {
                    await sw.WriteAsync("[]");
                }
            }
        }

        public async Task<T> ReadJsonFile<T>(string filePath)
        {
            await CreateFileIfNotExists(filePath);
            return JsonConvert.DeserializeObject<T>(await File.ReadAllTextAsync(filePath));
        }

        public async Task WriteJsonFile<T>(string filePath, T content)
        {
            await File.WriteAllTextAsync(filePath, JsonConvert.SerializeObject(content));
        }
    }
}
