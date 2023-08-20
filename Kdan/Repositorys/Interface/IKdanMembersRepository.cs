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
        public Task<List<KdanMembers>> CheckDayEmployeeInformation(DateOnly dateOnly);
    }
}
