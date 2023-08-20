using Kdan.Services.Interface;
using Newtonsoft.Json;

namespace Kdan.Services
{
    public class ReadFileService : IReadFileService
    {
        public ReadFileService() { }
        /// <summary>
        /// 讀取json檔到資料庫
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public IEnumerable<T> ReadJsonFile<T>(string jsonFilePath)
        {
            if (File.Exists(jsonFilePath))
            {
                string outputStr = File.ReadAllText(jsonFilePath);
                IEnumerable<T> response = JsonConvert.DeserializeObject<IEnumerable<T>>(outputStr);
                return response;
            }
            throw new FileNotFoundException($"can not find this json file in this path {jsonFilePath}");
        }
    }
}
