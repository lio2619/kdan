using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Kdan.Parameters
{
    public class KdanMemberPara
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        [Required(ErrorMessage = "員工編號必須填寫")]
        [JsonPropertyName("employeeNumber")]
        //[MaxLength(36, ErrorMessage = "員工編號不可超過36位元")]
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// 上班打卡時間
        /// </summary>
        [JsonPropertyName("clockIn")]
        public DateTime? ClockIn { get; set; }
        /// <summary>
        /// 下班打卡時間
        /// </summary>
        [JsonPropertyName("clockOut")]
        public DateTime? ClockOut { get; set; }
    }
}
