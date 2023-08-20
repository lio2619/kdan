using AutoMapper;
using Kdan.Repositorys.Interface;
using Kdan.Services.Interface;
using Kdan.Parameters;
using Kdan.Models;

namespace Kdan.Services
{
    public class KdanMembersService : IKdanMembersService
    {
        private readonly IKdanMembersRepository _kdanMembersRepository;
        private readonly IReadFileService _readFileService;
        private readonly IMapper _mapper;
        public KdanMembersService(IKdanMembersRepository kdanMembersRepository, IReadFileService readFileService, IMapper mapper)
        {
            _kdanMembersRepository = kdanMembersRepository;
            _readFileService = readFileService;
            _mapper = mapper;
        }
        /// <summary>
        /// 將json檔裡面的資料移到DB裡面
        /// </summary>
        /// <param name="jsonFilePath"></param>
        /// <returns></returns>
        public async Task JsonFileToDBFunction(string jsonFilePath)
        {
            var response = _readFileService.ReadJsonFile<KdanMemberPara>(jsonFilePath);
            var kdanMembers = _mapper.Map<IEnumerable<KdanMembers>>(response);
            await _kdanMembersRepository.CreateRange(kdanMembers);
        }
        /// <summary>
        /// 打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public async Task ClockInOrOutFunction(KdanClockPara kdanClockPara)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            if (DateOnly.FromDateTime(kdanClockPara.Clock) != today)
            {
                throw new BadHttpRequestException("已經超出打卡時間");
            }
            var goToWork = new TimeOnly(8, 00);
            var getOffWork = new TimeOnly(17, 30);
            if (goToWork > TimeOnly.FromDateTime(kdanClockPara.Clock))      //上班時間
            {
                await _kdanMembersRepository.CheckTheCardIsClockIn(kdanClockPara);
                var kdanClock = _mapper.Map<KdanMembers>(kdanClockPara);
                await _kdanMembersRepository.Create(kdanClock);
            }
            else if(getOffWork < TimeOnly.FromDateTime(kdanClockPara.Clock))    //下班時間
            {
                await _kdanMembersRepository.CheckTheCardIsClockOut(kdanClockPara);
                var response = await _kdanMembersRepository.CheckPrimaryKeyByNumberAndClock(kdanClockPara);
                response.ClockOut = kdanClockPara.Clock;
                await _kdanMembersRepository.Update(response);
            }
            else
            {
                throw new BadHttpRequestException("已經超出打卡時間");
            }
        }
        /// <summary>
        /// 補打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        public async Task SupplementaryClockFunction(KdanClockPara kdanClockPara)
        {
            var goToWork = new TimeOnly(8, 00);
            var getOffWork = new TimeOnly(17, 30);
            if (goToWork > TimeOnly.FromDateTime(kdanClockPara.Clock))      //上班時間
            {
                await _kdanMembersRepository.CheckTheCardIsClockIn(kdanClockPara);
                var kdanClock = _mapper.Map<KdanMembers>(kdanClockPara);
                await _kdanMembersRepository.Create(kdanClock);
            }
            else if (getOffWork < TimeOnly.FromDateTime(kdanClockPara.Clock))    //下班時間
            {
                await _kdanMembersRepository.CheckTheCardIsClockOut(kdanClockPara);
                var response = await _kdanMembersRepository.CheckPrimaryKeyByNumberAndClock(kdanClockPara);
                response.ClockOut = kdanClockPara.Clock;
                await _kdanMembersRepository.Update(response);
            }
            else
            {
                throw new BadHttpRequestException("已經超出打卡時間");
            }
        }
        //public async Task ListAllEmployeeTodayInformationFunction()
        //public async Task ListAllEmployeeDayInformationFunction(DateOnly dateOnly)
        //public async Task ListEmployeesNotClockedOutBetweenDatesFunction(DateOnly start, DateOnly end)
        //public async Task ListFiveEmployeesTodayClockInEarliestFunction(DateOnly dateOnly)
    }
}
