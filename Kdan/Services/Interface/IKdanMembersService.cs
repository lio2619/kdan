using Kdan.Parameters;

namespace Kdan.Services.Interface
{
    public interface IKdanMembersService
    {
        /// <summary>
        /// 將json檔裡面的資料移到DB裡面
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public Task JsonFileToDB(string jsonFilePath);
    }
}
