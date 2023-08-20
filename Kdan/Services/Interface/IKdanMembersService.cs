using Kdan.Parameters;
using Kdan.Dtos;

namespace Kdan.Services.Interface
{
    public interface IKdanMembersService
    {
        /// <summary>
        /// 將json檔裡面的資料移到DB裡面
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public Task JsonFileToDBFunction(string jsonFilePath);
        /// <summary>
        /// 打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public Task ClockInOrOutFunction(KdanClockPara kdanClockPara);
        /// <summary>
        /// 補打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public Task SupplementaryClockFunction(KdanClockPara kdanClockPara);
        /// <summary>
        /// 取得今天所有員工的資料
        /// </summary>
        /// <returns></returns>
        public Task<List<KdanMembersInformationDto>> ListAllEmployeeTodayInformationFunction();
        /// <summary>
        /// 取得特定日期所有員工的資料
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public Task<List<KdanMembersInformationDto>> ListAllEmployeeDayInformationFunction(KdanDesignatedDayPara kdanDesignatedDayPara);
        /// <summary>
        /// 列出在這個區間內沒有打下班卡的清單
        /// </summary>
        /// <param name="kdanDatePara"></param>
        /// <returns></returns>
        public Task<List<int>> ListEmployeesNotClockedOutBetweenDatesFunction(KdanDatePara kdanDatePara);
        /// <summary>
        /// 取得今天最早打卡的5位員工的資料
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public Task<List<int>> ListFiveEmployeesTodayClockInEarliestFunction(KdanDesignatedDayPara kdanDesignatedDayPara);
    }
}
