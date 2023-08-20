using Microsoft.AspNetCore.Mvc;
using Kdan.Services.Interface;
using Kdan.Parameters;
using Kdan.Dtos;
using System.Globalization;

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
                    var filePath = Path.GetFullPath(formFile.FileName);
                    var path = Path.GetDirectoryName(formFile.FileName);
                    await _kdanMembersService.JsonFileToDBFunction("C:\\Users\\lio26\\Downloads\\member.json");
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
        [HttpPost]
        [Route("SupplementaryClock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SupplementaryClock(KdanClockPara kdanClockPara)
        {
            try
            {
                await _kdanMembersService.SupplementaryClockFunction(kdanClockPara);
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
        [HttpPost]
        [Route("DesignatedDayEmployeeInformation")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<KdanMembersInformationDto>>> ListAllEmployeeDayInformation(string dateOnly)
        {
            try
            {
                DateOnly firstdate = DateOnly.ParseExact(dateOnly,
                                         "MM/dd/yyyy",
                                         CultureInfo.InvariantCulture);
                var response = await _kdanMembersService.ListAllEmployeeDayInformationFunction(firstdate);
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
