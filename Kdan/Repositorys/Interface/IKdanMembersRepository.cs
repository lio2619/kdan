using Kdan.Models;
using Kdan.Parameters;

namespace Kdan.Repositorys.Interface
{
    public interface IKdanMembersRepository : IBaseRepository<KdanMembers>
    {
        /// <summary>
        /// 透過員工編號與打卡時間取得主鍵
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public Task<KdanMembers> CheckPrimaryKeyByNumberAndClock(KdanClockPara kdanClockPara);
        /// <summary>
        /// 確定是否當天有打過上班卡
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Task<KdanMembers> CheckTheCardIsClockIn(KdanClockPara kdanClockPara);
        /// <summary>
        /// 確定是否當天有打過下班卡
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public Task<KdanMembers> CheckTheCardIsClockOut(KdanClockPara kdanClockPara);
        /// <summary>
        /// 取出指定日期的員工資訊
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public Task<List<KdanMembers>> CheckDayEmployeeInformation(DateOnly dateOnly);
        /// <summary>
        /// 取出指定日期區間內未打下班卡的員工
        /// </summary>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public Task<List<int>> CheckDayRangeNotClockOutEmployees(DateOnly startDay, DateOnly endDay);
        /// <summary>
        /// 列出指定日期內最早打卡的五位員工編號
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public Task<List<int>> CheckDayFiveEarliestClockInEmployee(DateOnly dateOnly);
    }
}
