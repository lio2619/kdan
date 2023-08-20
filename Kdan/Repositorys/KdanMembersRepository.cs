using Microsoft.EntityFrameworkCore;
using Kdan.Repositorys.Interface;
using Kdan.Models;
using Kdan.Context;
using Kdan.Parameters;
using System;

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
        /// <summary>
        /// 取出指定日期的員工資訊
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public async Task<List<KdanMembers>> CheckDayEmployeeInformation(DateOnly dateOnly)
        {
            var information = await _kdanContext.KdanMembers.Where(x => DateOnly.FromDateTime((DateTime)x.ClockIn) == dateOnly ||
                                                                DateOnly.FromDateTime((DateTime)x.ClockOut) == dateOnly).AsNoTracking().ToListAsync();
            return information;
        }
        /// <summary>
        /// 取出指定日期區間內未打下班卡的員工
        /// </summary>
        /// <param name="startDay"></param>
        /// <param name="endDay"></param>
        /// <returns></returns>
        public async Task<List<int>> CheckDayRangeNotClockOutEmployees(DateOnly startDay, DateOnly endDay)
        {
            var response = await _kdanContext.KdanMembers.Where(x => DateOnly.FromDateTime((DateTime)x.ClockIn) >= startDay &&
                                                         DateOnly.FromDateTime((DateTime)x.ClockIn) <= startDay &&
                                                         x.ClockOut == null).Select(y => y.EmployeeNumber).ToListAsync();
            return response;
        }
        public async Task<List<int>> CheckDayFiveEarliestClockInEmployee(DateOnly dateOnly)
        {
            var response = await _kdanContext.KdanMembers.Where(x => DateOnly.FromDateTime((DateTime)x.ClockIn) == dateOnly)
                                                        .OrderBy(z => z.ClockIn).Select(y => y.EmployeeNumber).Take(5).ToListAsync();
            return response;
        }
    }
}
