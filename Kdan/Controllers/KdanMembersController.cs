using Microsoft.AspNetCore.Mvc;
using Kdan.Services.Interface;
using Kdan.Parameters;
using Kdan.Dtos;

namespace Kdan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KdanMembersController : ControllerBase
    {
        private readonly IKdanMembersService _kdanMembersService;
        public KdanMembersController(IKdanMembersService kdanMembersService)
        {
            _kdanMembersService = kdanMembersService;
        }
        /// <summary>
        /// 將json檔裡面的資料移到DB裡面
        /// </summary>
        /// <param name="formFiles"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> JsonFileToDB(List<IFormFile> formFiles)
        {
            try
            {
                if (formFiles.Count < 1)
                {
                    return BadRequest("沒有上傳檔案");
                }
                foreach (var formFile in formFiles)
                {
                    if (Path.GetExtension(formFile.FileName) != ".json")
                    {
                        throw new BadHttpRequestException("副檔名不符");
                    }
                    var filePath = Path.GetFullPath(formFile.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        formFile.CopyTo(stream);
                    }
                    await _kdanMembersService.JsonFileToDBFunction(filePath);
                }
                return Ok();
            }
            catch (FileNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Clock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ClockInOrOut(KdanClockPara kdanClockPara)
        {
            try
            {
                await _kdanMembersService.ClockInOrOutFunction(kdanClockPara);
                return Ok();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 補打卡功能
        /// </summary>
        /// <param name="kdanClockPara"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("Clock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SupplementaryClock(KdanClockPara kdanClockPara)
        {
            try
            {
                await _kdanMembersService.SupplementaryClockFunction(kdanClockPara);
                return NoContent();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 列出今天員工資訊清單
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("TodayEmployeeInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<KdanMembersInformationDto>>> ListAllEmployeeTodayInformation()
        {
            try
            {
                var response = await _kdanMembersService.ListAllEmployeeTodayInformationFunction();
                return Ok(response);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 列出指定日期員工資訊清單
        /// </summary>
        /// <param name="kdanDesignatedDayPara"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DesignatedDayEmployeeInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<KdanMembersInformationDto>>> ListAllEmployeeDayInformation(KdanDesignatedDayPara kdanDesignatedDayPara)
        {
            try
            {
                var response = await _kdanMembersService.ListAllEmployeeDayInformationFunction(kdanDesignatedDayPara);
                return Ok(response);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 列出指定日期區間內沒有打卡的員工編號
        /// </summary>
        /// <param name="kdanDatePara"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmployeesNotClockedOutBetweenDates")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<int>>> ListEmployeesNotClockedOutBetweenDates(KdanDatePara kdanDatePara)
        {
            try
            {
                var response = await _kdanMembersService.ListEmployeesNotClockedOutBetweenDatesFunction(kdanDatePara);
                return Ok(response);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        /// <summary>
        /// 列出指定日期內最早打卡的五位員工編號
        /// </summary>
        /// <param name="kdanDesignatedDayPara"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("EmployeesFiveEarlestClockIn")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<int>>> ListFiveEmployeesTodayClockInEarliest(KdanDesignatedDayPara kdanDesignatedDayPara)
        {
            try
            {
                var response = await _kdanMembersService.ListFiveEmployeesTodayClockInEarliestFunction(kdanDesignatedDayPara);
                return Ok(response);
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
