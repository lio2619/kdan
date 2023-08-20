using System.ComponentModel.DataAnnotations;

namespace Kdan.Parameters
{
    public class KdanDesignatedDayPara
    {
        [Required]
        [RegularExpression(@"^((0[1-9]|1[0-2])/(0[1-9]|[1-2]\d|3[01])/(19\d{2}|20\d{2}))$", ErrorMessage = ("請依月份/日期/西元年份輸入"))]
        public string Day { get; set; }
    }
}
