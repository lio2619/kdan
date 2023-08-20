using System.ComponentModel.DataAnnotations;

namespace Kdan.Parameters
{
    public class KdanClockPara
    {
        /// <summary>
        /// 員工編號
        /// </summary>
        [Required(ErrorMessage = "員工編號必須填寫")]
        //[MaxLength(36, ErrorMessage = "員工編號不可超過36位元")]
        public int EmployeeNumber { get; set; }
        /// <summary>
        ///時間
        /// </summary>
        public DateTime Clock { get; set; }
    }
}
