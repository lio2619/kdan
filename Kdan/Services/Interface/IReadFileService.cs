namespace Kdan.Services.Interface
{
    public interface IReadFileService
    {
        /// <summary>
        /// 讀取json檔到資料庫
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public IEnumerable<T> ReadJsonFile<T>(string jsonFilePath);
    }
}
