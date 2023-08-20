using AutoMapper;
using Kdan.Repositorys.Interface;
using Kdan.Services.Interface;
using Kdan.Parameters;
using Kdan.Models;
using Kdan.Dtos;
using System.Globalization;
using System;

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
        /// <exception cref="BadHttpRequestException"></exception>
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
        /// <exception cref="BadHttpRequestException"></exception>
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
        /// <summary>
        /// 取得今天所有員工的資料
        /// </summary>
        /// <returns></returns>
        public async Task<List<KdanMembersInformationDto>> ListAllEmployeeTodayInformationFunction()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var response = await _kdanMembersRepository.CheckDayEmployeeInformation(today);
            var kdanInformation = _mapper.Map<List<KdanMembersInformationDto>>(response);
            kdanInformation.ForEach(x =>
            {
                x.BreakTime = TimeOnly.Parse("12:00");
                if (x.ClockIn == TimeOnly.Parse("00:00") || x.ClockOut == TimeOnly.Parse("00:00"))
                {
                    x.TotalWorkingTime = "No Working Time";
                }
                else
                {
                    x.TotalWorkingTime = (x.ClockOut - x.ClockIn).TotalHours.ToString();
                }
            });
            return kdanInformation;
        }
        /// <summary>
        /// 取得指定日期所有員工的資料
        /// </summary>
        /// <param name="dateOnly"></param>
        /// <returns></returns>
        public async Task<List<KdanMembersInformationDto>> ListAllEmployeeDayInformationFunction(KdanDesignatedDayPara kdanDesignatedDayPara)
        {
            DateOnly firstDate = DateOnly.ParseExact(kdanDesignatedDayPara.Day, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var response = await _kdanMembersRepository.CheckDayEmployeeInformation(firstDate);
            var kdanInformation = _mapper.Map<List<KdanMembersInformationDto>>(response);
            kdanInformation.ForEach(x =>
            {
                x.BreakTime = TimeOnly.Parse("12:00");
                if (x.ClockIn == TimeOnly.Parse("00:00") || x.ClockOut == TimeOnly.Parse("00:00"))
                {
                    x.TotalWorkingTime = "No Working Time";
                }
                else
                {
                    x.TotalWorkingTime = (x.ClockOut - x.ClockIn).TotalHours.ToString();
                }
            });
            return kdanInformation;
        }
        /// <summary>
        /// 列出在這個區間內沒有打下班卡的清單
        /// </summary>
        /// <param name="kdanDatePara"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<KdanEmployeeNumbersDto> ListEmployeesNotClockedOutBetweenDatesFunction(KdanDatePara kdanDatePara)
        {
            KdanEmployeeNumbersDto kdanEmployeeNumbersDto = new();
            DateOnly startDay = DateOnly.ParseExact(kdanDatePara.StartDay, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            DateOnly endDay = DateOnly.ParseExact(kdanDatePara.EndDay, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            if (startDay >  endDay)
            {
                throw new BadHttpRequestException("開始日期大於結束日期");
            }
            var response = await _kdanMembersRepository.CheckDayRangeNotClockOutEmployees(startDay, endDay);
            kdanEmployeeNumbersDto.EmployeeNumber = response;
            return kdanEmployeeNumbersDto;
        }
        /// <summary>
        /// 取得今天最早打卡的5位員工的資料
        /// </summary>
        /// <param name="kdanDesignatedDayPara"></param>
        /// <returns></returns>
        public async Task<KdanEmployeeNumbersDto> ListFiveEmployeesTodayClockInEarliestFunction(KdanDesignatedDayPara kdanDesignatedDayPara)
        {
            KdanEmployeeNumbersDto kdanEmployeeNumbersDto = new();
            DateOnly firstDate = DateOnly.ParseExact(kdanDesignatedDayPara.Day, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            var response = await _kdanMembersRepository.CheckDayFiveEarliestClockInEmployee(firstDate);
            kdanEmployeeNumbersDto.EmployeeNumber = response;
            return kdanEmployeeNumbersDto;
        }
    }
}
