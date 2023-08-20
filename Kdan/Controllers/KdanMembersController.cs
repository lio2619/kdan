using Microsoft.AspNetCore.Mvc;
using Kdan.Services.Interface;
using Kdan.Models;

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
                    await _kdanMembersService.JsonFileToDB("C:\\Users\\lio26\\Downloads\\member.json");
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
    }
}
