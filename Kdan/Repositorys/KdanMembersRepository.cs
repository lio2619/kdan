using Microsoft.EntityFrameworkCore;
using Kdan.Repositorys.Interface;
using Kdan.Models;
using Kdan.Context;
using Kdan.Parameters;

namespace Kdan.Repositorys
{
    public class KdanMembersRepository : BaseRepository<KdanMembers>, IKdanMembersRepository
    {
        private readonly KdanContext _kdanContext;
        public KdanMembersRepository(KdanContext kdanContext) : base(kdanContext)
        {
            _kdanContext = kdanContext;
        }
        public async Task<KdanMembers> CheckPrimaryKeyByNumberAndClock(KdanClockPara kdanClockPara)
        {
            var check = await _kdanContext.KdanMembers.Where(x => x.EmployeeNumber == kdanClockPara.EmployeeNumber &&
                                                            DateOnly.FromDateTime((DateTime)x.ClockIn) == DateOnly.FromDateTime(kdanClockPara.Clock)).FirstOrDefaultAsync();
            if (check == null)
            {
                throw new BadHttpRequestException($"{kdanClockPara.EmployeeNumber} 在 {DateOnly.FromDateTime(kdanClockPara.Clock)} 沒有打過卡");
            }
            return check;
        }
        /// <summary>
        /// 確定是否當天有打過上班卡
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<KdanMembers> CheckTheCardIsClockIn(KdanClockPara kdanClockPara)
        {
            var check = await _kdanContext.KdanMembers.Where(x => x.EmployeeNumber == kdanClockPara.EmployeeNumber && 
                                                            DateOnly.FromDateTime((DateTime)x.ClockIn) == DateOnly.FromDateTime(kdanClockPara.Clock)).FirstOrDefaultAsync();
            if (check != null)
            {
                throw new BadHttpRequestException($"{kdanClockPara.EmployeeNumber} 在 {DateOnly.FromDateTime(kdanClockPara.Clock)} 已經打過卡了");
            }
            return check;
        }
        /// <summary>
        /// 確定是否當天有打過下班卡
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<KdanMembers> CheckTheCardIsClockOut(KdanClockPara kdanClockPara)
        {
            var check = await _kdanContext.KdanMembers.Where(x => x.EmployeeNumber == kdanClockPara.EmployeeNumber && x.ClockIn != null &&
                                                            DateOnly.FromDateTime((DateTime)x.ClockOut) == DateOnly.FromDateTime(kdanClockPara.Clock)).FirstOrDefaultAsync();
            if (check != null)
            {
                throw new BadHttpRequestException($"{kdanClockPara.EmployeeNumber} 在 {DateOnly.FromDateTime(kdanClockPara.Clock)} 已經打過上班卡");
            }
            return check;
        }
    }
}
